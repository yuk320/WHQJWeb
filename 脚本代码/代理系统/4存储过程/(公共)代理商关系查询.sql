-- =============================================
-- 用途: 查询代理关系 （所有下级（无下级限制）、所有上级（无上级限制）、获取代理所有下线（代理、直属玩家、代理的玩家）的注册信息）
-- =============================================
/** 获取代理所有下级关系（无限级） **/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentBelowAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentBelowAgent]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentBelowAgent] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	AgentID INT,
	ParentAgent INT,
	LevelID INT 
)
WITH ENCRYPTION AS
BEGIN
	DECLARE @dwLevel INT
	SET  @dwLevel = 1
	INSERT  INTO  @tbUserInfo SELECT UserID,AgentID,ParentAgent,@dwLevel FROM WHQJAgentDB.DBO.AgentInfo(NOLOCK) WHERE UserID = @dwUserID
	WHILE @@ROWCOUNT > 0
	BEGIN 
		SET  @dwLevel = @dwLevel + 1
		INSERT  INTO  @tbUserInfo SELECT a.UserID,a.AgentID,a.ParentAgent,@dwLevel FROM WHQJAgentDB.DBO.AgentInfo(NOLOCK) a INNER JOIN  @tbUserInfo b ON a.ParentAgent = b.AgentID WHERE b.LevelID = @dwLevel - 1
	END 
	RETURN 
END
GO

/** 获取代理所有上级关系（无限级） **/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentAboveAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentAboveAgent]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentAboveAgent] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	AgentID INT,
	ParentAgent INT,
	LevelID INT 
)
WITH ENCRYPTION AS
BEGIN
	DECLARE  @dwLevel INT
	SET  @dwLevel = 1
	INSERT  INTO  @tbUserInfo SELECT UserID,AgentID,ParentAgent,@dwLevel FROM WHQJAgentDB.DBO.AgentInfo(NOLOCK) WHERE UserID = @dwUserID
	WHILE @@ROWCOUNT > 0
	BEGIN 
		SET  @dwLevel = @dwLevel + 1
		INSERT  INTO  @tbUserInfo SELECT a.UserID,a.AgentID,a.ParentAgent, @dwLevel AS LevelID FROM WHQJAgentDB.DBO.AgentInfo(NOLOCK) a INNER JOIN  @tbUserInfo b ON a.AgentID=b.ParentAgent WHERE b.LevelID = @dwLevel - 1 AND b.ParentAgent<>0
	END 
	RETURN 
END
GO

/** 获取代理所有下线（代理、直属玩家、代理的玩家）的注册信息 **/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentBelowUserRegister]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentBelowUserRegister]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentBelowUserRegister] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	SpreaderID INT,
	LevelID INT,
	RegisterDate DATETIME,
	RegisterOrigin TINYINT,
	GameID INT,
	NickName NVARCHAR(31),
	AgentID INT
)
WITH ENCRYPTION AS
BEGIN
	IF @dwUserID = 0 RETURN
	IF NOT EXISTS (SELECT 1 FROM WHQJAgentDB.DBO.AgentInfo WHERE UserID = @dwUserID) RETURN
	INSERT INTO @tbUserInfo 
	SELECT a.UserID,b.SpreaderID,LevelID,b.RegisterDate,b.RegisterOrigin,b.GameID,b.NickName,b.AgentID FROM DBO.WF_GetAgentBelowAgent(@dwUserID) a LEFT JOIN WHQJAccountsDB.DBO.AccountsInfo(NOLOCK) b ON a.UserID = b.UserID WHERE LevelID>1
	INSERT INTO @tbUserInfo SELECT a.UserID,a.SpreaderID,b.LevelID,a.RegisterDate,a.RegisterOrigin,a.GameID,a.NickName,a.AgentID FROM WHQJAccountsDB.DBO.AccountsInfo(NOLOCK) a LEFT JOIN @tbUserInfo b ON a.SpreaderID = b.UserID WHERE a.SpreaderID<>0

	RETURN 
END
GO
