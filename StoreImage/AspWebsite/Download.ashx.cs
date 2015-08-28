using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBinaryDemo.Data;

namespace AspWebsite
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int imageId = int.Parse(context.Request["id"]);
            string mime;
            byte[] bytes = LoadImage(imageId, out mime);
            context.Response.ContentType = mime;
            context.Response.Write(bytes);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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