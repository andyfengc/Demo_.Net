using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PizzaAppMvc3
{
    public class SignInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool Remember { get; set; }
    }

    public class SignUpModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardType { get; set; }

        [Display(Name = "Credit Card Type")]
        public System.Web.Mvc.SelectListItem[] CreditCardTypes { get; set; }

        [Required]
        [Display(Name = "Credit Card CVV2")]
        public string CreditCardCVV2 { get; set; }

        [Required]
        public string CreditCardExpireMonth { get; set; }

        [Display(Name = "Credit Card Expire Month")]
        public System.Web.Mvc.SelectListItem[] CreditCardExpireMonths { get; set; }

        [Required]
        public string CreditCardExpireYear { get; set; }

        [Display(Name = "Credit Card Expire Year")]
        public System.Web.Mvc.SelectListItem[] CreditCardExpireYears { get; set; }
    }

    public class ProfileModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Current Credit Card Number")]
        public string CurrentCreditCardNumber { get; set; }

        [Required]
        [Display(Name = "New Credit Card Number")]
        public string NewCreditCardNumber { get; set; }

        [Required]
        public string NewCreditCardType { get; set; }

        [Display(Name = "New Credit Card Type")]
        public SelectListItem[] NewCreditCardTypes { get; set; }

        [Required]
        [Display(Name = "New Credit Card CVV2")]
        public string NewCreditCardCVV2 { get; set; }

        [Required]
        public string NewCreditCardExpireMonth { get; set; }

        [Display(Name = "New Credit Card Expire Month")]
        public SelectListItem[] NewCreditCardExpireMonths { get; set; }

        [Required]
        public string NewCreditCardExpireYear { get; set; }

        [Display(Name = "New Credit Card Expire Year")]
        public SelectListItem[] NewCreditCardExpireYears { get; set; }
    }    
}