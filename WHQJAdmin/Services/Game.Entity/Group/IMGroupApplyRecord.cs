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
	/// 实体类 IMGroupApplyRecord。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupApplyRecord  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupApplyRecord" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 茶馆标识
		/// </summary>
		public const string _GroupID = "GroupID" ;

		/// <summary>
		/// 申请者标识
		/// </summary>
		public const string _ApplyerID = "ApplyerID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ApplyerGameID = "ApplyerGameID" ;

		/// <summary>
		/// 头像标识
		/// </summary>
		public const string _ApplyerCustomID = "ApplyerCustomID" ;

		/// <summary>
		/// 申请者昵称
		/// </summary>
		public const string _ApplyerNickName = "ApplyerNickName" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ApplyType = "ApplyType" ;

		/// <summary>
		/// 申请状态（0：等待审核 1：同意 2：拒绝 3: 撤销）
		/// </summary>
		public const string _ApplyStatus = "ApplyStatus" ;

		/// <summary>
		/// 创建者标识
		/// </summary>
		public const string _CreaterID = "CreaterID" ;

		/// <summary>
		/// 茶馆名称
		/// </summary>
		public const string _GroupName = "GroupName" ;

		/// <summary>
		/// 删除掩码（0：未删除 1：申请者删除 2：馆主删除）
		/// </summary>
		public const string _DeleteMask = "DeleteMask" ;

		/// <summary>
		/// 备注信息
		/// </summary>
		public const string _Remarks = "Remarks" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDateTime = "CollectDateTime" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//
		private long m_groupID;					//茶馆标识
		private int m_applyerID;				//申请者标识
		private int m_applyerGameID;			//
		private int m_applyerCustomID;			//头像标识
		private string m_applyerNickName;		//申请者昵称
		private byte m_applyType;				//
		private byte m_applyStatus;				//申请状态（0：等待审核 1：同意 2：拒绝 3: 撤销）
		private int m_createrID;				//创建者标识
		private string m_groupName;				//茶馆名称
		private byte m_deleteMask;				//删除掩码（0：未删除 1：申请者删除 2：馆主删除）
		private string m_remarks;				//备注信息
		private DateTime m_collectDateTime;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupApplyRecord
		/// </summary>
		public IMGroupApplyRecord()
		{
			m_recordID=0;
			m_groupID=0;
			m_applyerID=0;
			m_applyerGameID=0;
			m_applyerCustomID=0;
			m_applyerNickName="";
			m_applyType=0;
			m_applyStatus=0;
			m_createrID=0;
			m_groupName="";
			m_deleteMask=0;
			m_remarks="";
			m_collectDateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 茶馆标识
		/// </summary>
		public long GroupID
		{
			get { return m_groupID; }
			set { m_groupID = value; }
		}

		/// <summary>
		/// 申请者标识
		/// </summary>
		public int ApplyerID
		{
			get { return m_applyerID; }
			set { m_applyerID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int ApplyerGameID
		{
			get { return m_applyerGameID; }
			set { m_applyerGameID = value; }
		}

		/// <summary>
		/// 头像标识
		/// </summary>
		public int ApplyerCustomID
		{
			get { return m_applyerCustomID; }
			set { m_applyerCustomID = value; }
		}

		/// <summary>
		/// 申请者昵称
		/// </summary>
		public string ApplyerNickName
		{
			get { return m_applyerNickName; }
			set { m_applyerNickName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte ApplyType
		{
			get { return m_applyType; }
			set { m_applyType = value; }
		}

		/// <summary>
		/// 申请状态（0：等待审核 1：同意 2：拒绝 3: 撤销）
		/// </summary>
		public byte ApplyStatus
		{
			get { return m_applyStatus; }
			set { m_applyStatus = value; }
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
		/// 茶馆名称
		/// </summary>
		public string GroupName
		{
			get { return m_groupName; }
			set { m_groupName = value; }
		}

		/// <summary>
		/// 删除掩码（0：未删除 1：申请者删除 2：馆主删除）
		/// </summary>
		public byte DeleteMask
		{
			get { return m_deleteMask; }
			set { m_deleteMask = value; }
		}

		/// <summary>
		/// 备注信息
		/// </summary>
		public string Remarks
		{
			get { return m_remarks; }
			set { m_remarks = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CollectDateTime
		{
			get { return m_collectDateTime; }
			set { m_collectDateTime = value; }
		}
		#endregion
	}
}
