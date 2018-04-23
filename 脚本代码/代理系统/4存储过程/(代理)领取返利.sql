USE WHQJAgentDB
GO

----------------------------------------------------------------------------------------------------
-- 版权：2018
-- 时间：2018-04-17
-- 用途：代理领取返利
----------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_AT_ReceiveAgentAward]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_AT_ReceiveAgentAward]
GO


SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

--------------------------------------------------------------------	
--
CREATE PROC [NET_AT_ReceiveAgentAward]
(
	@dwUserID			INT,					--用户标识
	@dwAward	    INT,			-- 返利基准
  @dwAwardType TINYINT,
  @strClientIP NVARCHAR(15), -- 用户IP

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @AgentID INT
DECLARE @DiamondAward INT
DECLARE @GoldAward INT

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

	SELECT @AgentID=AgentID,@DiamondAward=DiamondAward,@GoldAward = GoldAward FROM AgentInfo WITH(NOLOCK) WHERE UserID=@UserID

  IF @AgentID IS NULL 
  BEGIN
    SET @strErrorDescribe=N'抱歉，您的帐号非代理，无法领取奖励！'
    RETURN 1003
  END

  DECLARE @ReceiveBefore BIGINT
  DECLARE @DateTime DATETIME 
  SET @DateTime = GETDATE()

  IF @dwAwardType = 1
  BEGIN
    SELECT @ReceiveBefore = Diamond FROM WHQJTreasureDB.DBO.UserCurrency(NOLOCK) WHERE UserID = @UserID
    IF @ReceiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.UserCurrency VALUES (@UserID,0)
      SET @ReceiveBefore = 0
    END

    DECLARE @ReceiveDiamondSave INT
    SELECT @ReceiveDiamondSave = StatusValue FROM SystemStatusInfo(NOLOCK) WHERE StatusName = N'ReceiveDiamondSave'
    IF @ReceiveDiamondSave IS NULL  SET @ReceiveDiamondSave = 0

    IF @DiamondAward - @ReceiveDiamondSave < @dwAward
    BEGIN
      SET @strErrorDescribe = N'抱歉，您可领取的奖励不足以领取。'
      RETURN 1004
    END

    -- 开启事务
    BEGIN TRAN

      -- 写入领取记录
      INSERT ReturnAwardReceive (UserID,AwardType,ReceiveAward,ReceiveBefore, CollectDate) 
      VALUES (@UserID,@dwAwardType,@dwAward,@ReceiveBefore, @DateTime)

      -- 写入钻石流水记录
      INSERT INTO WHQJRecordDB.DBO.RecordDiamondSerial
        (SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 13, @ReceiveBefore, @dwAward, @strClientIP, @DateTime)

      -- 更新代理信息中的可用奖励
      UPDATE AgentInfo SET DiamondAward = DiamondAward - @dwAward WHERE UserID = @UserID
      -- 更新用户钻石数据
      UPDATE WHQJTreasureDB.DBO.UserCurrency  SET Diamond = Diamond + @dwAward WHERE UserID = @UserID

      IF @@Error > 0 
      BEGIN
        ROLLBACK TRAN
        RETURN 2001
      END

    COMMIT TRAN
    
  END
  ELSE IF @dwAwardType = 2
  BEGIN
    DECLARE @ReceiveBeforeInsure BIGINT 
    SELECT @ReceiveBefore = Score,@ReceiveBeforeInsure = InsureScore FROM WHQJTreasureDB.DBO.GameScoreInfo(NOLOCK) WHERE UserID = @UserID
    IF @ReceiveBefore IS NULL
    BEGIN
      INSERT WHQJTreasureDB.DBO.GameScoreInfo(UserID,Score,InsureScore,RegisterIP) VALUES (@UserID,0,0,@strClientIP)
      SET @ReceiveBefore = 0
      SET @ReceiveBeforeInsure = 0
    END

    DECLARE @ReceiveGoldSave INT
    SELECT @ReceiveGoldSave = StatusValue FROM SystemStatusInfo(NOLOCK) WHERE StatusName = N'ReceiveGoldSave'
    IF @ReceiveGoldSave IS NULL  SET @ReceiveGoldSave = 0

    IF @GoldAward - @ReceiveGoldSave < @dwAward
    BEGIN
      SET @strErrorDescribe = N'抱歉，您可领取的奖励不足以领取。'
      RETURN 1004
    END

    IF EXISTS (SELECT 1 FROM WHQJTreasureDB.DBO.GameScoreLocker(NOLOCK) WHERE UserID = @UserID)
    BEGIN
      SET @strErrorDescribe = N'抱歉，您领取奖励前必须退出游戏房间。'
      RETURN 1005
    END

    -- 开启事务
    BEGIN TRAN

      -- 写入领取记录
      INSERT ReturnAwardReceive (UserID,AwardType,ReceiveAward,ReceiveBefore, CollectDate) 
      VALUES (@UserID,@dwAwardType,@dwAward,@ReceiveBefore, @DateTime)

      -- 写入金币流水记录
      INSERT INTO WHQJRecordDB.DBO.RecordTreasureSerial
      (SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
      VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 9, @ReceiveBefore, @ReceiveBeforeInsure, @dwAward, @strClientIP, @DateTime)


      -- 更新代理信息中的可用奖励
      UPDATE AgentInfo SET GoldAward = GoldAward - @dwAward WHERE UserID = @UserID
      -- 更新用户金币数据
      UPDATE WHQJTreasureDB.DBO.GameScoreInfo  SET Score = Score + @dwAward WHERE UserID = @UserID

      IF @@Error > 0 
      BEGIN
        ROLLBACK TRAN
        RETURN 2001
      END

    COMMIT TRAN

  END
  ELSE 
  BEGIN
    SET @strErrorDescribe = N'抱歉，领取参数不正确。'
    RETURN 1000
  END

  RETURN 0
END
GO