using System;
using System.Web;
using System.Web.Security;

namespace PizzaApp.Account
{
    public partial class SignOut : System.Web.UI.Page
    {
        #region Event Handlers
        protected void Page_Init(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx?SignedIn=False");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}