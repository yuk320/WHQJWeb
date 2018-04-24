/*
 * 版本：4.0
 * 时间：2018/4/24
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.Platform
{
	/// <summary>
	/// 实体类 StreamCreateTableFeeInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class StreamCreateTableFeeInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "StreamCreateTableFeeInfo" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _NickName = "NickName" ;

		/// <summary>
		/// 房间标识
		/// </summary>
		public const string _ServerID = "ServerID" ;

		/// <summary>
		/// 房间ID
		/// </summary>
		public const string _RoomID = "RoomID" ;

		/// <summary>
		/// 房间底分
		/// </summary>
		public const string _CellScore = "CellScore" ;

		/// <summary>
		/// 房间参与游戏的人数
		/// </summary>
		public const string _JoinGamePeopleCount = "JoinGamePeopleCount" ;

		/// <summary>
		/// 局数限制
		/// </summary>
		public const string _CountLimit = "CountLimit" ;

		/// <summary>
		/// 时间限制
		/// </summary>
		public const string _TimeLimit = "TimeLimit" ;

		/// <summary>
		/// 创建费用
		/// </summary>
		public const string _CreateTableFee = "CreateTableFee" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CreateDate = "CreateDate" ;

		/// <summary>
		/// 离开时间
		/// </summary>
		public const string _DissumeDate = "DissumeDate" ;

		/// <summary>
		/// 代理税收比例千分比
		/// </summary>
		public const string _TaxAgency = "TaxAgency" ;

		/// <summary>
		/// 房间税收总数
		/// </summary>
		public const string _TaxCount = "TaxCount" ;

		/// <summary>
		/// 代理税收返现
		/// </summary>
		public const string _TaxRevenue = "TaxRevenue" ;

		/// <summary>
		/// 支付模式
		/// </summary>
		public const string _PayMode = "PayMode" ;

		/// <summary>
		/// 房间状态(0 未开始,1 游戏中,2 结束)
		/// </summary>
		public const string _RoomStatus = "RoomStatus" ;

		/// <summary>
		/// 理论上消耗钻石
		/// </summary>
		public const string _NeedRoomCard = "NeedRoomCard" ;

		/// <summary>
		/// 房间所有玩家的成绩
		/// </summary>
		public const string _RoomScoreInfo = "RoomScoreInfo" ;

		/// <summary>
		/// 游戏模式 0:局数 1:时间 2:圈数 3:积分
		/// </summary>
		public const string _GameMode = "GameMode" ;

		/// <summary>
		/// 游戏模式
		/// </summary>
		public const string _PlayMode = "PlayMode" ;

		/// <summary>
		/// 群组标识
		/// </summary>
		public const string _GroupID = "GroupID" ;
		#endregion

		#region 私有变量
		private int m_recordID;						//记录标识
		private int m_userID;						//用户标识
		private string m_nickName;					//
		private int m_serverID;						//房间标识
		private int m_roomID;						//房间ID
		private long m_cellScore;					//房间底分
		private byte m_joinGamePeopleCount;			//房间参与游戏的人数
		private byte m_countLimit;					//局数限制
		private int m_timeLimit;					//时间限制
		private long m_createTableFee;				//创建费用
		private DateTime m_createDate;				//创建时间
		private DateTime m_dissumeDate;				//离开时间
		private long m_taxAgency;					//代理税收比例千分比
		private long m_taxCount;					//房间税收总数
		private long m_taxRevenue;					//代理税收返现
		private byte m_payMode;						//支付模式
		private byte m_roomStatus;					//房间状态(0 未开始,1 游戏中,2 结束)
		private byte m_needRoomCard;				//理论上消耗钻石
		private System.Byte[] m_roomScoreInfo;		//房间所有玩家的成绩
		private byte m_gameMode;					//游戏模式 0:局数 1:时间 2:圈数 3:积分
		private byte m_playMode;					//游戏模式
		private long m_groupID;						//群组标识
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化StreamCreateTableFeeInfo
		/// </summary>
		public StreamCreateTableFeeInfo()
		{
			m_recordID=0;
			m_userID=0;
			m_nickName="";
			m_serverID=0;
			m_roomID=0;
			m_cellScore=0;
			m_joinGamePeopleCount=0;
			m_countLimit=0;
			m_timeLimit=0;
			m_createTableFee=0;
			m_createDate=DateTime.Now;
			m_dissumeDate=DateTime.Now;
			m_taxAgency=0;
			m_taxCount=0;
			m_taxRevenue=0;
			m_payMode=0;
			m_roomStatus=0;
			m_needRoomCard=0;
			m_roomScoreInfo=null;
			m_gameMode=0;
			m_playMode=0;
			m_groupID=0;
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
		/// 
		/// </summary>
		public string NickName
		{
			get { return m_nickName; }
			set { m_nickName = value; }
		}

		/// <summary>
		/// 房间标识
		/// </summary>
		public int ServerID
		{
			get { return m_serverID; }
			set { m_serverID = value; }
		}

		/// <summary>
		/// 房间ID
		/// </summary>
		public int RoomID
		{
			get { return m_roomID; }
			set { m_roomID = value; }
		}

		/// <summary>
		/// 房间底分
		/// </summary>
		public long CellScore
		{
			get { return m_cellScore; }
			set { m_cellScore = value; }
		}

		/// <summary>
		/// 房间参与游戏的人数
		/// </summary>
		public byte JoinGamePeopleCount
		{
			get { return m_joinGamePeopleCount; }
			set { m_joinGamePeopleCount = value; }
		}

		/// <summary>
		/// 局数限制
		/// </summary>
		public byte CountLimit
		{
			get { return m_countLimit; }
			set { m_countLimit = value; }
		}

		/// <summary>
		/// 时间限制
		/// </summary>
		public int TimeLimit
		{
			get { return m_timeLimit; }
			set { m_timeLimit = value; }
		}

		/// <summary>
		/// 创建费用
		/// </summary>
		public long CreateTableFee
		{
			get { return m_createTableFee; }
			set { m_createTableFee = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate
		{
			get { return m_createDate; }
			set { m_createDate = value; }
		}

		/// <summary>
		/// 离开时间
		/// </summary>
		public DateTime DissumeDate
		{
			get { return m_dissumeDate; }
			set { m_dissumeDate = value; }
		}

		/// <summary>
		/// 代理税收比例千分比
		/// </summary>
		public long TaxAgency
		{
			get { return m_taxAgency; }
			set { m_taxAgency = value; }
		}

		/// <summary>
		/// 房间税收总数
		/// </summary>
		public long TaxCount
		{
			get { return m_taxCount; }
			set { m_taxCount = value; }
		}

		/// <summary>
		/// 代理税收返现
		/// </summary>
		public long TaxRevenue
		{
			get { return m_taxRevenue; }
			set { m_taxRevenue = value; }
		}

		/// <summary>
		/// 支付模式
		/// </summary>
		public byte PayMode
		{
			get { return m_payMode; }
			set { m_payMode = value; }
		}

		/// <summary>
		/// 房间状态(0 未开始,1 游戏中,2 结束)
		/// </summary>
		public byte RoomStatus
		{
			get { return m_roomStatus; }
			set { m_roomStatus = value; }
		}

		/// <summary>
		/// 理论上消耗钻石
		/// </summary>
		public byte NeedRoomCard
		{
			get { return m_needRoomCard; }
			set { m_needRoomCard = value; }
		}

		/// <summary>
		/// 房间所有玩家的成绩
		/// </summary>
		public System.Byte[] RoomScoreInfo
		{
			get { return m_roomScoreInfo; }
			set { m_roomScoreInfo = value; }
		}

		/// <summary>
		/// 游戏模式 0:局数 1:时间 2:圈数 3:积分
		/// </summary>
		public byte GameMode
		{
			get { return m_gameMode; }
			set { m_gameMode = value; }
		}

		/// <summary>
		/// 游戏模式
		/// </summary>
		public byte PlayMode
		{
			get { return m_playMode; }
			set { m_playMode = value; }
		}

		/// <summary>
		/// 群组标识
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}
		#endregion
	}
}
