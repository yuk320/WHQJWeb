using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;
using Game.Entity.Agent;
using Game.Entity.Group;

namespace Game.Data
{
    /// <summary>
    /// 代理库数据层
    /// </summary>
    public class GroupDataProvider : BaseDataProvider, IGroupDataProvider
    {
        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupDataProvider(string connString)
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
        /// <param name="optionName">配置键值</param>
        /// <returns></returns>
        public IMGroupOption GetGroupOption(string optionName)
        {
            string sqlQuery =
                $"SELECT * FROM {IMGroupOption.Tablename} WITH(NOLOCK) WHERE {IMGroupOption._OptionName}=@OptionName";
            var prams = new List<DbParameter> {Database.MakeInParam("OptionName", optionName)};
            return Database.ExecuteObject<IMGroupOption>(sqlQuery, prams);
        }

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="optionInfo">配置信息</param>
        /// <returns></returns>
        public int UpdateGroupOption(IMGroupOption optionInfo)
        {
            string sqlQuery = @"UPDATE IMGroupOption SET OptionValue=@OptionValue,SortID=@SortID,
                    OptionTip=@OptionTip,OptionDescribe=@OptionDescribe WHERE OptionName=@OptionName";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("OptionValue", optionInfo.OptionValue),
                Database.MakeInParam("SortID", optionInfo.SortID),
                Database.MakeInParam("OptionTip", optionInfo.OptionTip),
                Database.MakeInParam("OptionDescribe", optionInfo.OptionDescribe),
                Database.MakeInParam("OptionName", optionInfo.OptionName)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        #endregion

        #region 俱乐部信息

        /// <summary>
        /// 获取俱乐部信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IMGroupProperty GetGroupInfo(long groupId)
        {
            return Database.ExecuteObject<IMGroupProperty>(
                $"SELECT * FROM {IMGroupProperty.Tablename}(NOLOCK) WHERE GroupID ={groupId} ");
        }

        /// <summary>
        /// 获取俱乐部成员列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<IMGroupMember> GetGroupMemberList(long groupId)
        {
            return Database.ExecuteObjectList<IMGroupMember>(
                $"SELECT * FROM {IMGroupMember.Tablename}(NOLOCK) WHERE GroupID = {groupId}");
        }

        /// <summary>
        /// 批量冻结、解冻俱乐部
        /// </summary>
        /// <param name="ids">俱乐部列表 （分隔符：,）</param>
        /// <param name="nullity">0：解冻 1：冻结</param>
        /// <returns></returns>
        public int NullityGroup(string ids, byte nullity)
        {
            return Database.ExecuteNonQuery(CommandType.Text,
                $" UPDATE IMGroupProperty SET GroupStatus = {nullity} WHERE GroupID IN ({ids})");
        }

        /// <summary>
        /// 批量强制解散
        /// </summary>
        /// <param name="ids">俱乐部列表 （分隔符：,）</param>
        /// <returns></returns>
        public int DeleteGroup(string ids)
        {
            return Database.ExecuteNonQuery(CommandType.Text, $"DELETE IMGroupProperty WHERE GroupID IN ({ids})");
        }

        /// <summary>
        /// 移交会长
        /// </summary>
        /// <param name="info">群组信息</param>
        /// <returns></returns>
        public int UpdateGroup(IMGroupProperty info)
        {
            string sql = $"UPDATE {IMGroupProperty.Tablename} " +
                         $"  SET {IMGroupProperty._CreaterID}={info.CreaterID} " +
                         $"         ,{IMGroupProperty._CreaterGameID}={info.CreaterGameID} " +
                         $"         ,{IMGroupProperty._CreaterNickName}='{info.CreaterNickName}' " +
                         $" WHERE {IMGroupProperty._GroupID}={info.GroupID}";
            return Database.ExecuteNonQuery(CommandType.Text,sql);
        }

        /// <summary>
        /// 获取俱乐部在线人数（金币房间与比赛房间）
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <returns></returns>
        public int GetGroupMemberOnline(long groupId)
        {
            string sql =
                $"DECLARE @treasureOnline INT SELECT @treasureOnline = COUNT(1) FROM WHQJTreasureDB.DBO.GameScoreLocker WHERE UserID IN (SELECT UserID FROM IMUserGroupInfo WHERE CHARINDEX(';{groupId};',GroupIDArray)>0) " +
                $"DECLARE @matchOnline INT SELECT @matchOnline = COUNT(1) FROM WHQJGameMatchDB.DBO.GameScoreLocker WHERE UserID IN (SELECT UserID FROM IMUserGroupInfo WHERE CHARINDEX(';{groupId};',GroupIDArray)>0) " +
                "SELECT @treasureOnline+@matchOnline AS memberOnline";
            return (int) Database.ExecuteScalar(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取俱乐部钻石数量
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <returns></returns>
        public long GetGroupWealth(long groupId)
        {
            return (long) Database.ExecuteScalar(CommandType.Text,
                $"SELECT Ingot FROM IMGroupWealth WHERE GroupID = {groupId}");
        }

        /// <summary>
        /// 获取俱乐部约战统计 
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <param name="userId">用户标识（两个维度：userId为0是为俱乐部统计，大于0时为该用户在该俱乐部的统计）</param>
        /// <returns></returns>
        public StreamGroupBalance GetGroupBattleRoomStream(long groupId, int userId=0)
        {
            string sql =
                $"SELECT ISNULL(SUM(BattleCount),0) AS BattleCount,ISNULL(SUM(BalanceCount),0) AS BalanceCount,ISNULL(MAX(CollectDate),GETDATE()) AS CollectDate FROM StreamGroupBalance WHERE GroupID = {groupId} ";
            if (userId > 0) sql += $" AND UserID = {userId}";
            else
            {
                sql += " GROUP BY GroupID ";
            }
            StreamGroupBalance sgb = Database.ExecuteObject<StreamGroupBalance>(sql) ?? new StreamGroupBalance();
            sgb.GroupID = groupId;
            sgb.UserID = userId;
            return sgb;
        }

        /// <summary>
        /// 获取俱乐部约战当前房间数
        /// </summary>
        /// <param name="groupId">俱乐部编号</param>
        /// <param name="roomStatus">房间状态</param>
        /// <returns></returns>
        public int GetGroupBattleRoomCurrent(long groupId, int roomStatus = -1)
        {
            string sql =
                $"SELECT COUNT(1) FROM WHQJPlatformDB.DBO.StreamCreateTableFeeInfo WHERE GroupID = {groupId} ";
            if (roomStatus > -1) sql += $" AND RoomStatus = {roomStatus}";
            return (int) Database.ExecuteScalar(CommandType.Text, sql);
        }

        #endregion
    }
}
