using ContosoUniversity.DAL;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class FileController : Controller
    {
        private SchoolContext db = new SchoolContext();
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}