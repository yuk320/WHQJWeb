using System;
using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.Agent;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AgentManager
{
    public partial class SystemSet : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);

            SystemStatusInfo config = new SystemStatusInfo
            {
                StatusName = CtrlHelper.GetText(txtStatusName),
                StatusValue = CtrlHelper.GetInt(txtStatusValue, 0),
                StatusString = CtrlHelper.GetText(txtStatusString),
                StatusTip = CtrlHelper.GetText(txtStatusTip),
                StatusDescription = CtrlHelper.GetText(txtStatusDescription)
            };

            int result = FacadeManage.aideAgentFacade.UpdateSystemStatusInfo(config);
            if(result > 0)
            {
                ShowInfo("修改成功");
            }
            else
            {
                ShowError("修改失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideAgentFacade.GetList(SystemStatusInfo.Tablename ,1, 100, SearchItems, Orderby);
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            SystemStatusInfo config = FacadeManage.aideAgentFacade.GetSystemStatusInfo(string.IsNullOrEmpty(StrParam) ? "AgentAwardType" : StrParam);
            if (config == null) return;
            CtrlHelper.SetText(txtStatusName, config.StatusName);
            CtrlHelper.SetText(txtStatusValue, config.StatusValue.ToString());
            CtrlHelper.SetText(txtStatusTip, config.StatusTip);
            CtrlHelper.SetText(txtStatusString, config.StatusString);
            CtrlHelper.SetText(txtStatusDescription, config.StatusDescription);
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if(ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1 ";
                }
                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if(ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY SortID ASC";
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }
    }
}
