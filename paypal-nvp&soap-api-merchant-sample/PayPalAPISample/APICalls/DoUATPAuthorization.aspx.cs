using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    public partial class DoUATPAuthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            DoUATPAuthorizationRequestType request = new DoUATPAuthorizationRequestType();
            request.UATPDetails = new UATPDetailsType();
            request.UATPDetails.UATPNumber = uatpNumber.Value;
            request.UATPDetails.ExpMonth = Convert.ToInt32(expMonth.Value);
            request.UATPDetails.ExpYear = Convert.ToInt32(expYear.Value);

            request.TransactionEntity = (TransactionEntityType)
                Enum.Parse(typeof(TransactionEntityType), transactionEntity.SelectedValue);

            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse( typeof(CurrencyCodeType), currencyID.Value);
            request.Amount = new BasicAmountType(currency, amount.Value);


            // Invoke the API
            DoUATPAuthorizationReq wrapper = new DoUATPAuthorizationReq();
            wrapper.DoUATPAuthorizationRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);
            DoUATPAuthorizationResponseType response = service.DoUATPAuthorization(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, response);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoUATPAuthorizationResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "DoUATPAuthorization");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("Correlation Id", response.CorrelationID);
            responseParams.Add("API Result", response.Ack.ToString());

            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("Transaction Id", response.AuthorizationCode);
                responseParams.Add("Payment status", response.AuthorizationInfo.PaymentStatus.ToString());
                if (response.AuthorizationInfo.PendingReason != null)
                {
                    responseParams.Add("Pending reason", response.AuthorizationInfo.PendingReason.ToString());
                }
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            Server.Transfer("../APIResponse.aspx");

        }
    }
}
