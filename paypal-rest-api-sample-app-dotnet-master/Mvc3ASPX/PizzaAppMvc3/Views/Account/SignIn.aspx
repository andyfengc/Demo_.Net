<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PizzaAppMvc3.SignInModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PizzaShop
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../Styles/StyleValidation.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #SignInActive
        {
            color: #555555;
            text-decoration: none;
            box-shadow: inset 0px 3px 8px rgba(0,0,0,0.125);
            background-color: rgb(229, 229, 229);
        }
    </style>
    <div class="container">
        <h2>SignIn</h2>
        <p>Sign in with your PizzaShop account. Don't have an account yet? <%: Html.ActionLink("SignUp", "SignUp") %> for one.</p>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
        <% using (Html.BeginForm()) { %>
            <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
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
                
                <div class="checkbox">
                    <%: Html.CheckBoxFor(m => m.Remember) %>
                    <%: Html.LabelFor(m => m.Remember) %>
                </div>                
                <p>
                    <input type="submit" value="Sign in" class="btn btn btn-primary" />
                </p>
            </div>
        <% } %>
    </div>
</asp:Content>
