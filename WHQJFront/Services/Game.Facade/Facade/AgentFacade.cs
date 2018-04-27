using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Entity.Agent;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 代理外观
    /// </summary>
    public class AgentFacade
    {
        #region Fields

        private readonly IAgentDataProvider agentData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AgentFacade()
        {
            agentData = ClassFactory.GetIAgentDataProvider();
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
            return agentData.GetSystemStatusInfo(key);
        }

        #endregion

        #region 代理信息

        /// <summary>
        /// 获取代理信息（通过代理标识或用户标识）
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AgentInfo GetAgentInfo(int agentId, int userId)
        {
            return agentData.GetAgentInfo(agentId, userId);
        }

        /// <summary>
        /// 代理用户登录（微信）
        /// </summary>
        /// <param name="unionid">微信标识</param>
        /// <param name="ip">登录ip</param>
        /// <returns></returns>
        public Message AgentWXLogin(string unionid, string ip)
        {
            return agentData.AgentWXLogin(unionid, ip);
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
            return agentData.AgentMobileLogin(mobile, pass, ip);
        }

        /// <summary>
        /// 获取代理下线对代理的累计充值总贡献
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sourceUserId"></param>
        /// <returns></returns>
        public long GetTotalDiamondAward(int userId, int sourceUserId)
        {
            return GetTotalDiamondAward(userId, null, sourceUserId);
        }

        /// <summary>
        /// 获取代理累计充值总返利
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="timeSpan">时间期间</param>
        /// <param name="sourceUserId">传值时代表某个下线对你的贡献</param>
        /// <returns></returns>
        public long GetTotalDiamondAward(int userId, string[] timeSpan = null, int sourceUserId = 0)
        {
            return agentData.GetTotalAward(userId, 1, sourceUserId, timeSpan);
        }

        /// <summary>
        /// 获取代理下线对代理的累计游戏税收总贡献
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sourceUserId"></param>
        /// <returns></returns>
        public long GetTotalGoldAward(int userId, int sourceUserId)
        {
            return GetTotalGoldAward(userId, null, sourceUserId);
        }

        /// <summary>
        /// 获取代理累计游戏税收总返利
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="timeSpan">时间期间</param>
        /// <param name="sourceUserId">传值时代表某个下线对你的贡献</param>
        /// <returns></returns>
        public long GetTotalGoldAward(int userId, string[] timeSpan = null, int sourceUserId = 0)
        {
            return agentData.GetTotalAward(userId, 2, sourceUserId, timeSpan);
        }

        /// <summary>
        /// 获取代理累计提取充值返利
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public long GetTotalDiamondReceive(int userId, string[] timeSpan = null)
        {
            return agentData.GetTotalReceive(userId, 1, timeSpan);
        }

        /// <summary>
        /// 获取代理累计提取游戏税收返利
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public long GetTotalGoldReceive(int userId, string[] timeSpan = null)
        {
            return agentData.GetTotalReceive(userId, 2, timeSpan);
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
            return agentData.GetBelowListPagerSet(sqlWhere, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取直属代理的下线玩家
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetBelowAgentsUser(int userId)
        {
            return agentData.GetBelowAgentsUser(userId);
        }

        /// <summary>
        /// 获取直属代理的下线代理
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetBelowAgentsAgent(int userId)
        {
            return agentData.GetBelowAgentsAgent(userId);
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
            return agentData.AddAgent(userId, info, gameId);
        }

        #endregion

        #region 代理返利

        /// <summary>
        /// 领取奖励
        /// </summary>
        /// <param name="userId">代理用户标识</param>
        /// <param name="awardType">1：充值返利【钻石】 2：游戏税收返利【金币】</param>
        /// <param name="award">领取的数量</param>
        /// <returns></returns>
        public Message ReceiveAgentAward(int userId, byte awardType, long award)
        {
            return agentData.ReceiveAgentAward(userId, awardType, award);
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
            return agentData.GiveAgentAward(userId, gameId, awardType, award, password);
        }

        #endregion
    }
}
