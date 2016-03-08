using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Web.Security;
using PayPal;
using PayPal.Api.Payments;

namespace PizzaApp
{
    public partial class SignUp : System.Web.UI.Page
    {
        #region Data
        private DataAccessLayer dataAccessObject;

        private bool CheckIsExistingUser()
        {
            bool isExistingUser = false;
            DataTable datTable = new DataTable();
            int rows = 0;
            var email = TextBoxEmail.Text.Trim();
            StringBuilder sqliteQuerySelect = new StringBuilder();
            sqliteQuerySelect.Append("SELECT ");
            sqliteQuerySelect.Append("count(*) AS NumberOfUsers ");
            sqliteQuerySelect.Append("FROM users ");
            sqliteQuerySelect.Append("WHERE email = @email");
            SQLiteDataAdapter sqliteDataAdapterSelect = new SQLiteDataAdapter();
            sqliteDataAdapterSelect.SelectCommand = new SQLiteCommand();
            sqliteDataAdapterSelect.SelectCommand.Parameters.AddWithValue("@email", email);
            dataAccessObject = new DataAccessLayer();
            datTable = dataAccessObject.Select(sqliteQuerySelect.ToString(), sqliteDataAdapterSelect);

            if (datTable != null && datTable.Rows.Count > 0)
            {
                var distinctRows = from DataRow dRow in datTable.Rows
                                   select new { column1 = dRow["NumberOfUsers"] };
                if (distinctRows != null)
                {
                    foreach (var row in distinctRows)
                    {
                        rows = Convert.ToInt32(row.column1);
                        break;
                    }
                }
            }
            if (rows == 1)
            {
                isExistingUser = true;
            }
            return isExistingUser;
        }

        private bool Insert()
        {
            bool isSuccess = false;
            int rowsAffacted = 0;
            var email = TextBoxEmail.Text.Trim();
            var password = TextBoxPassword.Text.Trim();
            var passwordConfirmation = TextBoxConfirmPassword.Text.Trim();
            var encryptedPassword = Secure.Encrypt(password);
            var signInCount = 1;
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
            var currentSignInAt = dateTimeNow;
            var lastSignInAt = currentSignInAt;

            // Set first (current) signed in IP Address
            var signInIPAddress = string.Empty;
            var currentSignInIP = signInIPAddress;
            var lastSignInIP = signInIPAddress;

            var createdAt = dateTimeNow;
            var updatedAt = createdAt;
            CreditCard credCard = CreateCreditCard();
            var creditCardID = credCard.id;
            var creditCardDescription = credCard.number;
            StringBuilder sqliteQueryInsert = new StringBuilder();
            sqliteQueryInsert.Append("INSERT INTO users");
            sqliteQueryInsert.Append("(");
            sqliteQueryInsert.Append("email, ");
            sqliteQueryInsert.Append("encrypted_password, ");
            sqliteQueryInsert.Append("sign_in_count, ");
            sqliteQueryInsert.Append("current_sign_in_at,");
            sqliteQueryInsert.Append("last_sign_in_at, ");
            sqliteQueryInsert.Append("current_sign_in_ip, ");
            sqliteQueryInsert.Append("last_sign_in_ip, ");
            sqliteQueryInsert.Append("created_at, ");
            sqliteQueryInsert.Append("updated_at, ");
            sqliteQueryInsert.Append("credit_card_id, ");
            sqliteQueryInsert.Append("credit_card_description ");
            sqliteQueryInsert.Append(") ");
            sqliteQueryInsert.Append("VALUES ");
            sqliteQueryInsert.Append("(");
            sqliteQueryInsert.Append("@email, ");
            sqliteQueryInsert.Append("@encrypted_password, ");
            sqliteQueryInsert.Append("@sign_in_count, ");
            sqliteQueryInsert.Append("@current_sign_in_at,");
            sqliteQueryInsert.Append("@last_sign_in_at, ");
            sqliteQueryInsert.Append("@current_sign_in_ip, ");
            sqliteQueryInsert.Append("@last_sign_in_ip, ");
            sqliteQueryInsert.Append("@created_at, ");
            sqliteQueryInsert.Append("@updated_at, ");
            sqliteQueryInsert.Append("@credit_card_id, ");
            sqliteQueryInsert.Append("@credit_card_description ");
            sqliteQueryInsert.Append(")");
            SQLiteDataAdapter sqliteDataAdapterInsert = new SQLiteDataAdapter();
            sqliteDataAdapterInsert.InsertCommand = new SQLiteCommand();
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@email", email);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@encrypted_password", encryptedPassword);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@sign_in_count", signInCount);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@current_sign_in_at", currentSignInAt);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@last_sign_in_at", lastSignInAt);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@current_sign_in_ip", currentSignInIP);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@last_sign_in_ip", lastSignInIP);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@created_at", createdAt);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@updated_at", updatedAt);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@credit_card_id", creditCardID);
            sqliteDataAdapterInsert.InsertCommand.Parameters.AddWithValue("@credit_card_description", creditCardDescription);
            dataAccessObject = new DataAccessLayer();
            rowsAffacted = dataAccessObject.Insert(sqliteQueryInsert.ToString(), sqliteDataAdapterInsert);
            if (rowsAffacted > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }
        #endregion

        #region PayPal
        private string AccessToken
        {
            get
            {
                string token = new OAuthTokenCredential
                                (
                                   "EBWKjlELKMYqRNQ6sYvFo64FtaRLRR5BdHEESmha49TM",
                                    "EO422dn3gQLgDbuwqTjzrFgFtaRLRR5BdHEESmha49TM",
                                    Configuration.GetConfiguration()
                                ).GetAccessToken();
                return token;
            }
        }

        private APIContext Api
        {
            get
            {
                APIContext context = new APIContext(AccessToken);
                context.Config = Configuration.GetConfiguration();
                return context;
            }
        }

        public CreditCard CreateCreditCard()
        {
            CreditCard CrdtCard = null;
            CreditCard credCard = new CreditCard();
            credCard.number = TextBoxCreditCardNumber.Text.Trim();
            credCard.cvv2 = TextBoxCreditCardCVV2.Text.Trim();
            credCard.type = DropDownListCreditCardType.SelectedValue.ToString().Trim();
            credCard.expire_month = System.Convert.ToInt32(DropDownListCreditCardExpireMonth.SelectedValue.ToString().Trim());
            credCard.expire_year = System.Convert.ToInt32(DropDownListCreditCardExpireYear.SelectedValue.ToString().Trim());
            CrdtCard = credCard.Create(Api);
            return CrdtCard;
        }
        #endregion PayPal
        
        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSignUp_Click(object sender, EventArgs e)
        {
            bool isExistingUser = CheckIsExistingUser();

            if (isExistingUser)
            {
                divAlertMessage.Visible = true;
                divAlertMessage.Attributes["class"] = "alert fade in alert-error";
                LabelAlertMessage.Text = "Email already exists.";
                TextBoxEmail.Focus();
            }
            else
            {
                bool isSuccess = Insert();
                if (isSuccess)
                {
                    FormsAuthentication.RedirectFromLoginPage(TextBoxEmail.Text.Trim(), false);
                }
            }
        }
        #endregion
    }
}