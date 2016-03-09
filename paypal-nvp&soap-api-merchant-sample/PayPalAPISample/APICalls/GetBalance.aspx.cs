using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The GetBalance API Operation obtains the available balance for a PayPal account.
    public partial class GetBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            GetBalanceRequestType request = new GetBalanceRequestType();
            // (Optional) Indicates whether to return all currencies. It is one of the following values:
            // * 0 – Return only the balance for the primary currency holding.
            // * 1 – Return the balance for each currency holding.
            // Note: This field is available since version 51. Prior versions return only the balance for the primary currency holding.
            request.ReturnAllCurrencies = returnAllCurrencies.SelectedValue;

            // Invoke the API
            GetBalanceReq wrapper = new GetBalanceReq();
            wrapper.GetBalanceRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the GetBalance method in service wrapper object  
            GetBalanceResponseType getBalanceResponse = service.GetBalance(wrapper);

            // Check for API return status
            processResponse(service, getBalanceResponse);
        }

        private void processResponse(PayPalAPIInterfaceServiceService service, GetBalanceResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "GetBalance");
            CurrContext.Items.Add("Response_redirectURL", null);
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
                keyParameters.Add("Balance reported at", response.BalanceTimeStamp);
                keyParameters.Add("Balance holding in primary currency",
                    response.Balance.value + response.Balance.currencyID.ToString());
                if (response.BalanceHoldings != null && response.BalanceHoldings.Count > 0)
                {
                    for (int i = 0; i < response.BalanceHoldings.Count; i++)
                    {
                        keyParameters.Add("Balance holding" + (i + 1),
                            response.BalanceHoldings[i].value + response.BalanceHoldings[i].currencyID.ToString());
                    }
                }
            }

            if (!response.Ack.Equals(AckCodeType.FAILURE))
            {
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyParameters);
            Server.Transfer("../APIResponse.aspx"); 
        }
    }
}
