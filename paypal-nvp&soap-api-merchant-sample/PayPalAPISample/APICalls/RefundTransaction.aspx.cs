using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The RefundTransaction API operation issues a refund to the PayPal account holder associated with a transaction.
    public partial class RefundTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            retryUntil.Text = calDate.SelectedDate.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            RefundTransactionRequestType request = new RefundTransactionRequestType();
            // (Required) Unique identifier of the transaction to be refunded.
            // Note: Either the transaction ID or the payer ID must be specified.
            request.TransactionID = transactionId.Value;
            // Type of refund you are making. It is one of the following values:
            // * Full – Full refund (default).
            // * Partial – Partial refund.
            // * ExternalDispute – External dispute. (Value available since version 82.0)
            // * Other – Other type of refund. (Value available since version 82.0)
            if (refundType.SelectedIndex != 0)
            {
                request.RefundType = (RefundType)
                    Enum.Parse(typeof(RefundType), refundType.SelectedValue);
                // (Optional) Refund amount. The amount is required if RefundType is Partial.
                // Note: If RefundType is Full, do not set the amount.
                if (request.RefundType.Equals(RefundType.PARTIAL) && refundAmount.Value != string.Empty)
                {
                    CurrencyCodeType currency = (CurrencyCodeType)
                        Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
                    request.Amount = new BasicAmountType(currency, refundAmount.Value);
                }
            }
            // (Optional) Custom memo about the refund.
            if (memo.Value != string.Empty)
            {
                request.Memo = memo.Value;
            }
            // (Optional) Maximum time until you must retry the refund.
            if (retryUntil.Text != string.Empty)
            {
                request.RetryUntil = retryUntil.Text;
            }
            // (Optional)Type of PayPal funding source (balance or eCheck) that can be used for auto refund. It is one of the following values:
            // * any – The merchant does not have a preference. Use any available funding source.
            // * default – Use the merchant's preferred funding source, as configured in the merchant's profile.
            // * instant – Use the merchant's balance as the funding source.
            // * eCheck – The merchant prefers using the eCheck funding source. If the merchant's PayPal balance can cover the refund amount, use the PayPal balance.
            // Note: This field does not apply to point-of-sale transactions.
            if (refundSource.SelectedIndex != 0)
            {
                request.RefundSource = (RefundSourceCodeType)
                    Enum.Parse(typeof(RefundSourceCodeType), refundSource.SelectedValue);
            }
            

            // Invoke the API
            RefundTransactionReq wrapper = new RefundTransactionReq();
            wrapper.RefundTransactionRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the RefundTransaction method in service wrapper object  
            RefundTransactionResponseType refundTransactionResponse = service.RefundTransaction(wrapper);

            // Check for API return status
            processResponse(service, refundTransactionResponse);
        }

        private void processResponse(PayPalAPIInterfaceServiceService service, RefundTransactionResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "RefundTransaction");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> keyParameters = new Dictionary<string, string>();
            keyParameters.Add("Correlation Id", response.CorrelationID);
            keyParameters.Add("API Result", response.Ack.ToString());

            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                keyParameters.Add("Refund transaction Id", response.RefundTransactionID);
                keyParameters.Add("Total refunded amount", 
                    response.TotalRefundedAmount.value + response.TotalRefundedAmount.currencyID);
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyParameters);
            Server.Transfer("../APIResponse.aspx");

        }

    }
}
