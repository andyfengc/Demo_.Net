using System;
using System.Linq;
using System.Web.Mvc;
using PayPal;
using System.Data;
using System.Text;
using System.Data.SQLite;
using System.Web.Security;
using PayPal.Api.Payments;

namespace PizzaAppMvc3
{
    public class AccountController : Controller
    {
        #region Data
        private DataAccessLayer dataAccessObject;

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
                        string encryptedPassword = Convert.ToString(row.column1);
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

        private int GetSignedInUserSignInCount(string email)
        {
            int signInCount = 0;
            DataTable datTable = GetUser(email);
            if (datTable != null && datTable.Rows.Count > 0)
            {
                var distinctRows = from DataRow dRow in datTable.Rows
                                   where dRow.Field<string>("email") == email
                                   select new { column1 = dRow["sign_in_count"] };
                if (distinctRows != null)
                {
                    foreach (var row in distinctRows)
                    {
                        signInCount = Convert.ToInt32(row.column1);
                        break;
                    }
                }
            }
            return signInCount;
        }

        private bool Update(string email)
        {
            bool isSuccess = false;

            int rowsAffacted = 0;
            int signInCount = 0;

            DataTable datTable = GetUser(email);

            if (datTable != null && datTable.Rows.Count > 0)
            {
                var distinctRows = from DataRow dRow in datTable.Rows
                                   where dRow.Field<string>("email") == email
                                   select new { column1 = dRow["sign_in_count"] };
                if (distinctRows != null)
                {
                    foreach (var row in distinctRows)
                    {
                        signInCount = Convert.ToInt32(row.column1.ToString());
                        signInCount++;
                        break;
                    }
                }
                StringBuilder sqliteQueryUpdate = new StringBuilder();
                sqliteQueryUpdate.Append("UPDATE Users ");
                sqliteQueryUpdate.Append("SET ");
                sqliteQueryUpdate.Append("sign_in_count = @sign_in_count ");
                sqliteQueryUpdate.Append("WHERE ");
                sqliteQueryUpdate.Append("email = @email");
                SQLiteDataAdapter sqliteDataAdapterUpdate = new SQLiteDataAdapter();
                sqliteDataAdapterUpdate.UpdateCommand = new SQLiteCommand();
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@email", email);
                sqliteDataAdapterUpdate.UpdateCommand.Parameters.AddWithValue("@sign_in_count", signInCount);
                dataAccessObject = new DataAccessLayer();
                rowsAffacted = dataAccessObject.Update(sqliteQueryUpdate.ToString(), sqliteDataAdapterUpdate);
            }
            if (rowsAffacted > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }

        private bool Insert(SignUpModel model)
        {
            bool isSuccess = false;
            int rowsAffacted = 0;
            var email = model.Email.Trim();
            var password = model.Password.Trim();
            var passwordConfirmation = model.ConfirmPassword.Trim();
            var encryptedPassword = Secure.Encrypt(password);
            var signInCount = 1;
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
            var currentSignInAt = dateTimeNow;
            var lastSignInAt = dateTimeNow;
            var signInIPAddress = string.Empty;
            var currentSignInIP = signInIPAddress;
            var lastSignInIP = signInIPAddress;
            var createdAt = dateTimeNow;
            var updatedAt = dateTimeNow;
            CreditCard credCard = CreateCreditCard(model);
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

        private bool CheckIsExistingUser(string email)
        {
            bool isExistingUser = false;
            DataTable datTable = new DataTable();
            int rows = 0;
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

        private bool CheckIsExistingUser(SignUpModel model)
        {
            bool isExistingUser = false;
            DataTable datTable = new DataTable();
            int rows = 0;
            var email = model.Email.Trim();
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

        private bool Update(ProfileModel model, string email)
        {
            bool isSuccess = false;
            int rowsAffacted = 0;
            var newPassword = model.NewPassword.Trim();
            var confirmNewPassword = model.ConfirmPassword.Trim();
            var encryptedNewPassword = Secure.Encrypt(newPassword);
            var signInCount = 0;
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.FFFFF");
            var currentSignInAt = dateTimeNow;
            var lastSignInAt = string.Empty;
            var signInIPAddress = string.Empty;
            var currentSignInIP = string.Empty;
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
                CreditCard credCard = CreateCreditCard(model);
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

        private bool DataBind(ProfileModel model, string email)
        {
            bool isSuccess = false;
            model.Email = email;
            DataTable datTable = GetUser(email);
            if (datTable != null && datTable.Rows.Count > 0)
            {
                string creditCardId = string.Empty;
                if (datTable.Rows[0]["credit_card_id"] != DBNull.Value)
                {
                    creditCardId = Convert.ToString(datTable.Rows[0]["credit_card_id"]);
                }
                CreditCard crdtCard = CreditCard.Get(Api, creditCardId);
                model.CurrentCreditCardNumber = crdtCard.number.Trim();
                isSuccess = true;
            }
            return isSuccess;
        }
        #endregion

        #region Paypal
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

        public CreditCard CreateCreditCard(SignUpModel model)
        {
            CreditCard card = null;
            CreditCard cardCredit = new CreditCard();
            cardCredit.number = model.CreditCardNumber.Trim();
            cardCredit.type = model.CreditCardType.Trim();
            cardCredit.cvv2 = model.CreditCardCVV2.Trim();
            cardCredit.expire_month = Convert.ToInt32(model.CreditCardExpireMonth.Trim());
            cardCredit.expire_year = Convert.ToInt32(model.CreditCardExpireYear.Trim());
            card = cardCredit.Create(Api);
            return card;
        }

        public CreditCard CreateCreditCard(ProfileModel model)
        {
            CreditCard card = null;
            CreditCard cardCredit = new CreditCard();
            cardCredit.number = model.NewCreditCardNumber.Trim();
            cardCredit.type = model.NewCreditCardType.Trim();
            cardCredit.cvv2 = model.NewCreditCardCVV2.Trim();
            cardCredit.expire_month = Convert.ToInt32(model.NewCreditCardExpireMonth.Trim());
            cardCredit.expire_year = Convert.ToInt32(model.NewCreditCardExpireYear.Trim());
            card = cardCredit.Create(Api);
            return card;
        }
        #endregion

        #region Register
        private SelectListItem[] RegisterCreditCardTypes(bool isValid)
        {
            var model = new SignUpModel();
            model.CreditCardTypes = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "visa", Value = "visa" }, 
                new SelectListItem { Text = "mastercard", Value = "mastercard" },
                new SelectListItem { Text = "discover", Value = "discover" },
                new SelectListItem { Text = "amex", Value = "amex" },
            };
            return model.CreditCardTypes;
        }

        private SelectListItem[] RegisterCreditCardExpireMonths(bool isValid)
        {
            var model = new SignUpModel();
            model.CreditCardExpireMonths = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "01", Value = "01" }, 
                new SelectListItem { Text = "02", Value = "02" },
                new SelectListItem { Text = "03", Value = "03" },
                new SelectListItem { Text = "04", Value = "04" },
                new SelectListItem { Text = "05", Value = "05" }, 
                new SelectListItem { Text = "06", Value = "06" },
                new SelectListItem { Text = "07", Value = "07" },
                new SelectListItem { Text = "08", Value = "08" },
                new SelectListItem { Text = "09", Value = "09" }, 
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "11", Value = "11" },
                new SelectListItem { Text = "12", Value = "12" },
            };
            return model.CreditCardExpireMonths;
        }

        private SelectListItem[] RegisterCreditCardExpireYears(bool isValid)
        {
            var model = new SignUpModel();
            model.CreditCardExpireYears = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "2013", Value = "2013" }, 
                new SelectListItem { Text = "2014", Value = "2014" }, 
                new SelectListItem { Text = "2015", Value = "2015" }, 
                new SelectListItem { Text = "2016", Value = "2016" }, 
                new SelectListItem { Text = "2017", Value = "2017" }, 
                new SelectListItem { Text = "2018", Value = "2018" }, 
                new SelectListItem { Text = "2019", Value = "2019" }, 
                new SelectListItem { Text = "2020", Value = "2020" }, 
                new SelectListItem { Text = "2021", Value = "2021" }, 
                new SelectListItem { Text = "2022", Value = "2022" }, 
                new SelectListItem { Text = "2023", Value = "2023" }, 
            };
            return model.CreditCardExpireYears;
        }

        private SelectListItem[] RegisterNewCreditCardTypes(bool isValid)
        {
            var model = new ProfileModel();

            model.NewCreditCardTypes = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "visa", Value = "visa" }, 
                new SelectListItem { Text = "mastercard", Value = "mastercard" },
                new SelectListItem { Text = "discover", Value = "discover" },
                new SelectListItem { Text = "amex", Value = "amex" },
            };
            return model.NewCreditCardTypes;
        }

        private SelectListItem[] RegisterNewCreditCardExpireMonths(bool isValid)
        {
            var model = new ProfileModel();

            model.NewCreditCardExpireMonths = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "01", Value = "01" }, 
                new SelectListItem { Text = "02", Value = "02" },
                new SelectListItem { Text = "03", Value = "03" },
                new SelectListItem { Text = "04", Value = "04" },
                new SelectListItem { Text = "05", Value = "05" }, 
                new SelectListItem { Text = "06", Value = "06" },
                new SelectListItem { Text = "07", Value = "07" },
                new SelectListItem { Text = "08", Value = "08" },
                new SelectListItem { Text = "09", Value = "09" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "11", Value = "11" },
                new SelectListItem { Text = "12", Value = "12" },
            };
            return model.NewCreditCardExpireMonths;
        }

        private SelectListItem[] RegisterNewCreditCardExpireYears(bool isValid)
        {
            var model = new ProfileModel();

            model.NewCreditCardExpireYears = new[]
            {
                new SelectListItem { Selected = isValid, Text = "--Select--", Value = string.Empty }, 
                new SelectListItem { Text = "2013", Value = "2013" }, 
                new SelectListItem { Text = "2014", Value = "2014" }, 
                new SelectListItem { Text = "2015", Value = "2015" }, 
                new SelectListItem { Text = "2016", Value = "2016" }, 
                new SelectListItem { Text = "2017", Value = "2017" }, 
                new SelectListItem { Text = "2018", Value = "2018" }, 
                new SelectListItem { Text = "2019", Value = "2019" }, 
                new SelectListItem { Text = "2020", Value = "2020" }, 
                new SelectListItem { Text = "2021", Value = "2021" }, 
                new SelectListItem { Text = "2022", Value = "2022" }, 
                new SelectListItem { Text = "2023", Value = "2023" }, 
            };
            return model.NewCreditCardExpireYears;
        }
        #endregion

        #region ActionResult

        //
        // GET: /Account/

        public ActionResult SignIn()
        {
            return View();
        }

        //
        // GET: /Account/
        [HttpPost]
        public ActionResult SignIn(SignInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email.Trim();
                var password = model.Password.Trim();
                bool isValid = IsPasswordValid(email, password);

                if (isValid)
                {
                    int signInCount = GetSignedInUserSignInCount(email);
                    if (signInCount > 0)
                    {
                        bool isSuccess = Update(email);
                    }
                    FormsAuthentication.SetAuthCookie(model.Email.Trim(), model.Remember);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The email or password provided is incorrect.");
                }
            }

            return View(model);
        }

        //
        // GET: /Account/

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/

        public ActionResult SignUp()
        {
            var model = new SignUpModel();
            model.CreditCardTypes = RegisterCreditCardTypes(false);
            model.CreditCardType = string.Empty;
            model.CreditCardExpireMonths = RegisterCreditCardExpireMonths(false);
            model.CreditCardExpireMonth = string.Empty;
            model.CreditCardExpireYears = RegisterCreditCardExpireYears(false);
            model.CreditCardExpireYear = string.Empty;
            return View(model);
        }

        //
        // GET: /Account/

        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExistingUser = CheckIsExistingUser(model);
                if (isExistingUser)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists.");
                }
                else
                {
                    bool isSuccess = Insert(model);
                    if (isSuccess)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false /* createPersistentCookie */);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registration failed.");
                    }
                }
            }

            if (model.CreditCardTypes == null)
            {
                model.CreditCardTypes = RegisterCreditCardTypes(true);
            }

            if (model.CreditCardExpireMonths == null)
            {
                model.CreditCardExpireMonths = RegisterCreditCardExpireMonths(true);
            }

            if (model.CreditCardExpireYears == null)
            {
                model.CreditCardExpireYears = RegisterCreditCardExpireYears(true);
            }
            return View(model);
        }

        //
        // GET: /Account/

        [Authorize]
        public ActionResult Profile()
        {
            var model = new ProfileModel();
            model.NewCreditCardTypes = RegisterNewCreditCardTypes(false);
            model.NewCreditCardType = string.Empty;
            model.NewCreditCardExpireMonths = RegisterNewCreditCardExpireMonths(false);
            model.NewCreditCardExpireMonth = string.Empty;
            model.NewCreditCardExpireYears = RegisterNewCreditCardExpireYears(false);
            model.NewCreditCardExpireYear = string.Empty;

            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name.Trim();
                bool isSuccess = DataBind(model, email);
            }
            else
            {
                RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Profile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                bool changeProfileSucceeded = false;
                try
                {
                    var email = User.Identity.Name.Trim();
                    bool isValid = IsPasswordValid(email, model.CurrentPassword.Trim());
                    if (isValid)
                    {
                        bool isSuccess = Update(model, email);
                        if (isSuccess)
                        {
                            changeProfileSucceeded = true;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The current password provided is incorrect.");
                    }
                }
                catch (Exception)
                {
                    changeProfileSucceeded = false;
                }

                if (changeProfileSucceeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Profile update failed.");
                }
            }

            if (model.NewCreditCardTypes == null)
            {
                model.NewCreditCardTypes = RegisterNewCreditCardTypes(true);
            }

            if (model.NewCreditCardExpireMonths == null)
            {
                model.NewCreditCardExpireMonths = RegisterNewCreditCardExpireMonths(true);
            }

            if (model.NewCreditCardExpireYears == null)
            {
                model.NewCreditCardExpireYears = RegisterNewCreditCardExpireYears(true);
            }
            return View(model);
        }

        #endregion
    }
}
