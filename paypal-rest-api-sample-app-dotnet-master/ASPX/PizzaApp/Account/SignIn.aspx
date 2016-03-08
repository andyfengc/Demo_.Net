<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SignIn.aspx.cs" Inherits="PizzaApp.SignIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #liSignIn
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
            SignIn</h2>
        <p>
            Sign in with your PizzaShop account. Don't have an account yet? <a href="/Account/SignUp.aspx">
                SignUp</a> for one.</p>
        <div class="control-group email required">
            <asp:Label runat="server" ID="LabelEmail" CssClass="email required control-label"
                Text="Email" />
            <div class="controls">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="string email required" ValidationGroup="SignInGroup"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxEmail"
                            Display="Dynamic" ErrorMessage="Please enter Email." ForeColor="#FF0000" runat="server"
                            ValidationGroup="SignInGroup" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="TextBoxEmail" ErrorMessage="Please enter valid Email." ForeColor="#FF0000"
                            ValidationGroup="SignInGroup" />
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                            TargetControlID="TextBoxEmail" WatermarkText="dummy@email.com" WatermarkCssClass="watermarked" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="control-group password required">
            <asp:Label runat="server" ID="LabelPassword" CssClass="password required control-label"
                Text="Password" />
            <div class="controls">
                <asp:TextBox runat="server" ID="TextBoxPassword" CssClass="password required" TextMode="Password"
                    ValidationGroup="SignInGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPassword"
                    ErrorMessage="Please enter Password." ForeColor="#FF0000" ValidationGroup="SignInGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group">
            <div class="checkbox">
                <asp:CheckBox runat="server" ID="CheckBoxPersist" Text="Remember" />
            </div>
        </div>
        <div>
            <asp:Button runat="server" ID="ButtonSignIn" CssClass="btn btn btn-primary" Text="Sign in"
                OnClick="ButtonSignIn_Click" ValidationGroup="SignInGroup" />
        </div>
    </div>
</asp:Content>
