using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebsite.Models;
using MyBinaryDemo.Data;

namespace AspMvcWebsite.Controllers
{
    public class DocumentController : Controller
    {

        [HttpGet]
        public ActionResult Show(int? id)
        {
            string mime;
            byte[] bytes = LoadImage(id.Value, out mime);
            return File(bytes, mime);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            SuccessModel viewModel = new SuccessModel();
            if (Request.Files.Count == 1)
            {
                var name = Request.Files[0].FileName;
                var size = Request.Files[0].ContentLength;
                var type = Request.Files[0].ContentType;
                viewModel.Success = HandleUpload(Request.Files[0].InputStream, name, size, type);
            }
            return Json(viewModel);
        }

        private bool HandleUpload(Stream fileStream, string name, int size, string type)
        {
            bool handled = false;

            try
            {
                byte[] documentBytes = new byte[fileStream.Length];
                fileStream.Read(documentBytes, 0, documentBytes.Length);

                Document databaseDocument = new Document
                {
                    CreatedOn = DateTime.Now,
                    FileContent = documentBytes,
                    IsDeleted = false,
                    Name = name,
                    Size = size,
                    Type = type
                };

                using (DocumentEntities databaseContext = new DocumentEntities())
                {
                    databaseContext.Documents.Add(databaseDocument);
                    handled = (databaseContext.SaveChanges() > 0);
                }
            }
            catch (Exception ex)
            {
                // Oops, something went wrong, handle the exception
            }

            return handled;
        }

        private byte[] LoadImage(int id, out string type)
        {
            byte[] fileBytes = null;
            string fileType = null;
            using (DocumentEntities databaseContext = new DocumentEntities())
            {
                var databaseDocument = databaseContext.Documents.FirstOrDefault(doc => doc.DocumentId == id);
                if (databaseDocument != null)
                {
                    fileBytes = databaseDocument.FileContent;
                    fileType = databaseDocument.Type;
                }
            }
            type = fileType;
            return fileBytes;
        }


    }
}
