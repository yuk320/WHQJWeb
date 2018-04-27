/*
 * 版本：4.0
 * 时间：2018/4/25
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.Treasure
{
	/// <summary>
	/// 实体类 AppPayConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class AppPayConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AppPayConfig" ;

		/// <summary>
		/// 充值标识
		/// </summary>
		public const string _ConfigID = "ConfigID" ;

		/// <summary>
		/// 苹果充值标识
		/// </summary>
		public const string _AppleID = "AppleID" ;

		/// <summary>
		/// 充值产品名称
		/// </summary>
		public const string _PayName = "PayName" ;

		/// <summary>
		/// 充值产品类型（0 普通  1 苹果）
		/// </summary>
		public const string _PayType = "PayType" ;

		/// <summary>
		/// 充值产品价格
		/// </summary>
		public const string _PayPrice = "PayPrice" ;

		/// <summary>
		/// 充值标志（0 普通  1  首充）
		/// </summary>
		public const string _PayIdentity = "PayIdentity" ;

		/// <summary>
		/// 图片资源类型（1、2、3、4）
		/// </summary>
		public const string _ImageType = "ImageType" ;

		/// <summary>
		/// 排序标识
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 充值获得类型 包括赠送（0：金币 1：钻石 2：游戏豆）
		/// </summary>
		public const string _ScoreType = "ScoreType" ;

		/// <summary>
		/// 充值获得数值
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 首冲额外赠送数量
		/// </summary>
		public const string _FristPresent = "FristPresent" ;

		/// <summary>
		/// 额外赠送数量（非首冲）
		/// </summary>
		public const string _PresentScore = "PresentScore" ;

		/// <summary>
		/// 配置时间
		/// </summary>
		public const string _ConfigTime = "ConfigTime" ;
		#endregion

		#region 私有变量
		private int m_configID;				//充值标识
		private string m_appleID;			//苹果充值标识
		private string m_payName;			//充值产品名称
		private byte m_payType;				//充值产品类型（0 普通  1 苹果）
		private decimal m_payPrice;			//充值产品价格
		private byte m_payIdentity;			//充值标志（0 普通  1  首充）
		private byte m_imageType;			//图片资源类型（1、2、3、4）
		private int m_sortID;				//排序标识
		private byte m_scoreType;			//充值获得类型 包括赠送（0：金币 1：钻石 2：游戏豆）
		private int m_score;				//充值获得数值
		private int m_fristPresent;			//首冲额外赠送数量
		private int m_presentScore;			//额外赠送数量（非首冲）
		private DateTime m_configTime;		//配置时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AppPayConfig
		/// </summary>
		public AppPayConfig()
		{
			m_configID=0;
			m_appleID="";
			m_payName="";
			m_payType=0;
			m_payPrice=0;
			m_payIdentity=0;
			m_imageType=0;
			m_sortID=0;
			m_scoreType=0;
			m_score=0;
			m_fristPresent=0;
			m_presentScore=0;
			m_configTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 充值标识
		/// </summary>
		public int ConfigID
		{
			get { return m_configID; }
			set { m_configID = value; }
		}

		/// <summary>
		/// 苹果充值标识
		/// </summary>
		public string AppleID
		{
			get { return m_appleID; }
			set { m_appleID = value; }
		}

		/// <summary>
		/// 充值产品名称
		/// </summary>
		public string PayName
		{
			get { return m_payName; }
			set { m_payName = value; }
		}

		/// <summary>
		/// 充值产品类型（0 普通  1 苹果）
		/// </summary>
		public byte PayType
		{
			get { return m_payType; }
			set { m_payType = value; }
		}

		/// <summary>
		/// 充值产品价格
		/// </summary>
		public decimal PayPrice
		{
			get { return m_payPrice; }
			set { m_payPrice = value; }
		}

		/// <summary>
		/// 充值标志（0 普通  1  首充）
		/// </summary>
		public byte PayIdentity
		{
			get { return m_payIdentity; }
			set { m_payIdentity = value; }
		}

		/// <summary>
		/// 图片资源类型（1、2、3、4）
		/// </summary>
		public byte ImageType
		{
			get { return m_imageType; }
			set { m_imageType = value; }
		}

		/// <summary>
		/// 排序标识
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 充值获得类型 包括赠送（0：金币 1：钻石 2：游戏豆）
		/// </summary>
		public byte ScoreType
		{
			get { return m_scoreType; }
			set { m_scoreType = value; }
		}

		/// <summary>
		/// 充值获得数值
		/// </summary>
		public int Score
		{
			get { return m_score; }
			set { m_score = value; }
		}

		/// <summary>
		/// 首冲额外赠送数量
		/// </summary>
		public int FristPresent
		{
			get { return m_fristPresent; }
			set { m_fristPresent = value; }
		}

		/// <summary>
		/// 额外赠送数量（非首冲）
		/// </summary>
		public int PresentScore
		{
			get { return m_presentScore; }
			set { m_presentScore = value; }
		}

		/// <summary>
		/// 配置时间
		/// </summary>
		public DateTime ConfigTime
		{
			get { return m_configTime; }
			set { m_configTime = value; }
		}
		#endregion
	}
}
