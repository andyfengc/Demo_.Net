using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class DoExpressCheckoutForParallelPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Context.Request.QueryString["token"] != null)
                {
                    token.Text = Request.QueryString["token"];
                }
                if (Context.Request.QueryString["PayerID"] != null)
                {
                    payerId.Text = Request.QueryString["PayerID"];
                }
                if (Context.Request.QueryString["currencyCodeType"] != null)
                {
                    currencyCode.Text = Request.QueryString["currencyCodeType"];
                }
                if (Context.Request.QueryString["paymentType"] != null)
                {
                    paymentType.Text = Request.QueryString["paymentType"];
                }
            }
        }
    }
}
