<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SignUp.aspx.cs" Inherits="PizzaApp.SignUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #liSignUp
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
    <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="Server" ID="ScriptManager1" />
    <div class="container" id="content">
        <div id="divAlertMessage" runat="server" visible="false" class="alert fade in alert-success">
            <asp:Button runat="server" ID="ButtonAlertMessage" CssClass="close" Text="X" />
            <asp:Label runat="server" ID="LabelAlertMessage"></asp:Label>
        </div>
        <h2>
            SignUp</h2>
        <p>
            Sign up for a new PizzaShop account.</p>
        <div class="control-group email required">
            <asp:Label runat="server" ID="LabelEmail" CssClass="email required control-label"
                Text="Email" />
            <div class="controls">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="string email required" ValidationGroup="SignUpGroup"
                            TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxEmail"
                            Display="Dynamic" ErrorMessage="Please enter Email." ForeColor="#FF0000" runat="server"
                            ValidationGroup="SignUpGroup" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="TextBoxEmail" ErrorMessage="Please enter valid Email." ForeColor="#FF0000"
                            ValidationGroup="SignUpGroup" />
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
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" ValidationGroup="SignUpGroup"
                    TabIndex="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPassword"
                    ErrorMessage="Please enter Password." ForeColor="#FF0000" ValidationGroup="SignUpGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group password required">
            <asp:Label runat="server" ID="LabelConfirmPassword" CssClass="password required control-label"
                Text="Confirm Password" />
            <div class="controls">
                <asp:TextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password" ValidationGroup="SignUpGroup"
                    TabIndex="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextBoxConfirmPassword"
                    ErrorMessage="Please enter Confirm Password." ForeColor="#FF0000" ValidationGroup="SignUpGroup"
                    runat="server" />
                <br />
                <asp:CompareValidator ID="CompareValidatorTextBoxPasswords" runat="server" ControlToCompare="TextBoxPassword"
                    ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Please re-enter, passwords do not match."
                    ForeColor="#FF0000" ValidationGroup="SignUpGroup" />
            </div>
        </div>
        <h4>
            Add Credit Card</h4>
        <p>
            Your credit card information is stored safely with PayPal.</p>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelCreditCardType" CssClass="string required control-label"
                Text="Credit Card Type" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListCreditCardType" runat="server" ValidationGroup="SignUpGroup"
                    TabIndex="4">
                    <asp:ListItem Value="--Select--" Text="--Select--" />
                    <asp:ListItem Value="visa" Text="visa" />
                    <asp:ListItem Value="mastercard" Text="mastercard" />
                    <asp:ListItem Value="discover" Text="discover" />
                    <asp:ListItem Value="amex" Text="amex" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="DropDownListCreditCardType"
                    ForeColor="#FF0000" ValidationGroup="SignUpGroup" runat="server" InitialValue="--Select--"
                    Text="Please select Credit Card Type." />
            </div>
        </div>
        <div class="control-group string required">
            <asp:Label runat="server" ID="LabelCreditCardNumber" CssClass="string required control-label"
                Text="Credit Card Number" />
            <div class="controls">
                <asp:TextBox ID="TextBoxCreditCardNumber" runat="server" ValidationGroup="SignUpGroup"
                    TabIndex="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TextBoxCreditCardNumber"
                    ErrorMessage="Please enter Credit Card Number." ForeColor="#FF0000" ValidationGroup="SignUpGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group string required">
            <asp:Label runat="server" ID="LabelCreditCardCVV2" CssClass="string required control-label"
                Text="Credit Card CVV2" />
            <div class="controls">
                <asp:TextBox ID="TextBoxCreditCardCVV2" runat="server" TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="TextBoxCreditCardCVV2"
                    ErrorMessage="Please enter Credit Card CVV2." ForeColor="#FF0000" ValidationGroup="SignUpGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelCreditCardMonth" CssClass="string required control-label"
                Text="Credit Card Month" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListCreditCardExpireMonth" runat="server" ValidationGroup="SignUpGroup"
                    TabIndex="7">
                    <asp:ListItem Value="--Select--" Text="--Select--" />
                    <asp:ListItem Value="01" Text="01" />
                    <asp:ListItem Value="02" Text="02" />
                    <asp:ListItem Value="03" Text="03" />
                    <asp:ListItem Value="04" Text="04" />
                    <asp:ListItem Value="05" Text="05" />
                    <asp:ListItem Value="06" Text="06" />
                    <asp:ListItem Value="07" Text="07" />
                    <asp:ListItem Value="08" Text="08" />
                    <asp:ListItem Value="09" Text="09" />
                    <asp:ListItem Value="10" Text="10" />
                    <asp:ListItem Value="11" Text="11" />
                    <asp:ListItem Value="12" Text="12" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="DropDownListCreditCardExpireMonth"
                    ForeColor="#FF0000" ValidationGroup="SignUpGroup" runat="server" InitialValue="--Select--"
                    Text="Please select Credit Card Expire Month." />
            </div>
        </div>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelCreditCardYear" CssClass="string required control-label"
                Text="Credit Card Year" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListCreditCardExpireYear" runat="server" ValidationGroup="SignUpGroup"
                    TabIndex="8">
                    <asp:ListItem Value="--Select--" Text="--Select--" />
                    <asp:ListItem Value="2013" Text="2013" />
                    <asp:ListItem Value="2014" Text="2014" />
                    <asp:ListItem Value="2015" Text="2015" />
                    <asp:ListItem Value="2016" Text="2016" />
                    <asp:ListItem Value="2017" Text="2017" />
                    <asp:ListItem Value="2018" Text="2018" />
                    <asp:ListItem Value="2019" Text="2019" />
                    <asp:ListItem Value="2020" Text="2020" />
                    <asp:ListItem Value="2021" Text="2021" />
                    <asp:ListItem Value="2022" Text="2022" />
                    <asp:ListItem Value="2023" Text="2023" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="DropDownListCreditCardExpireYear"
                    ForeColor="#FF0000" ValidationGroup="SignUpGroup" runat="server" InitialValue="--Select--"
                    Text="Please select Credit Card Expire Year." />
            </div>
        </div>
        <div>
            <asp:Button ID="ButtonSignUp" runat="server" CssClass="btn btn btn-primary" Text="Sign up"
                ValidationGroup="SignUpGroup" TabIndex="9" OnClick="ButtonSignUp_Click" />
        </div>
    </div>
</asp:Content>
