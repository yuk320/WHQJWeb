----------------------------------------------------------------------
-- 版权：2017
-- 时间：2018-04-27
-- 用途：在线充值
----------------------------------------------------------------------

USE [WHQJTreasureDB]
GO

-- 在线充值
IF EXISTS (SELECT *
FROM DBO.SYSOBJECTS
WHERE ID = OBJECT_ID(N'[dbo].NET_PW_FinishOnLineOrder') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_FinishOnLineOrder
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

---------------------------------------------------------------------------------------
-- 在线充值
CREATE PROCEDURE NET_PW_FinishOnLineOrder
	@strOrdersID		NVARCHAR(32),
	--	订单编号
	@PayAmount			DECIMAL(18,2),
	--	用户帐号
	@strErrorDescribe	NVARCHAR(127) OUTPUT
--	输出信息
WITH
	ENCRYPTION
AS

-- 属性设置
SET NOCOUNT ON

-- 订单信息
DECLARE @ConfigID INT
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @Amount DECIMAL(18,2)
DECLARE @ScoreType TINYINT
DECLARE @Score INT
DECLARE @PresentScore INT
DECLARE @OtherPresent INT
DECLARE @BeforeScore BIGINT
DECLARE @OrderStatus TINYINT
DECLARE @PayIdentity TINYINT
DECLARE @DateTime DATETIME
DECLARE @CurrentTime DATETIME
DECLARE @OrderAddress NVARCHAR(15)
DECLARE @STime NVARCHAR(10)
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()
	-- 订单查询
	SELECT @ConfigID=ConfigID,@UserID=UserID, @Amount=Amount,@ScoreType=ScoreType, @Score=Score,@OrderAddress=OrderAddress, @OtherPresent=OtherPresent, @OrderStatus=OrderStatus
	FROM OnLinePayOrder WITH(NOLOCK)
	WHERE OrderID = @strOrdersID
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单不存在!'
		RETURN 1001
	END
	IF @OrderStatus=2
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单已完成!'
		RETURN 1002
	END
	IF @Amount != @PayAmount
	BEGIN
		SET @strErrorDescribe=N'抱歉！支付金额错误!'
		RETURN 1003
	END

		-- 获取用户信息
	SELECT @Nullity=Nullity FROM WHQJAccountsDB.dbo.AccountsInfo(NOLOCK) WHERE UserID = @UserID
	IF @Nullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号不存在！'
		RETURN 1004
	END
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号已冻结状态！'
		RETURN 1005
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

	-- 插入代币 交付给服务端进行代币处理。
	INSERT INTO MiddleMoney (RecordID,UserID,MiddleMoney,MoneyType) VALUES (@strOrdersID,@UserID,@Amount,@ScoreType)

	UPDATE OnLinePayOrder SET OrderStatus = 1,BeforeScore = @BeforeScore WHERE OrderID = @strOrdersID AND UserID = @UserID

END
RETURN 0
GO
