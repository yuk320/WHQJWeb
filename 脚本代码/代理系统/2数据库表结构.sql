USE [WHQJAgentDB]
GO

/** 代理系统代理信息表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name=N'AgentInfo')
BEGIN
	DROP TABLE DBO.AgentInfo
END
GO

/****** Object:  Table [dbo].[AgentInfo]    Script Date: 2018/4/13 9:39:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgentInfo](
	[AgentID] [int] IDENTITY(1,1) NOT NULL,
	[ParentAgent] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[Compellation] [nvarchar](16) NOT NULL,
	[QQAccount] [nvarchar](32) NOT NULL,
	[WCNickName] [nvarchar](32) NOT NULL,
	[ContactPhone] [nvarchar](11) NOT NULL,
	[ContactAddress] [nvarchar](50) NOT NULL,
	[AgentDomain] [nvarchar](50) NOT NULL,
	[AgentLevel] [tinyint] NOT NULL,
	[AgentNote] [nvarchar](100) NOT NULL,
	[DiamondAward] [bigint] NOT NULL,
	[GoldAward] [bigint] NOT NULL,
	[BelowUser] [int] NOT NULL,
	[BelowAgent] [int] NOT NULL,
	[Nullity] [tinyint] NOT NULL,
	[CollectDate] [datetime] NOT NULL
 CONSTRAINT [PK_AgentInfo] PRIMARY KEY CLUSTERED 
(
	[AgentID] ASC,[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_ParentAgent]  DEFAULT ((0)) FOR [ParentAgent]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_Password]  DEFAULT ('') FOR [Password]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_Compellation]  DEFAULT ('') FOR [Compellation]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_QQAccount]  DEFAULT ('') FOR [QQAccount]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_WCNickName]  DEFAULT ('') FOR [WCNickName]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_ContactPhone]  DEFAULT ('') FOR [ContactPhone]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_ContactAddress]  DEFAULT ('') FOR [ContactAddress]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_AgentDomain]  DEFAULT ('') FOR [AgentDomain]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_AgentLevel]  DEFAULT ((0)) FOR [AgentLevel]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_AgentNote]  DEFAULT ('') FOR [AgentNote]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_DiamondAward]  DEFAULT ((0)) FOR [DiamondAward]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_GoldAward]  DEFAULT ((0)) FOR [GoldAward]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_BelowUser]  DEFAULT ((0)) FOR [BelowUser]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_BelowAgent]  DEFAULT ((0)) FOR [BelowAgent]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_Nullity]  DEFAULT ((0)) FOR [Nullity]
GO

ALTER TABLE [dbo].[AgentInfo] ADD  CONSTRAINT [DF_AgentInfo_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'AgentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级代理标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'ParentAgent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安全密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'Compellation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'QQ账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'QQAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'WCNickName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'ContactPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'ContactAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理域名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'AgentDomain'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'AgentLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'AgentNote'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'钻石返利' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'DiamondAward'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金币返利' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'GoldAward'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下线玩家数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'BelowUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下线代理数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'BelowAgent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'禁用标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'Nullity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentInfo', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO

/** 代理下线信息表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name=N'AgentBelowInfo')
BEGIN
	DROP TABLE DBO.AgentBelowInfo
END
GO

/****** Object:  Table [dbo].[AgentBelowInfo]    Script Date: 2018/4/26 10:51:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgentBelowInfo](
	[AgentID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CollectDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AgentBelowInfo] PRIMARY KEY CLUSTERED 
(
	[AgentID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AgentBelowInfo] ADD  CONSTRAINT [DF_AgentBelowInfo_AgentID]  DEFAULT ((0)) FOR [AgentID]
GO

ALTER TABLE [dbo].[AgentBelowInfo] ADD  CONSTRAINT [DF_AgentBelowInfo_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[AgentBelowInfo] ADD  CONSTRAINT [DF_AgentBelowInfo_CollectDate]  DEFAULT (GETDATE()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentBelowInfo', @level2type=N'COLUMN',@level2name=N'AgentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理下线用户标识（无论是下线玩家还是下级代理）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentBelowInfo', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收集时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentBelowInfo', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO


/** 代理系统设置表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name=N'SystemStatusInfo')
BEGIN
	DROP TABLE DBO.SystemStatusInfo
END
GO

/****** Object:  Table [dbo].[SystemStatusInfo]    Script Date: 2018/4/13 9:49:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemStatusInfo](
	[StatusName] [nvarchar](32) NOT NULL,
	[StatusValue] [int] NOT NULL CONSTRAINT [DF_SystemStatusInfo_StatusValue]  DEFAULT ((0)),
	[StatusString] [nvarchar](512) NOT NULL CONSTRAINT [DF_SystemStatusInfo_StatusString]  DEFAULT (''),
	[StatusTip] [nvarchar](50) NOT NULL CONSTRAINT [DF_SystemStatusInfo_StatusTip]  DEFAULT (''),
	[StatusDescription] [nvarchar](100) NOT NULL CONSTRAINT [DF_SystemStatusInfo_StatusDescription]  DEFAULT (''),
	[SortID] [int] NOT NULL CONSTRAINT [DF_SystemStatusInfo_SortID]  DEFAULT ((0)),
 CONSTRAINT [PK_SystemStatusInfo] PRIMARY KEY CLUSTERED 
(
	[StatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态名字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemStatusInfo', @level2type=N'COLUMN',@level2name=N'StatusName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态数值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemStatusInfo', @level2type=N'COLUMN',@level2name=N'StatusValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态字符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemStatusInfo', @level2type=N'COLUMN',@level2name=N'StatusString'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemStatusInfo', @level2type=N'COLUMN',@level2name=N'StatusTip'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemStatusInfo', @level2type=N'COLUMN',@level2name=N'StatusDescription'
GO

/** 代理返利配置表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name = N'ReturnAwardConfig')
BEGIN
	DROP TABLE DBO.ReturnAwardConfig
END
GO

/****** Object:  Table [dbo].[ReturnAwardConfig]    Script Date: 2018/4/16 11:12:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReturnAwardConfig](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[AwardType] [tinyint] NOT NULL,
	[AwardLevel] [int] NOT NULL,
	[AwardScale] [decimal](18, 6) NOT NULL,
	[Nullity] [bit] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ReturnAwardConfig] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReturnAwardConfig] ADD  CONSTRAINT [DF_ReturnAwardConfig_AwardType]  DEFAULT ((0)) FOR [AwardType]
GO

ALTER TABLE [dbo].[ReturnAwardConfig] ADD  CONSTRAINT [DF_ReturnAwardConfig_AwardLevel]  DEFAULT ((0)) FOR [AwardLevel]
GO

ALTER TABLE [dbo].[ReturnAwardConfig] ADD  CONSTRAINT [DF_ReturnAwardConfig_AwardScale]  DEFAULT ((0)) FOR [AwardScale]
GO

ALTER TABLE [dbo].[ReturnAwardConfig] ADD  CONSTRAINT [DF_ReturnAwardConfig_Nullity]  DEFAULT ((0)) FOR [Nullity]
GO

ALTER TABLE [dbo].[ReturnAwardConfig] ADD  CONSTRAINT [DF_ReturnAwardConfig_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利配置标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'ConfigID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'AwardType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'AwardLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'AwardScale'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用（0：启用、1：禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'Nullity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardConfig', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

/** 代理返利记录表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name = N'ReturnAwardRecord')
BEGIN
	DROP TABLE DBO.ReturnAwardRecord
END
GO
/****** Object:  Table [dbo].[ReturnAwardRecord]    Script Date: 2018/4/16 15:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReturnAwardRecord](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[SourceUserID] [int] NOT NULL,
	[TargetUserID] [int] NOT NULL,
	[ReturnBase] [bigInt] NOT NULL,
	[AwardLevel] [int] NOT NULL,
	[AwardType] [tinyint] NOT NULL,
	[AwardScale] [decimal](18, 6) NOT NULL,
	[Award] [int] NOT NULL,
	[CollectDate] [datetime] NOT NULL,
	[ExtraField] NVARCHAR(32) NOT NULL,
 CONSTRAINT [PK_ReturnAwardRecord] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_SourceUserID]  DEFAULT ((0)) FOR [SourceUserID]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_TargetUserID]  DEFAULT ((0)) FOR [TargetUserID]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_ReturnBase]  DEFAULT ((0)) FOR [ReturnBase]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_AwardLevel]  DEFAULT ((0)) FOR [AwardLevel]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_AwardType]  DEFAULT ((0)) FOR [AwardType]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_AwardScale]  DEFAULT ((0)) FOR [AwardScale]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_Award]  DEFAULT ((0)) FOR [Award]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

ALTER TABLE [dbo].[ReturnAwardRecord] ADD  CONSTRAINT [DF_ReturnAwardRecord_ExtraField]  DEFAULT (N'') FOR [ExtraField]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'RecordID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利原用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'SourceUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'TargetUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利基准' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'ReturnBase'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利推广级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'AwardLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'AwardType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'AwardScale'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利值 （根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】））' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'Award'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段：充值返利记录充值【充值订单号】；游戏税收返利记录【游戏名称-房间名称】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardRecord', @level2type=N'COLUMN',@level2name=N'ExtraField'
GO

/** 代理返利统计表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name = N'ReturnAwardSteam')
BEGIN
	DROP TABLE DBO.ReturnAwardSteam
END
GO
/****** Object:  Table [dbo].[ReturnAwardSteam]    Script Date: 2018/4/16 15:51:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReturnAwardSteam](
	[DateID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[AwardType] [int] NOT NULL,
	[AwardLevel] [int] NOT NULL,
	[Award] [bigint] NOT NULL,
	[InsertTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ReturnAwardSteam] PRIMARY KEY CLUSTERED 
(
	[DateID] ASC,
	[UserID] ASC,
	[AwardType] ASC,
	[AwardLevel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_DateID]  DEFAULT ((0)) FOR [DateID]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_AwardType]  DEFAULT ((0)) FOR [AwardType]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_AwardLevel]  DEFAULT ((0)) FOR [AwardLevel]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_Award]  DEFAULT ((0)) FOR [Award]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_InsertTime]  DEFAULT (getdate()) FOR [InsertTime]
GO

ALTER TABLE [dbo].[ReturnAwardSteam] ADD  CONSTRAINT [DF_ReturnAwardSteam_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'DateID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'AwardType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利级别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'AwardLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'Award'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'InsertTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardSteam', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

/** 代理返利领取表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE name = N'ReturnAwardReceive')
BEGIN
	DROP TABLE DBO.ReturnAwardReceive
END
GO
/****** Object:  Table [dbo].[ReturnAwardReceive]    Script Date: 2018/4/16 17:56:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReturnAwardReceive](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[AwardType] [tinyint] NOT NULL,
	[ReceiveAward] [int] NOT NULL,
	[ReceiveBefore] [int] NOT NULL,
	[CollectDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ReturnAwardReceive] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReturnAwardReceive] ADD  CONSTRAINT [DF_ReturnAwardReceive_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[ReturnAwardReceive] ADD  CONSTRAINT [DF_ReturnAwardReceive_AwardType]  DEFAULT ((0)) FOR [AwardType]
GO

ALTER TABLE [dbo].[ReturnAwardReceive] ADD  CONSTRAINT [DF_ReturnAwardReceive_ReceiveAward]  DEFAULT ((0)) FOR [ReceiveAward]
GO

ALTER TABLE [dbo].[ReturnAwardReceive] ADD  CONSTRAINT [DF_ReturnAwardReceive_ReceiveBefore]  DEFAULT ((0)) FOR [ReceiveBefore]
GO

ALTER TABLE [dbo].[ReturnAwardReceive] ADD  CONSTRAINT [DF_ReturnAwardReceive_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'RecordID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'AwardType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'ReceiveAward'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取前对应字段值 根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'ReceiveBefore'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领取时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardReceive', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO

/** 代理返利转赠记录表 **/
IF EXISTS (SELECT 1 FROM SYS.TABLES WHERE Name = N'ReturnAwardGrant')
BEGIN
	DROP TABLE DBO.ReturnAwardGrant
END
GO

/****** Object:  Table [dbo].[ReturnAwardGrant]    Script Date: 2018/4/18 10:10:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReturnAwardGrant](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[SourceUserID] [int] NOT NULL,
	[TargetUserID] [int] NOT NULL,
	[TradeType] [tinyint] NOT NULL,
	[SourceBefore] [bigint] NOT NULL,
	[TargetBefore] [bigint] NOT NULL,
	[Amount] [bigint] NOT NULL,
	[ClientIP] [nvarchar](15) NOT NULL,
	[CollectDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ReturnAwardGrant] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_SourceUserID]  DEFAULT ((0)) FOR [SourceUserID]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_TargetUserID]  DEFAULT ((0)) FOR [TargetUserID]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_TradeType]  DEFAULT ((0)) FOR [TradeType]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_SourceBefore]  DEFAULT ((0)) FOR [SourceBefore]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_TargetBefore]  DEFAULT ((0)) FOR [TargetBefore]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_ClientIP]  DEFAULT (N'') FOR [ClientIP]
GO

ALTER TABLE [dbo].[ReturnAwardGrant] ADD  CONSTRAINT [DF_ReturnAwardGrant_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转赠记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'RecordID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转出代理用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'SourceUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转入方用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'TargetUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转赠类型： 同返利类型 1：充值返利【钻石】 2：游戏税收返利【金币】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'TradeType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转出方原值 根据类型不同 1：代表转出前钻石 2：代表转出前携带金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'SourceBefore'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转入方原值 根据类型不同 1：代表转入前钻石 2：代表转入前携带金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'TargetBefore'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转赠数值： 根据类型 1：代表钻石 2：代表金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'ClientIP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReturnAwardGrant', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO


