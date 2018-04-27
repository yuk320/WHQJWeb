----------------------------------------------------------------------
-- 版权：2017
-- 时间：2018-04-27
-- 用途：苹果充值
----------------------------------------------------------------------

USE [WHQJTreasureDB]
GO

-- 在线充值
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_FinishOnLineOrderIOS') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_FinishOnLineOrderIOS
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

---------------------------------------------------------------------------------------
-- 在线充值
CREATE PROCEDURE NET_PW_FinishOnLineOrderIOS
	@strOrdersID		NVARCHAR(32),			--	订单编号
	@PayAmount			DECIMAL(18,2),			--  支付金额
	@dwUserID			INT,					--	充值用户
	@strAppleID			NVARCHAR(32),			--  产品标识
	@strIPAddress		NVARCHAR(31),			--	用户帐号
	@strErrorDescribe	NVARCHAR(127) OUTPUT	--	输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 帐号资料
DECLARE @Accounts NVARCHAR(31)
DECLARE @NickName NVARCHAR(31)
DECLARE @UserID INT
DECLARE @GameID INT
DECLARE @SpreaderID INT
DECLARE @Nullity TINYINT
DECLARE @BindSpread INT

-- 订单信息
DECLARE @ConfigID INT
DECLARE @Amount DECIMAL(18,2)
DECLARE @ScoreType TINYINT
DECLARE @Score INT
DECLARE @FristPresent INT
DECLARE @PresentScore INT
DECLARE @OtherPresent INT
DECLARE @BeforeScore BIGINT
DECLARE @OrderStatus TINYINT
DECLARE @DateTime DATETIME

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()

	-- 获取用户信息
	SELECT @UserID=UserID,@GameID=GameID,@SpreaderID=SpreaderID,@Accounts=Accounts,@NickName=NickName,@Nullity=Nullity FROM WHQJAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID = @dwUserID
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号不存在！'
		RETURN 1001
	END
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号已冻结状态！'
		RETURN 1002
	END

	-- 充值推广验证
	SELECT @BindSpread=StatusValue FROM WHQJAccountsDB.dbo.SystemStatusInfo WITH(NOLOCK) WHERE StatusName = N'JJPayBindSpread'
	IF @SpreaderID<=0 AND @BindSpread=0
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号未绑定推广人！'
		RETURN 1003
	END

	-- 配置查询
	SELECT @ConfigID=ConfigID,@Amount=PayPrice,@ScoreType = ScoreType,@Score=Score,@PresentScore=PresentScore,@FristPresent = FristPresent FROM AppPayConfig WITH(NOLOCK) WHERE PayType=1 AND AppleID=@strAppleID
	IF @ConfigID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值产品不存在!'
		RETURN 2001
	END
	IF @Amount <= 0 OR @Score <=0
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值产品配置异常！'
		RETURN 2002
	END
	IF @Amount!=@PayAmount
	BEGIN
		SET @strErrorDescribe=N'抱歉！支付金额错误!'
		RETURN 2003
	END

	-- 订单查询
	IF EXISTS(SELECT OnLineID FROM OnLinePayOrder WITH(NOLOCK) WHERE OrderID = @strOrdersID)
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单已完成!'
		RETURN 2004
	END

	SET @OtherPresent = @FristPresent
	-- 账户按类型首充
	IF EXISTS(SELECT 1 FROM OnLinePayOrder WHERE UserID=@UserID AND ConfigID=@ConfigID AND OrderStatus>1)
	BEGIN
		SET @OtherPresent = @PresentScore
	END

	IF @ScoreType = 0 
	BEGIN
		SELECT @BeforeScore = Score FROM WHQJTreasureDB.DBO.GameScoreInfo(NOLOCK) WHERE UserID = @UserID
		IF @BeforeScore IS NULL 
		BEGIN
			INSERT WHQJTreasureDB.DBO.GameScoreInfo (UserID,Score) VALUES (@UserID,0)
			SET @BeforeScore = 0
		END
	END	
	ELSE IF @ScoreType = 1
	BEGIN
		SELECT @BeforeScore = Diamond FROM WHQJTreasureDB.DBO.UserCurrency(NOLOCK) WHERE UserID = @UserID
		IF @BeforeScore IS NULL 
		BEGIN
			INSERT WHQJTreasureDB.DBO.UserCurrency (UserID,Diamond) VALUES (@UserID,0)
			SET @BeforeScore = 0
		END
	END
	
	-- 记录订单
	INSERT INTO OnLinePayOrder(ConfigID,ShareID,UserID,GameID,Accounts,NickName,OrderID,OrderType,Amount,ScoreType,Score,OtherPresent,OrderStatus,OrderDate,OrderAddress,BeforeScore)
	VALUES(@ConfigID,800,@UserID,@GameID,@Accounts,@NickName,@strOrdersID,1,@Amount,@ScoreType,@Score,@OtherPresent,1,@DateTime,@strIPAddress,@BeforeScore)

	-- 插入代币 交付给服务端进行代币处理。
	INSERT INTO MiddleMoney (RecordID,UserID,MiddleMoney,MoneyType) VALUES (@strOrdersID,@UserID,@Amount,@ScoreType)

END
RETURN 0
GO
