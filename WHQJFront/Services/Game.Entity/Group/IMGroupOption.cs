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
	/// 实体类 IMGroupOption。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class IMGroupOption  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "IMGroupOption" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OptionName = "OptionName" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OptionValue = "OptionValue" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OptionDescribe = "OptionDescribe" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OptionTip = "OptionTip" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _SortID = "SortID" ;
		#endregion

		#region 私有变量
		private int m_stationID;				//
		private string m_optionName;			//
		private int m_optionValue;				//
		private string m_optionDescribe;		//
		private string m_optionTip;				//
		private int m_sortID;					//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化IMGroupOption
		/// </summary>
		public IMGroupOption()
		{
			m_stationID=0;
			m_optionName="";
			m_optionValue=0;
			m_optionDescribe="";
			m_optionTip="";
			m_sortID=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int StationID
		{
			get { return m_stationID; }
			set { m_stationID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OptionName
		{
			get { return m_optionName; }
			set { m_optionName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int OptionValue
		{
			get { return m_optionValue; }
			set { m_optionValue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OptionDescribe
		{
			get { return m_optionDescribe; }
			set { m_optionDescribe = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string OptionTip
		{
			get { return m_optionTip; }
			set { m_optionTip = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}
		#endregion
	}
}
