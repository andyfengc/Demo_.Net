using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The MassPay API operation makes a payment to one or more PayPal account holders.
    public partial class MassPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            MassPayRequestType request = new MassPayRequestType();
             ReceiverInfoCodeType receiverInfoType = (ReceiverInfoCodeType)
                Enum.Parse(typeof(ReceiverInfoCodeType), receiverType.SelectedValue);

             request.ReceiverType = receiverInfoType;
             // (Optional) The subject line of the email that PayPal sends when the transaction completes. The subject line is the same for all recipients.
            if (emailSubject.Value != string.Empty)
            {
                request.EmailSubject = emailSubject.Value;
            }


            // (Required) Details of each payment.
            // Note:
            // A single MassPayRequest can include up to 250 MassPayItems.
            MassPayRequestItemType massPayItem = new MassPayRequestItemType();
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
            massPayItem.Amount = new BasicAmountType(currency, amount.Value);

            // (Optional) How you identify the recipients of payments in this call to MassPay. It is one of the following values:
            // * EmailAddress
            // * UserID
            // * PhoneNumber
            if (receiverInfoType.Equals(ReceiverInfoCodeType.EMAILADDRESS) && emailId.Value != string.Empty)
            {
                massPayItem.ReceiverEmail = emailId.Value;
            }
            else if (receiverInfoType.Equals(ReceiverInfoCodeType.PHONENUMBER) && phoneNumber.Value != string.Empty)
            {
                massPayItem.ReceiverPhone = phoneNumber.Value;
            }
            else if (receiverInfoType.Equals(ReceiverInfoCodeType.USERID) && receiverId.Value != string.Empty)
            {
                massPayItem.ReceiverID = receiverId.Value;
            }

            if (note.Value != string.Empty)
            {
                massPayItem.Note = note.Value;
            }
            if (uniqueId.Value != string.Empty)
            {
                massPayItem.UniqueId = uniqueId.Value;
            }
            request.MassPayItem.Add(massPayItem);            

            // Invoke the API
            MassPayReq wrapper = new MassPayReq();
            wrapper.MassPayRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();
            
            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the MassPay method in service wrapper object  
            MassPayResponseType massPayResponse = service.MassPay(wrapper);

            // Check for API return status
            processResponse(service, massPayResponse);
        }

        private void processResponse(PayPalAPIInterfaceServiceService service, MassPayResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "MassPay");
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
            }

            if (!response.Ack.Equals(AckCodeType.FAILURE))
            {
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyParameters);
            Server.Transfer("../APIResponse.aspx");            
        }
    }
}
