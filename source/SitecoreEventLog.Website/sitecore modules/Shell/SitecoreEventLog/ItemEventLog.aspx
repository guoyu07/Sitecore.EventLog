﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEventLog.aspx.cs" Inherits="SitecoreEventLog.Website.sitecore_modules.Shell.SitecoreEventLog.ItemEventLog" %>

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
    <h3 style="padding-left: 25px; margin-bottom:0;">Log for item</h3>
    <form runat="server">
        <div class="container" style="margin-top: 30px">
            <asp:LinkButton Text="Back to overview" ID="btnBack" Visible="false" OnClick="Back_Click" runat="server" />
            <asp:GridView runat="server"
                ID="EventLogGrid"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="false"
                DataKeyNames="DetailId"
                AllowPaging="true"
                OnRowDataBound="EventLogGrid_RowDataBound"
                AllowSorting="true"
                PageSize="12"
                OnRowCommand="EventLogGrid_RowCommand"
                SelectMethod="EventLogGrid_GetData">
                <Columns>
                    <asp:BoundField DataField="Date" HeaderText="Date" />
                    <asp:BoundField DataField="EventType" HeaderText="EventType" />
                    <asp:BoundField DataField="SourceDatabase" HeaderText="Database" />
                    <asp:BoundField DataField="ItemVersion" HeaderText="Version" />
                    <asp:BoundField DataField="UserName" HeaderText="User" />
                    <asp:ButtonField ButtonType="Link" CommandName="Details" Text="Details" HeaderText="Actions" />
                </Columns>
            </asp:GridView>

            <asp:GridView runat="server"
                ID="EventLogPublishDetailGrid"
                CssClass="table table-striped table-bordered"
                EnableSortingAndPagingCallbacks="True"
                AutoGenerateColumns="false"
                Visible="false">
                <Columns>
                    <asp:BoundField DataField="Cultures" HeaderText="Cultures" />
                    <asp:BoundField DataField="TargetDatabases" HeaderText="Target databases" />
                    <asp:BoundField DataField="IsSitePublish" HeaderText="Site publish" />
                    <asp:BoundField DataField="WithSubItems" HeaderText="With Subitems?" />
                    <asp:BoundField DataField="WithRelatedItems" HeaderText="With related items?" />
                    <asp:BoundField DataField="RootItemId" HeaderText="RootItem Id" />
                    <asp:BoundField DataField="RootItemPath" HeaderText="RootItem Path" />
                </Columns>
            </asp:GridView>

            <asp:GridView runat="server"
                ID="EventLogSaveDetailGrid"
                CssClass="table table-striped table-bordered"
                EnableSortingAndPagingCallbacks="True"
                AutoGenerateColumns="false"
                Visible="false">
                <Columns>
                    <asp:BoundField DataField="Language" HeaderText="Language" />
                    <asp:BoundField DataField="FormattedChanges" HeaderText="Changes" HtmlEncode="false"/>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>