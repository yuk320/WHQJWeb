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
	/// 实体类 IMGroupWealth。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupWealth  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupWealth" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Ingot = "Ingot" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RoomCard = "RoomCard" ;
		#endregion

		#region 私有变量
		private long m_groupID;			//
		private long m_ingot;			//
		private long m_roomCard;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupWealth
		/// </summary>
		public IMGroupWealth()
		{
			m_groupID=0;
			m_ingot=0;
			m_roomCard=0;
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
		public long Ingot
		{
			get { return m_ingot; }
			set { m_ingot = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long RoomCard
		{
			get { return m_roomCard; }
			set { m_roomCard = value; }
		}
		#endregion
	}
}
