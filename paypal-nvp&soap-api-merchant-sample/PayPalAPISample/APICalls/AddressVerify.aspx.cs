using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

// The AddressVerify API operation confirms whether a postal address and postal code match those of the specified PayPal account holder.
namespace PayPalAPISample.APICalls
{
    public partial class AddressVerify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            AddressVerifyRequestType request = new AddressVerifyRequestType();

            // (Required) Email address of a PayPal member to verify.
            request.Email = email.Value;

            // (Required) First line of the billing or shipping postal address to verify. To pass verification, the value of Street must match the first 3 single-byte characters of a postal address on file for the PayPal member.
            request.Street = street.Value;

            // (Required) Postal code to verify. To pass verification, the value of Zip must match the first 5 single-byte characters of the postal code of the verified postal address for the verified PayPal member.
            request.Zip = zip.Value;

            // Invoke the API
            AddressVerifyReq wrapper = new AddressVerifyReq();
            wrapper.AddressVerifyRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the AddressVerify method in service wrapper object  
            AddressVerifyResponseType addressVerifyResponse = service.AddressVerify(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, addressVerifyResponse);
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. 
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, AddressVerifyResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "AddressVerify");
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
                keyResponseParameters.Add("Address confirmation code", response.ConfirmationCode.ToString());
                keyResponseParameters.Add("Street address match", response.StreetMatch.ToString());
                keyResponseParameters.Add("Zip code match", response.ZipMatch.ToString());
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            Server.Transfer("../APIResponse.aspx");
        }
    }
}
