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
	/// 实体类 IMUserGroupInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMUserGroupInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMUserGroupInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GroupIDArray = "GroupIDArray" ;
		#endregion

		#region 私有变量
		private int m_userID;					//
		private string m_groupIDArray;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMUserGroupInfo
		/// </summary>
		public IMUserGroupInfo()
		{
			m_userID=0;
			m_groupIDArray="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string GroupIDArray
		{
			get { return m_groupIDArray; }
			set { m_groupIDArray = value; }
		}
		#endregion
	}
}
