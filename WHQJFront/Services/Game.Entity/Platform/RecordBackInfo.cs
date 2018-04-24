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
	/// 实体类 RecordBackInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class RecordBackInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordBackInfo" ;

		/// <summary>
		/// 视频id
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 约战房间唯一标识
		/// </summary>
		public const string _PersonalRoomGUID = "PersonalRoomGUID" ;

		/// <summary>
		/// 用户 ID
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 房间 ID
		/// </summary>
		public const string _RoomID = "RoomID" ;

		/// <summary>
		/// 用户积分（货币）
		/// </summary>
		public const string _Score = "Score" ;

		/// <summary>
		/// 游戏局数
		/// </summary>
		public const string _GamesNum = "GamesNum" ;

		/// <summary>
		/// 游戏模式 0:局数 1:时间 2:圈数 3:积分
		/// </summary>
		public const string _GameMode = "GameMode" ;

		/// <summary>
		/// 游戏圈数
		/// </summary>
		public const string _LoopCount = "LoopCount" ;

		/// <summary>
		/// 写入日期
		/// </summary>
		public const string _Dates = "Dates" ;
		#endregion

		#region 私有变量
		private string m_iD;						//视频id
		private string m_personalRoomGUID;			//约战房间唯一标识
		private int m_userID;						//用户 ID
		private int m_roomID;						//房间 ID
		private long m_score;						//用户积分（货币）
		private int m_gamesNum;						//游戏局数
		private byte m_gameMode;					//游戏模式 0:局数 1:时间 2:圈数 3:积分
		private byte m_loopCount;					//游戏圈数
		private DateTime m_dates;					//写入日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordBackInfo
		/// </summary>
		public RecordBackInfo()
		{
			m_iD="";
			m_personalRoomGUID="";
			m_userID=0;
			m_roomID=0;
			m_score=0;
			m_gamesNum=0;
			m_gameMode=0;
			m_loopCount=0;
			m_dates=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 视频id
		/// </summary>
		public string ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 约战房间唯一标识
		/// </summary>
		public string PersonalRoomGUID
		{
			get { return m_personalRoomGUID; }
			set { m_personalRoomGUID = value; }
		}

		/// <summary>
		/// 用户 ID
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
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
		/// 游戏局数
		/// </summary>
		public int GamesNum
		{
			get { return m_gamesNum; }
			set { m_gamesNum = value; }
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
		/// 游戏圈数
		/// </summary>
		public byte LoopCount
		{
			get { return m_loopCount; }
			set { m_loopCount = value; }
		}

		/// <summary>
		/// 写入日期
		/// </summary>
		public DateTime Dates
		{
			get { return m_dates; }
			set { m_dates = value; }
		}
		#endregion
	}
}
