using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The GetBillingAgreementCustomerDetails API operation obtains information about a billing agreement's PayPal account holder.
    public partial class GetBillingAgreementCustomerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["token"] != null && token.Value == string.Empty)
            {
                token.Value = Request.Params["token"];
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            GetBillingAgreementCustomerDetailsRequestType request = new GetBillingAgreementCustomerDetailsRequestType();
            // (Required) The time-stamped token returned in the SetCustomerBillingAgreement response.
            // Note: The token expires after 3 hours.
            request.Token = token.Value;

            // Invoke the API
            GetBillingAgreementCustomerDetailsReq wrapper = new GetBillingAgreementCustomerDetailsReq();
            wrapper.GetBillingAgreementCustomerDetailsRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();
            
            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the GetBillingAgreementCustomerDetails method in service wrapper object  
            GetBillingAgreementCustomerDetailsResponseType getBillingAgreementCustomerDetailsResponse =
                    service.GetBillingAgreementCustomerDetails(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, getBillingAgreementCustomerDetailsResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, GetBillingAgreementCustomerDetailsResponseType response)
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
                keyResponseParameters.Add("Payer", response.GetBillingAgreementCustomerDetailsResponseDetails.PayerInfo.Payer);
                AddressType billingAddr = response.GetBillingAgreementCustomerDetailsResponseDetails.PayerInfo.Address;
                if (billingAddr != null)
                {
                    if (billingAddr.AddressOwner != null)
                    {
                        keyResponseParameters.Add("Billing address - Owner", billingAddr.AddressOwner.ToString());
                    }
                    keyResponseParameters.Add("Billing address - Street 1", billingAddr.Street1);
                    keyResponseParameters.Add("Billing address - City", billingAddr.CityName);
                    keyResponseParameters.Add("Billing address - State", billingAddr.StateOrProvince);
                    if (billingAddr.Country != null)
                    {
                        keyResponseParameters.Add("Billing address - Country", billingAddr.Country.ToString());
                    }
                }
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            CurrContext.Items.Add("Response_apiName", "GetBillingAgreementCustomerDetails");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            CurrContext.Items.Add("Response_redirectURL", null);
            Server.Transfer("../APIResponse.aspx");
            
        }

    }
}
