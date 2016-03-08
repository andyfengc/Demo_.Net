<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PizzaAppMvc3.ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #ProfileActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        }  
    </style>
    <div class="container">
    <h2>Edit Profile</h2>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Profile change was unsuccessful. Please correct the errors and try again.") %>
        <div>  
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.Email) %>
            </div>
            <div class="controls">
                <%: Html.TextBoxFor(m => m.Email, new { @readonly = "readonly" }) %>
            </div>
            <div class="password required control-label">
                <%: Html.LabelFor(m => m.CurrentPassword) %>
            </div>
            <div class="string password required">
                <%: Html.PasswordFor(m => m.CurrentPassword) %>
                <%: Html.ValidationMessageFor(m => m.CurrentPassword) %>
            </div>                
            <div class="password required control-label">
                <%: Html.LabelFor(m => m.NewPassword) %>
            </div>
            <div class="string password required">
                <%: Html.PasswordFor(m => m.NewPassword) %>
                <%: Html.ValidationMessageFor(m => m.NewPassword) %>
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
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.CurrentCreditCardNumber) %>
            </div>
            <div class="controls">
                <%: Html.TextBoxFor(m => m.CurrentCreditCardNumber, new { @readonly = "readonly" }) %>
            </div>
                <div class="string required control-label">
                <%: Html.LabelFor(m => m.NewCreditCardNumber) %>
            </div>
            <div class="controls">
                <%: Html.TextBoxFor(m => m.NewCreditCardNumber) %>
                <%: Html.ValidationMessageFor(m => m.NewCreditCardNumber) %>
            </div>
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.NewCreditCardTypes) %>
            </div>
            <div class="controls"> 
                <%: Html.DropDownListFor(m => m.NewCreditCardType, new SelectList(Model.NewCreditCardTypes, "Value", "Text")) %>  
                <%: Html.ValidationMessageFor(m => m.NewCreditCardType) %>  
            </div>
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.NewCreditCardCVV2) %>
            </div>
            <div class="controls">
                <%: Html.TextBoxFor(m => m.NewCreditCardCVV2) %>
                <%: Html.ValidationMessageFor(m => m.NewCreditCardCVV2) %>
            </div>             
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.NewCreditCardExpireMonths) %>
            </div>
            <div class="controls"> 
                <%: Html.DropDownListFor(m => m.NewCreditCardExpireMonth, new SelectList(Model.NewCreditCardExpireMonths, "Value", "Text")) %>  
                <%: Html.ValidationMessageFor(m => m.NewCreditCardExpireMonth) %> 
            </div>
            <div class="string optional control-label">
                <%: Html.LabelFor(m => m.NewCreditCardExpireYears) %>
            </div>
            <div class="controls"> 
                <%: Html.DropDownListFor(m => m.NewCreditCardExpireYear, new SelectList(Model.NewCreditCardExpireYears, "Value", "Text")) %> 
                <%: Html.ValidationMessageFor(m => m.NewCreditCardExpireYear) %>   
            </div>
            <p>
                <input type="submit" value="Update" class="btn btn btn-primary" />
            </p>
        </div>
        <% } %>
    </div>
</asp:Content>
