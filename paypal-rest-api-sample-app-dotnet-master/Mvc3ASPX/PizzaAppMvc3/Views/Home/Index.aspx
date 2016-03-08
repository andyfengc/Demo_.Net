<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #IndexActive,
        #HomeActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        }  
    </style>
    <div class="container" id="content"> 
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 1" src="http://www.gravatar.com/avatar/0000000000000000000000000000000?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    11$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=11&amp;order%5Bdescription%5D=Pizza+1" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 2" src="http://www.gravatar.com/avatar/0000000000000000000000000000001?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    12$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=12&amp;order%5Bdescription%5D=Pizza+2" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 3" src="http://www.gravatar.com/avatar/0000000000000000000000000000002?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    13$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=13&amp;order%5Bdescription%5D=Pizza+3" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 4" src="http://www.gravatar.com/avatar/0000000000000000000000000000003?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    14$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=14&amp;order%5Bdescription%5D=Pizza+4" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 5" src="http://www.gravatar.com/avatar/0000000000000000000000000000004?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    15$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=15&amp;order%5Bdescription%5D=Pizza+5" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 6" src="http://www.gravatar.com/avatar/0000000000000000000000000000005?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    16$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=16&amp;order%5Bdescription%5D=Pizza+6" class="btn btn-small">Buy</a>
                </div>
            </div>
        </div>
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 7" src="http://www.gravatar.com/avatar/0000000000000000000000000000006?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    21$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=21&amp;order%5Bdescription%5D=Pizza+7" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 8" src="http://www.gravatar.com/avatar/0000000000000000000000000000007?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    22$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=22&amp;order%5Bdescription%5D=Pizza+8" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 9" src="http://www.gravatar.com/avatar/0000000000000000000000000000008?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    23$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=23&amp;order%5Bdescription%5D=Pizza+9" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 10" src="http://www.gravatar.com/avatar/0000000000000000000000000000009?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    24$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=24&amp;order%5Bdescription%5D=Pizza+10" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 11" src="http://www.gravatar.com/avatar/00000000000000000000000000000010?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    25$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=25&amp;order%5Bdescription%5D=Pizza+11" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 12" src="http://www.gravatar.com/avatar/00000000000000000000000000000011?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    26$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=26&amp;order%5Bdescription%5D=Pizza+12" class="btn btn-small">Buy</a>
                </div>
            </div>
        </div>
        <div class="row pizza-row">
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 13" src="http://www.gravatar.com/avatar/00000000000000000000000000000012?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    31$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=31&amp;order%5Bdescription%5D=Pizza+13" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 14" src="http://www.gravatar.com/avatar/00000000000000000000000000000013?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    32$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=32&amp;order%5Bdescription%5D=Pizza+14" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 15" src="http://www.gravatar.com/avatar/00000000000000000000000000000014?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    33$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=33&amp;order%5Bdescription%5D=Pizza+15" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 16" src="http://www.gravatar.com/avatar/00000000000000000000000000000015?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    34$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=34&amp;order%5Bdescription%5D=Pizza+16" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 17" src="http://www.gravatar.com/avatar/00000000000000000000000000000016?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                   35$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=35&amp;order%5Bdescription%5D=Pizza+17" class="btn btn-small">Buy</a>
                </div>
            </div>
            <div class="span2">
                <div class="image">
                    <img alt="Pizza 18" src="http://www.gravatar.com/avatar/00000000000000000000000000000017?d=identicon&amp;f=y" />
                </div>
                <div class="details">
                    36$ - <a href="../PlaceOrders/PlaceOrders?order%5Bamount%5D=36&amp;order%5Bdescription%5D=Pizza+18" class="btn btn-small">Buy</a>
                </div>
            </div>
        </div>
	    <br/>
        <br />
	    <div class="row">
		    <div class="span6 offset3">
		    <p>This is a sample application which showcases the new PayPal REST APIs. The App uses mock data to demonstrate how you can use the REST APIs for the following operations:</p>
		    <ul>
			    <li>Saving Credit Card information with PayPal for later use</li>
			    <li>Making Payments using a saved Credit Card</li>
			    <li>Making Payments using PayPal</li>
		    </ul>
		    </div>
	    </div>
    </div>
</asp:Content>
