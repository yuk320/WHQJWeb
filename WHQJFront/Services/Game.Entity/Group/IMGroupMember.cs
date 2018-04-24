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
	/// 实体类 IMGroupMember。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupMember  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupMember" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _MemberID = "MemberID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _BattleCount = "BattleCount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _JoinDateTime = "JoinDateTime" ;
		#endregion

		#region 私有变量
		private long m_groupID;					//
		private long m_memberID;				//
		private int m_battleCount;				//
		private DateTime m_joinDateTime;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupMember
		/// </summary>
		public IMGroupMember()
		{
			m_groupID=0;
			m_memberID=0;
			m_battleCount=0;
			m_joinDateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long MemberID
		{
			get { return m_memberID; }
			set { m_memberID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int BattleCount
		{
			get { return m_battleCount; }
			set { m_battleCount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime JoinDateTime
		{
			get { return m_joinDateTime; }
			set { m_joinDateTime = value; }
		}
		#endregion
	}
}
