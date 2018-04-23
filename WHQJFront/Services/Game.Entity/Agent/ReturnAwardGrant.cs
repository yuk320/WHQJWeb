/*
 * 版本：4.0
 * 时间：2018/4/20
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
	/// 实体类 ReturnAwardGrant。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReturnAwardGrant  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAwardGrant" ;

		/// <summary>
		/// 转赠记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 转出代理用户标识
		/// </summary>
		public const string _SourceUserID = "SourceUserID" ;

		/// <summary>
		/// 转入方用户标识
		/// </summary>
		public const string _TargetUserID = "TargetUserID" ;

		/// <summary>
		/// 转赠类型： 同返利类型 1：充值返利【钻石】 2：游戏税收返利【金币】
		/// </summary>
		public const string _TradeType = "TradeType" ;

		/// <summary>
		/// 转出方原值 根据类型不同 1：代表转出前钻石 2：代表转出前携带金币
		/// </summary>
		public const string _SourceBefore = "SourceBefore" ;

		/// <summary>
		/// 转入方原值 根据类型不同 1：代表转入前钻石 2：代表转入前携带金币
		/// </summary>
		public const string _TargetBefore = "TargetBefore" ;

		/// <summary>
		/// 转赠数值： 根据类型 1：代表钻石 2：代表金币
		/// </summary>
		public const string _Amount = "Amount" ;

		/// <summary>
		/// 操作地址
		/// </summary>
		public const string _ClientIP = "ClientIP" ;

		/// <summary>
		/// 操作时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//转赠记录标识
		private int m_sourceUserID;			//转出代理用户标识
		private int m_targetUserID;			//转入方用户标识
		private byte m_tradeType;			//转赠类型： 同返利类型 1：充值返利【钻石】 2：游戏税收返利【金币】
		private long m_sourceBefore;		//转出方原值 根据类型不同 1：代表转出前钻石 2：代表转出前携带金币
		private long m_targetBefore;		//转入方原值 根据类型不同 1：代表转入前钻石 2：代表转入前携带金币
		private long m_amount;				//转赠数值： 根据类型 1：代表钻石 2：代表金币
		private string m_clientIP;			//操作地址
		private DateTime m_collectDate;		//操作时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAwardGrant
		/// </summary>
		public ReturnAwardGrant()
		{
			m_recordID=0;
			m_sourceUserID=0;
			m_targetUserID=0;
			m_tradeType=0;
			m_sourceBefore=0;
			m_targetBefore=0;
			m_amount=0;
			m_clientIP="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 转赠记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 转出代理用户标识
		/// </summary>
		public int SourceUserID
		{
			get { return m_sourceUserID; }
			set { m_sourceUserID = value; }
		}

		/// <summary>
		/// 转入方用户标识
		/// </summary>
		public int TargetUserID
		{
			get { return m_targetUserID; }
			set { m_targetUserID = value; }
		}

		/// <summary>
		/// 转赠类型： 同返利类型 1：充值返利【钻石】 2：游戏税收返利【金币】
		/// </summary>
		public byte TradeType
		{
			get { return m_tradeType; }
			set { m_tradeType = value; }
		}

		/// <summary>
		/// 转出方原值 根据类型不同 1：代表转出前钻石 2：代表转出前携带金币
		/// </summary>
		public long SourceBefore
		{
			get { return m_sourceBefore; }
			set { m_sourceBefore = value; }
		}

		/// <summary>
		/// 转入方原值 根据类型不同 1：代表转入前钻石 2：代表转入前携带金币
		/// </summary>
		public long TargetBefore
		{
			get { return m_targetBefore; }
			set { m_targetBefore = value; }
		}

		/// <summary>
		/// 转赠数值： 根据类型 1：代表钻石 2：代表金币
		/// </summary>
		public long Amount
		{
			get { return m_amount; }
			set { m_amount = value; }
		}

		/// <summary>
		/// 操作地址
		/// </summary>
		public string ClientIP
		{
			get { return m_clientIP; }
			set { m_clientIP = value; }
		}

		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
