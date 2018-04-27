USE WHQJPlatformManagerDB
GO

 -- 删除旧的代理管理入口
DELETE DBO.Base_Module WHERE ModuleID = 103
-- 插入新的模块
DELETE DBO.Base_Module WHERE ModuleID = 10 OR ParentID = 10
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (10,0,N'代理系统',N'',10,0,1,N'',0)
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (1000,10,N'系统设置',N'/Module/AgentManager/SystemSet.aspx',1,0,0,N'',0)
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (1001,10,N'代理管理',N'/Module/AgentManager/AgentList.aspx',2,0,0,N'',0)
INSERT DBO.Base_Module (ModuleID,ParentID,Title,Link,OrderNo,Nullity,IsMenu,[Description],ManagerPopedom)
VALUES (1002,10,N'代理返利',N'/Module/AgentManager/AgentReturnConfigList.aspx',3,0,0,N'',0)
GO

-- 插入新模块的权限
DELETE DBO.Base_ModulePermission WHERE ModuleID IN (1000,1001)
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1000,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1000,N'修改',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1001,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1001,N'新增',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1001,N'修改',4,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1001,N'冻结',8,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1002,N'查看',1,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1002,N'新增',2,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1002,N'修改',4,0,0,1)
GO
INSERT INTO DBO.Base_ModulePermission ([ModuleID] ,[PermissionTitle] ,[PermissionValue] ,[Nullity] ,[StateFlag] ,[ParentID])
VALUES (1002,N'删除',8,0,0,1)
GO

USE WHQJAgentDB
GO

/** 代理系统-系统配置-配置项 S **/
TRUNCATE TABLE DBO.SystemStatusInfo
INSERT SystemStatusInfo ([StatusName],[StatusValue],[StatusString],[StatusTip],[StatusDescription],[SortID])
VALUES (N'AgentAwardType',3,'代理返利模式','返利模式','键值：1代表仅开放充值返利【钻石】，2代表仅开放游戏税收返利【金币】，3代表同时开启1、2两种返利模式',10)
GO
INSERT SystemStatusInfo ([StatusName],[StatusValue],[StatusString],[StatusTip],[StatusDescription],[SortID])
VALUES (N'ReceiveDiamondSave',100,'提取钻石保留','提取钻石保留','键值：0代表提取钻石时无保留，大于0代表每次提取钻石不可大于可提取值-保留值',50)
GO
INSERT SystemStatusInfo ([StatusName],[StatusValue],[StatusString],[StatusTip],[StatusDescription],[SortID])
VALUES (N'ReceiveGoldSave',10000,'提取金币保留','提取金币保留','键值：0代表提取金币时无保留，大于0代表每次提取金币不可大于可提取值-保留值',51)
GO
/** 代理系统-系统配置-配置项 E **/

/** 代理系统-返利配置-配置项 S **/
TRUNCATE TABLE DBO.ReturnAwardConfig
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (1,1,0.35,0)
GO
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (1,2,0.07,0)
GO
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (1,3,0.03,0)
GO
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (2,1,0.35,0)
GO
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (2,2,0.07,0)
GO
INSERT ReturnAwardConfig(AwardType,AwardLevel,AwardScale,Nullity) 
VALUES (2,3,0.03,0)
GO
/** 代理系统-返利配置-配置项 E **/

