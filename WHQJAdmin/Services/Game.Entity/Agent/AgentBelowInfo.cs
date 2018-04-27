/*
 * 版本：4.0
 * 时间：2018/4/26
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.Agent
{
	/// <summary>
	/// 实体类 AgentBelowInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class AgentBelowInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AgentBelowInfo" ;

		/// <summary>
		/// 代理标识
		/// </summary>
		public const string _AgentID = "AgentID" ;

		/// <summary>
		/// 代理下线用户标识（无论是下线玩家还是下级代理）
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 收集时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_agentID;					//代理标识
		private int m_userID;					//代理下线用户标识（无论是下线玩家还是下级代理）
		private DateTime m_collectDate;			//收集时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AgentBelowInfo
		/// </summary>
		public AgentBelowInfo()
		{
			m_agentID=0;
			m_userID=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 代理标识
		/// </summary>
		public int AgentID
		{
			get { return m_agentID; }
			set { m_agentID = value; }
		}

		/// <summary>
		/// 代理下线用户标识（无论是下线玩家还是下级代理）
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 收集时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
        #endregion

	    #region 虚拟属性

	    public virtual string NickName { get; set; }

        public virtual int GameID { get; set; }

        public virtual string RegisterDate { get; set; }

        public virtual byte RegisterOrigin { get; set; }

	    #endregion
    }
}
