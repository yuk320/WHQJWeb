using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Entity.Group;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 群组外观
    /// </summary>
    public class GroupFacade
    {
        #region Fields
        /// <summary>
        /// 群组库数据接口
        /// </summary>
        private readonly IGroupDataProvider groupData;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupFacade()
        {
            groupData = ClassFactory.GetIGroupDataProvider();
        }

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
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            return groupData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 系统配置
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        public IMGroupOption GetGroupOption(string key)
        {
            return groupData.GetGroupOption(key);
        }
        #endregion
    }
}
