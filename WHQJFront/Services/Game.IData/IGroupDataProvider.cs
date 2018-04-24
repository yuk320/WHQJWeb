using Game.Kernel;
using Game.Entity.Group;

// ReSharper disable InconsistentNaming

namespace Game.IData
{
    /// <summary>
    ///  群组库数据层接口
    /// </summary>
    public interface IGroupDataProvider //:IProvider
    {
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

        #region 系统配置

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        IMGroupOption GetGroupOption(string key);

        #endregion
    }
}
