using Game.Entity.Agent;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using Game.Entity.Accounts;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Add);
            string agentNote = CtrlHelper.GetText(txtAgentNote);
            string compellation = CtrlHelper.GetText(txtCompellation);
            string address = CtrlHelper.GetText(txtContactAddress);
            string phone = CtrlHelper.GetText(txtContactPhone);
            string domain = CtrlHelper.GetText(txtDomain);
            int gameid = CtrlHelper.GetInt(txtGameID, 0);
            string qqaccount = CtrlHelper.GetText(txtQQAccount);
            string wxnickname = CtrlHelper.GetText(txtWCNickName);

            //判断用户是否存在
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(gameid);
            if(info==null || info.UserID <= 0)
            {
                MessageBox("游戏ID无效，请重新输入");
                return;
            }
            if(!string.IsNullOrEmpty(info.Compellation)&&!info.Compellation.Equals(compellation))
            {
                MessageBox("真实姓名与实名认证资料不符");
                return;
            }
            AgentInfo agent = new AgentInfo
            {
                AgentDomain = domain,
                AgentLevel = 1,
                AgentNote = agentNote,
                Compellation = compellation,
                ContactAddress = address,
                ContactPhone = phone,
                QQAccount = qqaccount,
                WCNickName = wxnickname,
                UserID = info.UserID
            };

            Message msg = FacadeManage.aideAgentFacade.InsertAgentUser(agent);
            if(msg.Success)
            {
                MessageBoxCloseRef(msg.Content);
            }
            else
            {
                MessageBox(msg.Content);
            }
        }
    }
}