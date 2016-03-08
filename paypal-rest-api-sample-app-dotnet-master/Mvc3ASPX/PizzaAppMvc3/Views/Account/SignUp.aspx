<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PizzaAppMvc3.SignUpModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #SignUpActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        }
    </style>
    <div class="container">
        <h3>SignUp</h3>
        <p>Sign up for a new PizzaShop account.</p>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
        <% using (Html.BeginForm()) { %>
            <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
            <div>                             
                <div class="email required control-label">
                    <%: Html.LabelFor(m => m.Email) %>
                </div>
                <div class="string email required">
                    <%: Html.TextBoxFor(m => m.Email, new { placeholder = "dummy@email.com" }) %>
                    <%: Html.ValidationMessageFor(m => m.Email) %>
                </div>                
                <div class="password required control-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="string password required">
                    <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>                
                <div class="password required control-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="string password required">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                <h4>Add Credit Card</h4>
                <p>Your credit card information is stored safely with PayPal.</p>
                <div class="string required control-label">
                    <%: Html.LabelFor(m => m.CreditCardNumber) %>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(m => m.CreditCardNumber) %>
                    <%: Html.ValidationMessageFor(m => m.CreditCardNumber) %>
                </div>
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.CreditCardTypes) %>
                </div>
                <div class="controls"> 
                    <%: Html.DropDownListFor(m => m.CreditCardType, new SelectList(Model.CreditCardTypes, "Value", "Text")) %>   
                    <%: Html.ValidationMessageFor(m => m.CreditCardType) %> 
                </div>
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.CreditCardCVV2) %>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(m => m.CreditCardCVV2) %>
                    <%: Html.ValidationMessageFor(m => m.CreditCardCVV2) %>
                </div>             
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.CreditCardExpireMonths) %>                   
                </div>
                <div class="controls"> 
                        <%: Html.DropDownListFor(m => m.CreditCardExpireMonth, new SelectList(Model.CreditCardExpireMonths, "Value", "Text")) %>  
                        <%: Html.ValidationMessageFor(m => m.CreditCardExpireMonth) %> 
                </div>
                <div class="string optional control-label">
                    <%: Html.LabelFor(m => m.CreditCardExpireYears) %>
                </div>
                <div class="controls"> 
                        <%: Html.DropDownListFor(m => m.CreditCardExpireYear, new SelectList(Model.CreditCardExpireYears, "Value", "Text")) %>  
                        <%: Html.ValidationMessageFor(m => m.CreditCardExpireYear) %> 
                </div>
                <p>
                    <input type="submit" value="Sign up" class="btn btn btn-primary" />
                </p>
            </div>
        <% } %>
    </div>
</asp:Content>
