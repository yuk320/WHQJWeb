using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Game.Facade.DataStruct
{
    /// <summary>
    /// 我的玩家、我的代理 实体类
    /// </summary>
    [Serializable]
    public class BelowData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BelowData()
        {
            UserID = 0;
            GameID = 0;
            NickName = "";
            Diamond = 0;
            MonthDiamond = 0;
            TotalDiamond = 0;
        }

        public int UserID { get; set; }
        /// <summary>
        /// 游戏标识
        /// </summary>
        public int GameID { get; set; }
        /// <summary>
        /// 代理标识
        /// </summary>
        public int AgentID { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public string RegisterDate { get; set; }
        /// <summary>
        /// 当前钻石
        /// </summary>
        public long Diamond { get; set; }
        /// <summary>
        /// 本月钻石（购、售）
        /// </summary>
        public long MonthDiamond { get; set; }
        /// <summary>
        /// 累计充值贡献
        /// </summary>
        public long TotalDiamond { get; set; }

        /// <summary>
        /// 累计税收贡献（游戏）
        /// </summary>
        public long TotalGold { get; set; }

        /// <summary>
        /// ToString (类似)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }

    [Serializable]
    public class BelowDetail : BelowData
    {
        public bool IsAgent => AgentID > 0;

        public string Compellation { get; set; } = "";

        public string QQAccount { get; set; } = "";

        public string ContactPhone { get; set; } = "";

        public string ContactAddress { get; set; } = "";

        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }

    /// <summary>
    /// 我的玩家、我的代理 IList类
    /// </summary>
    public class BelowList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BelowList()
        {
            dataList = new List<BelowData>();
            Link = false;
            PageCount = 0;
            RecordCount = 0;
            PageSize = 0;
            PageIndex = 0;
        }

        public IList<BelowData> dataList { get; set; }

        public bool Link { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public override string ToString()
        {
            string result;
            if (dataList.Count > 0)
            {
                result = dataList.Aggregate("[", (current, underData) => current + (underData + ","));
                result = result.Substring(result.Length - 1, 1);
                result += "]";
            }
            else
            {
                result = "[]";
            }
            return result;
        }
    }
}
