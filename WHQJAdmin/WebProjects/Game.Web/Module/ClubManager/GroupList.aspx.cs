using System;
using System.Data;
using System.Text;
using Game.Entity.Accounts;
using Game.Entity.Agent;
using Game.Entity.Enum;
using Game.Entity.Group;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;

namespace Game.Web.Module.ClubManager
{
    public partial class GroupList : AdminPage
    {
        #region 窗体事件
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DBDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            DBDataBind();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            StringBuilder condition = new StringBuilder(" WHERE 1=1");
            

            ViewState["SearchItems"] = condition.ToString();
            DBDataBind();
        }
       
        /// <summary>
        /// 批量冻结群组
        /// </summary>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Add);
            int result = FacadeManage.aideGroupFacade.NullityGroup(StrCIdList, 1);
            if(result > 0)
            {
                ShowInfo("冻结成功");
                DBDataBind();
            }
            else
            {
                ShowError("冻结失败");
            }
        }
        /// <summary>
        /// 批量解冻群组
        /// </summary>
        protected void btnJiedong_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Add);
            int result = FacadeManage.aideGroupFacade.NullityGroup(StrCIdList, 0);
            if(result > 0)
            {
                ShowInfo("解冻成功");
                DBDataBind();
            }
            else
            {
                ShowError("解冻失败");
            }
        }
        /// <summary>
        /// 批量强制解散
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDismiss_OnClick(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            int result = FacadeManage.aideGroupFacade.DeleteGroup(StrCIdList);
            if (result > 0)
            {
                ShowInfo("强制解散成功");
                DBDataBind();
            }
            else
            {
                ShowError("强制解散失败");
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DBDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideGroupFacade.GetList(IMGroupProperty.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }


        #endregion

        #region 公共方法

        /// <summary>
        /// 获取俱乐部在线人数
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupMemberOnline(long groupId)
        {
            return FacadeManage.aideGroupFacade.GetGroupMemberOnline(groupId).ToString();
        }

        /// <summary>
        /// 获取俱乐部钻石数量
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupWealth(long groupId)
        {
            return FacadeManage.aideGroupFacade.GetGroupWealth(groupId).ToString();
        }

        /// <summary>
        /// 获取俱乐部约战房间数量
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="roomStatus">房间状态：-1为所有 1为进行中</param>
        /// <returns></returns>
        public string GetGroupBattleRoomCount(long groupId, int roomStatus = -1)
        {
            if (roomStatus == -1)
            {
                return FacadeManage.aideGroupFacade.GetGroupBattleRoomStream(groupId).BattleCount.ToString();
            }
            return FacadeManage.aideGroupFacade.GetGroupBattleRoomCurrent(groupId).ToString();
        }

        #endregion

        #region 公共属性

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
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY CreateDateTime DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }

        #endregion





    }
}