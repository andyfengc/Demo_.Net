using System.Web.Mvc;

namespace PizzaAppMvc3
{
    public class HomeController : Controller
    {
        #region ActionResult
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
