using System;
using System.Collections.Generic;
using System.Web;

using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PayPalAPISample.APICalls
{
    // The UpdateRecurringPaymentsProfile API operation updates a recurring payments profile.
    public partial class UpdateRecurringPaymentsProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            billingStartDate.Text = calDate.SelectedDate.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Create request object
            UpdateRecurringPaymentsProfileRequestType request = new UpdateRecurringPaymentsProfileRequestType();
            populateRequestObject(request);

            // Invoke the API
            UpdateRecurringPaymentsProfileReq wrapper = new UpdateRecurringPaymentsProfileReq();
            wrapper.UpdateRecurringPaymentsProfileRequest = request;

            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);
            // # API call 
            // Invoke the UpdateRecurringPaymentsProfile method in service wrapper object  
            UpdateRecurringPaymentsProfileResponseType updateRPProfileResponse = service.UpdateRecurringPaymentsProfile(wrapper);

            // Check for API return status            
            setKeyResponseObjects(service, updateRPProfileResponse);
        }

        private void populateRequestObject(UpdateRecurringPaymentsProfileRequestType request)
        {

            
            CurrencyCodeType currency = (CurrencyCodeType)
                Enum.Parse(typeof(CurrencyCodeType), "USD");
            // Set EC-Token or Credit card requestDetails
            UpdateRecurringPaymentsProfileRequestDetailsType profileDetails = new UpdateRecurringPaymentsProfileRequestDetailsType();
            request.UpdateRecurringPaymentsProfileRequestDetails = profileDetails;

            // (Required) Recurring payments profile ID returned in the CreateRecurringPaymentsProfile response.
            profileDetails.ProfileID = profileId.Value;
            // (Optional) The reason for the update to the recurring payments profile. This message is included in the email notification to the buyer for the recurring payments profile update. This note can also be seen by both you and the buyer on the Status History page of the PayPal account.
            if (note.Value != string.Empty)
            {
                profileDetails.Note = note.Value;                                                                                                                                                                                                                               
            }            
            // Populate Recurring Payments Profile Details
            // (Optional) Full name of the person receiving the product or service paid for by the recurring payment. If not present, the name in the buyer's PayPal account is used.
            if (subscriberName.Value != string.Empty)
            {
                profileDetails.SubscriberName = subscriberName.Value;
            }
            // (Optional) The subscriber's shipping address associated with this profile, if applicable. If you do not specify it, the ship-to address from buyer's PayPal account is used.
            // Note: Shipping Address is optional, but if you update any of the address fields, you must enter all of them. For example, if you want to update the subscriber's street address, you must specify all of the fields listed in ShipTo: AddressType, not just the field for the street address.
            if (shippingName.Value != string.Empty && shippingStreet1.Value != string.Empty && shippingCity.Value != string.Empty
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
                if (shippingStreet2.Value != string.Empty)
                {
                    shippingAddr.Street2 = shippingStreet2.Value;
                }
                // (Optional) Phone number.
                if (shippingPhone.Value != string.Empty)
                {
                    shippingAddr.Phone = shippingPhone.Value;
                }
                profileDetails.SubscriberShippingAddress = shippingAddr;
            }
            // (Optional) The number of additional billing cycles to add to this profile.
            if (additionalBillingCycles.Value != string.Empty)
            {
                profileDetails.AdditionalBillingCycles = Convert.ToInt32(additionalBillingCycles.Value);
            }
            // (Optional) Billing amount for each cycle in the subscription period, not including shipping and tax amounts.
            // Note: For recurring payments with Express Checkout, the payment amount can be increased by no more than 20% every 180 days (starting when the profile is created).
            if (amount.Value != string.Empty)
            {
                profileDetails.Amount = new BasicAmountType(currency, amount.Value);
            }
            // (Optional) The current past due or outstanding amount for this profile. You can only decrease the outstanding amount. It cannot be increased.
            if (outstandingBalance.Value != string.Empty)
            {
                profileDetails.OutstandingBalance =
                        new BasicAmountType(currency, outstandingBalance.Value);
            }
            // (Optional) The number of failed payments allowed before the profile is automatically suspended. The specified value cannot be less than the current number of failed payments for this profile.
            if (maxFailedPayments.Value != string.Empty)
            {
                profileDetails.MaxFailedPayments = Convert.ToInt32(maxFailedPayments.Value);
            }
            // (Optional) This field indicates whether you would like PayPal to automatically bill the outstanding balance amount in the next billing cycle. It is one of the following values:
            // * NoAutoBill – PayPal does not automatically bill the outstanding balance.
            // * AddToNextBilling – PayPal automatically bills the outstanding balance
            if (autoBillOutstandingAmount.SelectedIndex != 0)
            {
                profileDetails.AutoBillOutstandingAmount = (AutoBillType)
                    Enum.Parse(typeof(AutoBillType), autoBillOutstandingAmount.SelectedValue);
            }
            // (Optional) The date when billing for this profile begins.
            // Note: The profile may take up to 24 hours for activation.
            if(billingStartDate.Text != string.Empty){
                profileDetails.BillingStartDate = billingStartDate.Text;
            }
            // (Optional) Credit card information for this profile, if applicable. Credit card billing address is optional, but if you update any of the address fields, you must enter all of them. For example, if you want to update the street address, you must specify all of the address fields listed in CreditCardDetailsType, not just the field for the street address.
            // Note: Only enter credit card details for recurring payments with direct payments.
            if (creditCardNumber.Value != string.Empty && cvv.Value != string.Empty)
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
            
            // (Optional) The trial period for this schedule.
            if (trialBillingAmount.Value != string.Empty && trialBillingFrequency.Value != string.Empty
                    && trialBillingCycles.Value != string.Empty)
            {
                BillingPeriodDetailsType_Update trialPeriod = new BillingPeriodDetailsType_Update();
                //Unit for billing during this subscription period; 
                //required if you specify an optional trial period. 
                //It is one of the following values: [Day, Week, SemiMonth, Month, Year]
                //For SemiMonth, billing is done on the 1st and 15th of each month.
                //Note:
                //The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                trialPeriod.BillingPeriod = (BillingPeriodType)
                    Enum.Parse(typeof(BillingPeriodType), trialBillingPeriod.SelectedValue);
                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                trialPeriod.BillingFrequency = Convert.ToInt32(trialBillingFrequency.Value);
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
                trialPeriod.Amount = new BasicAmountType(currency, trialBillingAmount.Value);

                trialPeriod.TotalBillingCycles = Convert.ToInt32(trialBillingCycles.Value);
                //(Optional) Shipping amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                if (trialShippingAmount.Value != string.Empty)
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
                if (trialTaxAmount.Value != string.Empty)
                {
                    trialPeriod.TaxAmount = new BasicAmountType(currency, trialTaxAmount.Value);
                }
                profileDetails.TrialPeriod = trialPeriod;
            }
            // (Optional) The regular payment period for this schedule.
            if (billingAmount.Value != string.Empty && billingFrequency.Value != string.Empty
                    && totalBillingCycles.Value != string.Empty)
            {
                BillingPeriodDetailsType_Update paymentPeriod = new BillingPeriodDetailsType_Update();
                //Unit for billing during this subscription period; 
                //required if you specify an optional trial period. 
                //It is one of the following values: [Day, Week, SemiMonth, Month, Year]
                //For SemiMonth, billing is done on the 1st and 15th of each month.
                //Note:
                //The combination of BillingPeriod and BillingFrequency cannot exceed one year.
                paymentPeriod.BillingPeriod = (BillingPeriodType)
                    Enum.Parse(typeof(BillingPeriodType), billingPeriod.SelectedValue);
                // Number of billing periods that make up one billing cycle; 
                // required if you specify an optional trial period.
                // The combination of billing frequency and billing period must be 
                // less than or equal to one year. For example, if the billing cycle is Month,
                // the maximum value for billing frequency is 12. Similarly, 
                // if the billing cycle is Week, the maximum value for billing frequency is 52.
                // Note:
                // If the billing period is SemiMonth, the billing frequency must be 1.
                paymentPeriod.BillingFrequency = Convert.ToInt32(billingFrequency.Value);
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
                paymentPeriod.Amount = new BasicAmountType(currency, billingAmount.Value);

                paymentPeriod.TotalBillingCycles = Convert.ToInt32(totalBillingCycles.Value);
                //(Optional) Shipping amount for each billing cycle during this payment period.
                //Note:
                //All amounts in the request must have the same currency.
                if (trialShippingAmount.Value != string.Empty)
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
                if (trialTaxAmount.Value != string.Empty)
                {
                    paymentPeriod.TaxAmount = new BasicAmountType(currency, taxAmount.Value);
                }
                profileDetails.PaymentPeriod = paymentPeriod;
            }
        }

        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, UpdateRecurringPaymentsProfileResponseType response)
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
            }
            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "UpdateRecurringPaymentsProfile");
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            Server.Transfer("../APIResponse.aspx");
        }

    }
}
