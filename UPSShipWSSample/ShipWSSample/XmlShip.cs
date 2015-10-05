using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ShipWSSample
{
    public class XmlShip
    {
        public static void Run()
        {
             string confirmMessage = GetConfirmMessage();
            var confirmRequest = GetConfirmRequest(confirmMessage);
            string confirmResponse = ProcessConfirmRequest(confirmRequest);
           string acceptMessage = GetAcceptMessage(confirmResponse);
            var acceptRequest = GetConfirmRequest(acceptMessage);
            string acceptResponse = ProcessAcceptRequest(acceptMessage);
            SaveLabel(acceptResponse);
        }

        private static void SaveLabel(string acceptResponse)
        {
            using (TextReader reader = new StringReader(acceptResponse))
            {
                XDocument document = XDocument.Load(reader);
                //// save
                string filename = @"c:\Users\afeng\Pictures\label.zpl";
                byte[] byteLabel;
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        byteLabel = Convert.FromBase64String(document.Descendants("GraphicImage").FirstOrDefault().Value);
                        writer.Write(byteLabel);
                    }
                }
            }
        }

        private static HttpWebRequest GetConfirmRequest(string confirmMessage)
        {
            HttpWebRequest request = WebRequest.Create("https://wwwcie.ups.com/ups.app/xml/ShipConfirm") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string postData = confirmMessage;
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }
            return request;
            
        }

        private static string ProcessConfirmRequest(HttpWebRequest confirmRequest)
        {
            HttpWebResponse response = confirmRequest.GetResponse() as HttpWebResponse;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        private static string GetConfirmMessage()
        {
            string filename = @"d:\Work\Workspace\Demo_.Net\UPSShipWSSample\ShipWSSample\ShipConfirm_Request.xml";
            string text = File.ReadAllText(filename);
            return text;
        }

        private static string ProcessAcceptRequest(string acceptMessage)
        {
            HttpWebRequest request = WebRequest.Create("https://wwwcie.ups.com/ups.app/xml/ShipAccept") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string postData = acceptMessage;
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        private static string GetAcceptMessage(string confirmResponse)
        {
            string digestNumber = GetDigestNumber(confirmResponse);
            string messageBase = @"<?xml version=""1.0"" encoding=""ISO-8859-1""?><AccessRequest><AccessLicenseNumber>4CF6E9703C30E4B6</AccessLicenseNumber><UserId>afeng.kobo</UserId><Password>Fc.ScmKobo</Password></AccessRequest><?xml version=""1.0"" encoding=""ISO-8859-1""?><ShipmentAcceptRequest><Request><TransactionReference><CustomerContext>Customer Comment</CustomerContext></TransactionReference><RequestAction>ShipAccept</RequestAction><RequestOption>1</RequestOption></Request><ShipmentDigest>{0}</ShipmentDigest></ShipmentAcceptRequest>";
            string message = string.Format(messageBase, digestNumber);
            return message;
        }

        private static string GetDigestNumber(string confirmResponse)
        {
            TextReader reader = new StringReader(confirmResponse);
            XDocument document = XDocument.Load(reader);
            var element = document.Descendants("ShipmentDigest").FirstOrDefault();
            string digestNumber = element.Value;
            return digestNumber;
        }
    }
}
