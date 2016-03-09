using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

// The BillOutstandingAmount API operation bills the buyer for the outstanding balance associated with a recurring payments profile.
namespace PayPalAPISample.APICalls
{
    public partial class BillOutstandingAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object

            BillOutstandingAmountRequestType request =
                new BillOutstandingAmountRequestType();

            BillOutstandingAmountRequestDetailsType details =
                new BillOutstandingAmountRequestDetailsType();
            request.BillOutstandingAmountRequestDetails = details;
            // (Required) Recurring payments profile ID returned in the CreateRecurringPaymentsProfile response.
            // Note:The profile must have a status of either Active or Suspended.
            details.ProfileID = profileId.Value;

            // (Optional) The amount to bill. The amount must be less than or equal to the current outstanding balance of the profile. If no value is specified, PayPal attempts to bill the entire outstanding balance amount.
            if (currencyCode.SelectedIndex != 0 && amount.Value != string.Empty)
            {
                CurrencyCodeType currency = (CurrencyCodeType)
                    Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
                details.Amount = new BasicAmountType(currency, amount.Value);
            }
            // (Optional) The reason for the non-scheduled payment. For profiles created using Express Checkout, this message is included in the email notification to the buyer for the non-scheduled payment transaction, and can also be seen by both you and the buyer on the Status History page of the PayPal account.
            if (note.Value != string.Empty)
            {
                details.Note = note.Value;
            }

            // Invoke the API
            BillOutstandingAmountReq wrapper = new BillOutstandingAmountReq();
            wrapper.BillOutstandingAmountRequest = request;
            // Create the PayPalAPIInterfaceServiceService service object to make the API call

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the BillOutstandingAmount method in service wrapper object  
            BillOutstandingAmountResponseType response =
                    service.BillOutstandingAmount(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, response);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, BillOutstandingAmountResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "BillOutstandingAmount");
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
                keyResponseParameters.Add("Profile ID", response.BillOutstandingAmountResponseDetails.ProfileID);
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            Server.Transfer("../APIResponse.aspx");
            
        }
    }
}
