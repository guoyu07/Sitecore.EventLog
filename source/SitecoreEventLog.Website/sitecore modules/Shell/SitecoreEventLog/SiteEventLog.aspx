﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteEventLog.aspx.cs" Inherits="SitecoreEventLog.Website.sitecore_modules.Shell.SitecoreEventLog.SiteEventLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Sitecore Event Log Item Details</title>
    <link rel="stylesheet" type="text/css" href="Resources/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Resources/DT_bootstrap.css">
    <script type="text/javascript" charset="utf-8" language="javascript" src="Resources/jquery.js"></script>
    <script type="text/javascript" charset="utf-8" language="javascript" src="Resources/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf-8" language="javascript" src="Resources/DT_bootstrap.js"></script>

    <script type="text/javascript" charset="utf-8" language="javascript" src="Resources/AuditTrail.js"></script>
</head>
<body>
    <h3 style="padding-left: 25px; margin-bottom:0;">Log for site</h3>
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 30px">
            <asp:GridView runat="server"
                ID="SiteEventLogGrid"
                AllowPaging="true"
                AllowSorting="true"
                PageSize="12"
                CssClass="table table-striped table-bordered" 
                AutoGenerateColumns="false"
                SelectMethod="SiteEventLogGrid_GetData">
                <Columns>
                    <asp:BoundField DataField="Date" HeaderText="Date" />
                    <asp:BoundField DataField="EventType" HeaderText="EventType" />
                    <%--<asp:BoundField DataField="PublishDetail.IsSitePublish" HeaderText="Is Site Publish" />--%>
                    <asp:BoundField DataField="SourceDatabase" HeaderText="Database" />
                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                    <asp:BoundField DataField="UserName" HeaderText="User" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>