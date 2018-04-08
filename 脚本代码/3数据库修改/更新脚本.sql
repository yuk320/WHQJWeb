USE [WHQJAccountsDB]
GO

-- V1.1.0
DELETE DBO.SystemStatusInfo WHERE StatusName = N'IOSNotStorePaySwitch'
DELETE DBO.SystemStatusInfo WHERE StatusName = N'JJGoldBuyProp'
-- V1.1.4 2017/12/26 删除全局系统配置的钻石购买大喇叭数量
DELETE DBO.SystemStatusInfo WHERE StatusName = N'JJDiamondBuyProp'

-- V1.1.0 2017/11/16 添加全局推广返利类型 0：金币 1：钻石
-- INSERT INTO SystemStatusInfo
--   (StatusName,StatusValue,StatusString,StatusTip,StatusDescription,SortID)
-- VALUES(N'SpreadReturnType', 0, N'全局推广返利类型', N'推广返利类型', N'键值：推广返利类型，在推广返利配置无可用配置时不生效，0表示金币 1表示钻石', 99)
-- V1.1.0 2017/11/23 添加全局推广返利领取门槛 0：无门槛 大于0代表 需要可领取数大于多少才能提取
-- INSERT INTO SystemStatusInfo
--   (StatusName,StatusValue,StatusString,StatusTip,StatusDescription,SortID)
-- VALUES(N'SpreadReceiveBase', 0, N'全局推广返利领取门槛', N'推广返利条件', N'键值：推广返利条件，0：无门槛 大于0代表 需要可领取数大于多少才能提取', 100)

-- V1.1.3 2017/12/13 用户表添加位置信息 已确定新版本会有
-- ALTER TABLE [dbo].[AccountsInfo] ADD [PlaceName] NVARCHAR(33) NOT NULL DEFAULT(N'')
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后一次登录地名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AccountsInfo', @level2type=N'COLUMN',@level2name=N'PlaceName'
-- GO

USE [WHQJNativeWebDB]
GO

-- V1.1.0
DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameAndroidConfig'
DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameIosConfig'

-- V1.1.6 游戏规则 增加排序字段。
ALTER TABLE [dbo].[GameRule] ADD [SortID] INT NOT NULL DEFAULT(0)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩法排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameRule', @level2type=N'COLUMN',@level2name=N'SortID'
GO

USE [WHQJTreasureDB]
GO

-- V1.1.0
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPayConfig', @level2type=N'COLUMN',@level2name=N'PresentScale'
GO
ALTER TABLE [dbo].[AppPayConfig] DROP CONSTRAINT [DF_AppPayConfig_PresentScale]
GO
ALTER TABLE [dbo].[AppPayConfig] DROP COLUMN [PresentScale]
GO
ALTER TABLE [dbo].[AppPayConfig] ADD [PresentDiamond] INT NOT NULL DEFAULT(0)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首冲赠送钻石数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPayConfig', @level2type=N'COLUMN',@level2name=N'PresentDiamond'
GO

-- V1.1.0
IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[SpreadReturnConfig]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[SpreadReturnConfig]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpreadReturnConfig]
(
  [ConfigID] [int] IDENTITY(1,1) NOT NULL,
  [SpreadLevel] [int] NOT NULL,
  [PresentScale] [decimal](18, 6) NOT NULL,
  [Nullity] [bit] NOT NULL,
  [UpdateTime] [datetime] NOT NULL,
  CONSTRAINT [PK_SpreadReturnConfig] PRIMARY KEY CLUSTERED
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_SpreadLevel]  DEFAULT ((0)) FOR [SpreadLevel]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_PresentScale]  DEFAULT ((0)) FOR [PresentScale]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_Nullity]  DEFAULT ((0)) FOR [Nullity]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'ConfigID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'SpreadLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值返点比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'PresentScale'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用（0：启用、1：禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'Nullity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

--
USE [WHQJRecordDB]
GO

IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[RecordSpreadReturn]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[RecordSpreadReturn]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecordSpreadReturn]
(
  [RecordID] [int] IDENTITY(1,1) NOT NULL,
  -- 记录标识
  [SourceUserID] [int] NOT NULL,
  -- 充值对象
  [TargetUserID] [int] NOT NULL,
  -- 返利对象
  [SourceDiamond] [int] NOT NULL,
  -- 充值所得钻石
  [SpreadLevel] [int] NOT NULL,
  -- 返利配置推广级别
  [ReturnScale] [decimal](18, 6) NOT NULL,
  -- 返利比例
  [ReturnNum] [int] NOT NULL,
  -- 返利数值 （根据ReturnType 0：金币 1：钻石）
  [ReturnType] [tinyint] NOT NULL,
  -- 返利类型 0：金币 1：钻石
  [CollectDate] [datetime] NOT NULL,
  -- 记录日期
  CONSTRAINT [PK_RecordSpreadReturn] PRIMARY KEY CLUSTERED
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SourceUserID]  DEFAULT ((0)) FOR [SourceUserID]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_TargetUserID]  DEFAULT ((0)) FOR [TargetUserID]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SourceDiamond]  DEFAULT ((0)) FOR [SourceDiamond]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SpreadLevel]  DEFAULT ((0)) FOR [SpreadLevel]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnScale]  DEFAULT ((0)) FOR [ReturnScale]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnNum]  DEFAULT ((0)) FOR [ReturnNum]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnType]  DEFAULT ((0)) FOR [ReturnType]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'RecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SourceUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'TargetUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值所得钻石' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SourceDiamond'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利配置推广级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SpreadLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnScale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利数值 （根据ReturnType 0：金币 1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（0：金币、1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO


IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[RecordSpreadReturnReceive]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[RecordSpreadReturnReceive]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecordSpreadReturnReceive]
(
  [RecordID] [int] IDENTITY(1,1) NOT NULL,
  -- 记录标识
  [UserID] [int] NOT NULL,
  -- 用户标识
  [ReceiveType] [tinyint] NOT NULL,
  -- 领取类型 0：金币 1：钻石
  [ReceiveNum] [int] NOT NULL,
  -- 领取数值 （根据ReceiveType 0：金币 1：钻石）
  [ReceiveBefore] [bigint] NOT NULL,
  -- 领取前数值（根据ReceiveType 0：金币 1：钻石）
  [ReceiveAddress] [nvarchar](15) NOT NULL,
  -- 领取地址
  [CollectDate] [datetime] NOT NULL,
  -- 记录日期
  CONSTRAINT [PK_RecordSpreadReturnReceive] PRIMARY KEY CLUSTERED
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_UserID]  DEFAULT ((0)) FOR [UserID]
GO
ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_ReceiveNum]  DEFAULT ((0)) FOR [ReceiveNum]
GO
ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_ReceiveType]  DEFAULT ((0)) FOR [ReceiveType]
GO
ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_ReceiveBefore]  DEFAULT ((0)) FOR [ReceiveBefore]
GO
ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_ReceiveAddress]  DEFAULT (N'') FOR [ReceiveBefore]
GO
ALTER TABLE [dbo].[RecordSpreadReturnReceive] ADD  CONSTRAINT [DF_RecordSpreadReturnReceive_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'RecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取类型（0：金币、1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'ReceiveType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取数值（根据ReceiveType 0：金币 1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'ReceiveNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取前数值（根据ReceiveType 0：金币 1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'ReceiveBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'ReceiveAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturnReceive', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO

-- V1.1.4暂不更新此修改 道具功能迁移
-- ALTER TABLE [dbo].[RecordBuyNewProperty] ADD [BeforeScore] BIGINT NOT NULL DEFAULT(0)
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买前携带金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProperty', @level2type=N'COLUMN',@level2name=N'BeforeInsure'
-- GO
-- ALTER TABLE [dbo].[RecordBuyNewProperty] ADD [Score] INT NOT NULL DEFAULT(0)
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买花费携带金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProperty', @level2type=N'COLUMN',@level2name=N'Insure'
-- GO


USE [WHQJPlatformManagerDB]
GO

-- V1.1.4 道具功能迁移
DELETE DBO.Base_Module WHERE ModuleID = 306
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (306,3,N'道具管理',N'/Module/AppManager/PropertyConfigList.aspx',7,0,0,N'',0)
GO
INSERT INTO [dbo].[Base_ModulePermission] ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (306,N'查看',1,0,0,1)
GO
INSERT INTO [dbo].[Base_ModulePermission] ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (306,N'编辑',2,0,0,1)
GO

-- V1.1.6 后台修复其他权限无查看在线玩家的问题。
INSERT DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (101,N'查看',1,0,0,1)
GO

USE [WHQJPlatformDB]
GO

-- V1.1.4暂不更新此修改 道具功能迁移
-- EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProperty', @level2type=N'COLUMN',@level2name=N'ExchangeRatio'
-- GO
-- ALTER TABLE [dbo].[GameProperty] DROP CONSTRAINT [DF_GameProperty_ExchangeRatio]
-- ALTER TABLE [dbo].[GameProperty] DROP CONSTRAINT [DF_GameProperty_Diamond]
-- GO
-- ALTER TABLE [dbo].[GameProperty] DROP COLUMN [ExchangeRatio]
-- GO
-- ALTER TABLE [dbo].[GameProperty] ADD [ExchangeDiamondRatio] INT NOT NULL DEFAULT(0)
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'钻石兑换道具比例1钻石:N' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProperty', @level2type=N'COLUMN',@level2name=N'ExchangeDiamondRatio'
-- GO
-- ALTER TABLE [dbo].[GameProperty] ADD [ExchangeGoldRatio] INT NOT NULL DEFAULT(0)
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金币兑换道具比例N金币:1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameProperty', @level2type=N'COLUMN',@level2name=N'ExchangeGoldRatio'
-- GO
-- UPDATE [dbo].[GameProperty] SET ExchangeDiamondRatio = 10 WHERE ID = 306 --大喇叭重新赋值
-- GO

USE [WHQJNativeWebDB]
GO

-- V1.1.7 游戏规则 简介字段修改
ALTER TABLE [dbo].[GameRule] DROP CONSTRAINT [DF_GameRule_KindIntro]
ALTER TABLE [dbo].[GameRule] ALTER COLUMN [KindIntro] NVARCHAR(MAX) NOT NULL
ALTER TABLE [dbo].[GameRule] ADD CONSTRAINT [DF_GameRule_KindIntro] DEFAULT (N'') FOR [KindIntro]
GO

-- V1.1.7 新增常见问题管理
 -- 建表
IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[Question]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[Question]
END

/****** Object:  Table [dbo].[Question]    Script Date: 2018/1/25 10:57:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionTitle] [nvarchar](128) NOT NULL CONSTRAINT [DF_Question_QuestionTitle]  DEFAULT (N''),
	[Answer] [nvarchar](256) NOT NULL CONSTRAINT [DF_Question_Answer]  DEFAULT (N''),
	[SortID] [int] NOT NULL CONSTRAINT [DF_Question_SortID]  DEFAULT ((0)),
	[UpdateAt] [datetime] NOT NULL CONSTRAINT [DF_Question_UpdateAt]  DEFAULT (getdate()),
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED
(
	[ID] ASC,
	[QuestionTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问答标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'QuestionTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'答案' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'Answer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'UpdateAt'
GO

 -- 插入默认数据
SET IDENTITY_INSERT [dbo].[Question] ON
INSERT [dbo].[Question] ([ID], [QuestionTitle], [Answer], [SortID]) VALUES (1, N'如何获取房卡？', N'请联系客服：12345678', 1)
INSERT [dbo].[Question] ([ID], [QuestionTitle], [Answer], [SortID]) VALUES (2, N'如何获取钻石？', N'请联系客服：12345678', 2)
INSERT [dbo].[Question] ([ID], [QuestionTitle], [Answer], [SortID]) VALUES (3, N'如何联系客服？', N'请联系客服：12345678', 3)
INSERT [dbo].[Question] ([ID], [QuestionTitle], [Answer], [SortID]) VALUES (4, N'如何获取游戏币？', N'请联系客服：12345678', 4)
SET IDENTITY_INSERT [dbo].[Question] OFF


USE [WHQJPlatformManagerDB]
GO
-- V1.1.7 后台新增常见问题管理模块
  -- 插入新的模块
DELETE DBO.Base_Module WHERE ModuleID = 405
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (405,3,N'常见问题',N'/Module/WebManager/QuestionList.aspx',9,0,0,N'',0)
GO
  -- 插入新模块的权限
DELETE DBO.Base_ModulePermission WHERE ModuleID = 405
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (405,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (405,N'新增',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (405,N'修改',4,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (405,N'删除',8,0,0,1)
GO


USE [WHQJNativeWebDB]
GO

-- v1.1.10 更新站点配置，中网站站点配置的说明及字段。
UPDATE ConfigInfo SET ConfigString = N'参数说明
字段1：网站二维码地址
字段2：网站图片服务器地址
字段3：网站前台服务器地址
字段4：H5游戏服务器地址
字段5：代理网站域名或IP
字段8：网站前台底部内容',Field5 = N'/Card'


-- v1.1.10 新建代理认证信息表
IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[AgentTokenInfo]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[AgentTokenInfo]
END
GO

/****** Object:  Table [dbo].[AgentTokenInfo]    Script Date: 2018/3/16 16:32:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgentTokenInfo](
	[UserID] [int] NOT NULL,
	[AgentID] [int] NOT NULL,
	[Token] [nvarchar](64) NOT NULL,
	[ExpirtAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AgentTokenInfo] PRIMARY KEY CLUSTERED
(
	[UserID] ASC,
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_AgentID]  DEFAULT ((0)) FOR [AgentID]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_Token]  DEFAULT (N'') FOR [Token]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_ExpirtAt]  DEFAULT (getdate()+(1)) FOR [ExpirtAt]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'AgentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'认证串（SHA256）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'Token'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'ExpirtAt'
GO


-- v1.1.10 代理后台登录（手机号+安全密码）存储
----------------------------------------------------------------------------------------------------
-- 版权：2018
-- 时间：2018-03-16
-- 用途：代理后台登录（手机号+安全密码）
----------------------------------------------------------------------------------------------------

USE WHQJAccountsDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_AgentAccountsLogin_MP') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_AgentAccountsLogin_MP
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

-- 帐号登录
CREATE PROCEDURE NET_PW_AgentAccountsLogin_MP
	@strMobile NVARCHAR(11),					-- 手机号码
	@strPassword NVARCHAR(32),					-- 安全密码
	@strClientIP NVARCHAR(15),					-- 连接地址
	@strErrorDescribe	NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 基本信息
DECLARE @UserID INT
DECLARE @FaceID INT
DECLARE @Accounts NVARCHAR(31)
DECLARE @Nickname NVARCHAR(31)
DECLARE @UnderWrite NVARCHAR(63)
DECLARE @AgentID INT
DECLARE @Nullity BIT
DECLARE @StunDown BIT

-- 扩展信息
DECLARE @GameID INT
DECLARE @CustomID INT
DECLARE @Gender TINYINT
DECLARE @Experience INT
DECLARE @Loveliness INT
DECLARE @MemberOrder INT
DECLARE @MemberOverDate DATETIME
DECLARE @CustomFaceVer TINYINT
DECLARE @SpreaderID INT
DECLARE @PlayTimeCount INT
DECLARE @AgentNullity TINYINT

-- 辅助变量
DECLARE @EnjoinLogon AS INT
DECLARE @StatusString NVARCHAR(127)

-- 执行逻辑
BEGIN
	-- 系统暂停
	SELECT @EnjoinLogon=StatusValue,@StatusString=StatusString FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'EnjoinLogon'
	IF @EnjoinLogon=1
	BEGIN
		SELECT @strErrorDescribe=@StatusString
		RETURN 1001
	END

	-- 效验地址
	SELECT @EnjoinLogon=EnjoinLogon FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@strClientIP AND (EnjoinOverDate>GETDATE() OR EnjoinOverDate IS NULL)
	IF @EnjoinLogon=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，系统禁止了您所在的 IP 地址的登录功能！'
		RETURN 1002
	END

  -- 查询代理
  SELECT @AgentID = AgentID,@UserID = UserID,@AgentNullity=Nullity FROM AccountsAgentInfo WITH(NOLOCK) WHERE ContactPhone = @strMobile AND [Password] = @strPassword

  IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号不存在！'
		RETURN 1002
	END

	-- 查询用户
	SELECT @GameID=GameID, @Accounts=Accounts, @Nickname=Nickname, @UnderWrite=UnderWrite, @FaceID=FaceID,@CustomID=CustomID,
		@Gender=Gender, @Nullity=Nullity, @StunDown=StunDown, @SpreaderID=SpreaderID,@PlayTimeCount=PlayTimeCount,@AgentID=AgentID
	FROM AccountsInfo WITH(NOLOCK) WHERE UserID=@UserID

	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已冻结！'
		RETURN 1003
	END

	-- 帐号关闭
	IF @StunDown=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已开启安全关闭！'
		RETURN 1004
	END

  -- 代理判断
	IF @AgentID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	IF @AgentNullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	IF @AgentNullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的代理帐号已冻结！'
		RETURN 2002
	END

	-- 更新信息
	UPDATE AccountsInfo SET WebLogonTimes=WebLogonTimes+1,LastLogonDate=GETDATE(),LastLogonIP=@strClientIP WHERE UserID=@UserID

	-- 记录日志
	DECLARE @DateID INT
	SET @DateID=CAST(CAST(GETDATE() AS FLOAT) AS INT)
	UPDATE SystemStreamInfo SET WebLogonSuccess=WebLogonSuccess+1 WHERE DateID=@DateID
	IF @@ROWCOUNT=0 INSERT SystemStreamInfo (DateID, WebLogonSuccess) VALUES (@DateID, 1)

	-- 输出变量
	SELECT @UserID AS UserID, @GameID AS GameID, @Accounts AS Accounts, @Nickname AS Nickname,@UnderWrite AS UnderWrite, @FaceID AS FaceID, @CustomID AS CustomID,
		@Gender AS Gender,@AgentID AS AgentID
END

RETURN 0
GO


-- 1.1.10 新增配置值，代理中心版本
USE WHQJAccountsDB
GO

INSERT DBO.SystemStatusInfo (StatusName,StatusValue,StatusString,StatusTip,StatusDescription,SortID)
VALUES (N'AgentHomeVersion',1, N'代理后台的版本号，可切换新老后台',N'代理后台版本',N'键值：1-老版本房卡后台，2-新版本房卡后台',9999)
GO
