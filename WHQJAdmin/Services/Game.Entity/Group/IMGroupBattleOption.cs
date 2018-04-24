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
	/// 实体类 IMGroupBattleOption。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupBattleOption  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupBattleOption" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PayTypeMask = "PayTypeMask" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CurrencyKindMask = "CurrencyKindMask" ;
		#endregion

		#region 私有变量
		private int m_stationID;				//
		private byte m_payTypeMask;				//
		private byte m_currencyKindMask;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupBattleOption
		/// </summary>
		public IMGroupBattleOption()
		{
			m_stationID=0;
			m_payTypeMask=0;
			m_currencyKindMask=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int StationID
		{
			get { return m_stationID; }
			set { m_stationID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte PayTypeMask
		{
			get { return m_payTypeMask; }
			set { m_payTypeMask = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte CurrencyKindMask
		{
			get { return m_currencyKindMask; }
			set { m_currencyKindMask = value; }
		}
		#endregion
	}
}
