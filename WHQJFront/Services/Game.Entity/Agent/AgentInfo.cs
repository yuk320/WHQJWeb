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
	/// 实体类 AgentInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class AgentInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AgentInfo" ;

		/// <summary>
		/// 代理标识
		/// </summary>
		public const string _AgentID = "AgentID" ;

		/// <summary>
		/// 父级代理标识
		/// </summary>
		public const string _ParentAgent = "ParentAgent" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 安全密码
		/// </summary>
		public const string _Password = "Password" ;

		/// <summary>
		/// 真实姓名
		/// </summary>
		public const string _Compellation = "Compellation" ;

		/// <summary>
		/// QQ账号
		/// </summary>
		public const string _QQAccount = "QQAccount" ;

		/// <summary>
		/// 微信昵称
		/// </summary>
		public const string _WCNickName = "WCNickName" ;

		/// <summary>
		/// 联系电话
		/// </summary>
		public const string _ContactPhone = "ContactPhone" ;

		/// <summary>
		/// 联系地址
		/// </summary>
		public const string _ContactAddress = "ContactAddress" ;

		/// <summary>
		/// 代理域名
		/// </summary>
		public const string _AgentDomain = "AgentDomain" ;

		/// <summary>
		/// 代理等级
		/// </summary>
		public const string _AgentLevel = "AgentLevel" ;

		/// <summary>
		/// 代理备注
		/// </summary>
		public const string _AgentNote = "AgentNote" ;

		/// <summary>
		/// 钻石返利
		/// </summary>
		public const string _DiamondAward = "DiamondAward" ;

		/// <summary>
		/// 金币返利
		/// </summary>
		public const string _GoldAward = "GoldAward" ;

		/// <summary>
		/// 下线玩家数
		/// </summary>
		public const string _BelowUser = "BelowUser" ;

		/// <summary>
		/// 下线代理数
		/// </summary>
		public const string _BelowAgent = "BelowAgent" ;

		/// <summary>
		/// 禁用标识
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 修改时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_agentID;					//代理标识
		private int m_parentAgent;				//父级代理标识
		private int m_userID;					//用户标识
		private string m_password;				//安全密码
		private string m_compellation;			//真实姓名
		private string m_qQAccount;				//QQ账号
		private string m_wCNickName;			//微信昵称
		private string m_contactPhone;			//联系电话
		private string m_contactAddress;		//联系地址
		private string m_agentDomain;			//代理域名
		private byte m_agentLevel;				//代理等级
		private string m_agentNote;				//代理备注
		private long m_diamondAward;			//钻石返利
		private long m_goldAward;				//金币返利
		private int m_belowUser;				//下线玩家数
		private int m_belowAgent;				//下线代理数
		private byte m_nullity;					//禁用标识
		private DateTime m_collectDate;			//修改时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AgentInfo
		/// </summary>
		public AgentInfo()
		{
			m_agentID=0;
			m_parentAgent=0;
			m_userID=0;
			m_password="";
			m_compellation="";
			m_qQAccount="";
			m_wCNickName="";
			m_contactPhone="";
			m_contactAddress="";
			m_agentDomain="";
			m_agentLevel=0;
			m_agentNote="";
			m_diamondAward=0;
			m_goldAward=0;
			m_belowUser=0;
			m_belowAgent=0;
			m_nullity=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 代理标识
		/// </summary>
		public int AgentID
		{
			get { return m_agentID; }
			set { m_agentID = value; }
		}

		/// <summary>
		/// 父级代理标识
		/// </summary>
		public int ParentAgent
		{
			get { return m_parentAgent; }
			set { m_parentAgent = value; }
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
		/// 安全密码
		/// </summary>
		public string Password
		{
			get { return m_password; }
			set { m_password = value; }
		}

		/// <summary>
		/// 真实姓名
		/// </summary>
		public string Compellation
		{
			get { return m_compellation; }
			set { m_compellation = value; }
		}

		/// <summary>
		/// QQ账号
		/// </summary>
		public string QQAccount
		{
			get { return m_qQAccount; }
			set { m_qQAccount = value; }
		}

		/// <summary>
		/// 微信昵称
		/// </summary>
		public string WCNickName
		{
			get { return m_wCNickName; }
			set { m_wCNickName = value; }
		}

		/// <summary>
		/// 联系电话
		/// </summary>
		public string ContactPhone
		{
			get { return m_contactPhone; }
			set { m_contactPhone = value; }
		}

		/// <summary>
		/// 联系地址
		/// </summary>
		public string ContactAddress
		{
			get { return m_contactAddress; }
			set { m_contactAddress = value; }
		}

		/// <summary>
		/// 代理域名
		/// </summary>
		public string AgentDomain
		{
			get { return m_agentDomain; }
			set { m_agentDomain = value; }
		}

		/// <summary>
		/// 代理等级
		/// </summary>
		public byte AgentLevel
		{
			get { return m_agentLevel; }
			set { m_agentLevel = value; }
		}

		/// <summary>
		/// 代理备注
		/// </summary>
		public string AgentNote
		{
			get { return m_agentNote; }
			set { m_agentNote = value; }
		}

		/// <summary>
		/// 钻石返利
		/// </summary>
		public long DiamondAward
		{
			get { return m_diamondAward; }
			set { m_diamondAward = value; }
		}

		/// <summary>
		/// 金币返利
		/// </summary>
		public long GoldAward
		{
			get { return m_goldAward; }
			set { m_goldAward = value; }
		}

		/// <summary>
		/// 下线玩家数
		/// </summary>
		public int BelowUser
		{
			get { return m_belowUser; }
			set { m_belowUser = value; }
		}

		/// <summary>
		/// 下线代理数
		/// </summary>
		public int BelowAgent
		{
			get { return m_belowAgent; }
			set { m_belowAgent = value; }
		}

		/// <summary>
		/// 禁用标识
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
