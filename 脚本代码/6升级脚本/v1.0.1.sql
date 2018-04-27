-- V1.0.1 新增配置
USE WHQJAccountsDB
GO

IF EXISTS (SELECT 1 FROM DBO.SystemStatusInfo WHERE StatusName = N'MobileBattleRecordMask')
	DELETE DBO.SystemStatusInfo WHERE StatusName = N'MobileBattleRecordMask'
GO

INSERT INTO [dbo].[SystemStatusInfo] ([StatusName],[StatusValue],[StatusString],[StatusTip],[StatusDescription],[SortID])
     VALUES (N'MobileBattleRecordMask',7,N'手机大厅战绩显示类型',N'大厅战绩类型',N'键值：
	 1：仅显示普通房卡战绩；
	 2：仅显示金币房卡战绩；
	 3：1+2；
	 4：仅显示金币游戏记录；
	 5：1+4；
	 6：2+4；
	 7：1+2+4；',50)
GO

USE WHQJGroupDB
GO


-- 俱乐部配置表 添加标题字段（后台用）
IF COL_LENGTH('IMGroupOption', 'OptionTip') IS NOT NULL  
BEGIN
  DECLARE @CONSTName VARCHAR(8000)
  SELECT @CONSTName=b.name FROM SYSCOLUMNS a,SYSOBJECTS b WHERE a.ID=OBJECT_ID('IMGroupOption') AND b.ID=a.CDEFAULT and a.NAME='OptionTip' and b.NAME like 'DF%'
  EXEC('ALTER TABLE IMGroupOption DROP CONSTRAINT '+@CONSTName)
  ALTER TABLE IMGroupOption DROP COLUMN OptionTip   
END
ALTER TABLE [dbo].[IMGroupOption] ADD OptionTip NVARCHAR(50) NOT NULL DEFAULT('')
GO

-- 俱乐部配置表 添加排序字段
IF COL_LENGTH('IMGroupOption', 'SortID') IS NOT NULL  
BEGIN
  DECLARE @CONSTName VARCHAR(8000)
  SELECT @CONSTName=b.name FROM SYSCOLUMNS a,SYSOBJECTS b WHERE a.ID=OBJECT_ID('IMGroupOption') AND b.ID=a.CDEFAULT and a.NAME='SortID' and b.NAME like 'DF%'
  EXEC('ALTER TABLE IMGroupOption DROP CONSTRAINT '+@CONSTName)
  ALTER TABLE IMGroupOption DROP COLUMN SortID   
END
ALTER TABLE [dbo].[IMGroupOption] ADD SortID INT NOT NULL DEFAULT(0)

-- 俱乐部配置 
TRUNCATE TABLE [dbo].[IMGroupOption]
INSERT DBO.IMGroupOption(StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID) 
VALUES(	0	,N'MaxMemberCount'	,	100, N'群组最大人数', N'单个群组最大人数 ',100)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'MaxCreateGroupCount',10, N'创建上限',N'单人最多能创建群组数量，配置范围1-10',101)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'CreateGroupTakeIngot',0,N'创建条件',N'创建群组的条件：0、没有条件 1、消耗金币 2、消耗钻石 3、检测用户金币数量 4、检测用户钻石数量',102)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'CreateGroupDeductIngot',0, N'创建消耗',N'与创建条件配合使用，当创建条件的配置不为0时，消耗或者检测金币钻石的数量，不配置则等同没有条件，键值等于数量',103)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'MaxJoinGroupCount',10, N'加入上限',N'单人最多加入群组数量，配置范围1-10',104)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'GroupPayType',3, N'支付类型',N'创建约战房间时由谁支付：1、开启群主支付 2、开启玩家支付 3、表示同时开启群主支付和玩家支付',105)
GO
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'GroupPayTypeChange',1, N'支付服务',N'支付服务是否支持修改：0、不可以修改 1、可以修改',106)
GO	
INSERT DBO.IMGroupOption (StationID,OptionName,OptionValue,OptionTip,OptionDescribe,SortID)
VALUES (0,N'GroupRoomType',3, N'创建房间类型',N'设定玩家可以创建的房间类型：1、开启金币房卡 2、开启积分房卡 3、同时开启金币房卡和积分房卡',107)
GO	

USE [WHQJPlatformManagerDB]
GO

  -- 插入新的模块
DELETE DBO.Base_Module WHERE ModuleID = 9 OR ParentID = 9
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (9,0,N'俱乐部系统',N'',9,0,1,N'',0)
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (901,9,N'系统设置',N'/Module/ClubManager/SystemSet.aspx',1,0,0,N'',0)
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (902,9,N'俱乐部管理',N'/Module/ClubManager/GroupList.aspx',2,0,0,N'',0)
GO

  -- 插入新模块的权限
DELETE DBO.Base_ModulePermission WHERE ModuleID IN (901,902)
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (901,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (901,N'新增',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (901,N'修改',4,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (901,N'删除',8,0,0,1)
GO

INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (902,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (902,N'冻结/解冻',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (902,N'移交群主',4,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (902,N'强制解散',8,0,0,1)
GO