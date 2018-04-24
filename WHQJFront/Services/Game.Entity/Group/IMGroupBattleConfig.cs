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
	/// 实体类 IMGroupBattleConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupBattleConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupBattleConfig" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _BattleConfig = "BattleConfig" ;
		#endregion

		#region 私有变量
		private short m_kindID;					//
		private long m_groupID;					//
		private string m_battleConfig;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupBattleConfig
		/// </summary>
		public IMGroupBattleConfig()
		{
			m_kindID=0;
			m_groupID=0;
			m_battleConfig="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public short KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

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
		public string BattleConfig
		{
			get { return m_battleConfig; }
			set { m_battleConfig = value; }
		}
		#endregion
	}
}
