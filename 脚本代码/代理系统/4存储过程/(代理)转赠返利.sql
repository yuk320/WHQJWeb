USE WHQJAgentDB
GO

----------------------------------------------------------------------------------------------------
-- 版权：2018
-- 时间：2018-04-18
-- 用途：代理转赠返利（根据功能可以单独调用 也可与领取返利配合使用）
----------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_AT_GiveAgentAward]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_AT_GiveAgentAward]
GO


SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

--------------------------------------------------------------------	
--
CREATE PROC [NET_AT_GiveAgentAward]
(
	@dwUserID			INT,					-- 代理用户标识
	@dwAward	    INT,			  -- 返利数量
	@dwAwardType  TINYINT,     -- 返利类型 1：钻石 2：金币
	@strPassword  NVARCHAR(32),  -- 代理安全密码
	@dwGameID     INT,        -- 目标GameID
	@strClientIP  NVARCHAR(15), -- 用户IP

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @AgentNullity TINYINT
DECLARE @AgentID INT
DECLARE @Password NVARCHAR(32)

-- 被转赠用户信息
DECLARE @TargetUserID INT
DECLARE @TargetNullity TINYINT

BEGIN
	-- 查询用户	
	SELECT @UserID=UserID,@Nullity=Nullity FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
  -- 用户存在
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号不存在！'
		RETURN 1001
	END	

	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已冻结！'
		RETURN 1002
	END	

  -- 查询被赠送用户
  SELECT @TargetUserID = UserID,@TargetNullity = Nullity FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID
  -- 用户存在
  IF @TargetUserID IS NULL
  BEGIN
    SET @strErrorDescribe=N'抱歉，你转赠的账号不存在！'
    RETURN 1003
  END

  IF @TargetNullity=1
  BEGIN
    SET @strErrorDescribe=N'抱歉，你转赠的账号已冻结！'
    RETURN 1004
  END

	SELECT @AgentID=AgentID,@AgentNullity = Nullity,@Password = [Password] FROM AgentInfo WITH(NOLOCK) WHERE UserID=@UserID

  IF @AgentID IS NULL 
  BEGIN
    SET @strErrorDescribe=N'抱歉，您的帐号非代理，无法领取奖励！'
    RETURN 1005
  END

  IF @AgentNullity=1 
  BEGIN
    SET @strErrorDescribe=N'抱歉，您的代理帐号已冻结！'
    RETURN 1006
  END

  IF @strPassword <> @Password
  BEGIN
    SET @strErrorDescribe=N'抱歉，您的安全密码不正确！'
    RETURN 1007
  END

  DECLARE @GiveBefore BIGINT
  DECLARE @ReceiveBefore BIGINT
  DECLARE @DateTime DATETIME 
  SET @DateTime = GETDATE()

  IF @dwAwardType = 1
  BEGIN
    SELECT @GiveBefore = Diamond FROM WHQJTreasureDB.DBO.UserCurrency(NOLOCK) WHERE UserID = @UserID
    IF @GiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.UserCurrency VALUES (@UserID,0)
      SET @GiveBefore = 0
    END
	
	IF @dwAward>@GiveBefore
	BEGIN
	  SET @strErrorDescribe=N'抱歉，您的余额不足以转赠！'
	  RETURN 1008
	END

    SELECT @ReceiveBefore = Diamond FROM WHQJTreasureDB.DBO.UserCurrency(NOLOCK) WHERE UserID = @TargetUserID
    IF @ReceiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.UserCurrency VALUES (@TargetUserID,0)
      SET @ReceiveBefore = 0
    END

    -- 开启事务
    BEGIN TRAN

      -- 写入转赠记录
      INSERT ReturnAwardGrant (SourceUserID,TargetUserID,TradeType,SourceBefore,TargetBefore,Amount,ClientIP,CollectDate) 
      VALUES (@UserID,@TargetUserID,@dwAwardType,@GiveBefore,@ReceiveBefore,@dwAward,@strClientIP,@DateTime)

      -- 写入钻石流水记录 转出方 TypeID=7 代理转赠
      INSERT INTO WHQJRecordDB.DBO.RecordDiamondSerial
        (SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 7, @GiveBefore, -@dwAward, @strClientIP, @DateTime)
      -- 写入钻石流水记录 转入方 TypeID=8 被代理转赠
      INSERT INTO WHQJRecordDB.DBO.RecordDiamondSerial
        (SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @TargetUserID, 8, @ReceiveBefore, @dwAward, @strClientIP, @DateTime)

      -- 更新代理用户钻石数据
      UPDATE WHQJTreasureDB.DBO.UserCurrency SET Diamond = Diamond - @dwAward WHERE UserID = @UserID
      -- 更新被转赠用户钻石数据
      UPDATE WHQJTreasureDB.DBO.UserCurrency  SET Diamond = Diamond + @dwAward WHERE UserID = @TargetUserID

      IF @@Error > 0 
      BEGIN
        ROLLBACK TRAN
        RETURN 2003
      END

    COMMIT TRAN
    
  END
  ELSE IF @dwAwardType = 2
  BEGIN
    DECLARE @GiveBeforeInsure BIGINT 
    DECLARE @ReceiveBeforeInsure BIGINT 
    SELECT @GiveBefore = Score,@GiveBeforeInsure = InsureScore FROM WHQJTreasureDB.DBO.GameScoreInfo(NOLOCK) WHERE UserID = @UserID
    IF @GiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.GameScoreInfo(UserID,Score,InsureScore,RegisterIP) VALUES (@UserID,0,0,@strClientIP)
      SET @GiveBefore = 0
      SET @GiveBeforeInsure = 0
    END

	IF @dwAward>@GiveBefore
	BEGIN
	  SET @strErrorDescribe=N'抱歉，您的余额不足以转赠！'
	  RETURN 1008
	END

    SELECT @ReceiveBefore = Score,@ReceiveBeforeInsure = InsureScore FROM WHQJTreasureDB.DBO.GameScoreInfo(NOLOCK) WHERE UserID = @TargetUserID
    IF @ReceiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.GameScoreInfo(UserID,Score,InsureScore) VALUES (@TargetUserID,0,0)
      SET @ReceiveBefore = 0
      SET @ReceiveBeforeInsure = 0
    END

    -- 检查金币房间锁表
    IF EXISTS (SELECT 1 FROM WHQJTreasureDB.DBO.GameScoreLocker(NOLOCK) WHERE UserID = @UserID OR UserID = @TargetUserID)
    BEGIN
      SET @strErrorDescribe = N'抱歉，您与被转赠的用户都必须退出游戏房间。'
      RETURN 1009
    END

    -- 开启事务
    BEGIN TRAN

      -- 写入转赠记录
      INSERT ReturnAwardGrant (SourceUserID,TargetUserID,TradeType,SourceBefore,TargetBefore,Amount,ClientIP,CollectDate) 
      VALUES (@UserID,@TargetUserID,@dwAwardType,@GiveBefore,@ReceiveBefore,@dwAward,@strClientIP,@DateTime)

      -- 写入金币流水记录 转出方 TypeID=10 代理转赠
      INSERT INTO WHQJRecordDB.DBO.RecordTreasureSerial
      (SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 10, @GiveBefore, @GiveBeforeInsure, -@dwAward, @strClientIP, @DateTime)
      -- 写入金币流水记录 转入方 TypeID=11 被代理转赠
      INSERT INTO WHQJRecordDB.DBO.RecordTreasureSerial
      (SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @TargetUserID, 10, @ReceiveBefore, @ReceiveBeforeInsure, @dwAward, @strClientIP, @DateTime)


      -- 更新代理用户金币数据
      UPDATE WHQJTreasureDB.DBO.GameScoreInfo SET Score = Score - @dwAward WHERE UserID = @UserID
      -- 更新被转赠用户金币数据
      UPDATE WHQJTreasureDB.DBO.GameScoreInfo  SET Score = Score + @dwAward WHERE UserID = @TargetUserID

      IF @@Error > 0 
      BEGIN
        ROLLBACK TRAN
        RETURN 2003
      END

    COMMIT TRAN

  END
  ELSE 
  BEGIN
    SET @strErrorDescribe = N'抱歉，转赠参数不正确。'
    RETURN 1000
  END

  RETURN 0
END
GO