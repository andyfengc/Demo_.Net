using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The DoNonReferencedCredit API issues a credit to a card not referenced by the original transaction.
    public partial class DoNonReferencedCredit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.cardExpiryDate.Text = DateTime.Now.AddYears(2).ToString("MM/yyyy");
            }
        }

        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            cardExpiryDate.Text = calDate.SelectedDate.ToString("MM/yyyy");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            DoNonReferencedCreditRequestType request = new DoNonReferencedCreditRequestType();
            request.DoNonReferencedCreditRequestDetails = new DoNonReferencedCreditRequestDetailsType();

            // (Required) Total of order, including shipping, handling, and tax. Amount = NetAmount + ShippingAmount + TaxAmount
            // Character length and limitations: Must not exceed $10,000 USD in any currency. No currency symbol. Must have 2 decimal places, decimal separator must be a period (.), and the optional thousands separator must be a comma (,).
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse( typeof(CurrencyCodeType), currencyId.Value);
            request.DoNonReferencedCreditRequestDetails.Amount = new BasicAmountType(currency, itemAmount.Value);
            // (Required) Information about the credit card to be charged.
            CreditCardDetailsType creditCard = new CreditCardDetailsType();
            request.DoNonReferencedCreditRequestDetails.CreditCard = creditCard;
            // (Required) Credit card number.
            creditCard.CreditCardNumber = creditCardNumber.Value;
            // (Optional) Type of credit card. For UK, only Maestro, MasterCard, Discover, and Visa are allowable. For Canada, only MasterCard and Visa are allowable and Interac debit cards are not supported. It is one of the following values:
            // * Visa
            // * MasterCard
            // * Discover
            // * Amex
            // * Maestro: See note.
            // Note: If the credit card type is Maestro, you must set currencyId to GBP. In addition, you must specify either StartMonth and StartYear or IssueNumber.
            creditCard.CreditCardType = (CreditCardTypeType)
                Enum.Parse(typeof(CreditCardTypeType), creditCardType.SelectedValue);
            // Card Verification Value, version 2. Your Merchant Account settings determine whether this field is required. To comply with credit card processing regulations, you must not store this value after a transaction has been completed.
            creditCard.CVV2 = cvv.Value;
            string[] cardExpiryDetails = cardExpiryDate.Text.Split(new char[] { '/' });

            // (Required) Credit card expiration month.
            if (cardExpiryDetails.Length > 0 && !string.IsNullOrEmpty(cardExpiryDetails[0]))
            {
                int month = 0;
                Int32.TryParse(cardExpiryDetails[0], out month);
                creditCard.ExpMonth = month;
            }

            // (Required) Credit card expiration year.
            if (cardExpiryDetails.Length > 1 && !string.IsNullOrEmpty(cardExpiryDetails[1]))
            {
                int year = 0;
                Int32.TryParse(cardExpiryDetails[1], out year);
                creditCard.ExpYear = year;
            }

            // (Optional) Field used by merchant to record why this credit was issued to a buyer. It is similar to a "memo" field (freeform text or string field).
            if (comment.Value != string.Empty)
            {
                request.DoNonReferencedCreditRequestDetails.Comment = comment.Value;
            }
            // Invoke the API
            DoNonReferencedCreditReq wrapper = new DoNonReferencedCreditReq();
            wrapper.DoNonReferencedCreditRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoNonReferencedCredit method in service wrapper object  
            DoNonReferencedCreditResponseType DoNonReferencedCreditResponse = service.DoNonReferencedCredit(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, DoNonReferencedCreditResponse);
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. 
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoNonReferencedCreditResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "DoNonReferencedCredit");
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
                keyResponseParameters.Add("Transaction ID", response.DoNonReferencedCreditResponseDetails.TransactionID);
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            Server.Transfer("../APIResponse.aspx");
        }
    }
}
