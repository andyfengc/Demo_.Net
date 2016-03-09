using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    public partial class EnterBoarding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            EnterBoardingRequestType request = new EnterBoardingRequestType();
            EnterBoardingRequestDetailsType boardingDetails = new EnterBoardingRequestDetailsType();
            boardingDetails.ProductList = productList.Value;
            boardingDetails.ProgramCode = programCode.Value;
            boardingDetails.ImageUrl = imageUrl.Value;
            request.EnterBoardingRequestDetails = boardingDetails;

            // Invoke the API
            EnterBoardingReq wrapper = new EnterBoardingReq();
            wrapper.EnterBoardingRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);
            EnterBoardingResponseType enterBoardingResponse = service.EnterBoarding(wrapper);

            // Check for API return status
            processResponse(service, enterBoardingResponse);
        }

        private void processResponse(PayPalAPIInterfaceServiceService service, EnterBoardingResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "EnterBoarding");
            if (response.Token != null)
            {
                string baseUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString().ToLower();
                CurrContext.Items.Add("Response_redirectURL", baseUrl
                    + "_partner-onboard-flow&onboarding_token=" + response.Token);
            }
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> keyParameters = new Dictionary<string, string>();
            keyParameters.Add("Correlation Id", response.CorrelationID);
            keyParameters.Add("API Result", response.Ack.ToString());

            if (response.Errors != null && response.Errors.Count > 0)
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
            }

            if (!response.Ack.Equals(AckCodeType.FAILURE))
            {
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyParameters);
            Server.Transfer("../APIResponse.aspx");

        }
        
    }
}
