using System;
using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.Group;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.ClubManager
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

            IMGroupOption config = new IMGroupOption();
            config.OptionName = CtrlHelper.GetText(txtStatusName);
            config.OptionValue = CtrlHelper.GetInt(txtStatusValue, 0);
            config.OptionTip = CtrlHelper.GetText(txtStatusTip);
            config.OptionDescribe = CtrlHelper.GetText(txtStatusDescription);

            int result = FacadeManage.aideGroupFacade.UpdateGroupOption(config);
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
            PagerSet pagerSet = FacadeManage.aideGroupFacade.GetList(IMGroupOption.Tablename ,1, 100, SearchItems, Orderby);
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            IMGroupOption config = FacadeManage.aideGroupFacade.GetGroupOption(string.IsNullOrEmpty(StrParam) ? "MaxMemberCount" : StrParam);
            if (config == null) return;
            CtrlHelper.SetText(txtStatusName, config.OptionName);
            CtrlHelper.SetText(txtStatusValue, config.OptionValue.ToString());
            CtrlHelper.SetText(txtStatusTip, config.OptionTip);
            CtrlHelper.SetText(txtStatusDescription, config.OptionDescribe);
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
