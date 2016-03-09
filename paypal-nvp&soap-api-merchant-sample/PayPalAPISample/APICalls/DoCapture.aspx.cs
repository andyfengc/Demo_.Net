using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // Captures an authorized payment.
    public partial class DoCapture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object

            DoCaptureRequestType request =
                new DoCaptureRequestType();

            // (Required) Authorization identification number of the payment you want to capture. This is the transaction ID returned from DoExpressCheckoutPayment, DoDirectPayment, or CheckOut. For point-of-sale transactions, this is the transaction ID returned by the CheckOut call when the payment action is Authorization.
            request.AuthorizationID = authorizationId.Value;

            // (Required) Amount to capture.
            // Note: You must set the currencyID attribute to one of the three-character currency codes for any of the supported PayPal currencies.
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
            request.Amount = new BasicAmountType(currency, amount.Value);

            // (Required) Indicates whether or not this is your last capture. It is one of the following values:
            // * Complete – This is the last capture you intend to make.
            // * NotComplete – You intend to make additional captures.
            if (completeType.SelectedIndex != 0)
            {
                request.CompleteType = (CompleteCodeType)
                    Enum.Parse(typeof(CompleteCodeType), completeType.SelectedValue);
            }
            // (Optional) An informational note about this settlement that is displayed to the buyer in email and in their transaction history.
            if (note.Value != string.Empty)
            {
                request.Note = note.Value;
            }
            // (Optional) Your invoice number or other identification number that is displayed to you and to the buyer in their transaction history. The value is recorded only if the authorization you are capturing is an Express Checkout order authorization.
            // Note: This value on DoCapture overwrites a value previously set on DoAuthorization.
            if (invoiceId.Value != string.Empty)
            {
                request.InvoiceID = invoiceId.Value;
            }
            // (Optional) Per transaction description of the payment that is passed to the buyer's credit card statement.
            //If you provide a value in this field, the full descriptor displayed on the buyer's statement has the following format:
            //<PP * | PAYPAL *><Merchant descriptor as set in the Payment Receiving Preferences><1 space><soft descriptor>
            //Character length and limitations: The soft descriptor can contain only the following characters:
            //Alphanumeric characters
            //- (dash)
            //* (asterisk)
            //. (period)
            //{space}
            //If you pass any other characters (such as ","), PayPal returns an error code.
            //The soft descriptor does not include the phone number, which can be toggled between your customer service number and PayPal's Customer Service number.
            //The maximum length of the soft descriptor is 22 characters. Of this, the PayPal prefix uses either 4 or 8 characters of the data format. Thus, the maximum length of the soft descriptor information that you can pass in this field is:
            //22 - len(<PP * | PAYPAL *>) - len(<Descriptor set in Payment Receiving Preferences> + 1)
            //For example, assume the following conditions:
            //The PayPal prefix toggle is set to PAYPAL * in PayPal's administration tools.
            //The merchant descriptor set in the Payment Receiving Preferences is set to EBAY.
            //The soft descriptor is passed in as JanesFlowerGifts LLC.
            //The resulting descriptor string on the credit card is:
            //PAYPAL *EBAY JanesFlow
            if (softDescriptor.Value != string.Empty)
            {
                request.Descriptor = softDescriptor.Value;
            }

            // Invoke the API
            DoCaptureReq wrapper = new DoCaptureReq();
            wrapper.DoCaptureRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoCapture method in service wrapper object  
            DoCaptureResponseType doCaptureResponse =
                    service.DoCapture(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, doCaptureResponse);
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. You must process API response objects as applicable to your application
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoCaptureResponseType doCaptureResponse)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);

            CurrContext.Items.Add("Response_apiName", "DoCapture");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            if (doCaptureResponse.Ack.Equals(AckCodeType.FAILURE) ||
                (doCaptureResponse.Errors != null && doCaptureResponse.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", doCaptureResponse.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("Transaction Id", doCaptureResponse.DoCaptureResponseDetails.PaymentInfo.TransactionID);
                responseParams.Add("Payment status", doCaptureResponse.DoCaptureResponseDetails.PaymentInfo.PaymentStatus.ToString());
                responseParams.Add("Pending reason", doCaptureResponse.DoCaptureResponseDetails.PaymentInfo.PendingReason.ToString());
            }
            Server.Transfer("../APIResponse.aspx");
        }

    }
}
