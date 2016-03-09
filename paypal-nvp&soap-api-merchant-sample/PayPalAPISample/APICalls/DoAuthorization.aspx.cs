using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // Authorize a payment.
    public partial class DoAuthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object

            DoAuthorizationRequestType request =
                new DoAuthorizationRequestType();

            // (Required) Value of the order's transaction identification number returned by PayPal.
            request.TransactionID = transactionId.Value;
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
            // (Required) Amount to authorize.
            request.Amount = new BasicAmountType(currency, amount.Value);

            // Invoke the API
            DoAuthorizationReq wrapper = new DoAuthorizationReq();
            wrapper.DoAuthorizationRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoAuthorization method in service wrapper object  
            DoAuthorizationResponseType doAuthorizationResponse =
                    service.DoAuthorization(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, doAuthorizationResponse);
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. You must process API response objects as applicable to your application
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoAuthorizationResponseType response)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("Transaction Id", response.TransactionID);
            responseParams.Add("Payment status", response.AuthorizationInfo.PaymentStatus.ToString());
            responseParams.Add("Pending reason", response.AuthorizationInfo.PendingReason.ToString());

            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "DoAuthorization");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);                
            }
            Server.Transfer("../APIResponse.aspx");
        }

    }
}
