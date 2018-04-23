using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.IData;
using Game.Kernel;
using Game.Entity.Agent;

namespace Game.Data
{
    /// <summary>
    /// 代理库数据层
    /// </summary>
    public class AgentDataProvider : BaseDataProvider, IAgentDataProvider
    {
        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AgentDataProvider(string connString)
            : base(connString)
        {

        }

        #endregion 构造方法

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
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }
        #endregion 公用分页

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="statusName">配置键值</param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string statusName)
        {
            string sqlQuery = @"SELECT * FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=@StatusName";
            var prams = new List<DbParameter> { Database.MakeInParam("StatusName", statusName) };
            return Database.ExecuteObject<SystemStatusInfo>(sqlQuery, prams);
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        public int UpdateSystemStatusInfo(SystemStatusInfo statusinfo)
        {
            string sqlQuery = @"UPDATE SystemStatusInfo SET StatusValue=@StatusValue,StatusString=@StatusString,
                    StatusTip=@StatusTip,StatusDescription=@StatusDescription WHERE StatusName=@StatusName";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("StatusValue", statusinfo.StatusValue),
                Database.MakeInParam("StatusString", statusinfo.StatusString),
                Database.MakeInParam("StatusTip", statusinfo.StatusTip),
                Database.MakeInParam("StatusDescription", statusinfo.StatusDescription),
                Database.MakeInParam("StatusName", statusinfo.StatusName)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
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
            string sql = $"SELECT * FROM AgentInfo WITH(NOLOCK) WHERE AgentID = {agentId}";
            return Database.ExecuteObject<AgentInfo>(sql);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        public Message InsertAgentUser(AgentInfo agent, int pGameId)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", agent.UserID),
                Database.MakeInParam("strCompellation", agent.Compellation),
                Database.MakeInParam("strAgentDomain", agent.AgentDomain),
                Database.MakeInParam("strQQAccount", agent.QQAccount),
                Database.MakeInParam("strWCNickName", agent.WCNickName),
                Database.MakeInParam("strContactPhone", agent.ContactPhone),
                Database.MakeInParam("strContactAddress", agent.ContactAddress),
                Database.MakeInParam("dwAgentLevel", agent.AgentLevel),
                Database.MakeInParam("strAgentNote", agent.AgentNote),
                Database.MakeInParam("dwParentGameID", pGameId),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };


            return MessageHelper.GetMessage(Database, "NET_PM_AddAgent", prams);
        }
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        public int UpdateAgentUser(AgentInfo agent)
        {
            string sqlQuery = @"UPDATE AgentInfo SET Password=@Password,Compellation=@Compellation,QQAccount=@QQAccount,WCNickName=@WCNickName,
            ContactPhone=@ContactPhone,ContactAddress=@ContactAddress,AgentDomain=@AgentDomain,AgentNote=@AgentNote WHERE AgentID=@AgentID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("Compellation", agent.Compellation),
                Database.MakeInParam("QQAccount", agent.QQAccount),
                Database.MakeInParam("WCNickName", agent.WCNickName),
                Database.MakeInParam("ContactPhone", agent.ContactPhone),
                Database.MakeInParam("ContactAddress", agent.ContactAddress),
                Database.MakeInParam("AgentDomain", agent.AgentDomain),
                Database.MakeInParam("AgentNote", agent.AgentNote),
                Database.MakeInParam("AgentID", agent.AgentID),
                Database.MakeInParam("Password",agent.Password)
            };

            

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        public int NullityAgentUser(string strList, int nullity)
        {
            string sql = $"UPDATE AgentInfo SET Nullity={nullity} WHERE AgentID IN({strList})";
            return Database.ExecuteNonQuery(sql);
        }
        #endregion
    }
}