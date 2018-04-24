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
	/// 实体类 IMGroupBattleRule。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupBattleRule  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupBattleRule" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 支付类型（0x01：房主支付 0x02：AA支付）
		/// </summary>
		public const string _PayType = "PayType" ;

		/// <summary>
		/// 货币类型（0：钻石 1：房卡）
		/// </summary>
		public const string _CurrencyKind = "CurrencyKind" ;

		/// <summary>
		/// 约战掩码（0x01: 允许馆员开房）
		/// </summary>
		public const string _BattleMask = "BattleMask" ;
		#endregion

		#region 私有变量
		private long m_groupID;				//
		private byte m_payType;				//支付类型（0x01：房主支付 0x02：AA支付）
		private byte m_currencyKind;		//货币类型（0：钻石 1：房卡）
		private byte m_battleMask;			//约战掩码（0x01: 允许馆员开房）
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupBattleRule
		/// </summary>
		public IMGroupBattleRule()
		{
			m_groupID=0;
			m_payType=0;
			m_currencyKind=0;
			m_battleMask=0;
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
		/// 支付类型（0x01：房主支付 0x02：AA支付）
		/// </summary>
		public byte PayType
		{
			get { return m_payType; }
			set { m_payType = value; }
		}

		/// <summary>
		/// 货币类型（0：钻石 1：房卡）
		/// </summary>
		public byte CurrencyKind
		{
			get { return m_currencyKind; }
			set { m_currencyKind = value; }
		}

		/// <summary>
		/// 约战掩码（0x01: 允许馆员开房）
		/// </summary>
		public byte BattleMask
		{
			get { return m_battleMask; }
			set { m_battleMask = value; }
		}
		#endregion
	}
}
