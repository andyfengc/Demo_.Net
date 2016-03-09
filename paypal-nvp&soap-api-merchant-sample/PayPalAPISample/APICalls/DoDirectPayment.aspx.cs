using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The DoDirectPayment API Operation enables you to process a credit card payment.
    public partial class DoDirectPayment : System.Web.UI.Page
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
            DoDirectPaymentRequestType request = new DoDirectPaymentRequestType();
            DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
            request.DoDirectPaymentRequestDetails = requestDetails;

            // (Optional) How you want to obtain payment. It is one of the following values:
            // * Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // * Sale – This is a final sale for which you are requesting payment (default).
            // Note: Order is not allowed for Direct Payment.
            requestDetails.PaymentAction = (PaymentActionCodeType)
                Enum.Parse(typeof(PaymentActionCodeType), paymentType.SelectedValue);

            // (Required) Information about the credit card to be charged.
            CreditCardDetailsType creditCard = new CreditCardDetailsType();
            requestDetails.CreditCard = creditCard;
            PayerInfoType payer = new PayerInfoType();
            // (Optional) First and last name of buyer.
            PersonNameType name = new PersonNameType();
            name.FirstName = firstName.Value;
            name.LastName = lastName.Value;
            payer.PayerName = name;
            // (Required) Details about the owner of the credit card.
            creditCard.CardOwner = payer;

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
            // Character length and limitations: For Visa, MasterCard, and Discover, the value is exactly 3 digits. For American Express, the value is exactly 4 digits.
            creditCard.CVV2 = cvv2Number.Value;
            string[] cardExpiryDetails = cardExpiryDate.Text.Split(new char[] { '/' });
            if (cardExpiryDetails.Length == 2)
            {
                // (Required) Credit card expiration month.
                creditCard.ExpMonth = Convert.ToInt32(cardExpiryDetails[0]);
                // (Required) Credit card expiration year.
                creditCard.ExpYear = Convert.ToInt32(cardExpiryDetails[1]);
            }

            requestDetails.PaymentDetails = new PaymentDetailsType();
            // (Optional) Your URL for receiving Instant Payment Notification (IPN) about this transaction. If you do not specify this value in the request, the notification URL from your Merchant Profile is used, if one exists.
            // Important: The notify URL applies only to DoExpressCheckoutPayment. This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            requestDetails.PaymentDetails.NotifyURL = ipnNotificationUrl.Value.Trim();

            // (Optional) Buyer's shipping address information. 
            AddressType billingAddr = new AddressType();
            if (firstName.Value != string.Empty && lastName.Value != string.Empty
                && street1.Value != string.Empty && country.Value != string.Empty)
            {
                billingAddr.Name = payerName.Value;
                // (Required) First street address.
                billingAddr.Street1 = street1.Value;
                // (Optional) Second street address.
                billingAddr.Street2 = street2.Value;
                // (Required) Name of city.
                billingAddr.CityName = city.Value;
                // (Required) State or province.
                billingAddr.StateOrProvince = state.Value;
                // (Required) Country code.
                billingAddr.Country = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), country.Value);
                // (Required) U.S. ZIP code or other country-specific postal code.
                billingAddr.PostalCode = postalCode.Value;

                // (Optional) Phone number.
                billingAddr.Phone = phone.Value;

                payer.Address = billingAddr;
            }

            // (Required) The total cost of the transaction to the buyer. If shipping cost and tax charges are known, include them in this value. If not, this value should be the current subtotal of the order. If the transaction includes one or more one-time purchases, this field must be equal to the sum of the purchases. This field must be set to a value greater than 0.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);
            BasicAmountType paymentAmount = new BasicAmountType(currency, amount.Value);            
            requestDetails.PaymentDetails.OrderTotal = paymentAmount;

            // Invoke the API
            DoDirectPaymentReq wrapper = new DoDirectPaymentReq();
            wrapper.DoDirectPaymentRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoDirectPayment method in service wrapper object  
            DoDirectPaymentResponseType response = service.DoDirectPayment(wrapper);
         
            // Check for API return status
            setKeyResponseObjects(service, response);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoDirectPaymentResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "DoDirectPayment");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("Correlation Id", response.CorrelationID);
            responseParams.Add("API Result", response.Ack.ToString());

            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);                
                responseParams.Add("Transaction Id", response.TransactionID);
                responseParams.Add("Payment status", response.PaymentStatus.ToString());
                if(response.PendingReason != null) {
                    responseParams.Add("Pending reason", response.PendingReason.ToString());
                }
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            Server.Transfer("../APIResponse.aspx");
            
        }
    }
}
