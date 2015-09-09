using System;
using System.Collections.Generic;
using System.Text;
using VoidWSSample.VoidWebReference;
using System.ServiceModel;

namespace VoidWSSample
{
    class VoidWSClient
    {
        static void Main()
        {
            try
            {
                VoidService voidService = new VoidService();
                VoidShipmentRequest voidRequest = new VoidShipmentRequest();
                RequestType request = new RequestType();
                String[] requestOption = { "1" };
                request.RequestOption = requestOption;
                voidRequest.Request = request;
                VoidShipmentRequestVoidShipment voidShipment = new VoidShipmentRequestVoidShipment();
                voidShipment.ShipmentIdentificationNumber = "1ZISDE016691676846";                
                voidRequest.VoidShipment = voidShipment;

                UPSSecurity upss = new UPSSecurity();
                UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = "4CF6E9703C30E4B6";
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = "afeng.kobo";
                upssUsrNameToken.Password = "Fc.ScmKobo";
                upss.UsernameToken = upssUsrNameToken;
                voidService.UPSSecurityValue = upss;

                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                Console.WriteLine(voidRequest);
                VoidShipmentResponse voidResponse = voidService.ProcessVoid(voidRequest);
                Console.WriteLine("The transaction was a " + voidResponse.Response.ResponseStatus.Description);
                Console.WriteLine("The shipment has been   : " + voidResponse.SummaryResult.Status.Description);
                Console.ReadKey();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                Console.WriteLine("");
                Console.WriteLine("---------Void Web Service returns error----------------");
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
                Console.WriteLine(" Generaal Exception= " + ex.Message);
                Console.WriteLine(" Generaal Exception-StackTrace= " + ex.StackTrace);
                Console.WriteLine("-------------------------");

            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
