using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    public partial class BAUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            BAUpdateRequestType request = new BAUpdateRequestType();            
            request.ReferenceID = referenceId.Value;
            if (billingAgreementStatus.SelectedIndex != 0)
            {
                request.BillingAgreementStatus = (MerchantPullStatusCodeType)
                    Enum.Parse(typeof(MerchantPullStatusCodeType), billingAgreementStatus.SelectedValue);
            }
            if (billingAgreementText.Value != string.Empty)
            {
                request.BillingAgreementDescription = billingAgreementText.Value;
            }

            // Invoke the API
            BillAgreementUpdateReq wrapper = new BillAgreementUpdateReq();
            wrapper.BAUpdateRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the BillAgreementUpdate method in service wrapper object  
            BAUpdateResponseType billingAgreementResponse =
                    service.BillAgreementUpdate(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, billingAgreementResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, BAUpdateResponseType response)
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
                keyResponseParameters.Add("Billing Agreement Status", response.BAUpdateResponseDetails.BillingAgreementStatus.ToString());
                keyResponseParameters.Add("Billing Agreement description", response.BAUpdateResponseDetails.BillingAgreementDescription);
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            CurrContext.Items.Add("Response_apiName", "BillAgreementUpdate");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            CurrContext.Items.Add("Response_redirectURL", null);
            Server.Transfer("../APIResponse.aspx");

        }

    }
}
