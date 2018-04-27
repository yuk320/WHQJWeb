using System;
using System.Collections.Generic;
using Game.Web.UI;
using Game.Entity.Agent;
using Game.Facade;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserUnder : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            IList<AgentBelowInfo> list = FacadeManage.aideAgentFacade.GetAgentBelowRegisterList(IntParam);
            foreach (var agentBelowInfo in list)
            {
                agentBelowInfo.AgentID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(agentBelowInfo.UserID)
                    .AgentID;
            }
            lbTotal.Text = list.Count.ToString();
            litNoData.Visible = list.Count <= 0;
            rptDataList.DataSource = list;
            rptDataList.DataBind();
        }
    }
}