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
	/// 实体类 ReturnAwardReceive。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReturnAwardReceive  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAwardReceive" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public const string _AwardType = "AwardType" ;

		/// <summary>
		/// 领取数量
		/// </summary>
		public const string _ReceiveAward = "ReceiveAward" ;

		/// <summary>
		/// 领取前对应字段值 根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public const string _ReceiveBefore = "ReceiveBefore" ;

		/// <summary>
		/// 领取时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//记录标识
		private int m_userID;				//用户标识
		private byte m_awardType;			//返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		private int m_receiveAward;			//领取数量
		private int m_receiveBefore;		//领取前对应字段值 根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		private DateTime m_collectDate;		//领取时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAwardReceive
		/// </summary>
		public ReturnAwardReceive()
		{
			m_recordID=0;
			m_userID=0;
			m_awardType=0;
			m_receiveAward=0;
			m_receiveBefore=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
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
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public byte AwardType
		{
			get { return m_awardType; }
			set { m_awardType = value; }
		}

		/// <summary>
		/// 领取数量
		/// </summary>
		public int ReceiveAward
		{
			get { return m_receiveAward; }
			set { m_receiveAward = value; }
		}

		/// <summary>
		/// 领取前对应字段值 根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public int ReceiveBefore
		{
			get { return m_receiveBefore; }
			set { m_receiveBefore = value; }
		}

		/// <summary>
		/// 领取时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
