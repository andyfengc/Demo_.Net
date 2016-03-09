using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    public partial class CreateBillingAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            CreateBillingAgreementRequestType request = new CreateBillingAgreementRequestType();
            // (Required) The time-stamped token returned in the SetCustomerBillingAgreement response.
            // Note: The token expires after 3 hours.
            request.Token = token.Value;

            // Invoke the API
            CreateBillingAgreementReq wrapper = new CreateBillingAgreementReq();
            wrapper.CreateBillingAgreementRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the CreateBillingAgreement method in service wrapper object 
            CreateBillingAgreementResponseType billingAgreementResponse =
                    service.CreateBillingAgreement(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, billingAgreementResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, CreateBillingAgreementResponseType response)
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
                keyResponseParameters.Add("Billing Agreement ID", response.BillingAgreementID);                
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            CurrContext.Items.Add("Response_apiName", "CreateCustomerBilling");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            CurrContext.Items.Add("Response_redirectURL", null);
            Server.Transfer("../APIResponse.aspx");
            
        }

    }
}
