----------------------------------------------------------------------
-- 版权：2018
-- 时间：2017-04-27
-- 用途：充值处理
----------------------------------------------------------------------

USE [WHQJTreasureDB]
GO

-- 充值处理
IF EXISTS (SELECT *
FROM DBO.SYSOBJECTS
WHERE ID = OBJECT_ID(N'[dbo].NET_PB_OperationOnLineOrder') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PB_OperationOnLineOrder
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

---------------------------------------------------------------------------------------
-- 充值处理
CREATE PROCEDURE NET_PB_OperationOnLineOrder
	@strOrdersID		NVARCHAR(50),  --	订单号
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
DECLARE @ScoreType TINYINT
DECLARE @Score INT
DECLARE @PresentScore INT
DECLARE @OtherPresent INT
DECLARE @OrderStatus TINYINT
DECLARE @OrderAddress NVARCHAR(15)

-- 执行逻辑
BEGIN
	-- 订单查询
	SELECT @ConfigID=ConfigID,@UserID=UserID,@ScoreType=ScoreType, @Score=Score,@OrderAddress=OrderAddress, @OtherPresent=OtherPresent, @OrderStatus=OrderStatus
	FROM OnLinePayOrder WITH(NOLOCK)
	WHERE OrderID = @strOrdersID
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单不存在!'
		RETURN 1001
	END
	IF @OrderStatus<>1
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单未支付或已完成!'
		RETURN 1002
	END

	SELECT @PresentScore = PresentScore FROM AppPayConfig(NOLOCK) WHERE ConfigID = @ConfigID
	IF @PresentScore IS NULL SET @PresentScore = 0
  
	-- 账户按类型首充
	IF EXISTS(SELECT 1 FROM OnLinePayOrder WHERE UserID=@UserID AND ConfigID=@ConfigID AND OrderStatus>1)
	BEGIN
		SET @OtherPresent = @PresentScore
	END

	DECLARE @DateTime DateTime
	DECLARE @BeforeScore BIGINT
	set @DateTime = GETDATE()
	
	IF @OtherPresent >0
	BEGIN
		IF @ScoreType = 0
		BEGIN

			IF EXISTS(SELECT 1 FROM GameScoreLocker WHERE UserID=@UserID) 
			BEGIN
				SET @strErrorDescribe=N'抱歉！游戏币充值时请离开游戏房间!'
				RETURN 1003
			END
			DECLARE @BeforeInsure BIGINT
			SELECT @BeforeScore = Score,@BeforeInsure = InsureScore FROM GameScoreInfo(NOLOCK) WHERE UserID = @UserID

			BEGIN TRAN
				-- 更新用户金币，只增加额外部分
				UPDATE GameScoreInfo SET Score = Score + @OtherPresent WHERE UserID = @UserID

				-- 写入金币流水记录
				INSERT INTO RecordTreasureSerial
					(SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
				VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 14, @BeforeScore, @BeforeInsure, @OtherPresent, @OrderAddress, @DateTime)
			COMMIT TRAN
		END

		IF @ScoreType = 1
		BEGIN

			SELECT @BeforeScore = Diamond FROM UserCurrency(NOLOCK) WHERE UserID = @UserID

			BEGIN TRAN
				-- 更新用户流水，只增加额外部分
				UPDATE UserCurrency SET Diamond = Diamond + @OtherPresent WHERE UserID = @UserID

				-- 写入钻石流水记录
				INSERT INTO WHQJRecordDB.dbo.RecordGoldSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
				VALUES(dbo.WF_GetSerialNumber(),0,@UserID,2,@BeforeScore,@OtherPresent,@OrderAddress,@DateTime)
			COMMIT TRAN
		END
	END

	-- 处理完成后
	-- 更新订单表 BeforeScore 更新ScoreType 分别插入原值 0:游戏币 1：钻石 2.。。
	UPDATE OnLinePayOrder SET OtherPresent=@OtherPresent,OrderStatus = 2,PayDate=GETDATE()  WHERE OrderID = @strOrdersID AND OrderStatus = 1

	-- 钻石充值
	IF @ScoreType = 1
	BEGIN
		-- 推广充值返利，如果存在返利配置，写入返利记录
		IF EXISTS (SELECT 1 FROM SpreadReturnConfig WHERE Nullity=0)
		BEGIN
			DECLARE @ReturnType TINYINT
			SELECT @ReturnType = StatusValue FROM WHQJAccountsDB.DBO.SystemStatusInfo WHERE StatusName = N'SpreadReturnType'
			IF @ReturnType IS NULL
			BEGIN
				SET @ReturnType = 0
			END
			INSERT WHQJRecordDB.DBO.RecordSpreadReturn (SourceUserID,TargetUserID,SourceDiamond,Spreadlevel,ReturnScale,ReturnNum,ReturnType,CollectDate)
			SELECT @UserID,A.UserID,@Score,B.SpreadLevel,B.PresentScale,@Score*B.PresentScale,@ReturnType,@DateTime FROM (SELECT UserID,LevelID FROM [dbo].[WF_GetAgentAboveAccounts](@UserID) ) AS A,SpreadReturnConfig AS B WHERE B.SpreadLevel=A.LevelID-1 AND A.LevelID>1 AND A.LevelID<=4 AND B.Nullity=0
		END

		-- 代理的充值返利 功能模块
		DECLARE @AgentAwardType INT
		SELECT @AgentAwardType = StatusValue FROM WHQJAgentDB.DBO.SystemStatusInfo WHERE StatusName = N'AgentAwardType'
		IF @AgentAwardType IS NOT NULL AND @AgentAwardType & 1 = 1
		BEGIN
			DECLARE @awardReturn INT
			EXEC @awardReturn = WHQJAgentDB.DBO.NET_PB_RecordAgentAward @UserID,@Score,1,@strOrdersID,@strErrorDescribe OUTPUT
		END
	END

END
RETURN 0
GO
