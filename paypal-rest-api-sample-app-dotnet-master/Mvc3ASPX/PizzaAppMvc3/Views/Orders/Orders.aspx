<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<PizzaAppMvc3.Order>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/StyleOrders.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #OrdersActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        } 
    </style>
    <script src="<%:Url.Content("~/Scripts/webGrid.js")%>" type="text/javascript"></script>
    <div class="container">
    <h2>Orders</h2>
    <br />  
    <%
    if (Model != null) 
    {
     var grid = new WebGrid(Model); %>
        <%: grid.GetHtml(columns: grid.Columns
            (
                grid.Column("OrderID", header: "Order ID", format: m => Convert.ToInt32(m.OrderID)),
                grid.Column("UserID", header: "User ID", format: m => Convert.ToInt32(m.UserID)),
                grid.Column("PaymentID", header: "Payment ID", format: m => m.PaymentID.ToString()),
                grid.Column("State", header: "State", format: m => m.State.ToString()),                            
                grid.Column("AmountInUSD", header: "Amount In USD", format: m => m.AmountInUSD.ToString()),
                grid.Column("Description", header: "Description", format: m => m.Description.ToString()),
                grid.Column("CreatedDateTime", header: "Created Date Time", format: m => Convert.ToDateTime(m.CreatedDateTime)),                            
                grid.Column("UpdatedDateTime", header: "Updated Date Time", format: m => Convert.ToDateTime(m.UpdatedDateTime))
            )
        ) %> 
        <%
    }
    else
    {%> 
        <p>Your orders are empty.</p>
    <%}%>
    </div> 
</asp:Content>
