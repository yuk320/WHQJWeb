using System.Data;
using Game.Data.Factory;
using Game.Entity.Agent;
using Game.IData;
using Game.Kernel;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 代理外观
    /// </summary>
    public class AgentFacade
    {
        #region Fields

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        private IAgentDataProvider agentData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AgentFacade()
        {
            agentData = ClassFactory.GetIAgentDataProvider();
        }

        #endregion 构造函数

        #region 公用分页
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            return agentData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="statusName">配置键值</param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string statusName)
        {
            return agentData.GetSystemStatusInfo(statusName);
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        public int UpdateSystemStatusInfo(SystemStatusInfo statusinfo)
        {
            return agentData.UpdateSystemStatusInfo(statusinfo);
        }
        #endregion

        #region 代理系统
        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="agentId">代理标识</param>
        /// <returns></returns>
        public AgentInfo GetAgentInfo(int agentId)
        {
            return agentData.GetAgentInfo(agentId);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        public Message InsertAgentUser(AgentInfo agent, int pGameId = 0)
        {
            return agentData.InsertAgentUser(agent, pGameId);
        }
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        public int UpdateAgentUser(AgentInfo agent)
        {
            return agentData.UpdateAgentUser(agent);
        }
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        public int NullityAgentUser(string strList, int nullity)
        {
            return agentData.NullityAgentUser(strList, nullity);
        }
        #endregion
    }
}