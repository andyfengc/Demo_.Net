using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The SetCustomerBillingAgreement API operation initiates the creation of a billing agreement.
    // Important: If you are using Express Checkout with version 54.0 or later of the API, do not use the SetCustomerBillingAgreement API operation to set up a billing agreement. Use the SetExpressCheckout API operation instead.
    public partial class SetCustomerBillingAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string requestUrl = Request.Url.OriginalString;
            //string authority = Request.Url.Authority;
            //string dnsSafeHost = Request.Url.DnsSafeHost;

            //if (Request.UrlReferrer != null && Request.UrlReferrer.Scheme == "https")
            //{
            //    requestUrl = requestUrl.Replace("http://", "https://");
            //    requestUrl = requestUrl.Replace(authority, dnsSafeHost);
            //}

            string requestUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();

            UriBuilder uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = Request.ApplicationPath
                + (Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "APICalls/GetBillingAgreementCustomerDetails.aspx";
            returnUrl.Value = uriBuilder.Uri.ToString();

            uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = Request.ApplicationPath
                + (Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "APICalls/SetCustomerBillingAgreement.aspx";
            cancelUrl.Value = uriBuilder.Uri.ToString();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            SetCustomerBillingAgreementRequestType request = new SetCustomerBillingAgreementRequestType();
           
            SetCustomerBillingAgreementRequestDetailsType requestDetails = new SetCustomerBillingAgreementRequestDetailsType();

            // (Optional) Email address of the buyer as entered during checkout. PayPal uses this value to pre-fill the PayPal membership sign-up portion of the PayPal login page.
            requestDetails.BuyerEmail = buyerEmail.Value;
            // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal.
            // Note: PayPal recommends that the value be the final review page on which the buyer confirms the billing agreement.
            requestDetails.ReturnURL = returnUrl.Value;
            // (Required) URL to which the customer is returned if he does not approve the use of PayPal to pay you.
            // Note: PayPal recommends that the value be the original page on which the customer chose to pay with PayPal or establish a billing agreement.
            requestDetails.CancelURL = cancelUrl.Value;

            // (Required) Details of the billing agreement such as the billing type, billing agreement description, and payment type.
            BillingAgreementDetailsType baDetails = new BillingAgreementDetailsType();
            // Description of goods or services associated with the billing agreement. This field is required for each recurring payment billing agreement. PayPal recommends that the description contain a brief summary of the billing agreement terms and conditions. For example, buyer is billed at "9.99 per month for 2 years."
            baDetails.BillingAgreementDescription = billingAgreementText.Value;
            // (Required) Type of billing agreement. For recurring payments, this field must be set to RecurringPayments. In this case, you can specify up to ten billing agreements. Other defined values are not valid.
            // Type of billing agreement for reference transactions. You must have permission from PayPal to use this field. This field must be set to one of the following values:
            // * MerchantInitiatedBilling- PayPal creates a billing agreement for each transaction associated with buyer. You must specify version 54.0 or higher to use this option.
            // * MerchantInitiatedBillingSingleAgreement- PayPal creates a single billing agreement for all transactions associated with buyer. Use this value unless you need per-transaction billing agreements. You must specify version 58.0 or higher to use this option.
            baDetails.BillingType = (BillingCodeType)
                Enum.Parse( typeof(BillingCodeType), billingType.SelectedValue);
            requestDetails.BillingAgreementDetails = baDetails;
            request.SetCustomerBillingAgreementRequestDetails = requestDetails;

            // Invoke the API
            SetCustomerBillingAgreementReq wrapper = new SetCustomerBillingAgreementReq();
            wrapper.SetCustomerBillingAgreementRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the SetCustomerBillingAgreement method in service wrapper object  
            SetCustomerBillingAgreementResponseType setCustomerBillingAgreementResponse =
                    service.SetCustomerBillingAgreement(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, setCustomerBillingAgreementResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, SetCustomerBillingAgreementResponseType response)
        {
            Dictionary<string, string> keyResponseParameters = new Dictionary<string, string>();
            keyResponseParameters.Add("API Status", response.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                keyResponseParameters.Add("Token", response.Token);
                string baseUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString();
                CurrContext.Items.Add("Response_redirectURL", baseUrl
                    + "_customer-billing-agreement&token=" + response.Token);
            }            
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            CurrContext.Items.Add("Response_apiName", "SetCustomerBillingAgreement");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            Server.Transfer("../APIResponse.aspx");
        }

    }
}
