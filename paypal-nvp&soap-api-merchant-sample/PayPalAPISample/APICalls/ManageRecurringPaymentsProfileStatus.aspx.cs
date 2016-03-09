using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The ManageRecurringPaymentsProfileStatus API operation cancels, suspends, or reactivates a recurring payments profile.
    public partial class ManageRecurringPaymentsProfileStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            
            ManageRecurringPaymentsProfileStatusRequestType request =
                new ManageRecurringPaymentsProfileStatusRequestType();
            ManageRecurringPaymentsProfileStatusRequestDetailsType details =
                new ManageRecurringPaymentsProfileStatusRequestDetailsType();
            request.ManageRecurringPaymentsProfileStatusRequestDetails = details;
            // (Required) Recurring payments profile ID returned in the CreateRecurringPaymentsProfile response.
            details.ProfileID = profileId.Value;
            // (Required) The action to be performed to the recurring payments profile. Must be one of the following:
            // * Cancel – Only profiles in Active or Suspended state can be canceled.
            // * Suspend – Only profiles in Active state can be suspended.
            // * Reactivate – Only profiles in a suspended state can be reactivated.
            details.Action = (StatusChangeActionType)
                Enum.Parse(typeof(StatusChangeActionType), action.SelectedValue);
            // (Optional) The reason for the change in status. For profiles created using Express Checkout, this message is included in the email notification to the buyer when the status of the profile is successfully changed, and can also be seen by both you and the buyer on the Status History page of the PayPal account.
            if (note.Value != string.Empty)
            {
                details.Note = note.Value;
            }

            // Invoke the API
            ManageRecurringPaymentsProfileStatusReq wrapper = new ManageRecurringPaymentsProfileStatusReq();
            wrapper.ManageRecurringPaymentsProfileStatusRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();
            
            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the ManageRecurringPaymentsProfileStatus method in service wrapper object  
            ManageRecurringPaymentsProfileStatusResponseType manageProfileStatusResponse =
                    service.ManageRecurringPaymentsProfileStatus(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, manageProfileStatusResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, ManageRecurringPaymentsProfileStatusResponseType response)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("API Status", response.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_redirectURL", null);
            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("Profile Id", response.ManageRecurringPaymentsProfileStatusResponseDetails.ProfileID);                
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "ManageRecurringPaymentsProfileStatus");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            Server.Transfer("../APIResponse.aspx");
        }

    }
}