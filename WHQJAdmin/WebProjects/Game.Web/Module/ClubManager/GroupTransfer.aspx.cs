using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Game.Entity.Group;

namespace Game.Web.Module.ClubManager
{
    public partial class GroupTransfer : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            IMGroupProperty groupInfo = FacadeManage.aideGroupFacade.GetGroupInfo(LongParam);
            if (groupInfo == null) return;
            CtrlHelper.SetText(lblGroupName, $"{groupInfo.GroupName}（{groupInfo.GroupID}）");
            CtrlHelper.SetText(lblCreatetInfo, $"{groupInfo.CreaterNickName}（{groupInfo.CreaterGameID}）");
            IList<IMGroupMember> list = FacadeManage.aideGroupFacade.GetGroupMemberList(LongParam);
            int ownerIndex=0;
            for (int i=0; i < list.Count; i++)
            {
                if (Convert.ToInt32(list[i].MemberID) == groupInfo.CreaterID)
                {
                    ownerIndex = i;
                    continue;
                }
                AccountsInfo ai = FacadeManage.aideAccountsFacade
                    .GetAccountInfoByUserId(list[i].MemberID);
                list[i].NickName = ai.NickName;
                list[i].GameID = ai.GameID;
            }
            list.RemoveAt(ownerIndex);
            ddlMemberUserId.DataSource = list;
            ddlMemberUserId.DataTextField = "ShowName";
            ddlMemberUserId.DataValueField = "MemberID";
            ddlMemberUserId.DataBind();

            ddlMemberUserId.Items.Insert(0,new ListItem("请选择新会长","0"));
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);
            int userId = Convert.ToInt32(ddlMemberUserId.SelectedValue);
            if (userId == 0)
            {
                MessageBox("请选择新会长");
                return;
            }
            IMGroupProperty groupInfo = FacadeManage.aideGroupFacade.GetGroupInfo(LongParam);
            AccountsInfo ai = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userId);
            groupInfo.CreaterID = userId;
            groupInfo.CreaterNickName = ai.NickName;
            groupInfo.CreaterGameID = ai.GameID;

            int result = FacadeManage.aideGroupFacade.UpdateGroup(groupInfo);
            if (result > 0)
            {
                MessageBox("移交成功","GroupList.aspx");
            }
            else
            {
                MessageBox("移交失败");
            }
        }
    }
}