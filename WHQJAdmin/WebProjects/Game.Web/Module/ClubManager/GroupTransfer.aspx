<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupTransfer.aspx.cs" Inherits="Game.Web.Module.ClubManager.GroupTransfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <link href="../../styles/layout.css" rel="stylesheet" type="text/css"/>
  <script type="text/javascript" src="../../scripts/common.js"></script>
  <script type="text/javascript" src="/scripts/jquery.js"></script>
  <title></title>
</head>
<body>
<form id="form1" runat="server">
  <!-- 头部菜单 Start -->
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
    <tr>
      <td width="19" height="25" valign="top" class="Lpd10">
        <div class="arr"></div>
      </td>
      <td width="1232" height="25" valign="top" align="left">你当前位置：俱乐部管理 - 移交会长</td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="titleOpBg Lpd10">
        <input type="button" value="关闭" class="btn wd1" onclick="window.close();"/>
        <input class="btnLine" type="button"/>
        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1"
                    onclick="btnSave_Click"/>
      </td>
    </tr>
  </table>
  <asp:ScriptManager ID="ScriptManager2" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
      <table id="table" width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
          <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
            <div class="hg3  pd7">
              移交会长
            </div>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">俱乐部名称：</td>
          <td>
            <asp:Label runat="server" ID="lblGroupName"></asp:Label>
            <span class="hong">*</span>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">原会长：</td>
          <td>
            <asp:Label runat="server" ID="lblCreatetInfo"></asp:Label>
            <span class="hong">*</span>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">新会长：</td>
          <td>
            <asp:DropDownList runat="server" ID="ddlMemberUserId" AutoPostBack=""></asp:DropDownList>
            <span class="hong">*</span>
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="titleOpBg Lpd10">
        <input type="button" value="关闭" class="btn wd1" onclick="window.close();"/>
        <input class="btnLine" type="button"/>
        <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click"/>
      </td>
    </tr>
  </table>
</form>
</body>
</html>
