/*
 * 版本：4.0
 * 时间：2018/4/17
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
	/// 实体类 ReturnAwardConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReturnAwardConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAwardConfig" ;

		/// <summary>
		/// 返利配置标识
		/// </summary>
		public const string _ConfigID = "ConfigID" ;

		/// <summary>
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public const string _AwardType = "AwardType" ;

		/// <summary>
		/// 返利级别（目前仅支持3级）
		/// </summary>
		public const string _AwardLevel = "AwardLevel" ;

		/// <summary>
		/// 返利比例
		/// </summary>
		public const string _AwardScale = "AwardScale" ;

		/// <summary>
		/// 是否启用（0：启用、1：禁用）
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 更新时间
		/// </summary>
		public const string _UpdateTime = "UpdateTime" ;
		#endregion

		#region 私有变量
		private int m_configID;				//返利配置标识
		private byte m_awardType;			//返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		private int m_awardLevel;			//返利级别（目前仅支持3级）
		private decimal m_awardScale;		//返利比例
		private bool m_nullity;				//是否启用（0：启用、1：禁用）
		private DateTime m_updateTime;		//更新时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAwardConfig
		/// </summary>
		public ReturnAwardConfig()
		{
			m_configID=0;
			m_awardType=0;
			m_awardLevel=0;
			m_awardScale=0;
			m_nullity=false;
			m_updateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 返利配置标识
		/// </summary>
		public int ConfigID
		{
			get { return m_configID; }
			set { m_configID = value; }
		}

		/// <summary>
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public byte AwardType
		{
			get { return m_awardType; }
			set { m_awardType = value; }
		}

		/// <summary>
		/// 返利级别（目前仅支持3级）
		/// </summary>
		public int AwardLevel
		{
			get { return m_awardLevel; }
			set { m_awardLevel = value; }
		}

		/// <summary>
		/// 返利比例
		/// </summary>
		public decimal AwardScale
		{
			get { return m_awardScale; }
			set { m_awardScale = value; }
		}

		/// <summary>
		/// 是否启用（0：启用、1：禁用）
		/// </summary>
		public bool Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			get { return m_updateTime; }
			set { m_updateTime = value; }
		}
		#endregion
	}
}
