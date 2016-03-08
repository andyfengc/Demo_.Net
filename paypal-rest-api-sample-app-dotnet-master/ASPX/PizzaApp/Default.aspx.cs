using System;
using System.Web;
using System.Web.UI;

namespace PizzaApp
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Event Handlers
        protected void Page_Init(Object sender, EventArgs e)
        {
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["SignedIn"] != null)
                    {
                        bool isSignedIn = Convert.ToBoolean(Request.QueryString["SignedIn"]);

                        if (isSignedIn)
                        {
                            divAlertMessage.Visible = true;
                            divAlertMessage.Attributes["class"] = "alert fade in alert-success";
                            LabelAlertMessage.Text = "Signed in successfully.";
                        }
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["SignedIn"] != null)
                    {
                        bool isSignedIn = Convert.ToBoolean(Request.QueryString["SignedIn"]);

                        if (!isSignedIn)
                        {
                            if(Request.UrlReferrer != null)
                            {
                                divAlertMessage.Visible = true;
                                divAlertMessage.Attributes["class"] = "alert fade in alert-success";
                                LabelAlertMessage.Text = "Signed out successfully.";
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
