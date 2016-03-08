using System;

namespace PizzaApp
{
    public partial class Error : System.Web.UI.Page
    {
        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailLinkButton_Click(object sender, EventArgs e)
        {
            if (this.MessagePanel.Visible)
            {
                MessagePanel.Visible = false;
                DetailLinkButton.Text = "Show Details";
            }
            else
            {
                if (Message.LastException != null)
                {
                    this.MessageTextBox.Text = "Error Caught in Application_Error event\n" +
                        "Error in: " + Request.Url.ToString() +
                        "\n\nError Message:" + Message.LastException.Message.ToString() +
                        "\n\nStack Trace:" + Message.LastException.StackTrace.ToString();
                }
                MessagePanel.Visible = true;
                DetailLinkButton.Text = "Hide Details";
            }
        }
        #endregion
    }
}