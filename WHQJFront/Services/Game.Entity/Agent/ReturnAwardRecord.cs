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
	/// 实体类 ReturnAwardRecord。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReturnAwardRecord  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ReturnAwardRecord" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 返利原用户
		/// </summary>
		public const string _SourceUserID = "SourceUserID" ;

		/// <summary>
		/// 返利用户
		/// </summary>
		public const string _TargetUserID = "TargetUserID" ;

		/// <summary>
		/// 返利基准
		/// </summary>
		public const string _ReturnBase = "ReturnBase" ;

		/// <summary>
		/// 返利推广级别（目前仅支持3级）
		/// </summary>
		public const string _AwardLevel = "AwardLevel" ;

		/// <summary>
		/// 返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		/// </summary>
		public const string _AwardType = "AwardType" ;

		/// <summary>
		/// 返利比例
		/// </summary>
		public const string _AwardScale = "AwardScale" ;

		/// <summary>
		/// 返利值 （根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】））
		/// </summary>
		public const string _Award = "Award" ;

		/// <summary>
		/// 记录时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//记录标识
		private int m_sourceUserID;			//返利原用户
		private int m_targetUserID;			//返利用户
		private long m_returnBase;			//返利基准
		private int m_awardLevel;			//返利推广级别（目前仅支持3级）
		private byte m_awardType;			//返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】）
		private decimal m_awardScale;		//返利比例
		private int m_award;				//返利值 （根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】））
		private DateTime m_collectDate;		//记录时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ReturnAwardRecord
		/// </summary>
		public ReturnAwardRecord()
		{
			m_recordID=0;
			m_sourceUserID=0;
			m_targetUserID=0;
			m_returnBase=0;
			m_awardLevel=0;
			m_awardType=0;
			m_awardScale=0;
			m_award=0;
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
		/// 返利原用户
		/// </summary>
		public int SourceUserID
		{
			get { return m_sourceUserID; }
			set { m_sourceUserID = value; }
		}

		/// <summary>
		/// 返利用户
		/// </summary>
		public int TargetUserID
		{
			get { return m_targetUserID; }
			set { m_targetUserID = value; }
		}

		/// <summary>
		/// 返利基准
		/// </summary>
		public long ReturnBase
		{
			get { return m_returnBase; }
			set { m_returnBase = value; }
		}

		/// <summary>
		/// 返利推广级别（目前仅支持3级）
		/// </summary>
		public int AwardLevel
		{
			get { return m_awardLevel; }
			set { m_awardLevel = value; }
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
		/// 返利比例
		/// </summary>
		public decimal AwardScale
		{
			get { return m_awardScale; }
			set { m_awardScale = value; }
		}

		/// <summary>
		/// 返利值 （根据返利类型（1：充值返利【钻石】、2：游戏税收返利【金币】））
		/// </summary>
		public int Award
		{
			get { return m_award; }
			set { m_award = value; }
		}

		/// <summary>
		/// 记录时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
