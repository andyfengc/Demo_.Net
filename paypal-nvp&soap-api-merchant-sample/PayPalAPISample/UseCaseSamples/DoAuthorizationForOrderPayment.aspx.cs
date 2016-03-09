using System;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class DoAuthorizationForOrderPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Context.Request.QueryString["TransactionId"] != null)
                {
                    authID.Text = Request.QueryString["TransactionId"];
                }
            }
        }
    }
}