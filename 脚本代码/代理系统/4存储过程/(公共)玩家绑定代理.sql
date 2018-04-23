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

	SELECT @SpreaderID=UserID,@Nullity=Nullity,@SpreaderAgent = SpreaderID,@AgentID = AgentID FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID
  
	IF @SpreaderID IS NULL 
	BEGIN
	SET @strErrorDescribe=N'抱歉，您绑定的玩家不存在！'
	RETURN 1004
	END

	-- 如果目标玩家非代理，则尝试找他的上级
	IF @AgentID = 0
	BEGIN
	SET @SpreaderID = @SpreaderAgent
	END

	-- 如果都非代理则不绑定
	IF @SpreaderID = 0
	BEGIN
	SET @strErrorDescribe=N'抱歉，您绑定的玩家非代理的下线！'
	RETURN 1005
	END

	-- 绑定代理关系
	UPDATE WHQJAccountsDB.DBO.AccountsInfo SET SpreaderID = @SpreaderID WHERE UserID = @UserID
	-- 更新上级下线人数
	UPDATE AgentInfo SET BelowUser = BelowUser + 1 WHERE UserID = @SpreaderID

	SET @strErrorDescribe = N'恭喜您，绑定成功！'
	RETURN 0
END
GO