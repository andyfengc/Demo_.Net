using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // Authorize a payment.
    public partial class DoReauthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object

            DoReauthorizationRequestType request =
                new DoReauthorizationRequestType();

            // (Required) Value of a previously authorized transaction identification number returned by PayPal.
            request.AuthorizationID = authorizationId.Value;

            // (Required) Amount to reauthorize.
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
            request.Amount = new BasicAmountType(currency, amount.Value);

            // Invoke the API
            DoReauthorizationReq wrapper = new DoReauthorizationReq();
            wrapper.DoReauthorizationRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoReauthorization method in service wrapper object  
            DoReauthorizationResponseType doReauthorizationResponse =
                    service.DoReauthorization(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, doReauthorizationResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoReauthorizationResponseType response)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("Correlation Id", response.CorrelationID);
            responseParams.Add("API Result", response.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "DoReauthorization");
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
                responseParams.Add("Authorization ID", response.AuthorizationID);
                responseParams.Add("Payment status", response.AuthorizationInfo.PaymentStatus.ToString());
            }
            Server.Transfer("../APIResponse.aspx");

        }

    }
}
