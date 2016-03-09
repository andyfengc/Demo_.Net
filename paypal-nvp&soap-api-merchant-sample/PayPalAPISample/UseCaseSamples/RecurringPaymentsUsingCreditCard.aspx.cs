using System;

namespace PayPalAPISample.UseCaseSamples
{
    public partial class RecurringPaymentsUsingCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                billingStartDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        protected void CalendarDate_SelectionChanged(object sender, EventArgs e)
        {
            // (Required) The date when billing for this profile begins.
            // Note: The profile may take up to 24 hours for activation.
            billingStartDate.Text = CalendarDate.SelectedDate.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}