using System;
using System.Web;
using System.Collections.Generic;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class PaymentCapture : System.Web.UI.Page
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