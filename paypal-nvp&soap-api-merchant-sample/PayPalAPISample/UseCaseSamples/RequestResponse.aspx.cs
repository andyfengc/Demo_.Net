using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.PayPalAPIInterfaceService;
using PayPal.Permissions.Model;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class RequestResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string call = Context.Request.Params["ButtonPayments"];

            switch (call)
            {
                case "SetExpressCheckout":
                    SetExpressCheckoutForRecurringPayments(Context);
                    break;
                case "CreateRecurringPaymentsProfile":
                    CreateRecurringPaymentsProfile(Context);
                    break;
                case "SetExpressCheckoutPaymentAuthorization":
                    SetExpressCheckoutPaymentAuthorization(Context);
                    break;
                case "SetExpressCheckoutPaymentOrder":
                    SetExpressCheckoutPaymentOrder(Context);
                    break;
                case "DoExpressCheckout":
                    DoExpressCheckout(Context);
                    break;
                case "DoExpressCheckoutForParallelPayment":
                    DoExpressCheckoutForParallelPayment(Context);
                    break;
                case "DoAuthorization":
                    DoAuthorization(Context);
                    break;
                case "DoCapture":
                    DoCapture(Context);
                    break;
                case "ParallelPayment":
                    ParallelPayment(Context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles Set Express Checkout For Recurring Payments
        /// </summary>
        /// <param name="contextHttp"></param>
        private void SetExpressCheckoutForRecurringPayments(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            SetExpressCheckoutRequestType setExpressCheckoutReq = new SetExpressCheckoutRequestType();
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();

            string requestUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();

            // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer confirms the order and payment or billing agreement.
            UriBuilder uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/SetExpressCheckoutForRecurringPayments.aspx";
            string returnUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the original page on which the buyer chose to pay with PayPal or establish a billing agreement.
            uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/SetExpressCheckoutForRecurringPayments.aspx";
            string cancelUrl = uriBuilder.Uri.ToString();
                        
            // (Required) URL to which the buyer's browser is returned after choosing 
            // to pay with PayPal. For digital goods, you must add JavaScript to this 
            // page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer 
            // confirms the order and payment or billing agreement.
            // Character length and limitations: 2048 single-byte characters            
            details.ReturnURL = returnUrl + "?currencyCodeType=" + parameters["currencyCode"];
            details.CancelURL = cancelUrl;
                        
            // (Optional) Email address of the buyer as entered during checkout.
            // PayPal uses this value to pre-fill the PayPal membership sign-up portion on the PayPal pages.
            // Character length and limitations: 127 single-byte alphanumeric characters            
            details.BuyerEmail = parameters["buyerMail"];

            decimal itemTotal = 0.0M;
            decimal orderTotal = 0.0M;

            // Cost of item. This field is required when you pass a value for ItemCategory.
            string amountItems = parameters["itemAmount"];
                         
            // Item quantity. This field is required when you pass a value for ItemCategory. 
            // For digital goods (ItemCategory=Digital), this field is required.
            // Character length and limitations: Any positive integer             
            string qtyItems = parameters["itemQuantity"];
                        
            // Item name. This field is required when you pass a value for ItemCategory.
            // Character length and limitations: 127 single-byte characters            
            string names = parameters["itemName"];

            List<PaymentDetailsItemType> lineItems = new List<PaymentDetailsItemType>();
            PaymentDetailsItemType item = new PaymentDetailsItemType();
            BasicAmountType amt = new BasicAmountType();

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            amt.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            amt.value = amountItems;
            item.Quantity = Convert.ToInt32(qtyItems);
            item.Name = names;
            item.Amount = amt;

            
            // Indicates whether an item is digital or physical. For digital goods, this field is required and must be set to Digital. It is one of the following values:
            // 1. Digital
            // 2. Physical            
            item.ItemCategory = (ItemCategoryType)Enum.Parse(typeof(ItemCategoryType), parameters["itemCategory"]);

            // (Optional) Item sales tax.
            // Note: You must set the currencyID attribute to one of 
            // the 3-character currency codes for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            if (parameters["salesTax"] != string.Empty)
            {
                item.Tax = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["salesTax"]);
            }

            itemTotal += Convert.ToDecimal(qtyItems) * Convert.ToDecimal(amountItems);
            orderTotal += itemTotal;

            List<PaymentDetailsType> payDetails = new List<PaymentDetailsType>();
            PaymentDetailsType paydtl = new PaymentDetailsType();
            
            // How you want to obtain payment. When implementing parallel payments, 
            // this field is required and must be set to Order.
            // When implementing digital goods, this field is required and must be set to Sale.
            // If the transaction does not include a one-time purchase, this field is ignored.
            // It is one of the following values:
            // Sale – This is a final sale for which you are requesting payment (default).
            // Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            paydtl.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), parameters["paymentType"]);

            // (Optional) Total shipping costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,)
            if (parameters["shippingTotal"] != string.Empty)
            {
                BasicAmountType shippingTotal = new BasicAmountType();
                shippingTotal.value = parameters["shippingTotal"];
                shippingTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
                orderTotal += Convert.ToDecimal(parameters["shippingTotal"]);
                paydtl.ShippingTotal = shippingTotal;
            }

            // (Optional) Total shipping insurance costs for this order. 
            // The value must be a non-negative currency amount or null if you offer insurance options.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency 
            // codes for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency. 
            // It includes no currency symbol. It must have 2 decimal places,
            // the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            // InsuranceTotal is available since version 53.0.
            if (parameters["insuranceTotal"] != string.Empty)
            {
                paydtl.InsuranceTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["insuranceTotal"]);
                paydtl.InsuranceOptionOffered = "true";
                orderTotal += Convert.ToDecimal(parameters["insuranceTotal"]);
            }

            // (Optional) Total handling costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, 
            // the decimal separator must be a period (.), and the optional 
            // thousands separator must be a comma (,). 
            if (parameters["handlingTotal"] != string.Empty)
            {
                paydtl.HandlingTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["handlingTotal"]);
                orderTotal += Convert.ToDecimal(parameters["handlingTotal"]);
            }

            // (Optional) Sum of tax for all items in this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency. It includes no currency symbol.
            // It must have 2 decimal places, the decimal separator must be a period (.),
            // and the optional thousands separator must be a comma (,).
            if (parameters["taxTotal"] != string.Empty)
            {
                paydtl.TaxTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["taxTotal"]);
                orderTotal += Convert.ToDecimal(parameters["taxTotal"]);
            }

            // (Optional) Description of items the buyer is purchasing.
            // Note:
            // The value you specify is available only if the transaction includes a purchase.
            // This field is ignored if you set up a billing agreement for a recurring payment 
            // that is not immediately charged.
            // Character length and limitations: 127 single-byte alphanumeric characters
            if (parameters["orderDescription"] != string.Empty)
            {
                paydtl.OrderDescription = parameters["orderDescription"];
            }

            BasicAmountType itemsTotal = new BasicAmountType();
            itemsTotal.value = Convert.ToString(itemTotal);

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            itemsTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            paydtl.OrderTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), Convert.ToString(orderTotal));
            paydtl.PaymentDetailsItem = lineItems;

            paydtl.ItemTotal = itemsTotal;

            // (Optional) Your URL for receiving Instant Payment Notification (IPN) 
            // about this transaction. If you do not specify this value in the request, 
            // the notification URL from your Merchant Profile is used, if one exists.
            // Important:
            // The notify URL applies only to DoExpressCheckoutPayment. 
            // This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            // Character length and limitations: 2,048 single-byte alphanumeric characters
            paydtl.NotifyURL = parameters["notifyURL"];

            payDetails.Add(paydtl);
            details.PaymentDetails = payDetails;

            if (parameters["billingAgreementText"] != string.Empty)
            {  
                // (Required) Type of billing agreement. For recurring payments,
                // this field must be set to RecurringPayments. 
                // In this case, you can specify up to ten billing agreements. 
                // Other defined values are not valid.
                // Type of billing agreement for reference transactions. 
                // You must have permission from PayPal to use this field. 
                // This field must be set to one of the following values:
                // 1. MerchantInitiatedBilling - PayPal creates a billing agreement 
                // for each transaction associated with buyer.You must specify 
                // version 54.0 or higher to use this option.
                // 2. MerchantInitiatedBillingSingleAgreement - PayPal creates a 
                // single billing agreement for all transactions associated with buyer.
                // Use this value unless you need per-transaction billing agreements. 
                // You must specify version 58.0 or higher to use this option.
                BillingAgreementDetailsType billingAgreement = new BillingAgreementDetailsType((BillingCodeType)Enum.Parse(typeof(BillingCodeType), parameters["billingType"]));

                
                // Description of goods or services associated with the billing agreement. 
                // This field is required for each recurring payment billing agreement.
                // PayPal recommends that the description contain a brief summary of 
                // the billing agreement terms and conditions. For example,
                // buyer is billed at "9.99 per month for 2 years".
                // Character length and limitations: 127 single-byte alphanumeric characters                
                billingAgreement.BillingAgreementDescription = parameters["billingAgreementText"];
                List<BillingAgreementDetailsType> billList = new List<BillingAgreementDetailsType>();
                billList.Add(billingAgreement);
                details.BillingAgreementDetails = billList;
            }

            setExpressCheckoutReq.SetExpressCheckoutRequestDetails = details;
            SetExpressCheckoutReq expressCheckoutReq = new SetExpressCheckoutReq();
            expressCheckoutReq.SetExpressCheckoutRequest = setExpressCheckoutReq;

            SetExpressCheckoutResponseType response = null;
            try
            {
                response = service.SetExpressCheckout(expressCheckoutReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.Message);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString() + "_express-checkout&token=" + response.Token;
            }

            responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());

            Display(contextHttp, "SetExpressCheckoutForRecurringPayments", "SetExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        /// <summary>
        /// Handles Create Recurring Payments Profile
        /// </summary>
        /// <param name="contextHttp"></param>
        private void CreateRecurringPaymentsProfile(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            CreateRecurringPaymentsProfileReq req = new CreateRecurringPaymentsProfileReq();
            CreateRecurringPaymentsProfileRequestType reqType = new CreateRecurringPaymentsProfileRequestType();

            // (Required) The date when billing for this profile begins.
            // Note:
            // The profile may take up to 24 hours for activation.
            // Character length and limitations: Must be a valid date, in UTC/GMT format
            RecurringPaymentsProfileDetailsType profileDetails = new RecurringPaymentsProfileDetailsType(parameters["billingStartDate"] + "T00:00:00:000Z");

            // Populate schedule details
            ScheduleDetailsType scheduleDetails = new ScheduleDetailsType();

             // (Required) Description of the recurring payment.
             // Note:
             // You must ensure that this field matches the corresponding billing agreement 
             // description included in the SetExpressCheckout request.
             // Character length and limitations: 127 single-byte alphanumeric characters
            scheduleDetails.Description = parameters["profileDescription"];

            // (Optional) Number of scheduled payments that can fail before the profile 
            // is automatically suspended. An IPN message is sent to the merchant when the 
            // specified number of failed payments is reached.
            // Character length and limitations: Number string representing an integer
            if (parameters["maxFailedPayments"] != string.Empty)
            {
                scheduleDetails.MaxFailedPayments = Convert.ToInt32(parameters["maxFailedPayments"]);
            }

            // (Optional) Indicates whether you would like PayPal to automatically bill 
            // the outstanding balance amount in the next billing cycle. 
            // The outstanding balance is the total amount of any previously failed 
            // scheduled payments that have yet to be successfully paid. 
            // It is one of the following values:
            // NoAutoBill – PayPal does not automatically bill the outstanding balance.
            // AddToNextBilling – PayPal automatically bills the outstanding balance.
            if (parameters["autoBillOutstandingAmount"] != string.Empty)
            {
                scheduleDetails.AutoBillOutstandingAmount = (AutoBillType)Enum.Parse(typeof(AutoBillType), parameters["autoBillOutstandingAmount"]);
            }

            CurrencyCodeType currency = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), "USD");

            if (parameters["trialBillingAmount"] != string.Empty)
            {
                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                int frequency = Convert.ToInt32(parameters["trialBillingFrequency"]);
                
                // Billing amount for each billing cycle during this payment period; 
                // required if you specify an optional trial period. 
                // This amount does not include shipping and tax amounts.
                // Note:
                // All amounts in the CreateRecurringPaymentsProfile request must have 
                // the same currency.
                // Character length and limitations: 
                // Value is a positive number which cannot exceed $10,000 USD in any currency. 
                // It includes no currency symbol. 
                // It must have 2 decimal places, the decimal separator must be a period (.),
                // and the optional thousands separator must be a comma (,).
                BasicAmountType paymentAmount = new BasicAmountType(currency, parameters["trialBillingAmount"]);

                // Unit for billing during this subscription period; 
                // required if you specify an optional trial period. 
                // It is one of the following values: [Day, Week, SemiMonth, Month, Year]
                // For SemiMonth, billing is done on the 1st and 15th of each month.
                // Note:
                // The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                BillingPeriodType period = (BillingPeriodType)Enum.Parse(typeof(BillingPeriodType), parameters["trialBillingPeriod"]);

                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                int numCycles = Convert.ToInt32(parameters["trialBillingCycles"]);

                BillingPeriodDetailsType trialPeriod = new BillingPeriodDetailsType(period, frequency, paymentAmount);
                trialPeriod.TotalBillingCycles = numCycles;
                scheduleDetails.TrialPeriod = trialPeriod;
            }

            if (parameters["billingAmount"] != string.Empty)
            {
                // (Required) Number of billing periods that make up one billing cycle.
                // The combination of billing frequency and billing period must be less than 
                // or equal to one year. For example, if the billing cycle is Month, 
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                int frequency = Convert.ToInt32(parameters["billingFrequency"]);
                
                // (Required) Billing amount for each billing cycle during this payment period. 
                // This amount does not include shipping and tax amounts.
                // Note:
                // All amounts in the CreateRecurringPaymentsProfile request must have the same 
                // currency.
                // Character length and limitations: Value is a positive number which cannot 
                // exceed $10,000 USD in any currency. It includes no currency symbol. 
                // It must have 2 decimal places, the decimal separator must be a period (.), 
                // and the optional thousands separator must be a comma (,). 
                BasicAmountType paymentAmount = new BasicAmountType(currency, parameters["billingAmount"]);
                
                // (Required) Unit for billing during this subscription period. 
                // It is one of the following values:
                // [Day, Week, SemiMonth, Month, Year]
                // For SemiMonth, billing is done on the 1st and 15th of each month.
                // Note:
                // The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                BillingPeriodType period = (BillingPeriodType)Enum.Parse(typeof(BillingPeriodType), parameters["billingPeriod"]);
                
                 // (Optional) Number of billing cycles for payment period.
                 // For the regular payment period, if no value is specified or the value is 0, 
                 // the regular payment period continues until the profile is canceled or deactivated.
                 // For the regular payment period, if the value is greater than 0, 
                 // the regular payment period will expire after the trial period is 
                 // finished and continue at the billing frequency for TotalBillingCycles cycles.
                int numCycles = Convert.ToInt32(parameters["totalBillingCycles"]);

                BillingPeriodDetailsType paymentPeriod = new BillingPeriodDetailsType(period, frequency, paymentAmount);
                paymentPeriod.TotalBillingCycles = numCycles;
                scheduleDetails.PaymentPeriod = paymentPeriod;
            }

            CreateRecurringPaymentsProfileRequestDetailsType reqDetails = new CreateRecurringPaymentsProfileRequestDetailsType(profileDetails, scheduleDetails);

            // Credit Card Number is required for CreateRecurringPaymentsProfile.  
            // Each CreateRecurringPaymentsProfile request creates a single recurring payments profile.
            CreditCardDetailsType cc = new CreditCardDetailsType();            

            // (Required) Credit Card Number.
            // Character length and limitations: Numeric characters only with no spaces 
            // or punctuation. The string must conform with modulo and length required 
            // by each credit card type.
            cc.CreditCardNumber = parameters["creditCardNumber"];

            // Card Verification Value, version 2. 
            // Your Merchant Account settings determine whether this field is required.
            // To comply with credit card processing regulations, you must not store this 
            // value after a transaction has been completed.
            // Character length and limitations: 
            // For Visa, MasterCard, and Discover, the value is exactly 3 digits. 
            // For American Express, the value is exactly 4 digits.
            cc.CVV2 = parameters["cvv"];

            // Expiry Month
            cc.ExpMonth = Convert.ToInt32(parameters["expMonth"]);

            // Expiry Year
            cc.ExpYear = Convert.ToInt32(parameters["expYear"]);
            
            // (Optional) Type of credit card. 
            // For UK, only Maestro, MasterCard, Discover, and Visa are allowable. 
            // For Canada, only MasterCard and Visa are allowable and 
            // Interac debit cards are not supported. It is one of the following values:
            // [Visa, MasterCard, Discover, Amex, Maestro: See note.]
            // Note:
            // If the credit card type is Maestro, you must set CURRENCYCODE to GBP. 
            // In addition, you must specify either STARTDATE or ISSUENUMBER.
            CreditCardTypeType type = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), parameters["creditCardType"]);
            cc.CreditCardType = type;

            reqDetails.CreditCard = cc;

            reqType.CreateRecurringPaymentsProfileRequestDetails = reqDetails;
            req.CreateRecurringPaymentsProfileRequest = reqType;

            CreateRecurringPaymentsProfileResponseType response = null;
            try
            {
                response = service.CreateRecurringPaymentsProfile(req);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.Message);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;
            
            responseValues.Add("Acknowledgement", response.Ack.ToString());

            Display(contextHttp, "CreateRecurringPaymentsProfile", "SetExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        /// <summary>
        /// Handles Set ExpressCheckout Payment Authorization
        /// </summary>
        /// <param name="contextHttp"></param>
        private void SetExpressCheckoutPaymentAuthorization(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            SetExpressCheckoutRequestType setExpressCheckoutReq = new SetExpressCheckoutRequestType();
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();

            string requestUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();

            // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer confirms the order and payment or billing agreement.
            UriBuilder uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/DoExpressCheckout.aspx";
            string returnUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the original page on which the buyer chose to pay with PayPal or establish a billing agreement.
            uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/SetExpressCheckoutPaymentAuthorization.aspx";
            string cancelUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer's browser is returned after choosing 
            // to pay with PayPal. For digital goods, you must add JavaScript to this 
            // page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer 
            // confirms the order and payment or billing agreement.
            // Character length and limitations: 2048 single-byte characters
            details.ReturnURL = returnUrl + "?currencyCodeType=" + parameters["currencyCode"] + "&paymentType=" + parameters["paymentType"];
            details.CancelURL = cancelUrl;
            
            // (Optional) Email address of the buyer as entered during checkout.
            // PayPal uses this value to pre-fill the PayPal membership sign-up portion on the PayPal pages.
            // Character length and limitations: 127 single-byte alphanumeric characters
            details.BuyerEmail = parameters["buyerMail"];

            decimal itemTotal = 0.0M;
            decimal orderTotal = 0.0M;

            // Cost of item. This field is required when you pass a value for ItemCategory.
            string amountItems = parameters["itemAmount"];

            // Item quantity. This field is required when you pass a value for ItemCategory. 
            // For digital goods (ItemCategory=Digital), this field is required.
            // Character length and limitations: Any positive integer
            // This field is introduced in version 53.0. 
            string qtyItems = parameters["itemQuantity"];
            
            // Item name. This field is required when you pass a value for ItemCategory.
            // Character length and limitations: 127 single-byte characters
            // This field is introduced in version 53.0. 
            string names = parameters["itemName"];

            List<PaymentDetailsItemType> lineItems = new List<PaymentDetailsItemType>();
            PaymentDetailsItemType item = new PaymentDetailsItemType();
            BasicAmountType amt = new BasicAmountType();

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            amt.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            amt.value = amountItems;
            item.Quantity = Convert.ToInt32(qtyItems);
            item.Name = names;
            item.Amount = amt;

            // Indicates whether an item is digital or physical. For digital goods, this field is required and must be set to Digital. It is one of the following values:
            // 1.Digital
            // 2.Physical
            // This field is available since version 65.1. 
            item.ItemCategory = (ItemCategoryType)Enum.Parse(typeof(ItemCategoryType), parameters["itemCategory"]);

            // (Optional) Item sales tax.
            // Note: You must set the currencyID attribute to one of 
            // the 3-character currency codes for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            if (parameters["salesTax"] != string.Empty)
            {
                item.Tax = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["salesTax"]);
            }

            itemTotal += Convert.ToDecimal(qtyItems) * Convert.ToDecimal(amountItems);
            orderTotal += itemTotal;

            List<PaymentDetailsType> payDetails = new List<PaymentDetailsType>();
            PaymentDetailsType paydtl = new PaymentDetailsType();

            // How you want to obtain payment. When implementing parallel payments, 
            // this field is required and must be set to Order.
            // When implementing digital goods, this field is required and must be set to Sale.
            // If the transaction does not include a one-time purchase, this field is ignored. 
            // It is one of the following values:
            // Sale – This is a final sale for which you are requesting payment (default).
            // Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            paydtl.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), parameters["paymentType"]);

            // (Optional) Total shipping costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,)
            if (parameters["shippingTotal"] != string.Empty)
            {
                BasicAmountType shippingTotal = new BasicAmountType();
                shippingTotal.value = parameters["shippingTotal"];
                shippingTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
                orderTotal += Convert.ToDecimal(parameters["shippingTotal"]);
                paydtl.ShippingTotal = shippingTotal;
            }

            // (Optional) Total shipping insurance costs for this order. 
            // The value must be a non-negative currency amount or null if you offer insurance options.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency 
            // codes for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency. 
            // It includes no currency symbol. It must have 2 decimal places,
            // the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            // InsuranceTotal is available since version 53.0.
            if (parameters["insuranceTotal"] != string.Empty)
            {
                paydtl.InsuranceTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["insuranceTotal"]);
                paydtl.InsuranceOptionOffered = "true";
                orderTotal += Convert.ToDecimal(parameters["insuranceTotal"]);
            }

            // (Optional) Total handling costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, 
            // the decimal separator must be a period (.), and the optional 
            // thousands separator must be a comma (,). 
            if (parameters["handlingTotal"] != string.Empty)
            {
                paydtl.HandlingTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["handlingTotal"]);
                orderTotal += Convert.ToDecimal(parameters["handlingTotal"]);
            }

            // (Optional) Sum of tax for all items in this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency. It includes no currency symbol.
            // It must have 2 decimal places, the decimal separator must be a period (.),
            // and the optional thousands separator must be a comma (,).
            if (parameters["taxTotal"] != string.Empty)
            {
                paydtl.TaxTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["taxTotal"]);
                orderTotal += Convert.ToDecimal(parameters["taxTotal"]);
            }

            // (Optional) Description of items the buyer is purchasing.
            // Note:
            // The value you specify is available only if the transaction includes a purchase.
            // This field is ignored if you set up a billing agreement for a recurring payment 
            // that is not immediately charged.
            // Character length and limitations: 127 single-byte alphanumeric characters
            if (parameters["orderDescription"] != string.Empty)
            {
                paydtl.OrderDescription = parameters["orderDescription"];
            }

            BasicAmountType itemsTotal = new BasicAmountType();
            itemsTotal.value = Convert.ToString(itemTotal);

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            itemsTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            paydtl.OrderTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), Convert.ToString(orderTotal));
            paydtl.PaymentDetailsItem = lineItems;

            paydtl.ItemTotal = itemsTotal;

            // (Optional) Your URL for receiving Instant Payment Notification (IPN) 
            // about this transaction. If you do not specify this value in the request, 
            // the notification URL from your Merchant Profile is used, if one exists.
            // Important:
            // The notify URL applies only to DoExpressCheckoutPayment. 
            // This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            // Character length and limitations: 2,048 single-byte alphanumeric characters
            paydtl.NotifyURL = parameters["notifyURL"];

            payDetails.Add(paydtl);
            details.PaymentDetails = payDetails;

            setExpressCheckoutReq.SetExpressCheckoutRequestDetails = details;

            SetExpressCheckoutReq expressCheckoutReq = new SetExpressCheckoutReq();
            expressCheckoutReq.SetExpressCheckoutRequest = setExpressCheckoutReq;

            SetExpressCheckoutResponseType response = null;
            try
            {
                response = service.SetExpressCheckout(expressCheckoutReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.Message);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString() + "_express-checkout&token=" + response.Token;
            }
            responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
                
            Display(contextHttp, "SetExpressCheckoutPaymentAuthorization", "SetExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        /// <summary>
        /// Handles Set ExpressCheckout Payment Order
        /// </summary>
        /// <param name="contextHttp"></param>
        private void SetExpressCheckoutPaymentOrder(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            SetExpressCheckoutRequestType setExpressCheckoutReq = new SetExpressCheckoutRequestType();
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();

            string requestUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();

            // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer confirms the order and payment or billing agreement.
            UriBuilder uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/DoExpressCheckout.aspx";
            string returnUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the original page on which the buyer chose to pay with PayPal or establish a billing agreement.
            uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/SetExpressCheckoutPaymentAuthorization.aspx";
            string cancelUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer's browser is returned after choosing 
            // to pay with PayPal. For digital goods, you must add JavaScript to this 
            // page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer 
            // confirms the order and payment or billing agreement.
            // Character length and limitations: 2048 single-byte characters
            details.ReturnURL = returnUrl + "?currencyCodeType=" + parameters["currencyCode"] + "&paymentType=" + parameters["paymentType"];
            details.CancelURL = cancelUrl;
            
            // (Optional) Email address of the buyer as entered during checkout.
            // PayPal uses this value to pre-fill the PayPal membership sign-up portion on the PayPal pages.
            // Character length and limitations: 127 single-byte alphanumeric characters
            details.BuyerEmail = parameters["buyerMail"];

            decimal itemTotal = 0.0M;
            decimal orderTotal = 0.0M;

            // Cost of item. This field is required when you pass a value for ItemCategory.
            string amountItems = parameters["itemAmount"];
            
            // Item quantity. This field is required when you pass a value for ItemCategory. 
            // For digital goods (ItemCategory=Digital), this field is required.
            // Character length and limitations: Any positive integer
            // This field is introduced in version 53.0. 
            string qtyItems = parameters["itemQuantity"];

            // Item name. This field is required when you pass a value for ItemCategory.
            // Character length and limitations: 127 single-byte characters
            // This field is introduced in version 53.0. 
            string names = parameters["itemName"];

            List<PaymentDetailsItemType> lineItems = new List<PaymentDetailsItemType>();
            PaymentDetailsItemType item = new PaymentDetailsItemType();
            BasicAmountType amt = new BasicAmountType();

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            amt.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            amt.value = amountItems;
            item.Quantity = Convert.ToInt32(qtyItems);
            item.Name = names;
            item.Amount = amt;

            // Indicates whether an item is digital or physical. For digital goods, this field is required and must be set to Digital. It is one of the following values:
            // 1.Digital
            // 2.Physical
            // This field is available since version 65.1. 
            item.ItemCategory = (ItemCategoryType)Enum.Parse(typeof(ItemCategoryType), parameters["itemCategory"]);
            lineItems.Add(item);
            
            // (Optional) Item sales tax.
            // Note: You must set the currencyID attribute to one of 
            // the 3-character currency codes for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            if (parameters["salesTax"] != string.Empty)
            {
                item.Tax = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["salesTax"]);
            }

            itemTotal += Convert.ToDecimal(qtyItems) * Convert.ToDecimal(amountItems);
            orderTotal += itemTotal;

            List<PaymentDetailsType> payDetails = new List<PaymentDetailsType>();
            PaymentDetailsType paydtl = new PaymentDetailsType();

            // How you want to obtain payment. When implementing parallel payments, 
            // this field is required and must be set to Order.
            // When implementing digital goods, this field is required and must be set to Sale.
            // If the transaction does not include a one-time purchase, this field is ignored. 
            // It is one of the following values:
            // Sale – This is a final sale for which you are requesting payment (default).
            // Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            paydtl.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), parameters["paymentType"]);

            // (Optional) Total shipping costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,)
            if (parameters["shippingTotal"] != string.Empty)
            {
                BasicAmountType shippingTotal = new BasicAmountType();
                shippingTotal.value = parameters["shippingTotal"];
                shippingTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
                orderTotal += Convert.ToDecimal(parameters["shippingTotal"]);
                paydtl.ShippingTotal = shippingTotal;
            }

            // (Optional) Total shipping insurance costs for this order. 
            // The value must be a non-negative currency amount or null if you offer insurance options.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency 
            // codes for any of the supported PayPal currencies.
            // Character length and limitations: 
            // Value is a positive number which cannot exceed $10,000 USD in any currency. 
            // It includes no currency symbol. It must have 2 decimal places,
            // the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            // InsuranceTotal is available since version 53.0.
            if (parameters["insuranceTotal"] != string.Empty)
            {
                paydtl.InsuranceTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["insuranceTotal"]);
                paydtl.InsuranceOptionOffered = "true";
                orderTotal += Convert.ToDecimal(parameters["insuranceTotal"]);
            }
            
            // (Optional) Total handling costs for this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency.
            // It includes no currency symbol. It must have 2 decimal places, 
            // the decimal separator must be a period (.), and the optional 
            // thousands separator must be a comma (,). 
            if (parameters["handlingTotal"] != string.Empty)
            {
                paydtl.HandlingTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["handlingTotal"]);
                orderTotal += Convert.ToDecimal(parameters["handlingTotal"]);
            }

            // (Optional) Sum of tax for all items in this order.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes
            // for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which 
            // cannot exceed $10,000 USD in any currency. It includes no currency symbol.
            // It must have 2 decimal places, the decimal separator must be a period (.),
            // and the optional thousands separator must be a comma (,).
            if (parameters["taxTotal"] != string.Empty)
            {
                paydtl.TaxTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), parameters["taxTotal"]);
                orderTotal += Convert.ToDecimal(parameters["taxTotal"]);
            }

            // (Optional) Description of items the buyer is purchasing.
            // Note:
            // The value you specify is available only if the transaction includes a purchase.
            // This field is ignored if you set up a billing agreement for a recurring payment 
            // that is not immediately charged.
            // Character length and limitations: 127 single-byte alphanumeric characters
            if (parameters["orderDescription"] != string.Empty)
            {
                paydtl.OrderDescription = parameters["orderDescription"];
            }

            BasicAmountType itemsTotal = new BasicAmountType();
            itemsTotal.value = Convert.ToString(itemTotal);

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables. 
            itemsTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            paydtl.OrderTotal = new BasicAmountType((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]), Convert.ToString(orderTotal));
            paydtl.PaymentDetailsItem = lineItems;

            paydtl.ItemTotal = itemsTotal;

            // (Optional) Your URL for receiving Instant Payment Notification (IPN) 
            // about this transaction. If you do not specify this value in the request, 
            // the notification URL from your Merchant Profile is used, if one exists.
            // Important:
            // The notify URL applies only to DoExpressCheckoutPayment. 
            // This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            // Character length and limitations: 2,048 single-byte alphanumeric characters
            paydtl.NotifyURL = parameters["notifyURL"];

            payDetails.Add(paydtl);
            details.PaymentDetails = payDetails;
            setExpressCheckoutReq.SetExpressCheckoutRequestDetails = details;

            SetExpressCheckoutReq expressCheckoutReq = new SetExpressCheckoutReq();
            expressCheckoutReq.SetExpressCheckoutRequest = setExpressCheckoutReq;

            SetExpressCheckoutResponseType response = null;
            try
            {
                response = service.SetExpressCheckout(expressCheckoutReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.Message);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString() + "_express-checkout&token=" + response.Token;
            }
            responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());

            Display(contextHttp, "SetExpressCheckoutPaymentOrder", "SetExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        /// <summary>
        /// Handles DoExpressCheckout
        /// </summary>
        /// <param name="contextHttp"></param>
        private void DoExpressCheckout(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<String, String> configurationMap = Configuration.GetAcctAndConfig();

            // Creating service wrapper object to make an API call by loading configuration map.
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            DoExpressCheckoutPaymentRequestType doCheckoutPaymentRequestType = new DoExpressCheckoutPaymentRequestType();
            DoExpressCheckoutPaymentRequestDetailsType details = new DoExpressCheckoutPaymentRequestDetailsType();

            // A timestamped token by which you identify to PayPal that you are processing
            // this payment with Express Checkout. The token expires after three hours. 
            // If you set the token in the SetExpressCheckout request, the value of the token
            // in the response is identical to the value in the request.
            // Character length and limitations: 20 single-byte characters
            details.Token = parameters["token"];

            // Unique PayPal Customer Account identification number.
            // Character length and limitations: 13 single-byte alphanumeric characters
            details.PayerID = parameters["payerID"];

            // (Optional) How you want to obtain payment. If the transaction does not include a one-time purchase, this field is ignored. 
            // It is one of the following values:
            // Sale – This is a final sale for which you are requesting payment (default).
            // Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            // Note:
            // You cannot set this field to Sale in SetExpressCheckout request and then change 
            // this value to Authorization or Order in the DoExpressCheckoutPayment request. 
            // If you set the field to Authorization or Order in SetExpressCheckout, you may set the field to Sale.
            // Character length and limitations: Up to 13 single-byte alphabetic characters
            // This field is deprecated. Use PaymentAction in PaymentDetailsType instead.
            details.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), parameters["paymentType"]);

            decimal itemTotalAmt = 0.0M;
            decimal orderTotalAmt = 0.0M;
            String amt = parameters["amt"];
            String quantity = parameters["itemQuantity"];
            itemTotalAmt = Convert.ToDecimal(amt) * Convert.ToDecimal(quantity);
            orderTotalAmt += itemTotalAmt;

            PaymentDetailsType paymentDetails = new PaymentDetailsType();
            BasicAmountType orderTotal = new BasicAmountType();
            orderTotal.value = Convert.ToString(orderTotalAmt);

            //PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables.
            orderTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            // (Required) The total cost of the transaction to the buyer. 
            // If shipping cost (not applicable to digital goods) and tax charges are known, 
            // include them in this value. If not, this value should be the current sub-total 
            // of the order. If the transaction includes one or more one-time purchases, this 
            // field must be equal to the sum of the purchases. Set this field to 0 if the 
            // transaction does not include a one-time purchase such as when you set up a 
            // billing agreement for a recurring payment that is not immediately charged. 
            // When the field is set to 0, purchase-specific fields are ignored. 
            // For digital goods, the following must be true:
            // total cost > 0
            // total cost <= total cost passed in the call to SetExpressCheckout
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // When multiple payments are passed in one transaction, all of the payments must 
            // have the same currency code.
            // Character length and limitations: Value is a positive number which cannot 
            // exceed $10,000 USD in any currency. It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            paymentDetails.OrderTotal = orderTotal;

            BasicAmountType itemTotal = new BasicAmountType();
            itemTotal.value = Convert.ToString(itemTotalAmt);

            //PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables.
            itemTotal.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);


            // Sum of cost of all items in this order. For digital goods, this field is 
            // required. PayPal recommends that you pass the same value in the call to 
            // DoExpressCheckoutPayment that you passed in the call to SetExpressCheckout.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency 
            // codes for any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which cannot 
            // exceed $10,000 USD in any currency. It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).        
            // paymentDetails.ItemTotal = itemTotal;
            List<PaymentDetailsItemType> paymentItems = new List<PaymentDetailsItemType>();
            PaymentDetailsItemType paymentItem = new PaymentDetailsItemType();

            // Item name. This field is required when you pass a value for ItemCategory.
            // Character length and limitations: 127 single-byte characters
            // This field is introduced in version 53.0. 
            paymentItem.Name = parameters["itemName"];

            // Item quantity. This field is required when you pass a value for ItemCategory. 
            // For digital goods (ItemCategory=Digital), this field is required.
            // Character length and limitations: Any positive integer
            // This field is introduced in version 53.0. 
            paymentItem.Quantity = Convert.ToInt32(parameters["itemQuantity"]);
            BasicAmountType amount = new BasicAmountType();

            // Cost of item. This field is required when you pass a value for ItemCategory.
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes for
            // any of the supported PayPal currencies.
            // Character length and limitations: Value is a positive number which cannot 
            // exceed $10,000 USD in any currency. It includes no currency symbol.
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            // This field is introduced in version 53.0.   
            amount.value = parameters["amt"];

            // PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables.
            amount.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            paymentItem.Amount = amount;
            paymentItems.Add(paymentItem);
            paymentDetails.PaymentDetailsItem = paymentItems;

            // (Optional) Your URL for receiving Instant Payment Notification (IPN) 
            // about this transaction. If you do not specify this value in the request, 
            // the notification URL from your Merchant Profile is used, if one exists.
            // Important:
            // The notify URL applies only to DoExpressCheckoutPayment. 
            // This value is ignored when set in SetExpressCheckout or GetExpressCheckoutDetails.
            // Character length and limitations: 2,048 single-byte alphanumeric characters
            paymentDetails.NotifyURL = parameters["notifyURL"];

            List<PaymentDetailsType> payDetailType = new List<PaymentDetailsType>();
            payDetailType.Add(paymentDetails);
            
            // When implementing parallel payments, you can create up to 10 sets of payment 
            // details type parameter fields, each representing one payment you are hosting 
            // on your marketplace.
            details.PaymentDetails = payDetailType;

            doCheckoutPaymentRequestType.DoExpressCheckoutPaymentRequestDetails = details;
            DoExpressCheckoutPaymentReq doExpressCheckoutPaymentReq = new DoExpressCheckoutPaymentReq();
            doExpressCheckoutPaymentReq.DoExpressCheckoutPaymentRequest = doCheckoutPaymentRequestType;
            DoExpressCheckoutPaymentResponseType response = null;
            try
            {
                response = service.DoExpressCheckoutPayment(doExpressCheckoutPaymentReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.StackTrace);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
                responseValues.Add("PaymentType", parameters["paymentType"]);
                responseValues.Add("TransactionId", response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID);
            }
            else
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
            }
                
            Display(contextHttp, "DoExpressCheckout", "DoExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }
        /// <summary>
        /// Handles DoExpressCheckoutForParallelPayments
        /// </summary>
        /// <param name="contextHttp"></param>
        private void DoExpressCheckoutForParallelPayment(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<String, String> configurationMap = Configuration.GetAcctAndConfig();

            // Creating service wrapper object to make an API call by loading configuration map.
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            DoExpressCheckoutPaymentRequestType doCheckoutPaymentRequestType = new DoExpressCheckoutPaymentRequestType();
            DoExpressCheckoutPaymentRequestDetailsType details = new DoExpressCheckoutPaymentRequestDetailsType();

            // A timestamped token by which you identify to PayPal that you are processing
            // this payment with Express Checkout. The token expires after three hours. 
            // If you set the token in the SetExpressCheckout request, the value of the token
            // in the response is identical to the value in the request.
            // Character length and limitations: 20 single-byte characters
            details.Token = parameters["token"];

            // Unique PayPal Customer Account identification number.
            // Character length and limitations: 13 single-byte alphanumeric characters
            details.PayerID = parameters["payerID"];

            // (Optional) How you want to obtain payment. If the transaction does not include a one-time purchase, this field is ignored. 
            // It is one of the following values:
            // Sale – This is a final sale for which you are requesting payment (default).
            // Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            // Note:
            // You cannot set this field to Sale in SetExpressCheckout request and then change 
            // this value to Authorization or Order in the DoExpressCheckoutPayment request. 
            // If you set the field to Authorization or Order in SetExpressCheckout, you may set the field to Sale.
            // Character length and limitations: Up to 13 single-byte alphabetic characters
            // This field is deprecated. Use PaymentAction in PaymentDetailsType instead.
            details.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), parameters["paymentType"]);

            PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
            BasicAmountType orderTotal1 = new BasicAmountType();
            orderTotal1.value = parameters["orderTotal"];

            //PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables.
            orderTotal1.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            // (Required) The total cost of the transaction to the buyer. 
            // If shipping cost (not applicable to digital goods) and tax charges are known, 
            // include them in this value. If not, this value should be the current sub-total 
            // of the order. If the transaction includes one or more one-time purchases, this 
            // field must be equal to the sum of the purchases. Set this field to 0 if the 
            // transaction does not include a one-time purchase such as when you set up a 
            // billing agreement for a recurring payment that is not immediately charged. 
            // When the field is set to 0, purchase-specific fields are ignored. 
            // For digital goods, the following must be true:
            // total cost > 0
            // total cost <= total cost passed in the call to SetExpressCheckout
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // When multiple payments are passed in one transaction, all of the payments must 
            // have the same currency code.
            // Character length and limitations: Value is a positive number which cannot 
            // exceed $10,000 USD in any currency. It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            paymentDetails1.OrderTotal = orderTotal1;

            SellerDetailsType sellerDetailsType1 = new SellerDetailsType();
            sellerDetailsType1.PayPalAccountID = parameters["receiverEmail_0"];

            paymentDetails1.PaymentRequestID = parameters["paymentRequestID_0"];
            paymentDetails1.SellerDetails = sellerDetailsType1;

            PaymentDetailsType paymentDetails2 = new PaymentDetailsType();
            BasicAmountType orderTotal2 = new BasicAmountType();
            orderTotal2.value = parameters["orderTotal"];

            //PayPal uses 3-character ISO-4217 codes for specifying currencies in fields and variables.
            orderTotal2.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);

            // (Required) The total cost of the transaction to the buyer. 
            // If shipping cost (not applicable to digital goods) and tax charges are known, 
            // include them in this value. If not, this value should be the current sub-total 
            // of the order. If the transaction includes one or more one-time purchases, this 
            // field must be equal to the sum of the purchases. Set this field to 0 if the 
            // transaction does not include a one-time purchase such as when you set up a 
            // billing agreement for a recurring payment that is not immediately charged. 
            // When the field is set to 0, purchase-specific fields are ignored. 
            // For digital goods, the following must be true:
            // total cost > 0
            // total cost <= total cost passed in the call to SetExpressCheckout
            // Note:
            // You must set the currencyID attribute to one of the 3-character currency codes 
            // for any of the supported PayPal currencies.
            // When multiple payments are passed in one transaction, all of the payments must 
            // have the same currency code.
            // Character length and limitations: Value is a positive number which cannot 
            // exceed $10,000 USD in any currency. It includes no currency symbol. 
            // It must have 2 decimal places, the decimal separator must be a period (.), 
            // and the optional thousands separator must be a comma (,).
            paymentDetails2.OrderTotal = orderTotal2;

            SellerDetailsType sellerDetailsType2 = new SellerDetailsType();
            sellerDetailsType2.PayPalAccountID = parameters["receiverEmail_1"];

            paymentDetails2.PaymentRequestID = parameters["paymentRequestID_1"];
            paymentDetails2.SellerDetails = sellerDetailsType2;

            List<PaymentDetailsType> payDetailType = new List<PaymentDetailsType>();
            payDetailType.Add(paymentDetails1);
            payDetailType.Add(paymentDetails2);

            // When implementing parallel payments, you can create up to 10 sets of payment 
            // details type parameter fields, each representing one payment you are hosting 
            // on your marketplace.
            details.PaymentDetails = payDetailType;

            doCheckoutPaymentRequestType.DoExpressCheckoutPaymentRequestDetails = details;
            DoExpressCheckoutPaymentReq doExpressCheckoutPaymentReq = new DoExpressCheckoutPaymentReq();
            doExpressCheckoutPaymentReq.DoExpressCheckoutPaymentRequest = doCheckoutPaymentRequestType;
            DoExpressCheckoutPaymentResponseType response = null;
            try
            {
                response = service.DoExpressCheckoutPayment(doExpressCheckoutPaymentReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.StackTrace);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
                responseValues.Add("PaymentType", parameters["paymentType"]);
                responseValues.Add("TransactionId", response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID);
            }
            else
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
            }

            Display(contextHttp, "DoExpressCheckoutForParallelPayment", "DoExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }


        /// <summary>
        /// Handles DoCapture
        /// </summary>
        /// <param name="contextHttp"></param>
        private void DoCapture(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<String, String> configurationMap = Configuration.GetAcctAndConfig();

            // Creating service wrapper object to make an API call by loading configuration map.
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // ## DoCaptureReq
            DoCaptureReq req = new DoCaptureReq();
            // 'Amount' to capture which takes mandatory params:
            //
            // * 'currencyCode'
            // * 'amount'
            BasicAmountType amount = new BasicAmountType(((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"])), parameters["amt"]);

            // 'DoCaptureRequest' which takes mandatory params:
            //
            // * 'Authorization ID' - Authorization identification number of the
            // payment you want to capture. This is the transaction ID returned from
            // DoExpressCheckoutPayment, DoDirectPayment, or CheckOut. For
            // point-of-sale transactions, this is the transaction ID returned by
            // the CheckOut call when the payment action is Authorization.
            // * 'amount' - Amount to capture
            // * 'CompleteCode' - Indicates whether or not this is your last capture.
            // It is one of the following values:
            // * Complete – This is the last capture you intend to make.
            // * NotComplete – You intend to make additional captures.
            // 'Note:
            // If Complete, any remaining amount of the original authorized
            // transaction is automatically voided and all remaining open
            // authorizations are voided.'
            DoCaptureRequestType reqType = new DoCaptureRequestType
            (
                    parameters["authID"],
                    amount,
                    (CompleteCodeType)Enum.Parse(typeof(CompleteCodeType), parameters["completeCodeType"])
            );

            req.DoCaptureRequest = reqType;
            DoCaptureResponseType response = null;
            try
            {
                response = service.DoCapture(req);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.StackTrace);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;
            responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
            
            Display(contextHttp, "DoCapture", "DoCapture", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        // <summary>
        /// Handles DoAuthorization
        /// </summary>
        /// <param name="contextHttp"></param>
        private void DoAuthorization(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<String, String> configurationMap = Configuration.GetAcctAndConfig();

            // Creating service wrapper object to make an API call by loading configuration map.
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            DoAuthorizationReq req = new DoAuthorizationReq();

            // 'Amount' which takes mandatory params: 
            // * 'currencyCode'
            // * 'amount'
            BasicAmountType amount = new BasicAmountType(((CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"])), parameters["amt"]);

            // 'DoAuthorizationRequest' which takes mandatory params: 
            // * 'Transaction ID' - Value of the order's transaction identification
            // number returned by PayPal.
            // * 'Amount' - Amount to authorize.
            DoAuthorizationRequestType reqType = new DoAuthorizationRequestType(parameters["authID"], amount);

            req.DoAuthorizationRequest = reqType;
            DoAuthorizationResponseType response = null;
            try
            {
                response = service.DoAuthorization(req);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.StackTrace);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
                responseValues.Add("TransactionId", response.TransactionID);
            }
            else
            {
                responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
            }

            Display(contextHttp, "DoAuthorization", "DoAuthorization", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        /// <summary>
        /// Handles ParallelPayment
        /// </summary>
        /// <param name="contextHttp"></param>
        private void ParallelPayment(HttpContext contextHttp)
        {
            NameValueCollection parameters = contextHttp.Request.Params;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            SetExpressCheckoutRequestType setExpressCheckoutReq = new SetExpressCheckoutRequestType();
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();

            string requestUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();

            // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer confirms the order and payment or billing agreement.
            UriBuilder uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/DoExpressCheckoutForParallelPayment.aspx";
            string returnUrl = uriBuilder.Uri.ToString();

            // (Required) URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you. For digital goods, you must add JavaScript to this page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the original page on which the buyer chose to pay with PayPal or establish a billing agreement.
            uriBuilder = new UriBuilder(requestUrl);
            uriBuilder.Path = contextHttp.Request.ApplicationPath
                + (contextHttp.Request.ApplicationPath.EndsWith("/") ? string.Empty : "/")
                + "UseCaseSamples/DoExpressCheckout.aspx";
            string cancelUrl = uriBuilder.Uri.ToString();


            // (Required) URL to which the buyer's browser is returned after choosing 
            // to pay with PayPal. For digital goods, you must add JavaScript to this 
            // page to close the in-context experience.
            // Note:
            // PayPal recommends that the value be the final review page on which the buyer 
            // confirms the order and payment or billing agreement.
            // Character length and limitations: 2048 single-byte characters
            details.ReturnURL = returnUrl + "?currencyCodeType=" + parameters["currencyCode"];
            details.CancelURL = cancelUrl;


            // (Optional) Email address of the buyer as entered during checkout.
            // PayPal uses this value to pre-fill the PayPal membership sign-up portion on the PayPal pages.
            // Character length and limitations: 127 single-byte alphanumeric characters
            details.BuyerEmail = parameters["buyerMail"];

            SellerDetailsType seller1 = new SellerDetailsType();
            seller1.PayPalAccountID = parameters["receiverEmail_0"];
            PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
            paymentDetails1.SellerDetails = seller1;
            paymentDetails1.PaymentRequestID = parameters["paymentRequestID_0"];
            BasicAmountType orderTotal1 = new BasicAmountType();
            orderTotal1.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            orderTotal1.value = parameters["orderTotal"];
            paymentDetails1.OrderTotal = orderTotal1;

            SellerDetailsType seller2 = new SellerDetailsType();
            seller2.PayPalAccountID = parameters["receiverEmail_1"];
            PaymentDetailsType paymentDetails2 = new PaymentDetailsType();
            paymentDetails2.SellerDetails = seller2;
            paymentDetails2.PaymentRequestID = parameters["paymentRequestID_1"];
            BasicAmountType orderTotal2 = new BasicAmountType();
            orderTotal2.currencyID = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), parameters["currencyCode"]);
            orderTotal2.value = parameters["orderTotal"];
            paymentDetails2.OrderTotal = orderTotal2;

            List<PaymentDetailsType> payDetails = new List<PaymentDetailsType>();
            payDetails.Add(paymentDetails1);
            payDetails.Add(paymentDetails2);

            details.PaymentDetails = payDetails;
            setExpressCheckoutReq.SetExpressCheckoutRequestDetails = details;

            SetExpressCheckoutReq expressCheckoutReq = new SetExpressCheckoutReq();
            expressCheckoutReq.SetExpressCheckoutRequest = setExpressCheckoutReq;
            SetExpressCheckoutResponseType response = null;

            try
            {
                response = service.SetExpressCheckout(expressCheckoutReq);
            }
            catch (System.Exception ex)
            {
                contextHttp.Response.Write(ex.Message);
                return;
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;
            if (!response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.Ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString() + "_express-checkout&token=" + response.Token;
            }
            responseValues.Add("Acknowledgement", response.Ack.ToString().Trim().ToUpper());
            Display(contextHttp, "ParallelPayment", "SetExpressCheckout", responseValues, service.getLastRequest(), service.getLastResponse(), response.Errors, redirectUrl);
        }

        private string IndentXml(XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
            {
                throw new System.ArgumentNullException("xmlDoc");
            }
            MemoryStream streamMemory = new MemoryStream();
            XmlTextWriter textWriterXml = new XmlTextWriter(streamMemory, Encoding.Unicode);
            textWriterXml.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(textWriterXml);
            textWriterXml.Flush();
            streamMemory.Flush();

            streamMemory.Position = 0;
            StreamReader readerStream = new StreamReader(streamMemory);
            return readerStream.ReadToEnd();
        }

        private void Display(HttpContext contextHttp, string method, string api, Dictionary<string, string> responseValues, string requestPayload, string responsePayload, List<ErrorType> errorMessages, string redirectUrl)
        {
            titleName.Text = "PayPal Adaptive Payments - " + api;
            LabelName.Text = api;

            GridViewResponseValues.DataSource = responseValues;
            GridViewResponseValues.DataBind();
            if (errorMessages != null && errorMessages.Count > 0)
            {
                RepeaterError.DataSource = errorMessages;
                RepeaterError.DataBind();
                GridViewResponseValues.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);                
            }
            else
            {
                if (responseValues["Acknowledgement"].Equals(AckCode.SUCCESS.ToString()))
                {
                    GridViewResponseValues.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(0, 200, 100);
                }
                else
                {
                    GridViewResponseValues.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                }
                if (redirectUrl != null)
                {
                    LabelWebFlow.Text = "This API has Web Flow to redirect the user to complete the API call, please click the hyperlink to redirect the user to ";
                    HyperLinkWebFlow.Text = redirectUrl;
                    HyperLinkWebFlow.NavigateUrl = redirectUrl;
                    LabelWebFlowSuffix.Text = ".<br/><br/>";
                } 
            }            

            XmlDocument docRequest = new XmlDocument();
            docRequest.LoadXml(requestPayload);

            TextBoxRequest.Text = IndentXml(docRequest);

            XmlDocument docResponse = new XmlDocument();
            docResponse.LoadXml(responsePayload);
            TextBoxResponse.Text = IndentXml(docResponse);

            if (method == "DoExpressCheckout")
            {
                string paymentType = responseValues["PaymentType"];
                string transactionId = responseValues["TransactionId"];                

                if (paymentType.ToUpper() == "AUTHORIZATION")
                {
                    LabelNotePaymentType.Text = "Please Note<br/><br/>";
                    LabelPrefixPaymentType.Text = "As the Payment Type is AUTHORIZATION, you can Capture Payment directly by calling the ";
                    HyperLinkPaymentType.Text = "DoCapture";
                    HyperLinkPaymentType.NavigateUrl = "/UseCaseSamples/PaymentCapture.aspx?TransactionId=" + transactionId;
                    LabelSuffixPaymentType.Text = " API.<br/><br/>";
                }
                else if (paymentType.ToUpper() == "ORDER")
                {
                    LabelNotePaymentType.Text = "Please Note<br/><br/>";
                    LabelPrefixPaymentType.Text = "As the Payment Type is ORDER, you have to call the ";
                    HyperLinkPaymentType.Text = "DoAuthorization";
                    HyperLinkPaymentType.NavigateUrl = "/UseCaseSamples/DoAuthorizationForOrderPayment.aspx?TransactionId=" + transactionId;
                    LabelSuffixPaymentType.Text = " API, before you can Capture Payment by calling the DoCapture API.<br/><br/>";
                }
            }

            if (method == "DoAuthorization")
            {
                string transactionId = responseValues["TransactionId"];

                LabelNoteTransactionId.Text = "Please Note<br/><br/>";
                LabelPrefixTransactionId.Text = "You can Capture Payment by calling the ";
                HyperLinkTransactionId.Text = "DoCapture";
                HyperLinkTransactionId.NavigateUrl = "/UseCaseSamples/PaymentCapture.aspx?TransactionId=" + transactionId;
                LabelSuffixTransactionId.Text = " API.<br/><br/>";
            }
        }
    }
}