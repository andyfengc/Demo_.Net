using System;
using System.Collections.Generic;
using System.Text;
using ShipWSSample.ShipWebReference;
using System.ServiceModel;

namespace ShipWSSample
{
    class ShipClient
    {
        static void Main()
        {
            try
            {
                ShipService shpSvc = new ShipService();
                ShipmentRequest shipmentRequest = new ShipmentRequest();
                UPSSecurity upss = new UPSSecurity();
                UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = "Your Access License";
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = "Your User Id";
                upssUsrNameToken.Password = "Your Password";
                upss.UsernameToken = upssUsrNameToken;
                shpSvc.UPSSecurityValue = upss;
                RequestType request = new RequestType();
                String[] requestOption = { "nonvalidate" };
                request.RequestOption = requestOption;
                shipmentRequest.Request = request;
                ShipmentType shipment = new ShipmentType();
                shipment.Description = "Ship webservice example";
                ShipperType shipper = new ShipperType();
                shipper.ShipperNumber = "Your Shipper Number";
                PaymentInfoType paymentInfo = new PaymentInfoType();
                ShipmentChargeType shpmentCharge = new ShipmentChargeType();
                BillShipperType billShipper = new BillShipperType();
                billShipper.AccountNumber = "Your Account Number";
                shpmentCharge.BillShipper = billShipper;
                shpmentCharge.Type = "01";
                ShipmentChargeType[] shpmentChargeArray = { shpmentCharge };
                paymentInfo.ShipmentCharge = shpmentChargeArray;
                shipment.PaymentInformation = paymentInfo;
                ShipWSSample.ShipWebReference.ShipAddressType shipperAddress = new ShipWSSample.ShipWebReference.ShipAddressType();
                String[] addressLine = { "480 Parkton Plaza" };
                shipperAddress.AddressLine = addressLine;
                shipperAddress.City = "Timonium";
                shipperAddress.PostalCode = "21093";
                shipperAddress.StateProvinceCode = "MD";
                shipperAddress.CountryCode = "US";
                shipperAddress.AddressLine = addressLine;
                shipper.Address = shipperAddress;
                shipper.Name = "ABC Associates";
                shipper.AttentionName = "ABC Associates";
                ShipPhoneType shipperPhone = new ShipPhoneType();
                shipperPhone.Number = "1234567890";
                shipper.Phone = shipperPhone;
                shipment.Shipper = shipper;
                ShipFromType shipFrom = new ShipFromType();
                ShipWSSample.ShipWebReference.ShipAddressType shipFromAddress = new ShipWSSample.ShipWebReference.ShipAddressType();
                String[] shipFromAddressLine = { "Ship From Street" };
                shipFromAddress.AddressLine = addressLine;
                shipFromAddress.City = "Timonium";
                shipFromAddress.PostalCode = "21093";
                shipFromAddress.StateProvinceCode = "MD";
                shipFromAddress.CountryCode = "US";
                shipFrom.Address = shipFromAddress;
                shipFrom.AttentionName = "Mr.ABC";
                shipFrom.Name = "ABC Associates";
                shipment.ShipFrom = shipFrom;
                ShipToType shipTo = new ShipToType();
                ShipToAddressType shipToAddress = new ShipToAddressType();
                String[] addressLine1 = { "Some Street" };
                shipToAddress.AddressLine = addressLine1;
                shipToAddress.City = "Roswell";
                shipToAddress.PostalCode = "30076";
                shipToAddress.StateProvinceCode = "GA";
                shipToAddress.CountryCode = "US";
                shipTo.Address = shipToAddress;
                shipTo.AttentionName = "DEF";
                shipTo.Name = "DEF Associates";
                ShipPhoneType shipToPhone = new ShipPhoneType();
                shipToPhone.Number = "1234567890";
                shipTo.Phone = shipToPhone;
                shipment.ShipTo = shipTo;
                ServiceType service = new ServiceType();
                service.Code = "01";
                shipment.Service = service;
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
                PackageType[] pkgArray = { package };
                shipment.Package = pkgArray;
                LabelSpecificationType labelSpec = new LabelSpecificationType();
                LabelStockSizeType labelStockSize = new LabelStockSizeType();
                labelStockSize.Height = "6";
                labelStockSize.Width = "4";
                labelSpec.LabelStockSize = labelStockSize;
                LabelImageFormatType labelImageFormat = new LabelImageFormatType();
                labelImageFormat.Code = "SPL";
                labelSpec.LabelImageFormat = labelImageFormat;
                shipmentRequest.LabelSpecification = labelSpec;
                shipmentRequest.Shipment = shipment;
                Console.WriteLine(shipmentRequest);
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                ShipmentResponse shipmentResponse = shpSvc.ProcessShipment(shipmentRequest);
                Console.WriteLine("The transaction was a " + shipmentResponse.Response.ResponseStatus.Description);
                Console.WriteLine("The 1Z number of the new shipment is " + shipmentResponse.ShipmentResults.ShipmentIdentificationNumber);
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
    }
}
