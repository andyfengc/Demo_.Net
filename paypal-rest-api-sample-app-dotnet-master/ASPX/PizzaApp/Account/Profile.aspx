<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="PizzaApp.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #liProfile
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
            padding: 0px 30px 100px 30px !important;
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
    <style type="text/css">
        .watermarked
        {
            background-color: #F0F8FF !important;
            color: #808080 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="content">
        <div id="divAlertMessage" runat="server" visible="false" class="alert fade in alert-success">
            <asp:Button runat="server" ID="ButtonAlertMessage" CssClass="close" Text="X" />
            <asp:Label runat="server" ID="LabelAlertMessage"></asp:Label>
        </div>
        <h2>
            Edit profile</h2>
        <div class="control-group email required">
            <asp:Label runat="server" ID="LabelEmail" Text="Email" />
            <div class="controls">
                <asp:TextBox ID="TextBoxEmail" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group password required">
            <asp:Label runat="server" ID="LabelNewPassword" CssClass="password required control-label"
                Text="New Password" />
            <div class="controls">
                <asp:TextBox ID="TextBoxNewPassword" runat="server" TextMode="Password" ValidationGroup="EditGroup"
                    TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxNewPassword"
                    ErrorMessage="Please enter New Password." ForeColor="#FF0000" ValidationGroup="EditGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group password required">
            <asp:Label runat="server" ID="LabelConfirmNewPassword" CssClass="password required control-label"
                Text="Confirm New Password" />
            <div class="controls">
                <asp:TextBox ID="TextBoxConfirmNewPassword" runat="server" TextMode="Password" ValidationGroup="EditGroup"
                    TabIndex="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxConfirmNewPassword"
                    ErrorMessage="Please enter Confirm New Password." ForeColor="#FF0000" ValidationGroup="EditGroup"
                    runat="server" />
                <br />
                <asp:CompareValidator ID="CompareValidatorTextBoxPasswords" runat="server" ControlToCompare="TextBoxNewPassword"
                    ControlToValidate="TextBoxConfirmNewPassword" ErrorMessage="Please re-enter, passwords do not match."
                    ForeColor="#FF0000" ValidationGroup="EditGroup" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label runat="server" ID="LabelCurrentCreditCardNumber" Text="Current Credit Card Number" />
            <div class="controls">
                <asp:TextBox ID="TextBoxCurrentCreditCardNumber" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelNewCreditCardType" CssClass="string required control-label"
                Text="New Credit Card Type" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListNewCreditCardType" runat="server" ValidationGroup="EditGroup"
                    TabIndex="3">
                    <asp:ListItem Value="--Select--" Text="--Select--" />
                    <asp:ListItem Value="visa" Text="visa" />
                    <asp:ListItem Value="mastercard" Text="mastercard" />
                    <asp:ListItem Value="discover" Text="discover" />
                    <asp:ListItem Value="amex" Text="amex" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="DropDownListNewCreditCardType"
                    ForeColor="#FF0000" ValidationGroup="EditGroup" runat="server" InitialValue="--Select--"
                    Text="Please select New Credit Card Type." />
            </div>
        </div>
        <div class="control-group string required">
            <asp:Label runat="server" ID="LabelNewCreditCardNumber" CssClass="string required control-label"
                Text="New Credit Card Number" />
            <div class="controls">
                <asp:TextBox ID="TextBoxNewCreditCardNumber" runat="server" ValidationGroup="EditGroup"
                    TabIndex="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="TextBoxNewCreditCardNumber"
                    ErrorMessage="Please enter New Credit Card Number." ForeColor="#FF0000" ValidationGroup="EditGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group string required">
            <asp:Label runat="server" ID="LabelNewCreditCardCVV2" CssClass="string required control-label"
                Text="New Credit Card CVV2" />
            <div class="controls">
                <asp:TextBox ID="TextBoxNewCreditCardCVV2" runat="server" TabIndex="5" ValidationGroup="EditGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="TextBoxNewCreditCardCVV2"
                    ErrorMessage="Please enter New Credit Card CVV2." ForeColor="#FF0000" ValidationGroup="EditGroup"
                    runat="server" />
            </div>
        </div>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelNewCreditCardExpireMonth" CssClass="string required control-label"
                Text="New Credit Card Expire Month" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListNewCreditCardExpireMonth" runat="server" ValidationGroup="EditGroup"
                    TabIndex="6">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="DropDownListNewCreditCardExpireMonth"
                    ForeColor="#FF0000" ValidationGroup="EditGroup" runat="server" InitialValue="--Select--"
                    Text="Please select New Credit Card Expire Month." />
            </div>
        </div>
        <div class="control-group select required">
            <asp:Label runat="server" ID="LabelNewCreditCardExpireYear" CssClass="string required control-label"
                Text="New Credit Card Expire Year" />
            <div class="controls">
                <asp:DropDownList ID="DropDownListNewCreditCardExpireYear" runat="server" ValidationGroup="EditGroup"
                    TabIndex="7">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="DropDownListNewCreditCardExpireYear"
                    ForeColor="#FF0000" ValidationGroup="EditGroup" runat="server" InitialValue="--Select--"
                    Text="Please select New Credit Card Expire Year." />
            </div>
        </div>
        <div class="control-group password required">
            <asp:Label runat="server" ID="LabelCurrentPassword" CssClass="string required control-label"
                Text="Current Password" />
            <div class="controls">
                <asp:TextBox ID="TextBoxCurrentPassword" runat="server" TextMode="Password" ValidationGroup="EditGroup"
                    TabIndex="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TextBoxCurrentPassword"
                    ErrorMessage="Please enter Current Password." ForeColor="#FF0000" ValidationGroup="EditGroup"
                    runat="server" />
            </div>
        </div>
        <div>
            <asp:Button ID="ButtonUpdate" runat="server" CssClass="btn btn btn-primary" Text="Update"
                ValidationGroup="EditGroup" TabIndex="9" OnClick="ButtonUpdate_Click" />
        </div>
    </div>
</asp:Content>
