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
	/// 实体类 OnLinePayOrder。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class OnLinePayOrder  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "OnLinePayOrder" ;

		/// <summary>
		/// 订单标识
		/// </summary>
		public const string _OnLineID = "OnLineID" ;

		/// <summary>
		/// 充值配置标识
		/// </summary>
		public const string _ConfigID = "ConfigID" ;

		/// <summary>
		/// 充值方式（根据GlobalShareInfo）
		/// </summary>
		public const string _ShareID = "ShareID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _GameID = "GameID" ;

		/// <summary>
		/// 用户账号
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 用户昵称
		/// </summary>
		public const string _NickName = "NickName" ;

		/// <summary>
		/// 订单号码
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 订单类型（0 普通充值  1 苹果充值）
		/// </summary>
		public const string _OrderType = "OrderType" ;

		/// <summary>
		/// 订单金额
		/// </summary>
		public const string _Amount = "Amount" ;

		/// <summary>
		/// 充值类型（0：金币 1：钻石 2：游戏豆）
		/// </summary>
		public const string _ScoreType = "ScoreType" ;

		/// <summary>
		/// 充值前数额
		/// </summary>
		public const string _BeforeScore = "BeforeScore" ;

		/// <summary>
		/// 充值数额
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 充值额外赠送数
		/// </summary>
		public const string _OtherPresent = "OtherPresent" ;

		/// <summary>
		/// 订单状态（0 未支付  1 已支付）
		/// </summary>
		public const string _OrderStatus = "OrderStatus" ;

		/// <summary>
		/// 订单时间
		/// </summary>
		public const string _OrderDate = "OrderDate" ;

		/// <summary>
		/// 订单地址
		/// </summary>
		public const string _OrderAddress = "OrderAddress" ;

		/// <summary>
		/// 付款时间
		/// </summary>
		public const string _PayDate = "PayDate" ;
		#endregion

		#region 私有变量
		private int m_onLineID;				//订单标识
		private int m_configID;				//充值配置标识
		private int m_shareID;				//充值方式（根据GlobalShareInfo）
		private int m_userID;				//用户标识
		private int m_gameID;				//游戏标识
		private string m_accounts;			//用户账号
		private string m_nickName;			//用户昵称
		private string m_orderID;			//订单号码
		private byte m_orderType;			//订单类型（0 普通充值  1 苹果充值）
		private decimal m_amount;			//订单金额
		private byte m_scoreType;			//充值类型（0：金币 1：钻石 2：游戏豆）
		private long m_beforeScore;			//充值前数额
		private int m_score;				//充值数额
		private int m_otherPresent;			//充值额外赠送数
		private byte m_orderStatus;			//订单状态（0 未支付  1 已支付）
		private DateTime m_orderDate;		//订单时间
		private string m_orderAddress;		//订单地址
		private DateTime m_payDate;			//付款时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化OnLinePayOrder
		/// </summary>
		public OnLinePayOrder()
		{
			m_onLineID=0;
			m_configID=0;
			m_shareID=0;
			m_userID=0;
			m_gameID=0;
			m_accounts="";
			m_nickName="";
			m_orderID="";
			m_orderType=0;
			m_amount=0;
			m_scoreType=0;
			m_beforeScore=0;
			m_score=0;
			m_otherPresent=0;
			m_orderStatus=0;
			m_orderDate=DateTime.Now;
			m_orderAddress="";
			m_payDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 订单标识
		/// </summary>
		public int OnLineID
		{
			get { return m_onLineID; }
			set { m_onLineID = value; }
		}

		/// <summary>
		/// 充值配置标识
		/// </summary>
		public int ConfigID
		{
			get { return m_configID; }
			set { m_configID = value; }
		}

		/// <summary>
		/// 充值方式（根据GlobalShareInfo）
		/// </summary>
		public int ShareID
		{
			get { return m_shareID; }
			set { m_shareID = value; }
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
		/// 游戏标识
		/// </summary>
		public int GameID
		{
			get { return m_gameID; }
			set { m_gameID = value; }
		}

		/// <summary>
		/// 用户账号
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 用户昵称
		/// </summary>
		public string NickName
		{
			get { return m_nickName; }
			set { m_nickName = value; }
		}

		/// <summary>
		/// 订单号码
		/// </summary>
		public string OrderID
		{
			get { return m_orderID; }
			set { m_orderID = value; }
		}

		/// <summary>
		/// 订单类型（0 普通充值  1 苹果充值）
		/// </summary>
		public byte OrderType
		{
			get { return m_orderType; }
			set { m_orderType = value; }
		}

		/// <summary>
		/// 订单金额
		/// </summary>
		public decimal Amount
		{
			get { return m_amount; }
			set { m_amount = value; }
		}

		/// <summary>
		/// 充值类型（0：金币 1：钻石 2：游戏豆）
		/// </summary>
		public byte ScoreType
		{
			get { return m_scoreType; }
			set { m_scoreType = value; }
		}

		/// <summary>
		/// 充值前数额
		/// </summary>
		public long BeforeScore
		{
			get { return m_beforeScore; }
			set { m_beforeScore = value; }
		}

		/// <summary>
		/// 充值数额
		/// </summary>
		public int Score
		{
			get { return m_score; }
			set { m_score = value; }
		}

		/// <summary>
		/// 充值额外赠送数
		/// </summary>
		public int OtherPresent
		{
			get { return m_otherPresent; }
			set { m_otherPresent = value; }
		}

		/// <summary>
		/// 订单状态（0 未支付  1 已支付）
		/// </summary>
		public byte OrderStatus
		{
			get { return m_orderStatus; }
			set { m_orderStatus = value; }
		}

		/// <summary>
		/// 订单时间
		/// </summary>
		public DateTime OrderDate
		{
			get { return m_orderDate; }
			set { m_orderDate = value; }
		}

		/// <summary>
		/// 订单地址
		/// </summary>
		public string OrderAddress
		{
			get { return m_orderAddress; }
			set { m_orderAddress = value; }
		}

		/// <summary>
		/// 付款时间
		/// </summary>
		public DateTime PayDate
		{
			get { return m_payDate; }
			set { m_payDate = value; }
		}
		#endregion
	}
}
