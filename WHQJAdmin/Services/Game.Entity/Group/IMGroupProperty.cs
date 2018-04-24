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
	/// 实体类 IMGroupProperty。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupProperty  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupProperty" ;

		/// <summary>
		/// 群组标识
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 创建者标识
		/// </summary>
		public const string _CreaterID = "CreaterID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _CreaterGameID = "CreaterGameID" ;

		/// <summary>
		/// 群主昵称
		/// </summary>
		public const string _CreaterNickName = "CreaterNickName" ;

		/// <summary>
		/// 群组名称
		/// </summary>
		public const string _GroupName = "GroupName" ;

		/// <summary>
		/// 群组级别
		/// </summary>
		public const string _GroupLevel = "GroupLevel" ;

		/// <summary>
		/// 群组状态（1：可用 2：不可用）
		/// </summary>
		public const string _GroupStatus = "GroupStatus" ;

		/// <summary>
		/// 群组公告
		/// </summary>
		public const string _GroupIntroduce = "GroupIntroduce" ;

		/// <summary>
		/// 成员数量
		/// </summary>
		public const string _MemberCount = "MemberCount" ;

		/// <summary>
		/// 最大成员数量
		/// </summary>
		public const string _MaxMemberCount = "MaxMemberCount" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CreateDateTime = "CreateDateTime" ;
		#endregion

		#region 私有变量
		private long m_groupID;						//群组标识
		private int m_stationID;					//站点标识
		private int m_createrID;					//创建者标识
		private int m_createrGameID;				//游戏标识
		private string m_createrNickName;			//群主昵称
		private string m_groupName;					//群组名称
		private byte m_groupLevel;					//群组级别
		private byte m_groupStatus;					//群组状态（1：可用 2：不可用）
		private string m_groupIntroduce;			//群组公告
		private short m_memberCount;				//成员数量
		private short m_maxMemberCount;				//最大成员数量
		private DateTime m_createDateTime;			//创建时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupProperty
		/// </summary>
		public IMGroupProperty()
		{
			m_groupID=0;
			m_stationID=0;
			m_createrID=0;
			m_createrGameID=0;
			m_createrNickName="";
			m_groupName="";
			m_groupLevel=0;
			m_groupStatus=0;
			m_groupIntroduce="";
			m_memberCount=0;
			m_maxMemberCount=0;
			m_createDateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 群组标识
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}

		/// <summary>
		/// 站点标识
		/// </summary>
		public int StationID
		{
			get { return m_stationID; }
			set { m_stationID = value; }
		}

		/// <summary>
		/// 创建者标识
		/// </summary>
		public int CreaterID
		{
			get { return m_createrID; }
			set { m_createrID = value; }
		}

		/// <summary>
		/// 游戏标识
		/// </summary>
		public int CreaterGameID
		{
			get { return m_createrGameID; }
			set { m_createrGameID = value; }
		}

		/// <summary>
		/// 群主昵称
		/// </summary>
		public string CreaterNickName
		{
			get { return m_createrNickName; }
			set { m_createrNickName = value; }
		}

		/// <summary>
		/// 群组名称
		/// </summary>
		public string GroupName
		{
			get { return m_groupName; }
			set { m_groupName = value; }
		}

		/// <summary>
		/// 群组级别
		/// </summary>
		public byte GroupLevel
		{
			get { return m_groupLevel; }
			set { m_groupLevel = value; }
		}

		/// <summary>
		/// 群组状态（1：可用 2：不可用）
		/// </summary>
		public byte GroupStatus
		{
			get { return m_groupStatus; }
			set { m_groupStatus = value; }
		}

		/// <summary>
		/// 群组公告
		/// </summary>
		public string GroupIntroduce
		{
			get { return m_groupIntroduce; }
			set { m_groupIntroduce = value; }
		}

		/// <summary>
		/// 成员数量
		/// </summary>
		public short MemberCount
		{
			get { return m_memberCount; }
			set { m_memberCount = value; }
		}

		/// <summary>
		/// 最大成员数量
		/// </summary>
		public short MaxMemberCount
		{
			get { return m_maxMemberCount; }
			set { m_maxMemberCount = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDateTime
		{
			get { return m_createDateTime; }
			set { m_createDateTime = value; }
		}
		#endregion
	}
}
