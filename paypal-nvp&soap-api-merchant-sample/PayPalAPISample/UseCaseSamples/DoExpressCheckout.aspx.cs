using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Specialized;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class DoExpressCheckout : System.Web.UI.Page
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