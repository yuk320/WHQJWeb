using System;
using Game.Web.UI;
using System.Text;
using Game.Kernel;
using Game.Utils;
using Game.Facade;
using Game.Entity.Treasure;

namespace Game.Web.Module.FilledManager
{
    public partial class RecordPayDiamond : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlGlobalShareInfo.DataSource = EnumDescription.GetFieldTexts(typeof(AppConfig.ShareType));
                ddlGlobalShareInfo.DataTextField = "Description";
                ddlGlobalShareInfo.DataValueField = "EnumValue";
                ddlGlobalShareInfo.DataBind();
                ShareInfoDataBind();
            }
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            ShareInfoDataBind();
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        private void SetCondition()
        {
            int service = int.Parse(ddlGlobalShareInfo.SelectedValue);
            string startDate = CtrlHelper.GetText(txtStartDate) + " 00:00:00";
            string endDate = CtrlHelper.GetText(txtEndDate) + " 23:59:59";
            StringBuilder condition = new StringBuilder("WHERE 1=1");
            if (service > 0)
            {
                condition.AppendFormat(" AND ShareID={0} ", service);
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                condition.AppendFormat(" AND OrderDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            }
            ViewState["SearchItems"] = condition.ToString();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            SetCondition();
            ShareInfoDataBind();
        }

        /// <summary>
        /// 查询今天
        /// </summary>
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetTodayTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetTodayTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            btnQuery_Click(sender, e);
        }

        /// <summary>
        /// 查询昨天
        /// </summary>
        protected void btnQueryYD_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetYesterdayTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetYesterdayTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            btnQuery_Click(sender, e);
        }

        /// <summary>
        /// 查询本周
        /// </summary>
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetWeekTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetWeekTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            btnQuery_Click(sender, e);
        }

        /// <summary>
        /// 查询上周
        /// </summary>
        protected void btnQueryYW_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetLastWeekTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetLastWeekTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            btnQuery_Click(sender, e);
        }

        /// <summary>
        /// 用户查询
        /// </summary>
        protected void btnQueryAcc_Click(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(ddlSearchType.SelectedValue);
            int status = Convert.ToInt32(ddlPayStatus.SelectedValue);
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);

            StringBuilder condition = new StringBuilder("WHERE 1=1");
            if (status >= 0)
            {
                condition.AppendFormat(" AND OrderStatus='{0}'", status);
            }
            if (!string.IsNullOrEmpty(queryContent))
            {
                switch (type)
                {
                    case 0:
                        condition.AppendFormat(" AND OrderID='{0}'", queryContent);
                        break;
                    case 2:
                        condition.AppendFormat(" AND NickName='{0}'", queryContent);
                        break;
                    default:
                        if (!Utils.Validate.IsPositiveInt(queryContent))
                        {
                            ShowError("输入的查询格式不正确");
                            return;
                        }
                        condition.AppendFormat(type == 1 ? " AND GameID={0}" : " AND UserID={0}", queryContent);
                        break;
                }
            }

            ViewState["SearchItems"] = condition.ToString();
            ShareInfoDataBind();
        }

        /// <summary>
        /// 充值类型
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        public string GetOrderShareName(int shareId)
        {
            return EnumDescription.GetFieldText((AppConfig.ShareType) shareId);
        }

        /// <summary>
        /// 充值状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetPayStatus(int status)
        {
            switch (status)
            {
                case 2:
                    return "<span>已完成</span>";
                case 1:
                    return "<span class='hong'>已支付，未处理</span>";
                default:
                    return "<span class='hong'>未支付</span>";
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void ShareInfoDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(OnLinePayOrder.Tablename,
                anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            ltTotal.Text = $"已支付金额：{FacadeManage.aideTreasureFacade.GetTotalPayAmount(SearchItems)}元 已支付订单数：{FacadeManage.aideTreasureFacade.GetTotalPayOrderCount(SearchItems)} (当前条件统计)";
            rptShareInfo.DataSource = pagerSet.PageSet;
            rptShareInfo.DataBind();
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
                    SetCondition();
                }
                return (string) ViewState["SearchItems"];
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
                    ViewState["Orderby"] = "ORDER BY OnLineID DESC";
                }
                return (string) ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }
    }
}