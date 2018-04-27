USE WHQJAgentDB
GO

----------------------------------------------------------------------------------------------------
-- 版权：2018
-- 时间：2018-04-17
-- 用途：玩家绑定代理 （重复绑定或绑定推广时无效）
----------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PB_UserAgentBind]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PB_UserAgentBind]
GO


SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

--------------------------------------------------------------------	
--
CREATE PROC [NET_PB_UserAgentBind]
(
	@dwUserID			INT,					--用户标识
	@dwGameID	    INT,			    --目标代理或玩家GameID
	@strClientIP  NVARCHAR(15),	--用户操作IP

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @SpreaderID INT
DECLARE @AgentID INT
DECLARE @SpreaderAgent INT
DECLARE @OriginSpreaderID INT

BEGIN
	-- 查询用户	
	SELECT @UserID=UserID,@Nullity=Nullity,@OriginSpreaderID=SpreaderID FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
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

	IF @OriginSpreaderID>0
	BEGIN
	SET @strErrorDescribe=N'抱歉，您已绑定关系，请勿重新绑定！'
	RETURN 1003
	END

	SELECT @SpreaderID=UserID,@Nullity=Nullity,@AgentID = AgentID FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID
	IF @SpreaderID IS NULL 
	BEGIN
	SET @strErrorDescribe=N'抱歉，您绑定的玩家不存在！'
	RETURN 1004
	END

	/** 推广关系绑定 Begin **/

	-- 绑定推广关系
	UPDATE WHQJAccountsDB.DBO.AccountsInfo SET SpreaderID = @SpreaderID WHERE UserID = @UserID

	-- 领取推广奖励
	DECLARE @SpreaderAward INT
	SELECT @SpreaderAward = StatusValue FROM WHQJAccountsDB.DBO.SystemStatusInfo WHERE StatusName = N'JJBindSpreadPresent'
	IF @SpreaderAward IS NULL
	BEGIN
		SET @SpreaderAward = 0
	END

	IF @SpreaderAward > 0 
	BEGIN

		DECLARE @DateTime DateTime
		DECLARE @DiamondBefore INT
		SET @DateTime = GETDATE()
		-- 查询玩家钻石
		SELECT @DiamondBefore = Diamond FROM WHQJTreasureDB.DBO.UserCurrency(NOLOCK) WHERE UserID = @UserID
		IF @DiamondBefore IS NULL
		BEGIN
			INSERT WHQJTreasureDB.DBO.UserCurrency (UserID,Diamond) VALUES (@UserID,0)
			SET @DiamondBefore = 0
		END

		UPDATE WHQJTreasureDB.DBO.UserCurrency 
			SET Diamond = Diamond + @SpreaderAward 
		 WHERE UserID = @UserID

		-- 写入钻石流水记录
		INSERT INTO WHQJRecordDB.DBO.RecordDiamondSerial
			(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
		VALUES(WHQJRecordDB.DBO.WF_GetSerialNumber(), 0, @UserID, 4, @DiamondBefore, @SpreaderAward, @strClientIP, @DateTime)

	END

	/** 推广关系绑定 End **/
	
	-- 如果目标玩家非代理，则尝试找他的上级
	IF @AgentID = 0
	BEGIN
		SELECT @AgentID = AgentID FROM WHQJAgentDB.DBO.AgentBelowInfo WHERE UserID = @SpreaderID
	END

	IF @AgentID > 0 
	BEGIN

		/** 代理关系绑定 Start **/

		DECLARE @ComeSpreadCount INT
		-- 绑定代理关系
		INSERT WHQJAgentDB.DBO.AgentBelowInfo (AgentID,UserID) VALUES (@AgentID,@UserID)
		
		DECLARE @BelowUser INT
		-- 统计代理玩家人数（非代理）
		SELECT @BelowUser = COUNT(UserID) FROM AgentBelowInfo WHERE AgentID=@AgentID AND UserID NOT IN (SELECT UserID FROM AgentInfo)
		-- 同步代理下线人数		
		UPDATE AgentInfo SET BelowUser = @BelowUser WHERE AgentID = @AgentID

		/** 代理关系绑定 End **/

	END

	SET @strErrorDescribe = N'恭喜您，绑定成功！'
	RETURN 0
END
GO