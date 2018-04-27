----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2018-04-27
-- 用途：手机端获取充值产品
----------------------------------------------------------------------------------------------------

USE WHQJTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetMobilePayConfig') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetMobilePayConfig
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetMobilePayConfig
	@dwUserID INT,
	@PayType INT
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

DECLARE @NowTime NVARCHAR(20)
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)

-- 执行逻辑
BEGIN
	-- 获取查询时间
	SET @NowTime = CONVERT(VARCHAR(10),GETDATE(),120) 
	SET @StartTime = @NowTime + N' 00:00:00'
	SET @EndTime = @NowTime + N' 23:59:59'

	-- 获取首充
	SELECT OnLineID FROM OnLinePayOrder WITH(NOLOCK) WHERE UserID=@dwUserID AND OrderStatus=1 AND OrderDate BETWEEN @StartTime AND @EndTime

	-- 获取充值产品
	SELECT apc.ConfigID,AppleID,PayName,PayType,PayPrice,PayIdentity,ImageType,SortID,Score,ScoreType,
	CASE ISNULL(pc.PayCount,0) WHEN 0 THEN FristPresent ELSE PresentScore END AS PresentScore,
	CASE ISNULL(pc.PayCount,0) WHEN 0 THEN 1 ELSE 0 END AS IsFristPay  
	FROM AppPayConfig(NOLOCK) AS apc 
	LEFT JOIN (SELECT ConfigID,COUNT(1) AS PayCount FROM OnLinePayOrder WITH(NOLOCK) WHERE UserID=@dwUserID AND OrderStatus>1 GROUP BY ConfigID,UserID) AS pc
	ON apc.ConfigID=pc.ConfigID
	WHERE  PayType = @PayType ORDER BY PayIdentity DESC,SortID ASC

END

RETURN 0

GO