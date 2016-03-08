<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PizzaAppMvc3.PlaceOrdersModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #OrdersActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        } 
    </style>
    <div class="container">
        <h2>Place Order</h2>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
        <% using (Html.BeginForm()) { %>
            <div>            
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.Amount) %>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(m => m.Amount, new { @readonly = "readonly" }) %>
                </div>
                
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.Description) %>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(m => m.Description, new { @readonly = "readonly" }) %>
                </div>
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.PaymentTypes) %>
                </div>
                <div class="controls"> 
                    <%: Html.DropDownListFor(m => m.PaymentType, new SelectList(Model.PaymentTypes, "Value", "Text")) %>    
                </div>          
                <p>
                    <input type="submit" value="Place order" class="btn btn btn-primary" />
                </p>
            </div>
        <% } %>
    </div>
</asp:Content>
