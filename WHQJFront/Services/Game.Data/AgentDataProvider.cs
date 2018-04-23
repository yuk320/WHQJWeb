using System;
using System.Collections.Generic;
using System.Data;
using Game.Kernel;
using Game.IData;
using System.Data.Common;
using Game.Entity.Agent;
using Game.Utils;

namespace Game.Data
{
    /// <summary>
    /// 代理数据访问层
    /// </summary>
    public sealed class AgentDataProvider : BaseDataProvider, IAgentDataProvider
    {
        #region Fields

        private readonly ITableProvider _tbAgentInfoProvider;
        private readonly ITableProvider _tbSystemStatusInfoProvider;

        #endregion

        #region 构造方法

        public AgentDataProvider(string connString)
            : base(connString)
        {
            _tbSystemStatusInfoProvider = GetTableProvider(SystemStatusInfo.Tablename);
            _tbAgentInfoProvider = GetTableProvider(AgentInfo.Tablename);
        }

        #endregion

        #region 系统配置

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string key)
        {
            return _tbSystemStatusInfoProvider.GetObject<SystemStatusInfo>($"WHERE StatusName = N'{key}'");
        }

        #endregion

        #region 代理信息

        /// <summary>
        /// 获取代理信息（通过代理标识或用户标识）
        /// </summary>
        /// <param name="agentId">代理标识</param>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public AgentInfo GetAgentInfo(int agentId, int userId)
        {
            return _tbAgentInfoProvider.GetObject<AgentInfo>($"WHERE AgentID = {agentId} OR UserID = {userId}");
        }

        /// <summary>
        /// 代理用户登录（微信）
        /// </summary>
        /// <param name="unionid">微信标识</param>
        /// <param name="ip">登录ip</param>
        /// <returns></returns>
        public Message AgentWXLogin(string unionid, string ip)
        {
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("strUserUin", unionid),
                    Database.MakeInParam("strClientIP", ip),
                    Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
                };

            return MessageHelper.GetMessageForObject<AgentInfo>(Database, "NET_AT_AgentLogin_WX", prams);
        }

        /// <summary>
        /// 代理用户登录（手机号码+安全密码）
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message AgentMobileLogin(string mobile, string pass, string ip)
        {
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("strMobile", mobile),
                    Database.MakeInParam("strPassword", pass),
                    Database.MakeInParam("strClientIP", ip),
                    Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
                };

            return MessageHelper.GetMessageForObject<AgentInfo>(Database, "NET_AT_AgentLogin_MP", prams);
        }

        /// <summary>
        /// 玩家绑定代理
        /// </summary>
        /// <param name="userId">玩家用户标识</param>
        /// <param name="gameId">预绑定上级游戏标识</param>
        /// <returns></returns>
        public Message UserBindAgent(int userId, int gameId)
        {
            List<DbParameter> parms = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwGameID", gameId),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessage(Database, "NET_PB_UserAgentBind", parms);
        }

        #endregion

        #region 返利相关

        /// <summary>
        /// 代理返利
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="awardType"></param>
        /// <param name="returnBase"></param>
        /// <returns></returns>
        public Message AgentAward(int userId, byte awardType, long returnBase)
        {
            List<DbParameter> parms = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwReturnBase", returnBase),
                Database.MakeInParam("dwAwardType", awardType),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessage(Database, "NET_PB_RecordAgentAward", parms);
        }

        public Message ReceiveAgentAward(int userId, byte awardType, long award)
        {
            List<DbParameter> parms = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwAward", award),
                Database.MakeInParam("dwAwardType", awardType),
                Database.MakeInParam("strClientIP", GameRequest.GetUserIP()),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessage(Database, "NET_AT_ReceiveAgentAward", parms);
        }

        /// <summary>
        /// 转赠奖励（单独使用时则为单向转赠，与领取返利一起使用则转赠返利）
        /// </summary>
        /// <param name="userId">代理用户标识</param>
        /// <param name="gameId">被转赠目标游戏标识</param>
        /// <param name="awardType">1：充值返利【钻石】 2：游戏税收返利【金币】</param>
        /// <param name="award">转赠数额</param>
        /// <param name="password">安全密码</param>
        /// <returns></returns>
        public Message GiveAgentAward(int userId, int gameId, byte awardType, long award, string password)
        {
            List<DbParameter> parms = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwAward", award),
                Database.MakeInParam("dwAwardType", awardType),
                Database.MakeInParam("strClientIP", GameRequest.GetUserIP()),
                Database.MakeInParam("strPassword", password),
                Database.MakeInParam("dwGameID", gameId),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessage(Database, "NET_AT_GiveAgentAward", parms);
        }

        /// <summary>
        /// 获取代理累计总奖励 by awardType 不传为 充值返利，传2 为游戏税收返利 
        /// </summary>
        /// <param name="userId">必填 代理的用户标识</param>
        /// <param name="typeId">默认不传统计代理充值返利</param>
        /// <param name="sourceUserId">默认不传统计代理所有该类返利</param>
        /// <returns></returns>
        public long GetTotalAward(int userId, int typeId = 1, int sourceUserId = 0)
        {
            string sql =
                $" SELECT ISNULL(SUM(Award),0) FROM ReturnAwardRecord WHERE TargetUserID = {userId} AND AwardType= {typeId} ";
            if (sourceUserId > 0)
            {
                sql += $"AND SourceUserID = {sourceUserId} ";
            }
            object result = Database.ExecuteScalar(CommandType.Text, sql);
            return Convert.ToInt64(result);
        }

        #endregion

        #region 直属下线

        /// <summary>
        /// 获取我的直属下线（玩家和代理）分页列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetBelowListPagerSet(string sqlWhere, int pageIndex, int pageSize)
        {
            string[] returnField = {"UserID", "GameID", "NickName", "AgentID", "RegisterDate"};
            PagerParameters prams = new PagerParameters("WHQJAccountsDB.DBO.AccountsInfo", "ORDER BY RegisterDate DESC",
                sqlWhere, pageIndex, pageSize)
            {
                Fields = returnField,
                CacherSize = 2
            };
            return GetPagerSet2(prams);
        }

        /// <summary>
        /// 获取直属代理的下线玩家
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetBelowAgentsUser(int userId)
        {
            return (int)Database.ExecuteScalar(CommandType.Text,
                $" SELECT Count(1) FROM WHQJAccountsDB.DBO.AccountsInfo WHERE SpreaderID IN ( SELECT UserID FROM WF_GetAgentBelowAgent({userId}) WHERE LevelID = 2) ");
        }

        /// <summary>
        /// 获取直属代理的下线代理
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetBelowAgentsAgent(int userId)
        {
            return (int)Database.ExecuteScalar(CommandType.Text,
                $" SELECT Count(1) FROM WF_GetAgentBelowAgent({userId}) WHERE LevelID = 3 ");
        }

        /// <summary>
        /// 代理添加直属代理
        /// </summary>
        /// <param name="userId">代理用户标识</param>
        /// <param name="info">代理资料</param>
        /// <param name="gameId">直属代理游戏标识</param>
        /// <returns></returns>
        public Message AddAgent(int userId, AgentInfo info, int gameId)
        {
            List<DbParameter> parms = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwGameID", gameId),
                Database.MakeInParam("strCompellation", info.Compellation),
                Database.MakeInParam("strAgentDomain", info.AgentDomain),
                Database.MakeInParam("strQQAccount", info.QQAccount),
                Database.MakeInParam("strWCNickName", info.WCNickName),
                Database.MakeInParam("strContactPhone", info.ContactPhone),
                Database.MakeInParam("strContactAddress", info.ContactAddress),
                Database.MakeInParam("strAgentNote", info.AgentNote),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessage(Database, "NET_AT_AddAgent", parms);
        }

        #endregion
    }
}
