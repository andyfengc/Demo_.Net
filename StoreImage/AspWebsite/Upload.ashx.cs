using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MyBinaryDemo.Data;

namespace AspWebsite
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count == 1)
            {
                var name = context.Request.Files[0].FileName;
                var size = context.Request.Files[0].ContentLength;
                var type = context.Request.Files[0].ContentType;
                HandleUpload(context.Request.Files[0].InputStream, name, size, type);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("OK");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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
    }
}