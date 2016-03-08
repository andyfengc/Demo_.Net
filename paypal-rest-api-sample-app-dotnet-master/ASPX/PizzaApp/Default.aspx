<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PizzaApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        #liHome, #liDefault
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container" id="content">
        <div id="divAlertMessage" runat="server" visible="false" class="alert fade in alert-success">
            <asp:Button runat="server" ID="ButtonAlertMessage" CssClass="close" Text="X" />
            <asp:Label runat="server" ID="LabelAlertMessage"></asp:Label>
        </div>
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image1" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000000?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    11$ -
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=11&amp;order%5Bdescription%5D=Pizza+1"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image2" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000001?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    12$ -
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=12&amp;order%5Bdescription%5D=Pizza+2"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image3" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000002?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    13$ -
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=13&amp;order%5Bdescription%5D=Pizza+3"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image4" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000003?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    14$ -
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=14&amp;order%5Bdescription%5D=Pizza+4"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image5" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000004?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    15$ -
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=15&amp;order%5Bdescription%5D=Pizza+5"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image6" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000005?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    16$ -
                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=16&amp;order%5Bdescription%5D=Pizza+6"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image7" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000006?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    21$ -
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=21&amp;order%5Bdescription%5D=Pizza+7"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image8" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000007?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    22$ -
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=22&amp;order%5Bdescription%5D=Pizza+8"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image9" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000008?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    23$ -
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=23&amp;order%5Bdescription%5D=Pizza+9"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image10" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000009?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    24$ -
                    <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=24&amp;order%5Bdescription%5D=Pizza+10"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image11" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000010?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    25$ -
                    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=25&amp;order%5Bdescription%5D=Pizza+11"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image12" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000011?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    26$ -
                    <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=26&amp;order%5Bdescription%5D=Pizza+12"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image13" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000012?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    31$ -
                    <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=31&amp;order%5Bdescription%5D=Pizza+13"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image14" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000013?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    32$ -
                    <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=32&amp;order%5Bdescription%5D=Pizza+14"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image15" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000014?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    33$ -
                    <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=33&amp;order%5Bdescription%5D=Pizza+15"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image16" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000015?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    34$ -
                    <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=34&amp;order%5Bdescription%5D=Pizza+16"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image17" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000016?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    35$ -
                    <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=35&amp;order%5Bdescription%5D=Pizza+17"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <asp:Image ID="Image18" runat="server" ImageUrl="http://www.gravatar.com/avatar/0000000000000000000000000000017?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    36$ -
                    <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/PlaceOrders/PlaceOrders.aspx?order%5Bamount%5D=36&amp;order%5Bdescription%5D=Pizza+18"
                        CssClass="btn btn-small" Text="Buy">
                    </asp:HyperLink>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="span6 offset3">
                <p>
                    This is a sample application which showcases the new PayPal REST APIs. The App uses
                    mock data to demonstrate how you can use the REST APIs for the following operations:</p>
                <ul>
                    <li>Saving Credit Card information with PayPal for later use</li>
                    <li>Making Payments using a saved Credit Card</li>
                    <li>Making Payments using PayPal</li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
