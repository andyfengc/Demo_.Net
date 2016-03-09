using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The DoReferenceTransaction API operation processes a payment from a buyer's account, which is identified by a previous transaction.
    public partial class DoReferenceTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            DoReferenceTransactionRequestType request = new DoReferenceTransactionRequestType();
            populateRequestObject(request);

            // Invoke the API
            DoReferenceTransactionReq wrapper = new DoReferenceTransactionReq();
            wrapper.DoReferenceTransactionRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();
            
            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the DoReferenceTransaction method in service wrapper object  
            DoReferenceTransactionResponseType doReferenceTxnResponse = service.DoReferenceTransaction(wrapper);

            // Check for API return status
            setKeyResponseObjects(service, doReferenceTxnResponse);
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoReferenceTransactionResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "DoReferenceTransaction");
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
                DoReferenceTransactionResponseDetailsType transactionDetails = response.DoReferenceTransactionResponseDetails;
                responseParams.Add("Transaction ID", transactionDetails.TransactionID);

                if (transactionDetails.PaymentInfo != null)
                {
                    responseParams.Add("Payment status", transactionDetails.PaymentInfo.PaymentStatus.ToString());
                }

                if (transactionDetails.PaymentInfo != null)
                {
                    responseParams.Add("Pending reason", transactionDetails.PaymentInfo.PendingReason.ToString());
                }
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            Server.Transfer("../APIResponse.aspx");
        }

        private void populateRequestObject(DoReferenceTransactionRequestType request)
        {
            DoReferenceTransactionRequestDetailsType referenceTransactionDetails = new DoReferenceTransactionRequestDetailsType();
            request.DoReferenceTransactionRequestDetails = referenceTransactionDetails;
            // (Required) A transaction ID from a previous purchase, such as a credit card charge using the DoDirectPayment API, or a billing agreement ID.
            referenceTransactionDetails.ReferenceID = referenceId.Value;
            // (Optional) How you want to obtain payment. It is one of the following values:
            // * Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // * Sale – This is a final sale for which you are requesting payment.
            referenceTransactionDetails.PaymentAction = (PaymentActionCodeType)
                Enum.Parse(typeof(PaymentActionCodeType), paymentAction.SelectedValue);

            // Populate payment requestDetails. 
            PaymentDetailsType paymentDetails = new PaymentDetailsType();
            referenceTransactionDetails.PaymentDetails = paymentDetails;
            // (Required) The total cost of the transaction to the buyer. If shipping cost and tax charges are known, include them in this value. If not, this value should be the current subtotal of the order. If the transaction includes one or more one-time purchases, this field must be equal to the sum of the purchases. Set this field to 0 if the transaction does not include a one-time purchase such as when you set up a billing agreement for a recurring payment that is not immediately charged. When the field is set to 0, purchase-specific fields are ignored.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            double orderTotal = 0.0;
            // (Optional) Sum of cost of all items in this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            double itemTotal = 0.0;
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), currencyCode.SelectedValue);

            // (Optional) Total shipping costs for this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            if (shippingTotal.Value != string.Empty)
            {
                paymentDetails.ShippingTotal = new BasicAmountType(currency, shippingTotal.Value);
                orderTotal += Convert.ToDouble(shippingTotal.Value);
            }
            // (Optional) Total handling costs for this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            if (handlingTotal.Value != string.Empty)
            {
                paymentDetails.HandlingTotal = new BasicAmountType(currency, handlingTotal.Value);
                orderTotal += Convert.ToDouble(handlingTotal.Value);
            }
            // (Optional) Sum of tax for all items in this order.
            // Note: You must set the currencyID attribute to one of the 3-character currency codes for any of the supported PayPal currencies.
            if (taxTotal.Value != string.Empty)
            {
                paymentDetails.TaxTotal = new BasicAmountType(currency, taxTotal.Value);
                orderTotal += Convert.ToDouble(taxTotal.Value);
            }
            // (Optional) Description of items the buyer is purchasing.
            // Note: The value you specify is available only if the transaction includes a purchase. This field is ignored if you set up a billing agreement for a recurring payment that is not immediately charged.
            if (orderDescription.Value != string.Empty)
            {
                paymentDetails.OrderDescription = orderDescription.Value;
            }
           
            // Each payment can include requestDetails about multiple payment items
            // This example shows just one payment item
            if (itemName.Value != null && itemAmount.Value != null && itemQuantity.Value != null)
            {
                PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
                // Item name. This field is required when you pass a value for ItemCategory.
                itemDetails.Name = itemName.Value;
                // Cost of item. This field is required when you pass a value for ItemCategory.
                itemDetails.Amount = new BasicAmountType(currency, itemAmount.Value);
                // Item quantity. This field is required when you pass a value forItemCategory.
                itemDetails.Quantity = Convert.ToInt32(itemQuantity.Value);
                // Indicates whether the item is digital or physical. For digital goods, this field is required and you must set it to Digital to get the best rates. It is one of the following values:
                // * Digital
                // * Physical
                // This field is introduced in version 69.0.
                itemDetails.ItemCategory = (ItemCategoryType)
                    Enum.Parse(typeof(ItemCategoryType), itemCategory.SelectedValue);
                itemTotal += Convert.ToDouble(itemDetails.Amount.value) * itemDetails.Quantity.Value;
                //if (salesTax.Value != string.Empty)
                //{
                //    itemDetails.Tax = new BasicAmountType(currency, salesTax.Value);
                //    orderTotal += Convert.ToDouble(salesTax.Value);
                //}
                // (Optional) Item description.
                // This field is available since version 53.0.
                if (itemDescription.Value != string.Empty)
                {
                    itemDetails.Description = itemDescription.Value;
                }
                paymentDetails.PaymentDetailsItem.Add(itemDetails);
            }
            orderTotal += itemTotal;
            paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
            paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());
            // (Optional) Your URL for receiving Instant Payment Notification (IPN) about this transaction. If you do not specify this value in the request, the notification URL from your Merchant Profile is used, if one exists.
            // Important:
            // The notify URL applies only to DoExpressCheckoutPayment. This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            paymentDetails.NotifyURL = ipnNotificationUrl.Value.Trim();
        }

    }
}
