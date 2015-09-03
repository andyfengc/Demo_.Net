using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ShipWSSample.ShipWebReference;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ShipWSSample
{
    class ShipClient
    {
        static void Main()
        {
            // Schedule shipment
            ScheduleShipment();

            // deserialize
            //ShipmentResponse response = null;
            ////Open the file written above and read values from it.
            //Stream stream = File.Open(@"c:\Users\afeng\Pictures\data.dat", FileMode.Open);
            //BinaryFormatter bformatter = new BinaryFormatter();

            //Console.WriteLine("Reading Response Information");
            //response = (ShipmentResponse)bformatter.Deserialize(stream);
            //stream.Close();
            //// output label
            //string filename = @"c:\Users\afeng\Pictures\label.gif";
            //byte[] byteLabel;

            //using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            //{
            //    using (BinaryWriter writer = new BinaryWriter(fs))
            //    {
            //        byteLabel =
            //            Convert.FromBase64String(
            //                response.ShipmentResults.PackageResults.FirstOrDefault().ShippingLabel.GraphicImage);
            //        writer.Write(byteLabel);
            //    }
            //}

            // print a label
            //PrintLabel1();// original printing
            //PrintLabel2();// printing according to paper size
        }

        private static void PrintLabel2()
        {
            string filename = @"c:\Users\afeng\Pictures\label.gif";
            PrintDocument pd = new PrintDocument();

            //Disable the printing document pop-up dialog shown during printing.
            PrintController printController = new StandardPrintController();
            pd.PrintController = printController;

            //For testing only: Hardcoded set paper size to particular paper.
            //pd.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom 6x4", 720, 478);
            //pd.DefaultPageSettings.PaperSize = new PaperSize("Custom 6x4", 720, 478);
            //pd.DefaultPageSettings.Landscape = true;
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            pd.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            pd.PrintPage += (sndr, args) =>
            {
                System.Drawing.Image i = System.Drawing.Image.FromFile(filename);

                //Adjust the size of the image to the page to print the full image without loosing any part of the image.
                // args - paper size, default is letter, 1100*850
                System.Drawing.Rectangle m = args.MarginBounds;

                // hardcode print size
                int width = (int)(8.25 * 96);
                int height = (int)(4.25 * 96);
                m.Height = height;
                m.Width = width;

                // calculate print size
                ////Logic below maintains Aspect Ratio.
                //if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                //{
                //    m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                //}
                //else
                //{
                //    m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                //}
                //Calculating optimal orientation.

                pd.DefaultPageSettings.Landscape = m.Width > m.Height;
                //Putting image in center of page.
                m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
                m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
                args.Graphics.DrawImage(i, m); //
            };
            pd.Print();
        }

        private static void PrintLabel1()
        {

            // directly print
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            pd.Print();
        }

        private static void PrintPage(object sender, PrintPageEventArgs e)
        {
            string filename = @"c:\Users\afeng\Pictures\label.gif";
            Image image = Image.FromFile(filename);
            Point loc = new Point(0, 0);
            e.Graphics.DrawImage(image, loc);
            //e.Graphics.DrawImage(image, new Rectangle(0, 0, 792, 408));
        }

        private static void ScheduleShipment()
        {
            try
            {
                ShipService shpSvc = new ShipService();
                ShipmentRequest shipmentRequest = new ShipmentRequest();


                // security
                UPSSecurity upss = new UPSSecurity();
                UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = "4CF6E9703C30E4B6";
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = "afeng.kobo";
                upssUsrNameToken.Password = "Fc.ScmKobo";
                upss.UsernameToken = upssUsrNameToken;
                shpSvc.UPSSecurityValue = upss;

                RequestType request = new RequestType();
                String[] requestOption = { "nonvalidate" };
                request.RequestOption = requestOption;
                shipmentRequest.Request = request;

                ShipmentType shipment = new ShipmentType();
                shipment.Description = "New shipment ...";

                // payment
                PaymentInfoType paymentInfo = new PaymentInfoType();
                ShipmentChargeType shpmentCharge = new ShipmentChargeType();
                BillShipperType billShipper = new BillShipperType();
                billShipper.AccountNumber = "1YA077";
                shpmentCharge.BillShipper = billShipper;
                shpmentCharge.Type = "01";
                ShipmentChargeType[] shpmentChargeArray = { shpmentCharge };
                paymentInfo.ShipmentCharge = shpmentChargeArray;
                shipment.PaymentInformation = paymentInfo;

                // shipper
                ShipperType shipper = new ShipperType();
                shipper.ShipperNumber = "1YA077";
                ShipWSSample.ShipWebReference.ShipAddressType shipperAddress =
                    new ShipWSSample.ShipWebReference.ShipAddressType();
                String[] addressLine = { "88 Foster Crescent" };
                shipperAddress.AddressLine = addressLine;
                shipperAddress.City = "Mississauga";
                shipperAddress.PostalCode = "L5R4A2";
                shipperAddress.StateProvinceCode = "ON";
                shipperAddress.CountryCode = "CA";
                shipper.Address = shipperAddress;
                shipper.Name = "CATO";
                shipper.AttentionName = "Ingram Micro - Mississauga";
                ShipPhoneType shipperPhone = new ShipPhoneType();
                shipperPhone.Number = "1234567890";
                shipper.Phone = shipperPhone;
                shipment.Shipper = shipper;

                // ship from
                ShipFromType shipFrom = new ShipFromType();
                ShipWSSample.ShipWebReference.ShipAddressType shipFromAddress =
                    new ShipWSSample.ShipWebReference.ShipAddressType();
                String[] shipFromAddressLine = { "135 Liberty St. Suite 101" };
                shipFromAddress.AddressLine = addressLine;
                shipFromAddress.City = "Toronto";
                shipFromAddress.PostalCode = "M6K1A7";
                shipFromAddress.StateProvinceCode = "ON";
                shipFromAddress.CountryCode = "CA";
                shipFrom.Address = shipFromAddress;
                shipFrom.AttentionName = "Mr. Andy Feng";
                shipFrom.Name = "Kobo Inc.";
                shipment.ShipFrom = shipFrom;

                // ship to
                ShipToType shipTo = new ShipToType();
                ShipToAddressType shipToAddress = new ShipToAddressType();
                String[] addressLine1 = { "5678 Yonge St." };
                shipToAddress.AddressLine = addressLine1;
                shipToAddress.City = "Markham";
                shipToAddress.PostalCode = "M1G3T8";
                shipToAddress.StateProvinceCode = "ON";
                shipToAddress.CountryCode = "CA";
                shipTo.Address = shipToAddress;
                shipTo.AttentionName = "Mr. John Smith";
                shipTo.Name = "DEF Associates";
                ShipPhoneType shipToPhone = new ShipPhoneType();
                shipToPhone.Number = "(905) 123-1234";
                shipTo.Phone = shipToPhone;
                shipment.ShipTo = shipTo;

                //service
                ServiceType service = new ServiceType();
                service.Code = "01";
                shipment.Service = service;

                // package
                PackageType package = new PackageType();
                PackageWeightType packageWeight = new PackageWeightType();
                packageWeight.Weight = "10";
                ShipUnitOfMeasurementType uom = new ShipUnitOfMeasurementType();
                uom.Code = "LBS";
                packageWeight.UnitOfMeasurement = uom;
                package.PackageWeight = packageWeight;
                PackagingType packType = new PackagingType();
                packType.Code = "02";
                package.Packaging = packType;
                // package 2
                PackageType package2 = new PackageType();
                PackageWeightType packageWeight2 = new PackageWeightType();
                packageWeight2.Weight = "9";
                ShipUnitOfMeasurementType uom2 = new ShipUnitOfMeasurementType();
                uom2.Code = "LBS";
                packageWeight2.UnitOfMeasurement = uom2;
                package2.PackageWeight = packageWeight2;
                PackagingType packType2 = new PackagingType();
                packType2.Code = "02";
                package2.Packaging = packType2;

                PackageType[] pkgArray = { package, package2 };
                shipment.Package = pkgArray;

                // label
                LabelSpecificationType labelSpec = new LabelSpecificationType();
                LabelStockSizeType labelStockSize = new LabelStockSizeType();
                labelStockSize.Height = "1";
                labelStockSize.Width = "1";
                labelSpec.LabelStockSize = labelStockSize;
                LabelImageFormatType labelImageFormat = new LabelImageFormatType();
                //labelImageFormat.Code = "SPL";
                labelImageFormat.Code = "GIF";
                labelSpec.LabelImageFormat = labelImageFormat;
                shipmentRequest.LabelSpecification = labelSpec;

                shipmentRequest.Shipment = shipment;

                // label
                //ShipmentTypeShipmentServiceOptions options = new ShipmentTypeShipmentServiceOptions();
                //options.LabelDelivery = new LabelDeliveryType();
                //shipment.ShipmentServiceOptions = options;


                Console.WriteLine(shipmentRequest);
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                // response
                ShipmentResponse shipmentResponse = shpSvc.ProcessShipment(shipmentRequest);

                Console.WriteLine("The transaction was a " + shipmentResponse.Response.ResponseStatus.Description);

                // output label image
                string filename = @"c:\Users\afeng\Pictures\label.gif";
                byte[] byteLabel;

                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        byteLabel =
                            Convert.FromBase64String(
                                shipmentResponse.ShipmentResults.PackageResults.FirstOrDefault().ShippingLabel.GraphicImage);
                        writer.Write(byteLabel);
                    }
                }

                // output label html page
                string htmlFilename = @"c:\Users\afeng\Pictures\label.html";
                byte[] htmlByteLabel;

                using (FileStream fs = new FileStream(htmlFilename, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        htmlByteLabel =
                            Convert.FromBase64String(
                                shipmentResponse.ShipmentResults.PackageResults.FirstOrDefault().ShippingLabel.HTMLImage);
                        writer.Write(htmlByteLabel);
                    }
                }

                // binary serialize
                Stream stream = File.Open(@"c:\Users\afeng\Pictures\data.dat", FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();
                Console.WriteLine("Writing binary serialize Information");
                bformatter.Serialize(stream, shipmentResponse);
                stream.Close();

                // xml serialize
                Stream requetStream = File.Open(@"c:\Users\afeng\Pictures\request.xml", FileMode.Create);
                XmlSerializer requestSerializer = new XmlSerializer(shipment.GetType());
                Console.WriteLine("Writing xml serialize Information");
                requestSerializer.Serialize(requetStream, shipment);
                requetStream.Close();
                Stream responseStream = File.Open(@"c:\Users\afeng\Pictures\response.xml", FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(shipmentResponse.GetType());
                Console.WriteLine("Writing xml serialize Information");
                serializer.Serialize(responseStream, shipmentResponse);
                responseStream.Close();

                // resize picture
                int width = (int)(8.25 * 72);
                int height = (int)(4.25 * 72);

                // resize picture 1
                Bitmap imgIn = new Bitmap(filename);
                double y = imgIn.Height;
                double x = imgIn.Width;
                double factor = 1;
                if (width > 0)
                {
                    factor = width / x;
                }
                else if (height > 0)
                {
                    factor = height / y;
                }
                System.IO.MemoryStream outStream = new System.IO.MemoryStream();
                Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

                // Set DPI of image (xDpi, yDpi)
                //imgOut.SetResolution(72, 72);
                imgOut.SetResolution(96, 96);

                Graphics g = Graphics.FromImage(imgOut);
                g.Clear(Color.White);
                g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
                    new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

                imgOut.Save(outStream, ImageFormat.Gif);
                string filename2 = @"c:\Users\afeng\Pictures\label2.gif";
                FileStream fs2 = new FileStream(filename2, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter writer2 = new BinaryWriter(fs2);
                writer2.Write(outStream.ToArray());
                writer2.Close();


                // resize picture 2
                //Creates a new Bitmap as the size of the window
                Bitmap bmp = new Bitmap(width, height);
                //Creates a new graphics to handle the image that is coming from the stream
                Graphics g2 = Graphics.FromImage((Image)bmp);
                g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //Resizes the image from the stream to fit our windows
                Bitmap imgIn2 = new Bitmap(filename);
                g2.DrawImage(imgIn2, 0, 0, width, height);
                string filename3 = @"c:\Users\afeng\Pictures\label3.gif";
                FileStream fs3 = new FileStream(filename3, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter writer3 = new BinaryWriter(fs3);
                System.IO.MemoryStream outStream2 = new System.IO.MemoryStream();
                bmp.Save(outStream2, ImageFormat.Gif);
                writer3.Write(outStream2.ToArray());
                writer3.Close();

                // resize picture 3
                Bitmap newImage = new Bitmap(width, height);
                using (Graphics gr = Graphics.FromImage(newImage))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    //gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.InterpolationMode = InterpolationMode.NearestNeighbor;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(new Bitmap(filename), new Rectangle(0, 0, width, height));
                    string filename4 = @"c:\Users\afeng\Pictures\label4.gif";
                    FileStream fs4 = new FileStream(filename4, FileMode.Create, FileAccess.ReadWrite);
                    //newImage.Save(filename4, System.Drawing.Imaging.ImageFormat.Gif);
                    BinaryWriter writer4 = new BinaryWriter(fs4);
                    System.IO.MemoryStream outStream4 = new System.IO.MemoryStream();
                    newImage.Save(outStream4, ImageFormat.Gif);
                    writer4.Write(outStream4.ToArray());
                    writer4.Close();
                }

                // resize picture 4
                ImageHandler ih = new ImageHandler();
                string filename5 = @"c:\Users\afeng\Pictures\label5.jpg";
                MemoryStream stream5 = new MemoryStream();
                stream5.Write(byteLabel, 0, byteLabel.Length);
                //Bitmap b = new Bitmap(filename);
                Bitmap b = new Bitmap(stream5);
                ih.Save(b, width, height, 100, filename5);


                Console.WriteLine("The 1Z number of the new shipment is " +
                                  shipmentResponse.ShipmentResults.ShipmentIdentificationNumber);
                Console.ReadKey();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                Console.WriteLine("");
                Console.WriteLine("---------Ship Web Service returns error----------------");
                Console.WriteLine("---------\"Hard\" is user error \"Transient\" is system error----------------");
                Console.WriteLine("SoapException Message= " + ex.Message);
                Console.WriteLine("");
                Console.WriteLine("SoapException Category:Code:Message= " + ex.Detail.LastChild.InnerText);
                Console.WriteLine("");
                Console.WriteLine("SoapException XML String for all= " + ex.Detail.LastChild.OuterXml);
                Console.WriteLine("");
                Console.WriteLine("SoapException StackTrace= " + ex.StackTrace);
                Console.WriteLine("-------------------------");
                Console.WriteLine("");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                Console.WriteLine("");
                Console.WriteLine("--------------------");
                Console.WriteLine("CommunicationException= " + ex.Message);
                Console.WriteLine("CommunicationException-StackTrace= " + ex.StackTrace);
                Console.WriteLine("-------------------------");
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("-------------------------");
                Console.WriteLine(" General Exception= " + ex.Message);
                Console.WriteLine(" General Exception-StackTrace= " + ex.StackTrace);
                Console.WriteLine("-------------------------");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public void SavePicture5()
        {

        }
    }


    /// <summary>
    /// Class contaning method to resize an image and save in JPEG format
    /// </summary>
    public class ImageHandler
    {
        /// <summary>
        /// Method to resize, convert and save the image.
        /// </summary>
        /// <param name="image">Bitmap image.</param>
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="quality">quality setting value.</param>
        /// <param name="filePath">file path.</param>      
        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }
}
