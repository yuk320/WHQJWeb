using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Entity.Accounts;
using Game.Entity.Agent;
using Game.Entity.Group;
using Game.Kernel;

namespace Game.IData
{
    /// <summary>
    /// 代理库数据层接口
    /// </summary>
    public interface IGroupDataProvider
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
        #endregion 公用分页

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="optionName">配置键值</param>
        /// <returns></returns>
        IMGroupOption GetGroupOption(string optionName);

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="optionInfo">配置信息</param>
        /// <returns></returns>
        int UpdateGroupOption(IMGroupOption optionInfo);

        #endregion

        #region 俱乐部信息

        /// <summary>
        /// 获取俱乐部信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IMGroupProperty GetGroupInfo(long groupId);

        /// <summary>
        /// 获取俱乐部成员列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IList<IMGroupMember> GetGroupMemberList(long groupId);

        /// <summary>
        /// 批量冻结、解冻俱乐部
        /// </summary>
        /// <param name="ids">俱乐部列表 （分隔符：,）</param>
        /// <param name="nullity">0：解冻 1：冻结</param>
        /// <returns></returns>
        int NullityGroup(string ids, byte nullity);

        /// <summary>
        /// 批量强制解散
        /// </summary>
        /// <param name="ids">俱乐部列表 （分隔符：,）</param>
        /// <returns></returns>
        int DeleteGroup(string ids);

        /// <summary>
        /// 移交会长
        /// </summary>
        /// <param name="info">群组信息</param>
        /// <returns></returns>
        int UpdateGroup(IMGroupProperty info);

        /// <summary>
        /// 获取俱乐部在线人数（金币房间与比赛房间）
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <returns></returns>
        int GetGroupMemberOnline(long groupId);

        /// <summary>
        /// 获取俱乐部钻石数量
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <returns></returns>
        long GetGroupWealth(long groupId);

        /// <summary>
        /// 获取俱乐部约战统计 
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <param name="userId">用户标识（两个维度：userId为0是为俱乐部统计，大于0时为该用户在该俱乐部的统计）</param>
        /// <returns></returns>
        StreamGroupBalance GetGroupBattleRoomStream(long groupId, int userId = 0);

        /// <summary>
        /// 获取俱乐部约战当前房间数
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <param name="roomStatus">房间状态</param>
        /// <returns></returns>
        int GetGroupBattleRoomCurrent(long groupId, int roomStatus = -1);

        #endregion
    }
}