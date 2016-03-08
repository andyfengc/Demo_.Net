<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Error.aspx.cs" Inherits="PizzaApp.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="content">
        <h2>
            Oops!</h2>
        <div>
            An error has occurred on the page. Please notify the development team.
            <br />
            <br />
            Thank you.
            <br />
            <br />
            <asp:LinkButton ID="DetailLinkButton" runat="server" OnClick="DetailLinkButton_Click">Show Details</asp:LinkButton>
            <br />
            <br />
            <asp:Panel ID="MessagePanel" runat="server" Height="100%" Visible="False" Width="100%">
                <br />
                <asp:TextBox ID="MessageTextBox" runat="server" Height="300px" ReadOnly="True" TextMode="MultiLine"
                    Width="100%"></asp:TextBox>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
