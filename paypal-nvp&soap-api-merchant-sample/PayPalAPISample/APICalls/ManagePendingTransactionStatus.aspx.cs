using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The ManagePendingTransactionStatus API operation accepts or denys a pending transaction held by Fraud Management Filters.
    public partial class ManagePendingTransactionStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            ManagePendingTransactionStatusRequestType request =
                new ManagePendingTransactionStatusRequestType();
            // (Required) The transaction ID of the payment transaction.
            request.TransactionID = transactionId.Value;
            // (Required) The operation you want to perform on the transaction. It is one of the following values:
            // * Accept – Accepts the payment
            // * Deny – Rejects the payment
            request.Action = (FMFPendingTransactionActionType)
                Enum.Parse(typeof(FMFPendingTransactionActionType), action.SelectedValue);

            // Invoke the API
            ManagePendingTransactionStatusReq wrapper = new ManagePendingTransactionStatusReq();
            wrapper.ManagePendingTransactionStatusRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the ManagePendingTransactionStatus method in service wrapper object  
            ManagePendingTransactionStatusResponseType manageProfileStatusResponse =
                    service.ManagePendingTransactionStatus(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, manageProfileStatusResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, ManagePendingTransactionStatusResponseType response)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);

            CurrContext.Items.Add("Response_apiName", "ManagePendingTransactionStatus");
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
                responseParams.Add("Transaction Id", response.TransactionID);
                responseParams.Add("Status", response.Status);
            }
            Server.Transfer("../APIResponse.aspx");

        }
    }
}
