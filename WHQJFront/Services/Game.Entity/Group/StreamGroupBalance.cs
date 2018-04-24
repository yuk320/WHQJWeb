/*
 * 版本：4.0
 * 时间：2018/4/23
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.Group
{
	/// <summary>
	/// 实体类 StreamGroupBalance。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class StreamGroupBalance  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "StreamGroupBalance" ;

		/// <summary>
		/// 茶馆标识
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 约战次数
		/// </summary>
		public const string _BattleCount = "BattleCount" ;

		/// <summary>
		/// 结算次数
		/// </summary>
		public const string _BalanceCount = "BalanceCount" ;

		/// <summary>
		/// 记录时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private long m_groupID;				//茶馆标识
		private int m_userID;				//用户标识
		private int m_battleCount;			//约战次数
		private int m_balanceCount;			//结算次数
		private DateTime m_collectDate;		//记录时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化StreamGroupBalance
		/// </summary>
		public StreamGroupBalance()
		{
			m_groupID=0;
			m_userID=0;
			m_battleCount=0;
			m_balanceCount=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 茶馆标识
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 约战次数
		/// </summary>
		public int BattleCount
		{
			get { return m_battleCount; }
			set { m_battleCount = value; }
		}

		/// <summary>
		/// 结算次数
		/// </summary>
		public int BalanceCount
		{
			get { return m_balanceCount; }
			set { m_balanceCount = value; }
		}

		/// <summary>
		/// 记录时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
