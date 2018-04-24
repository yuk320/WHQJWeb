using System.Collections.Generic;

using Game.Kernel;
using System.Data;
using Game.Entity.Platform;
// ReSharper disable InconsistentNaming

namespace Game.IData
{
    /// <summary>
    /// 平台库数据层接口
    /// </summary>
    public interface IPlatformDataProvider //: IProvider
    {
        #region 开房信息
        /// <summary>
        /// 钻石消耗记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetCreateRoomCost(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取创建房间信息
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <param name="groupid">群组编号：0代表非群组约战，大于0代表具群组编号所属的约战房间</param>
        /// <returns></returns>
        StreamCreateTableFeeInfo GetStreamCreateTableFeeInfo(int roomid, long groupid = 0);

        /// <summary>
        /// 获取房间总记录
        /// </summary>
        /// <param name="groupId">群组编号</param>
        /// <param name="roomId">房间编号</param>
        /// <param name="userId">房主编号</param>
        /// <returns></returns>
        PersonalRoomScoreInfo GetPersonalRoomScoreInfo(long groupId, int roomId, int userId);
        #endregion

        #region 游戏信息
        /// <summary>
        /// 根据游戏标识获取游戏
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        MobileKindItem GetGameKindItemByID(int kindid);
        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <returns></returns>
        IList<MobileKindItem> GetMobileKindItemList();
        /// <summary>
        /// 获取游戏列表和版本配置
        /// </summary>
        /// <returns></returns>
        DataSet GetMobileGameAndVersion();
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息by ID
        /// </summary>
        /// <returns></returns>
        GameProperty GetGameProperty(int id);

        #endregion

        #region 公共分页

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
    }
}
