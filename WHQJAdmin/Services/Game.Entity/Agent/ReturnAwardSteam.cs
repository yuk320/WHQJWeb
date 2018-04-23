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
	/// 实体类 ReturnAwardSteam。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReturnAwardSteam  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAwardSteam" ;

		/// <summary>
		/// 时间标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public const string _AwardType = "AwardType" ;

		/// <summary>
		/// 返利级别
		/// </summary>
		public const string _AwardLevel = "AwardLevel" ;

		/// <summary>
		/// 返利值
		/// </summary>
		public const string _Award = "Award" ;

		/// <summary>
		/// 插入时间
		/// </summary>
		public const string _InsertTime = "InsertTime" ;

		/// <summary>
		/// 更新时间
		/// </summary>
		public const string _UpdateTime = "UpdateTime" ;
		#endregion

		#region 私有变量
		private int m_dateID;				//时间标识
		private int m_userID;				//用户标识
		private int m_awardType;			//返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		private int m_awardLevel;			//返利级别
		private long m_award;				//返利值
		private DateTime m_insertTime;		//插入时间
		private DateTime m_updateTime;		//更新时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAwardSteam
		/// </summary>
		public ReturnAwardSteam()
		{
			m_dateID=0;
			m_userID=0;
			m_awardType=0;
			m_awardLevel=0;
			m_award=0;
			m_insertTime=DateTime.Now;
			m_updateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 时间标识
		/// </summary>
		public int DateID
		{
			get { return m_dateID; }
			set { m_dateID = value; }
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
		public int AwardType
		{
			get { return m_awardType; }
			set { m_awardType = value; }
		}

		/// <summary>
		/// 返利级别
		/// </summary>
		public int AwardLevel
		{
			get { return m_awardLevel; }
			set { m_awardLevel = value; }
		}

		/// <summary>
		/// 返利值
		/// </summary>
		public long Award
		{
			get { return m_award; }
			set { m_award = value; }
		}

		/// <summary>
		/// 插入时间
		/// </summary>
		public DateTime InsertTime
		{
			get { return m_insertTime; }
			set { m_insertTime = value; }
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
