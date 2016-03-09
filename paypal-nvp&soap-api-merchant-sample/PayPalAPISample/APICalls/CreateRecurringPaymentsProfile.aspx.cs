using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;


namespace PayPalAPISample.APICalls
{
    // The CreateRecurringPaymentsProfile API operation creates a recurring payments profile. You must invoke the CreateRecurringPaymentsProfile API operation for each profile you want to create. The API operation creates a profile and an associated billing agreement.
    public partial class CreateRecurringPaymentsProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            // (Required) The date when billing for this profile begins.
            // Note: The profile may take up to 24 hours for activation.
            billingStartDate.Text = calDate.SelectedDate.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            CreateRecurringPaymentsProfileRequestType request = new CreateRecurringPaymentsProfileRequestType();
            populateRequest(request);

            // Invoke the API
            CreateRecurringPaymentsProfileReq wrapper = new CreateRecurringPaymentsProfileReq();
            wrapper.CreateRecurringPaymentsProfileRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the CreateRecurringPaymentsProfile method in service wrapper object  
            CreateRecurringPaymentsProfileResponseType createRPProfileResponse = service.CreateRecurringPaymentsProfile(wrapper);

            // Check for API return status            
            setKeyResponseObjects(service, createRPProfileResponse);
        }

        private void populateRequest(CreateRecurringPaymentsProfileRequestType request)
        {
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), "USD");

            // Set EC-Token or Credit card requestDetails
            CreateRecurringPaymentsProfileRequestDetailsType profileDetails = new CreateRecurringPaymentsProfileRequestDetailsType();
            request.CreateRecurringPaymentsProfileRequestDetails = profileDetails;
            // A timestamped token, the value of which was returned in the response to the first call to SetExpressCheckout. You can also use the token returned in the SetCustomerBillingAgreement response. Either this token or a credit card number is required. If you include both token and credit card number, the token is used and credit card number is ignored Call CreateRecurringPaymentsProfile once for each billing agreement included in SetExpressCheckout request and use the same token for each call. Each CreateRecurringPaymentsProfile request creates a single recurring payments profile.
            // Note: Tokens expire after approximately 3 hours.
            if(token.Value != string.Empty) 
            {
                profileDetails.Token = token.Value;
            }
            // Credit card information for recurring payments using direct payments. Either a token or a credit card number is required. If you include both token and credit card number, the token is used and credit card number is ignored.
            else if (creditCardNumber.Value != string.Empty && cvv.Value != string.Empty) 
            {
                CreditCardDetailsType cc = new CreditCardDetailsType();
                // (Required) Credit card number.
                cc.CreditCardNumber = creditCardNumber.Value;
                // Card Verification Value, version 2. Your Merchant Account settings determine whether this field is required. To comply with credit card processing regulations, you must not store this value after a transaction has been completed.
                cc.CVV2 = cvv.Value;
                // (Required) Credit card expiration month.
                cc.ExpMonth = Convert.ToInt32(expMonth.SelectedValue);
                // (Required) Credit card expiration year.
                cc.ExpYear = Convert.ToInt32(expYear.SelectedValue);
                profileDetails.CreditCard = cc;
            }


            // Populate Recurring Payments Profile Details
            RecurringPaymentsProfileDetailsType rpProfileDetails = 
                new RecurringPaymentsProfileDetailsType(billingStartDate.Text);
            profileDetails.RecurringPaymentsProfileDetails = rpProfileDetails;
            // (Optional) Full name of the person receiving the product or service paid for by the recurring payment. If not present, the name in the buyer's PayPal account is used.
            if(subscriberName.Value != string.Empty) 
            {
                rpProfileDetails.SubscriberName = subscriberName.Value;
            }

            // (Optional) The subscriber's shipping address associated with this profile, if applicable. If not specified, the ship-to address from buyer's PayPal account is used.
            if(shippingName.Value != string.Empty && shippingStreet1.Value != string.Empty && shippingCity.Value != string.Empty
                && shippingState.Value != string.Empty && shippingPostalCode.Value != string.Empty && shippingCountry.Value != string.Empty) 
            {
                AddressType shippingAddr = new AddressType();
                // Person's name associated with this shipping address. It is required if using a shipping address.
                shippingAddr.Name = shippingName.Value;
                // First street address. It is required if using a shipping address.
                shippingAddr.Street1 = shippingStreet1.Value;
                // Name of city. It is required if using a shipping address.
                shippingAddr.CityName = shippingCity.Value;
                // State or province. It is required if using a shipping address.
                shippingAddr.StateOrProvince = shippingState.Value;
                // Country code. It is required if using a shipping address.
                shippingAddr.CountryName = shippingCountry.Value;
                // U.S. ZIP code or other country-specific postal code. It is required if using a U.S. shipping address; may be required for other countries.
                shippingAddr.PostalCode = shippingPostalCode.Value;

                // (Optional) Second street address.
                if(shippingStreet2.Value != string.Empty) 
                {
                    shippingAddr.Street2 = shippingStreet2.Value;
                }
                // (Optional) Phone number.
                if(shippingPhone.Value != string.Empty) 
                {
                    shippingAddr.Phone = shippingPhone.Value;
                }
                rpProfileDetails.SubscriberShippingAddress = shippingAddr;
            }


            // (Required) Describes the recurring payments schedule, including the regular payment period, whether there is a trial period, and the number of payments that can fail before a profile is suspended.
            ScheduleDetailsType scheduleDetails = new ScheduleDetailsType();
            // (Required) Description of the recurring payment.
            // Note: You must ensure that this field matches the corresponding billing agreement description included in the SetExpressCheckout request.
            if(profileDescription.Value != string.Empty) 
            {
                scheduleDetails.Description = profileDescription.Value;
            }
            // (Optional) Number of scheduled payments that can fail before the profile is automatically suspended. An IPN message is sent to the merchant when the specified number of failed payments is reached.
            if(maxFailedPayments.Value != string.Empty) 
            {
                scheduleDetails.MaxFailedPayments = Convert.ToInt32(maxFailedPayments.Value);
            }
            // (Optional) Indicates whether you would like PayPal to automatically bill the outstanding balance amount in the next billing cycle. The outstanding balance is the total amount of any previously failed scheduled payments that have yet to be successfully paid. It is one of the following values:
            // * NoAutoBill – PayPal does not automatically bill the outstanding balance.
            // * AddToNextBilling – PayPal automatically bills the outstanding balance.
            if(autoBillOutstandingAmount.SelectedIndex != 0) 
            {
                scheduleDetails.AutoBillOutstandingAmount = (AutoBillType)
                    Enum.Parse(typeof(AutoBillType), autoBillOutstandingAmount.SelectedValue);
            }
            // (Optional) Information about activating a profile, such as whether there is an initial non-recurring payment amount immediately due upon profile creation and how to override a pending profile PayPal suspends when the initial payment amount fails.
            if(initialAmount.Value != string.Empty)
            {
                // (Optional) Initial non-recurring payment amount due immediately upon profile creation. Use an initial amount for enrolment or set-up fees.
                // Note: All amounts included in the request must have the same currency.
                ActivationDetailsType activationDetails = 
                    new ActivationDetailsType(new BasicAmountType(currency, initialAmount.Value));
                // (Optional) Action you can specify when a payment fails. It is one of the following values:
                // * ContinueOnFailure – By default, PayPal suspends the pending profile in the event that the initial payment amount fails. You can override this default behavior by setting this field to ContinueOnFailure. Then, if the initial payment amount fails, PayPal adds the failed payment amount to the outstanding balance for this recurring payment profile.
                //   When you specify ContinueOnFailure, a success code is returned to you in the CreateRecurringPaymentsProfile response and the recurring payments profile is activated for scheduled billing immediately. You should check your IPN messages or PayPal account for updates of the payment status.
                // * CancelOnFailure – If this field is not set or you set it to CancelOnFailure, PayPal creates the recurring payment profile, but places it into a pending status until the initial payment completes. If the initial payment clears, PayPal notifies you by IPN that the pending profile has been activated. If the payment fails, PayPal notifies you by IPN that the pending profile has been canceled.
                if(failedInitialAmountAction.SelectedIndex != 0) 
                {
                    activationDetails.FailedInitialAmountAction = (FailedPaymentActionType)
                        Enum.Parse(typeof(FailedPaymentActionType), failedInitialAmountAction.SelectedValue);
                }
                scheduleDetails.ActivationDetails = activationDetails;
            }
            // (Optional) Trial period for this schedule.
            if(trialBillingAmount.Value != string.Empty && trialBillingFrequency.Value != string.Empty 
                    && trialBillingCycles.Value != string.Empty) 
            {
                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                int frequency = Convert.ToInt32(trialBillingFrequency.Value); 
                
                //Billing amount for each billing cycle during this payment period; 
                //required if you specify an optional trial period. 
                //This amount does not include shipping and tax amounts.
                //Note:
                //All amounts in the CreateRecurringPaymentsProfile request must have 
                //the same currency.
                //Character length and limitations: 
                //Value is a positive number which cannot exceed $10,000 USD in any currency. 
                //It includes no currency symbol. 
                //It must have 2 decimal places, the decimal separator must be a period (.),
                //and the optional thousands separator must be a comma (,).
                BasicAmountType paymentAmount = new BasicAmountType(currency, trialBillingAmount.Value);

                //Unit for billing during this subscription period; 
                //required if you specify an optional trial period. 
                //It is one of the following values: [Day, Week, SemiMonth, Month, Year]
                //For SemiMonth, billing is done on the 1st and 15th of each month.
                //Note:
                //The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                BillingPeriodType period = (BillingPeriodType)
                    Enum.Parse(typeof(BillingPeriodType), trialBillingPeriod.SelectedValue);

                //Number of billing periods that make up one billing cycle; 
                //required if you specify an optional trial period.
                //The combination of billing frequency and billing period must be 
                //less than or equal to one year. For example, if the billing cycle is Month,
                //the maximum value for billing frequency is 12. Similarly, 
                //if the billing cycle is Week, the maximum value for billing frequency is 52.
                //Note:
                //If the billing period is SemiMonth, the billing frequency must be 1.
                int numCycles = Convert.ToInt32(trialBillingCycles.Value);

                BillingPeriodDetailsType trialPeriod = new BillingPeriodDetailsType(period, frequency, paymentAmount);
                trialPeriod.TotalBillingCycles = numCycles;
                //(Optional) Shipping amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                if(trialShippingAmount.Value != string.Empty) 
                {
                    trialPeriod.ShippingAmount = new BasicAmountType(currency, trialShippingAmount.Value);
                }
                //(Optional) Tax amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                //Character length and limitations: 
                //Value is a positive number which cannot exceed $10,000 USD in any currency.
                //It includes no currency symbol. It must have 2 decimal places, 
                //the decimal separator must be a period (.), and the optional 
                //thousands separator must be a comma (,).
                if(trialTaxAmount.Value != string.Empty)
                {
                    trialPeriod.TaxAmount = new BasicAmountType(currency, trialTaxAmount.Value);
                }

                scheduleDetails.TrialPeriod = trialPeriod;
            }
            // (Required) Regular payment period for this schedule.
            if(billingAmount.Value != string.Empty && billingFrequency.Value != string.Empty 
                    && totalBillingCycles.Value != string.Empty) 
            {
                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                int frequency = Convert.ToInt32(billingFrequency.Value);
                //Billing amount for each billing cycle during this payment period; 
                //required if you specify an optional trial period. 
                //This amount does not include shipping and tax amounts.
                //Note:
                //All amounts in the CreateRecurringPaymentsProfile request must have 
                //the same currency.
                //Character length and limitations: 
                //Value is a positive number which cannot exceed $10,000 USD in any currency. 
                //It includes no currency symbol. 
                //It must have 2 decimal places, the decimal separator must be a period (.),
                //and the optional thousands separator must be a comma (,).
                BasicAmountType paymentAmount = new BasicAmountType(currency, billingAmount.Value);
                BillingPeriodType period = (BillingPeriodType)
                    Enum.Parse(typeof(BillingPeriodType), billingPeriod.SelectedValue);
                //Number of billing periods that make up one billing cycle; 
                //required if you specify an optional trial period.
                //The combination of billing frequency and billing period must be 
                //less than or equal to one year. For example, if the billing cycle is Month,
                //the maximum value for billing frequency is 12. Similarly, 
                //if the billing cycle is Week, the maximum value for billing frequency is 52.
                //Note:
                //If the billing period is SemiMonth, the billing frequency must be 1.
                int numCycles = Convert.ToInt32(totalBillingCycles.Value);

                //Unit for billing during this subscription period; 
                //required if you specify an optional trial period. 
                //It is one of the following values: [Day, Week, SemiMonth, Month, Year]
                //For SemiMonth, billing is done on the 1st and 15th of each month.
                //Note:
                //The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                BillingPeriodDetailsType paymentPeriod = new BillingPeriodDetailsType(period, frequency, paymentAmount);
                paymentPeriod.TotalBillingCycles = numCycles;
                //(Optional) Shipping amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                if(trialShippingAmount.Value != string.Empty) 
                {
                    paymentPeriod.ShippingAmount = new BasicAmountType(currency, shippingAmount.Value);
                }

                //(Optional) Tax amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                //Character length and limitations: 
                //Value is a positive number which cannot exceed $10,000 USD in any currency.
                //It includes no currency symbol. It must have 2 decimal places, 
                //the decimal separator must be a period (.), and the optional 
                //thousands separator must be a comma (,).
                if(trialTaxAmount.Value != string.Empty)
                {
                    paymentPeriod.TaxAmount = new BasicAmountType(currency, taxAmount.Value);                     
                }
                scheduleDetails.PaymentPeriod = paymentPeriod;
            }
            profileDetails.ScheduleDetails = scheduleDetails;
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, CreateRecurringPaymentsProfileResponseType response)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("API Status", response.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_redirectURL", null);
            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("Transaction Id", response.CreateRecurringPaymentsProfileResponseDetails.TransactionID);
                responseParams.Add("Profile Id", response.CreateRecurringPaymentsProfileResponseDetails.ProfileID);
                responseParams.Add("Profile Status", response.CreateRecurringPaymentsProfileResponseDetails.ProfileStatus.ToString());
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "CreateRecurringPaymentsProfile");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            Server.Transfer("../APIResponse.aspx");
        }

    }
}
