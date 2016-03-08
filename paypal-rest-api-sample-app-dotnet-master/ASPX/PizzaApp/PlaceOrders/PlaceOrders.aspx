<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlaceOrders.aspx.cs" Inherits="PizzaApp.PlaceOrders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        .progressInner
        {
            background-color: #DDDDDD;
            color: #fff;
            width: 150px;
            text-align: center;
            vertical-align: middle;
            position: absolute;
            bottom: 50%;
            left: 45%;
        }
        .progressOuter
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: #DDDDDD; /* filter:alpha(opacity=70); */ /* opacity:0.7; */ /* opacity:0.50; */ /* firefox, opera, safari, chrome */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=30)"; /* IE 8 */ /* filter:alpha(opacity=50); */ /* IE 4, 5, 6 and 7 */ /* zoom:1 /* so the element "hasLayout" */ /* or, to trigger "hasLayout" set a width or height */
            height: 100%;
            width: 100%;
            min-height: 100%;
            min-width: 100%;
            opacity: 0.5;
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
    <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="Server" ID="ScriptManager1" />
    <div class="container" id="content">
        <div id="divAlertMessage" runat="server" visible="false" class="alert fade in alert-success">
            <asp:Button runat="server" ID="ButtonAlertMessage" CssClass="close" Text="X" />
            <asp:Label runat="server" ID="LabelAlertMessage"></asp:Label>
        </div>
        <h2>
            Place Order</h2>
        <div class="control-group">
            <label class="string optional control-label">
                Amount</label>
            <div class="controls">
                <asp:TextBox ID="TextBoxOrderAmount" runat="server" CssClass="string optional" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="string optional control-label">
                Description</label>
            <div class="controls">
                <asp:TextBox ID="TextBoxOrderDescription" runat="server" CssClass="string optional"
                    ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group select optional">
            <label class="select optional control-label">
                Payment Method</label>
            <div class="controls">
                <asp:DropDownList ID="DropDownPaymentMethod" runat="server" ValidationGroup="SignUpGroup"
                    TabIndex="4">
                    <asp:ListItem Value="credit_card" Text="credit_card" />
                    <asp:ListItem Value="paypal" Text="paypal" />
                </asp:DropDownList>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanelButtonPlaceOrder" runat="server">
                <ContentTemplate>
                    <asp:Button ID="ButtonPlaceOrder" runat="server" CssClass="btn btn btn-primary" Text="Place order"
                        OnClick="ButtonPlaceOrder_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgressButtonPlaceOrder" runat="server">
        <ProgressTemplate>
            <div class="progressOuter">
                <div class="progressInner">
                    <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
