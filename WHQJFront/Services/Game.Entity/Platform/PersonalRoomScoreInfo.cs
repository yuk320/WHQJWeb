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
	/// 实体类 PersonalRoomScoreInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PersonalRoomScoreInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "PersonalRoomScoreInfo" ;

		/// <summary>
		/// 用户 ID
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PersonalRoomGUID = "PersonalRoomGUID" ;

		/// <summary>
		/// 房间 ID
		/// </summary>
		public const string _RoomID = "RoomID" ;

		/// <summary>
		/// 用户积分（货币）
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 胜局数目
		/// </summary>
		public const string _WinCount = "WinCount" ;

		/// <summary>
		/// 输局数目
		/// </summary>
		public const string _LostCount = "LostCount" ;

		/// <summary>
		/// 和局数目
		/// </summary>
		public const string _DrawCount = "DrawCount" ;

		/// <summary>
		/// 逃局数目
		/// </summary>
		public const string _FleeCount = "FleeCount" ;

		/// <summary>
		/// 记录时间
		/// </summary>
		public const string _WriteTime = "WriteTime" ;

		/// <summary>
		/// 回放码
		/// </summary>
		public const string _PlayBackCode = "PlayBackCode" ;

		/// <summary>
		/// 座位id
		/// </summary>
		public const string _ChairID = "ChairID" ;

		/// <summary>
		/// 游戏类型
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 群组标识
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 游戏模式
		/// </summary>
		public const string _PlayMode = "PlayMode" ;
		#endregion

		#region 私有变量
		private int m_userID;						//用户 ID
		private string m_personalRoomGUID;			//
		private int m_roomID;						//房间 ID
		private long m_score;						//用户积分（货币）
		private int m_winCount;						//胜局数目
		private int m_lostCount;					//输局数目
		private int m_drawCount;					//和局数目
		private int m_fleeCount;					//逃局数目
		private DateTime m_writeTime;				//记录时间
		private int m_playBackCode;					//回放码
		private int m_chairID;						//座位id
		private int m_kindID;						//游戏类型
		private long m_groupID;						//群组标识
		private byte m_playMode;					//游戏模式
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化PersonalRoomScoreInfo
		/// </summary>
		public PersonalRoomScoreInfo()
		{
			m_userID=0;
			m_personalRoomGUID="";
			m_roomID=0;
			m_score=0;
			m_winCount=0;
			m_lostCount=0;
			m_drawCount=0;
			m_fleeCount=0;
			m_writeTime=DateTime.Now;
			m_playBackCode=0;
			m_chairID=0;
			m_kindID=0;
			m_groupID=0;
			m_playMode=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 用户 ID
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PersonalRoomGUID
		{
			get { return m_personalRoomGUID; }
			set { m_personalRoomGUID = value; }
		}

		/// <summary>
		/// 房间 ID
		/// </summary>
		public int RoomID
		{
			get { return m_roomID; }
			set { m_roomID = value; }
		}

		/// <summary>
		/// 用户积分（货币）
		/// </summary>
		public long Score
		{
			get { return m_score; }
			set { m_score = value; }
		}

		/// <summary>
		/// 胜局数目
		/// </summary>
		public int WinCount
		{
			get { return m_winCount; }
			set { m_winCount = value; }
		}

		/// <summary>
		/// 输局数目
		/// </summary>
		public int LostCount
		{
			get { return m_lostCount; }
			set { m_lostCount = value; }
		}

		/// <summary>
		/// 和局数目
		/// </summary>
		public int DrawCount
		{
			get { return m_drawCount; }
			set { m_drawCount = value; }
		}

		/// <summary>
		/// 逃局数目
		/// </summary>
		public int FleeCount
		{
			get { return m_fleeCount; }
			set { m_fleeCount = value; }
		}

		/// <summary>
		/// 记录时间
		/// </summary>
		public DateTime WriteTime
		{
			get { return m_writeTime; }
			set { m_writeTime = value; }
		}

		/// <summary>
		/// 回放码
		/// </summary>
		public int PlayBackCode
		{
			get { return m_playBackCode; }
			set { m_playBackCode = value; }
		}

		/// <summary>
		/// 座位id
		/// </summary>
		public int ChairID
		{
			get { return m_chairID; }
			set { m_chairID = value; }
		}

		/// <summary>
		/// 游戏类型
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 群组标识
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}

		/// <summary>
		/// 游戏模式
		/// </summary>
		public byte PlayMode
		{
			get { return m_playMode; }
			set { m_playMode = value; }
		}
		#endregion
	}
}
