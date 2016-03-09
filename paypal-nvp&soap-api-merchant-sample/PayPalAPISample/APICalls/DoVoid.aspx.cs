using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // Void an order or an authorization.
    public partial class DoVoid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            DoVoidRequestType request =
                new DoVoidRequestType();   
            // (Required) Original authorization ID specifying the authorization to void or, to void an order, the order ID.
            // Important: If you are voiding a transaction that has been reauthorized, use the ID from the original authorization, and not the reauthorization.
            request.AuthorizationID = authorizationId.Value;
            // (Optional) Informational note about this void that is displayed to the buyer in email and in their transaction history.
            if (note.Value != string.Empty)
            {
                request.Note = note.Value;                
            }

            // Invoke the API
            DoVoidReq wrapper = new DoVoidReq();
            wrapper.DoVoidRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();
            
            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoVoid method in service wrapper object  
            DoVoidResponseType doVoidResponse =
                    service.DoVoid(wrapper);


            // Check for API return status
            setKeyResponseObjects(service, doVoidResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoVoidResponseType doVoidResponse)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);

            CurrContext.Items.Add("Response_apiName", "DoVoid");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            if (doVoidResponse.Ack.Equals(AckCodeType.FAILURE) ||
                (doVoidResponse.Errors != null && doVoidResponse.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", doVoidResponse.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("Authorization Id", doVoidResponse.AuthorizationID);

                //Selenium Test Case
                responseParams.Add("Acknowledgement", doVoidResponse.Ack.ToString());

            }
            Server.Transfer("../APIResponse.aspx");

        }

    }
}
