using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The GetTransactionDetails API operation obtains information about a specific transaction.
    public partial class GetTransactionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Search_Submit(object sender, EventArgs e)
        {
            // Create request object
            GetTransactionDetailsRequestType request = new GetTransactionDetailsRequestType();
            // (Required) Unique identifier of a transaction.
            // Note: The details for some kinds of transactions cannot be retrieved with GetTransactionDetails. You cannot obtain details of bank transfer withdrawals, for example.
            request.TransactionID = transactionId.Value;            

            // Invoke the API
            GetTransactionDetailsReq wrapper = new GetTransactionDetailsReq();
            wrapper.GetTransactionDetailsRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the GetTransactionDetails method in service wrapper object  
            GetTransactionDetailsResponseType transactionDetails = service.GetTransactionDetails(wrapper);

            // Check for API return status
            processResponse(service, transactionDetails);
        }

        private void processResponse(PayPalAPIInterfaceServiceService service, GetTransactionDetailsResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "GetTransactionDetails");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> keyResponseParameters = new Dictionary<string, string>();
            keyResponseParameters.Add("Correlation Id", response.CorrelationID);
            keyResponseParameters.Add("API Result", response.Ack.ToString());

            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                PaymentTransactionType transactionDetails = response.PaymentTransactionDetails;
                keyResponseParameters.Add("Payment receiver", transactionDetails.ReceiverInfo.Receiver);
                keyResponseParameters.Add("Payer", transactionDetails.PayerInfo.Payer);
                keyResponseParameters.Add("Payment date", transactionDetails.PaymentInfo.PaymentDate);
                keyResponseParameters.Add("Payment status", transactionDetails.PaymentInfo.PaymentStatus.ToString());
                keyResponseParameters.Add("Gross amount",
                    transactionDetails.PaymentInfo.GrossAmount.value + transactionDetails.PaymentInfo.GrossAmount.currencyID.ToString());

                if (transactionDetails.PaymentInfo.SettleAmount != null)
                {
                    keyResponseParameters.Add("Settlement amount", 
                        transactionDetails.PaymentInfo.SettleAmount.value + transactionDetails.PaymentInfo.SettleAmount.currencyID.ToString());
                }
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            Server.Transfer("../APIResponse.aspx");

        }
    }
}
