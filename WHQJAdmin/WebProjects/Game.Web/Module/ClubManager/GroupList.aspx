<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="Game.Web.Module.ClubManager.GroupList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <title></title>
  <link href="../../styles/layout.css" rel="stylesheet" type="text/css"/>

  <script type="text/javascript" src="../../scripts/common.js"></script>

  <script type="text/javascript" src="../../scripts/comm.js">
  </script>
</head>
<body>
<form id="form1" runat="server">
  <!-- 头部菜单 Start -->
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
    <tr>
      <td width="19" height="25" valign="top" class="Lpd10">
        <div class="arr">
        </div>
      </td>
      <td width="1232" height="25" valign="top" align="left">
        你当前位置：俱乐部系统 - 俱乐部管理
      </td>
    </tr>
  </table>
  <!-- 头部菜单 End -->
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
    <tr>
      <td align="center" style="width: 80px">
        俱乐部查询：
      </td>
      <td>
        <asp:TextBox ID="txtSearch" runat="server" CssClass="text" PlaceHolder="俱乐部关键词搜索，支持名称、ID、群主昵称、群主ID等关键词查询"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery_Click"/>
      </td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="Tmg7">
    <tr>
      <td height="39" class="titleOpBg">
        <asp:Button ID="btnDongjie" runat="server" Text="冻结" CssClass="btn wd1" OnClick="btnDongjie_Click" OnClientClick="return deleteop()"/>
        <asp:Button ID="btnJiedong" runat="server" Text="解冻" CssClass="btn wd1" OnClick="btnJiedong_Click" OnClientClick="return deleteop()"/>
        <asp:Button ID="btnDismiss" runat="server" Text="强制解散" CssClass="btn wd2" OnClick="btnDismiss_OnClick" OnClientClick="return deleteop()"/>
      </td>
    </tr>
  </table>
  <div id="content">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
      <tr align="center" class="bold">
        <td class="listTitle">
          <input type="checkbox" name="chkAll" onclick="SelectAll(this.checked);"/>
        </td>
        <td class="listTitle2">
          管理
        </td>
        <td class="listTitle2">
          俱乐部编号
        </td>
        <td class="listTitle2">
          俱乐部名称
        </td>
        <td class="listTitle2">
          创建日期
        </td>
        <td class="listTitle2">
          俱乐部会长
        </td>
        <td class="listTitle2">
          在线/总/最大 人数
        </td>
        <td class="listTitle2">
          俱乐部钻石数量
        </td>
        <td class="listTitle2">
          钻石消耗量
        </td>
        <td class="listTitle2">
          当前房间数量
        </td>
        <td class="listTitle2">
          创建房间次数
        </td>
      </tr>
      <asp:Repeater ID="rptDataList" runat="server">
        <ItemTemplate>
          <tr align="center" class="list" onmouseover="currentcolor = this.style.backgroundColor;this.style.backgroundColor = '#caebfc';this.style.cursor = 'default';"
              onmouseout="this.style.backgroundColor = currentcolor">
            <td style="width: 30px;">
              <input name='cid' type='checkbox' value='<%# Eval("GroupID").ToString() %>'/>
            </td>
            <td>
              <a href="javascript:;" class="l" onclick="openWindowOwn('GroupCount.aspx?param=<%# Eval("GroupID") %>', '', 700, 490);">统计</a> 
              <a href="javascript:;" class="l" onclick="openWindowOwn('GroupTransfer.aspx?param=<%# Eval("GroupID") %>', '', 700, 490);">移交会长</a>
            </td>
            <td>
              <%# Eval("GroupID") %>
            </td>
            <td>
              <%# Eval("GroupName") %>
            </td>
            <td>
              <%# Convert.ToDateTime(Eval("CreateDateTime")).ToString("yyyy-MM-dd") %>
            </td>
            <td><a class="l" href="javascript:void(0)" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param=<%# Eval("CreaterID") %>', '<%# Eval("CreaterNickName") %>', 700,490);"><%# Eval("CreaterGameID") %> | <%# Eval("CreaterNickName") %></a></td>
            <td>
              <%# GetGroupMemberOnline(Convert.ToInt64(Eval("GroupID"))) %> / <%# Eval("MemberCount") %> / <%# Eval("MaxMemberCount") %>
            </td>
            <td>
              <%# GetGroupWealth(Convert.ToInt64(Eval("GroupID"))) %>
            </td>
            <td>0</td>
            <td>
              <%# GetGroupBattleRoomCount(Convert.ToInt64(Eval("GroupID")),1) %>
            </td>
            <td>
              <%# GetGroupBattleRoomCount(Convert.ToInt64(Eval("GroupID"))) %>
            </td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
          <tr align="center" class="listBg" onmouseover="currentcolor = this.style.backgroundColor;this.style.backgroundColor = '#caebfc';this.style.cursor = 'default';"
              onmouseout="this.style.backgroundColor = currentcolor">
            <td style="width: 30px;">
              <input name='cid' type='checkbox' value='<%# Eval("GroupID").ToString() %>'/>
            </td>
            <td>
              <a href="javascript:;" class="l" onclick="openWindowOwn('GroupCount.aspx?param=<%# Eval("GroupID") %>', '', 700, 490);">统计</a> 
              <a href="javascript:;" class="l" onclick="openWindowOwn('GroupTransfer.aspx?param=<%# Eval("GroupID") %>', '', 700, 490);">移交会长</a>
            </td>
            <td>
              <%# Eval("GroupID") %>
            </td>
            <td>
              <%# Eval("GroupName") %>
            </td>
            <td>
              <%# Convert.ToDateTime(Eval("CreateDateTime")).ToString("yyyy-MM-dd") %>
            </td>
            <td><a class="l" href="javascript:void(0)" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx.aspx?param=<%# Eval("CreaterID") %>', '<%# Eval("CreaterNickName") %>', 700,490);"><%# Eval("CreaterGameID") %> | <%# Eval("CreaterNickName") %></a></td>
            <td>
              <%# GetGroupMemberOnline(Convert.ToInt64(Eval("GroupID"))) %> / <%# Eval("MemberCount") %> / <%# Eval("MaxMemberCount") %>
            </td>
            <td>
              <%# GetGroupWealth(Convert.ToInt64(Eval("GroupID"))) %>
            </td>
            <td>0</td>
            <td>
              <%# GetGroupBattleRoomCount(Convert.ToInt64(Eval("GroupID")),1) %>
            </td>
            <td>
              <%# GetGroupBattleRoomCount(Convert.ToInt64(Eval("GroupID"))) %>
            </td>
          </tr>
        </AlternatingItemTemplate>
      </asp:Repeater>
      <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
    </table>
  </div>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="listTitleBg">
        <span>选择：</span>&nbsp;<a class="l1" href="javascript:SelectAll(true);">全部</a>&nbsp;-&nbsp;<a class="l1" href="javascript:SelectAll(false);">无</a>
      </td>
      <td align="right" class="page">
        <gsp:AspNetPager ID="anpPage" OnPageChanged="anpPage_PageChanged" runat="server" AlwaysShow="true" FirstPageText="首页" LastPageText="末页"
                         PageSize="20" NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table"
                         NumericButtonCount="5" CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%" UrlPaging="false">
        </gsp:AspNetPager>
      </td>
    </tr>
  </table>
</form>
</body>
</html>
