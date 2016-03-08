using System.ComponentModel.DataAnnotations;

namespace PizzaAppMvc3
{
    public class PlaceOrdersModels
    {
        [DataType(DataType.Text)]
        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string PaymentType { get; set; }

        [Display(Name = "Payment Type")]
        public System.Web.Mvc.SelectListItem[] PaymentTypes { get; set; }
    }
}