using System;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Agent;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentReturnRecordList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddlAwardType.DataSource = EnumDescription.GetFieldTexts(typeof(AppConfig.AwardType));
            ddlAwardType.DataTextField = "Description";
            ddlAwardType.DataValueField = "EnumValue";
            ddlAwardType.DataBind();
            ddlAwardType.Items.Insert(0, new ListItem("全部返利类型", "0"));
            BindData();
        }
        /// <summary>
        /// 搜索按钮的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string sqlWhere = "WHERE 1=1";
            string query = CtrlHelper.GetText(txtSearch);
            if (!string.IsNullOrEmpty(query))
            {
                string sqlUserId = $"SELECT UserID FROM WHQJAccountsDB.DBO.AccountsInfo WHERE NickName = '{query}' ";
                if (Utils.Validate.IsPositiveInt(query))
                {
                    sqlUserId += $" OR UserID ={query} OR GameID = {query} ";
                }
                sqlWhere += $" AND (SourceUserID IN ({sqlUserId}) OR TargetUserID IN ({sqlUserId})) ";
            }

            int awardType = Convert.ToInt32(ddlAwardType.SelectedValue);
            if (awardType > 0)
            {
                sqlWhere += $" AND AwardType = {awardType} ";
            }

            SearchItems = sqlWhere;
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideAgentFacade.GetList(ReturnAwardRecord.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1";
                }
                return (string)ViewState["SearchItems"];
            }
            set { ViewState["SearchItems"] = value; }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY CollectDate DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }

    }
}