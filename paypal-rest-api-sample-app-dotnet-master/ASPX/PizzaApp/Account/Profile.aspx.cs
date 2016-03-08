﻿using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SQLite;
using PayPal;
using PayPal.Api.Payments;

namespace PizzaApp
{
    public partial class Profile : System.Web.UI.Page
    {
        #region Data
        DataAccessLayer dataAccessObject;

        private DataTable GetUser(string email)
        {
            DataTable datTable = new DataTable();
            StringBuilder sqliteQuerySelect = new StringBuilder();
            sqliteQuerySelect.Append("SELECT ");
            sqliteQuerySelect.Append("id, ");
            sqliteQuerySelect.Append("email, ");
            sqliteQuerySelect.Append("encrypted_password, ");
            sqliteQuerySelect.Append("sign_in_count, ");
            sqliteQuerySelect.Append("current_sign_in_at, ");
            sqliteQuerySelect.Append("last_sign_in_at, ");
            sqliteQuerySelect.Append("last_sign_in_ip, ");
            sqliteQuerySelect.Append("created_at, ");
            sqliteQuerySelect.Append("updated_at, ");
            sqliteQuerySelect.Append("credit_card_id, ");
            sqliteQuerySelect.Append("credit_card_description ");
            sqliteQuerySelect.Append("FROM users ");
            sqliteQuerySelect.Append("WHERE email = @email");
            SQLiteDataAdapter sqliteDataAdapterSelect = new SQLiteDataAdapter();
            sqliteDataAdapterSelect.SelectCommand = new SQLiteCommand();
            sqliteDataAdapterSelect.SelectCommand.Parameters.AddWithValue("@email", email);
            dataAccessObject = new DataAccessLayer();
            datTable = dataAccessObject.Select(sqliteQuerySelect.ToString(), sqliteDataAdapterSelect);
            return datTable;
        }

        private bool IsPasswordValid(string email, string password)
        {
            bool isValid = false;

            string decryptedPassword = null;
            DataTable datTable = GetUser(email);
            if (datTable != null && datTable.Rows.Count > 0)
            {
                var distinctRows = from DataRow dRow in datTable.Rows
                                   where dRow.Field<string>("email") == email
                                   select new { column1 = dRow["encrypted_password"] };
                if (distinctRows != null)
                {
                    foreach (var row in distinctRows)
                    {
                        string encryptedPassword = row.column1.ToString();
                        decryptedPassword = Secure.Decrypt(encryptedPassword);
                        break;
                    }
                }
                if (password.Trim().Equals(decryptedPassword.Trim()))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        private bool Update(string email)
        {
            bool isSuccess = false;

            int rowsAffacted = 0;
            var newPassword = TextBoxNewPassword.Text.Trim();
            var confirmNewPassword = TextBoxConfirmNewPassword.Text.Trim();
            var encryptedNewPassword = Secure.Encrypt(newPassword);
            var signInCount = 0;
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
            var currentSignInAt = dateTimeNow;

            // Set last signed in IP Address from database
            var lastSignInAt = string.Empty;

            // Set first signed in IP Address from database
            var signInIPAddress = string.Empty;

            // Set current signed in IP Address
            var currentSignInIP = string.Empty;

            // Set last signed in IP Address from database
            var lastSignInIP = string.Empty;

            var createdAt = string.Empty;
            var updatedAt = dateTimeNow;
            DataTable datTable = GetUser(email);
            if (datTable != null && datTable.Rows.Count > 0)
            {
                var distinctRows = from DataRow dRow in datTable.Rows
                                   where dRow.Field<string>("email") == email
                                   select new { column1 = dRow["sign_in_count"], column2 = dRow["last_sign_in_at"], column3 = dRow["last_sign_in_ip"], column4 = dRow["created_at"] };
                if (distinctRows != null)
                {
                    foreach (var row in distinctRows)
                    {
                        signInCount = Convert.ToInt32(row.column1.ToString());
                        signInCount++;
                        DateTime lastSignInAtDateTime = Convert.ToDateTime(row.column2);
                        lastSignInAt = lastSignInAtDateTime.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
                        lastSignInIP = Convert.ToString(row.column3);
                        DateTime createdAtDateTime = Convert.ToDateTime(row.column4);
                        createdAt = createdAtDateTime.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
                        break;
                    }
                }
                CreditCard credCard = CreateCreditCard();
                var creditCardID = credCard.id;
                var creditCardDescription = credCard.number;
                StringBuilder sqliteQueryUpdate = new StringBuilder();
                sqliteQueryUpdate.Append("UPDATE Users ");
                sqliteQueryUpdate.Append("SET ");
                sqliteQueryUpdate.Append("encrypted_password = @encrypted_password, ");
                sqliteQueryUpdate.Append("sign_in_count = @sign_in_count, ");
                sqliteQueryUpdate.Append("current_sign_in_at = @current_sign_in_at, ");
                sqliteQueryUpdate.Append("last_sign_in_at = @last_sign_in_at, ");
                sqliteQueryUpdate.Append("current_sign_in_ip = @current_sign_in_ip, ");
                sqliteQueryUpdate.Append("last_sign_in_ip = @last_sign_in_ip, ");
                sqliteQueryUpdate.Append("created_at = @created_at, ");
                sqliteQueryUpdate.Append("credit_card_id = @credit_card_id, ");
                sqliteQueryUpdate.Append("credit_card_description = @credit_card_description ");
                sqliteQueryUpdate.Append("WHERE ");
                sqliteQueryUpdate.Append("email = @email");
                SQLiteDataAdapter sqliteDataAdapterUpdate = new SQLiteDataAdapter();
                sqliteDataAdapterUpdate.UpdateCommand = new SQLiteCommand();
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@email", email);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@encrypted_password", encryptedNewPassword);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@sign_in_count", signInCount);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@current_sign_in_at", currentSignInAt);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@last_sign_in_at", lastSignInAt);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@current_sign_in_ip", currentSignInIP);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@last_sign_in_ip", lastSignInIP);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@created_at", createdAt);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@updated_at", updatedAt);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@credit_card_id", creditCardID);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@credit_card_description", creditCardDescription);
                dataAccessObject = new DataAccessLayer();
                rowsAffacted = dataAccessObject.Update(sqliteQueryUpdate.ToString(), sqliteDataAdapterUpdate);
            }
            if (rowsAffacted > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }

        private bool DataBind(string email)
        {
            bool isSuccess = false;
            DataTable datTable = GetUser(email);
            if (datTable != null && datTable.Rows.Count > 0)
            {
                if (datTable.Rows[0]["email"] != DBNull.Value)
                {
                    TextBoxEmail.Text = Convert.ToString(datTable.Rows[0]["email"]);
                }
                string creditCardID = string.Empty;
                if (datTable.Rows[0]["credit_card_id"] != DBNull.Value)
                {
                    creditCardID = Convert.ToString(datTable.Rows[0]["credit_card_id"]);
                }
                CreditCard crdtCard = CreditCard.Get(Api, creditCardID);
                TextBoxCurrentCreditCardNumber.Text = crdtCard.number.Trim();
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

        private CreditCard CreateCreditCard()
        {
            CreditCard CrdtCard = null;
            CreditCard credCard = new CreditCard();
            credCard.number = TextBoxNewCreditCardNumber.Text.Trim();
            credCard.cvv2 = TextBoxNewCreditCardCVV2.Text.Trim();
            credCard.type = DropDownListNewCreditCardType.SelectedValue.ToString().Trim();
            credCard.expire_month = System.Convert.ToInt32(DropDownListNewCreditCardExpireMonth.SelectedValue.ToString().Trim());
            credCard.expire_year = System.Convert.ToInt32(DropDownListNewCreditCardExpireYear.SelectedValue.ToString().Trim());
            CrdtCard = credCard.Create(Api);
            return CrdtCard;
        }

        #endregion
                
        #region Event Handlers
        protected void Page_Init(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var email = HttpContext.Current.User.Identity.Name.Trim();
                bool isSuccess = DataBind(email);
            }
            else
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }    
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            bool isValid = IsPasswordValid(TextBoxEmail.Text.Trim(), TextBoxCurrentPassword.Text.Trim());
            if (isValid)
            {
                bool isSuccess = Update(TextBoxEmail.Text.Trim());
                if (isSuccess)
                {
                    FormsAuthentication.RedirectFromLoginPage(TextBoxEmail.Text.Trim(), false);
                }
            }
            else
            {
                divAlertMessage.Visible = true;
                divAlertMessage.Attributes["class"] = "alert fade in alert-error";
                LabelAlertMessage.Text = "Invalid Email or Password.";
            }

        }
        #endregion
    }
}