using System.Collections.Generic;
using System.Data;
using Game.Entity.Agent;
using Game.Kernel;

namespace Game.IData
{
    /// <summary>
    /// 代理库数据层接口
    /// </summary>
    public interface IAgentDataProvider
    {
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
        PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);
        #endregion 

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="statusName">配置键值</param>
        /// <returns></returns>
        SystemStatusInfo GetSystemStatusInfo(string statusName);
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        int UpdateSystemStatusInfo(SystemStatusInfo statusinfo);
        #endregion

        #region 返利配置

        /// <summary>
        /// 获取代理返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        ReturnAwardConfig GetAgentReturnConfig(int configId);

        /// <summary>
        /// 删除代理返利配置
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteAgentReturnConfig(string ids);

        /// <summary>
        /// 保存代理返利配置（新增，修改）
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        int SaveAgentReturnConfig(ReturnAwardConfig cfg);

        #endregion

        #region 代理系统
        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="agentId">代理标识</param>
        /// <returns></returns>
        AgentInfo GetAgentInfo(int agentId);
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        Message InsertAgentUser(AgentInfo agent, int pGameId);
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        int UpdateAgentUser(AgentInfo agent);
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        int NullityAgentUser(string strList, int nullity);
        #endregion

        #region 直属下线

        /// <summary>
        /// 获取代理下线信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        AgentBelowInfo GetAgentBelowInfo(int userId);

        /// <summary>
        /// 获取直属下线的注册情况
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        IList<AgentBelowInfo> GetAgentBelowRegisterList(int agentId);

        #endregion
    }
}