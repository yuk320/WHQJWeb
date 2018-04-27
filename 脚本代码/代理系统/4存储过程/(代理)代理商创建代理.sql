----------------------------------------------------------------------
-- 时间：2018-04-16
-- 用途：代理商添加代理用户
----------------------------------------------------------------------
USE WHQJAgentDB
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_AT_AddAgent]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_AT_AddAgent]
GO

----------------------------------------------------------------------
CREATE PROC [NET_AT_AddAgent]
(
	@dwUserID			INT,					--用户标识
	@dwGameID			INT,					--游戏 I D
	@strCompellation	NVARCHAR(16),			--代理姓名
	@strAgentDomain		NVARCHAR(50),			--代理域名
	@strQQAccount       NVARCHAR(32),			--Q Q 账号
	@strWCNickName      NVARCHAR(32),			--微信昵称
	@strContactPhone    NVARCHAR(32),			--联系电话
	@strContactAddress  NVARCHAR(32),			--联系地址
	@strAgentNote       NVARCHAR(32),			--代理备注

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @AgentID INT
DECLARE @AgentLevel TINYINT

DECLARE @NewAgentLevel TINYINT
DECLARE @ParentAgent INT

DECLARE @NewUserID INT
DECLARE @NewNullity TINYINT
DECLARE @NewAgentID INT

BEGIN
	-- 查询用户	
	SELECT @UserID=UserID,@Nullity=Nullity,@AgentID=AgentID FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
	SET @ParentAgent = @AgentID

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

	-- 查询代理信息
	SELECT @AgentLevel=AgentLevel FROM AgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
	IF @AgentLevel IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的账号为非代理商！'
		RETURN 1003
	END
	-- IF @AgentLevel=3
	-- BEGIN
	-- 	SET @strErrorDescribe=N'抱歉，三级代理商无法添加下级代理！'
	-- 	RETURN 1004
	-- END
	-- 设置代理级别
	SET @NewAgentLevel= @AgentLevel + 1

	-- 查询代理域名重复信息
	IF EXISTS(SELECT AgentID FROM AgentInfo WITH(NOLOCK) WHERE AgentDomain=@strAgentDomain)
	BEGIN
		SET @strErrorDescribe=N'抱歉，代理域名已存在！'
		RETURN 2001
	END

	-- 添加的代理账号验证
	SELECT @NewUserID=UserID,@NewNullity=Nullity,@NewAgentID=AgentID FROM WHQJAccountsDB.DBO.AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID
	IF @NewUserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，设置为代理的账号不存在！'
		RETURN 2002
	END	
	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，设置为代理的账号已冻结！'
		RETURN 2003
	END	
	IF @NewAgentID>0
	BEGIN
		SET @strErrorDescribe=N'抱歉，设置为代理的账号已经是代理商！'
		RETURN 2004
	END

	-- 代理信息
	INSERT INTO AgentInfo(ParentAgent,UserID,Compellation,QQAccount,WCNickName,ContactPhone,ContactAddress,AgentDomain,AgentLevel,AgentNote,Nullity,CollectDate)
	VALUES(@ParentAgent,@NewUserID,@strCompellation,@strQQAccount,@strWCNickName,@strContactPhone,@strContactAddress,@strAgentDomain,@NewAgentLevel,@strAgentNote,0,getdate())
	SELECT @NewAgentID = SCOPE_IDENTITY()

	-- 更新用户信息，设置代理标识
	UPDATE WHQJAccountsDB.DBO.AccountsInfo SET AgentID=@NewAgentID WHERE UserID = @NewUserID

	-- 设置用户
	IF @@ERROR=0 
	BEGIN
		DECLARE @OriginAgentID INT
		SELECT @OriginAgentID FROM AgentBelowInfo(NOLOCK) WHERE UserID=@NewUserID

		-- 如果新增代理时别人的代理下线时。
		IF @OriginAgentID > 0 
		BEGIN
			-- 先清除原有代理关系
			DELETE AgentBelowInfo WHERE AgentID=@OriginAgentID AND UserID = @NewUserID
			-- 更新原代理的下线玩家数
			UPDATE AgentInfo SET BelowUser = BelowUser - 1 WHERE AgentID = @OriginAgentID
		END
		-- ELSE  -- 当新增代理没有代理关系时。
		-- BEGIN
		-- 	-- 根据全局配置，添加代理（含代理添加代理）时，是否携带原玩家的推广下线
		-- 	DECLARE @IsAddAgentCarryUser INT
		-- 	SELECT @IsAddAgentCarryUser = StatusValue FROM SystemStatusInfo WHERE StatusName = N'IsAddAgentCarryUser'
		-- 	IF @IsAddAgentCarryUser IS NULL
		-- 	BEGIN
		-- 		SET @IsAddAgentCarryUser = 0
		-- 	END
		-- 	IF IsAddAgentCarryUser > 0
		-- 	BEGIN
		-- 		-- 但无法将已有代理关系的玩家携带过来
		-- 		INSERT AgentBelowInfo (AgentID,UserID) 
		-- 		SELECT @NewAgentID,a.UserID
		-- 		FROM WHQJAccountsDB.DBO.AccountsInfo a WHERE SpreaderID = @NewUserID AND a.UserID NOT IN (SELECT UserID FROM AgentBelowInfo)
		-- 	END
			
		-- 	DECLARE @BelowUser INT
		-- 	-- 统计代理玩家人数（非代理）
		-- 	SELECT @BelowUser = COUNT(UserID) FROM AgentBelowInfo WHERE AgentID=@NewAgentID
		-- 	-- 同步代理下线人数		
		-- 	UPDATE AgentInfo SET BelowUser = @BelowUser WHERE AgentID = @NewAgentID
		-- END

		-- 建立新的代理关系
		INSERT AgentBelowInfo (AgentID,UserID) VALUES (@ParentAgent,@NewUserID)

		-- 统计代理下级代理人数
		DECLARE @BelowAgent INT
		SELECT @BelowAgent = COUNT(UserID) FROM AgentBelowInfo WHERE AgentID=@NewAgentID AND UserID IN (SELECT UserID FROM AgentInfo)
		-- 同步代理下级代理人数	
		UPDATE AgentInfo SET BelowAgent = @BelowAgent WHERE AgentID = @ParentAgent

		SET @strErrorDescribe=N'恭喜您！代理创建成功。'
		RETURN 0
	END
	ELSE
	BEGIN
		SET @strErrorDescribe=N'抱歉，代理创建失败。'
		RETURN 2005
	END
END
GO