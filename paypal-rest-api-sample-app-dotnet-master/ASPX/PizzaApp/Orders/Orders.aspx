<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Orders.aspx.cs" Inherits="PizzaApp.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #liOrders
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        }
        .watermarked
        {
            background-color: #F0F8FF !important;
            color: #808080 !important;
        }
        .main
        {
            padding: 0px 30px 50px 30px !important;
        }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
    $(document).ready(function () {
        $("#MainContent_ButtonAlertMessage").click(function () {
            $("#MainContent_divAlertMessage").hide();
            return false;
        });
    });

    // For Visual Studio 2008
    $(document).ready(function () {
        $("#ctl00_MainContent_ButtonAlertMessage").click(function () {
        $("#ctl00_MainContent_divAlertMessage").hide();
        return false;
        });
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="content">
        <div id="divAlertMessage" runat="server" visible="false" class="alert fade in alert-success">
            <asp:Button runat="server" ID="ButtonAlertMessage" CssClass="close" Text="X" />
            <asp:Label runat="server" ID="LabelAlertMessage"></asp:Label>
        </div>
        <h2>
            Orders</h2>
        <div style="width: 100%">
            <asp:GridView runat="server" ID="GridViewOrders" AutoGenerateColumns="false">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="true" ForeColor="#FFFFFF" />
                <PagerStyle BackColor="#2461BF" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="true" ForeColor="#FFFFFF" />
                <AlternatingRowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Order ID" />
                    <asp:BoundField DataField="user_id" HeaderText="User ID" />
                    <asp:BoundField DataField="payment_id" HeaderText="Payment ID" />
                    <asp:BoundField DataField="state" HeaderText="State" />
                    <asp:BoundField DataField="amount" HeaderText="Amount (USD)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="description" HeaderText="Description" />
                    <asp:BoundField DataField="created_at" HeaderText="Created Date Time" />
                    <asp:BoundField DataField="updated_at" HeaderText="Updated Date Time " />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=GridViewOrders.ClientID %>').Scrollable();
        })
    </script>
</asp:Content>
