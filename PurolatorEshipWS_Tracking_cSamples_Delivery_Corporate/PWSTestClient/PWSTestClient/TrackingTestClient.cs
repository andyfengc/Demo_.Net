using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using PWSTestClient.TrackingProxy;
using PWSTestClient;

namespace PWSTestClient.TrackingTestClient
{
    class TestClient
    {
        private TrackingProxy.TrackingService CreateTrackingService()
        {
            TrackingProxy.TrackingService service = new TrackingProxy.TrackingService();
            // Setup the credentials for basic authentication
            service.Credentials = new NetworkCredential("6d84d385039a440c91f73bf4901b65bd", "UkjDSu.=");

            // Set the request's context values
            service.RequestContextValue = new TrackingProxy.RequestContext();
            service.RequestContextValue.Version = "1.2";
            service.RequestContextValue.Language = TrackingProxy.Language.en;
            service.RequestContextValue.GroupID = "";
            service.RequestContextValue.RequestReference = "RequestReference";
            return service;
        }

        public void CallGetDeliveryDetails()
        {
            TrackingProxy.TrackingService service = CreateTrackingService();

            TrackingProxy.GetDeliveryDetailsRequestContainer request = new GetDeliveryDetailsRequestContainer();
            TrackingProxy.GetDeliveryDetailsResponseContainer response = new GetDeliveryDetailsResponseContainer();

            // Setup the request to perform a tracking on a shipment
            request.PIN = new TrackingProxy.PIN();
            request.PIN.Value = "JFV242486848";

            try
            {
                // Call the service
                response = service.GetDeliveryDetails(request);

                // Display the response
                Display(response.ResponseInformation);
                Display(response.DeliveryDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" EXCEPTION: {0}", ex.Message);
            }
        }

        public void CallTrackPackagesByPin()
        {
            TrackingProxy.TrackingService service = CreateTrackingService();

            TrackingProxy.TrackPackagesByPinRequestContainer request = new TrackPackagesByPinRequestContainer();
            TrackingProxy.TrackPackagesByPinResponseContainer response = new TrackPackagesByPinResponseContainer();

            // Setup the request to perform a tracking on a shipment
            request.PINs = new TrackingProxy.PIN[1];
            request.PINs[0] = new TrackingProxy.PIN();
            request.PINs[0].Value = "JFV242486848";

            try
            {
                // Call the service
                response = service.TrackPackagesByPin(request);

                // Display the response
                Display(response.ResponseInformation);
                Display(response.TrackingInformationList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" EXCEPTION: {0}", ex.Message);
            }
        }

        public void CallTrackPackagesByReference()
        {
            TrackingProxy.TrackingService service = CreateTrackingService();

            TrackingProxy.TrackPackagesByReferenceRequestContainer request = new TrackPackagesByReferenceRequestContainer();
            TrackingProxy.TrackPackagesByReferenceResponseContainer response = new TrackPackagesByReferenceResponseContainer();
            request.TrackPackageByReferenceSearchCriteria = new TrackPackageByReferenceSearchCriteria();

            // Setup the request to perform a tracking on a shipment
            request.TrackPackageByReferenceSearchCriteria.Reference = "ref1";
            request.TrackPackageByReferenceSearchCriteria.DestinationPostalCode = "V2S8B7";
            request.TrackPackageByReferenceSearchCriteria.DestinationCountryCode = null;
            request.TrackPackageByReferenceSearchCriteria.BillingAccountNumber = null;
            request.TrackPackageByReferenceSearchCriteria.ShipmentFromDate = null;
            request.TrackPackageByReferenceSearchCriteria.ShipmentToDate = "2009-02-06";

            try
            {
                // Call the service
                response = service.TrackPackagesByReference(request);

                // Display the response
                Display(response.ResponseInformation);
                Display(response.TrackingInformationList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" EXCEPTION: {0}", ex.Message);
            }
        }

        private void Display(TrackingProxy.ResponseInformation respInf)
        {
            if (respInf == null)
                return;

            int i = 0;
            if (respInf.Errors != null && respInf.Errors.Length > 0)
            {
                foreach (TrackingProxy.Error error in respInf.Errors)
                {
                    i++;
                    Util.Print("Error", i);
                    Util.Push();
                    Util.Print("Error code", error.Code);
                    Util.Print("Error description", error.Description);
                    Util.Print("Additional Information", error.AdditionalInformation);
                    Util.Pop();
                }
            }

            i = 0;
            if (respInf.InformationalMessages != null && respInf.InformationalMessages.Length > 0)
            {
                foreach (TrackingProxy.InformationalMessage msg in respInf.InformationalMessages)
                {
                    i++;
                    Util.Print("InformationalMessage", i);
                    Util.Push();
                    Util.Print("", msg.Code);
                    Util.Print("message", msg.Message);
                    Util.Pop();
                }
            }
        }

        private void Display(TrackingInformation[] trackingInformationList)
        {
            if (trackingInformationList == null || trackingInformationList.Length == 0)
                return;

            int i = 0;
            foreach (TrackingInformation t in trackingInformationList)
            {
                i++;
                Util.Print("TrackingInformation:", i);
                Util.Print("PIN", t.PIN.Value);
                Display(t.Scans);
                Console.WriteLine();
            }
        }

        private void Display(Scan[] scans)
        {
            Console.WriteLine("Scans:");
            if (scans != null && scans.Length > 0)
            {
                int i = 0;
                foreach (Scan scan in scans)
                {
                    i++;
                    Util.Print("Scan", i);
                    Util.Push();
                    Display(scan);
                    Util.Pop();
                }
            }
            else
            {
                Util.Print("Scans not available");
            }
        }

        private void Display(Scan scan)
        {
            Util.Print("ScanType", scan.ScanType);
            Util.Print("PIN", scan.PIN.Value);
            Util.Print("Depot name", scan.Depot.Name);
            Util.Print("ScanDate", scan.ScanDate);
            Util.Print("ScanTime", scan.ScanTime);
            Util.Print("Description", scan.Description);
            Util.Print("Comment", scan.Comment);
            Util.Print("SummaryScanIndicator", scan.SummaryScanIndicator);
            Util.Push();
            if (scan is ProofOfPickUpScan)
                Display(((ProofOfPickUpScan)scan).ScanDetails);
            else if (scan is DeliveryScan)
                Display(((DeliveryScan)scan).ScanDetails);
            else if (scan is OnDeliveryScan)
                Display(((OnDeliveryScan)scan).ScanDetails);
            Util.Pop();
        }

        private void Display(ProofOfPickUpScanDetails sd)
        {
            Util.Print("PickUpConfirmationNumber", sd.PickUpConfirmationNumber);
            Util.Print("PickUpAddress");
            Util.Push();
            Display(sd.PickUpAddress);
            Util.Pop();
            Util.Print("PickUpContactName", sd.PickUpContactName);
            Util.Print("PickUpCompanyName", sd.PickUpCompanyName);
            Util.Print("PickUpLocation", sd.PickUpLocation);
            Util.Print("CommitedDelivery", sd.CommitedDeliveryDate);
            Util.Print("PremiumServiceText", sd.PremiumServiceText);
            Util.Print("ProductTypeText", sd.ProductTypeText);
            Util.Print("SpecialHandlingText", sd.SpecialHandlingText);
            Util.Print("PaymentTypeText", sd.PaymentTypeText);
        }

        private void Display(TrackingProxy.Address adr)
        {
            if (adr == null)
                return;

            Util.Print("Name", adr.Name);
            Util.Print("Company", adr.Company);
            Util.Print("Department", adr.Department);
            Util.Print("StreetNumber", adr.StreetNumber);
            Util.Print("StreetSuffix", adr.StreetSuffix);
            Util.Print("StreetName", adr.StreetName);
            Util.Print("StreetType", adr.StreetType);
            Util.Print("StreetDirection", adr.StreetDirection);
            Util.Print("Suite", adr.Suite);
            Util.Print("Floor", adr.Floor);
            Util.Print("StreetAddress2", adr.StreetAddress2);
            Util.Print("StreetAddress3", adr.StreetAddress3);
            Util.Print("City", adr.City);
            Util.Print("Province", adr.Province);
            Util.Print("Country", adr.Country);
            Util.Print("PostalCode", adr.PostalCode);
            Display("PhoneNumber", adr.PhoneNumber);
            Display("FaxNumber", adr.FaxNumber);
        }

        private void Display(string text, TrackingProxy.PhoneNumber ph)
        {
            if (ph == null)
                return;

            Console.Write(text);
            Console.WriteLine(": {0}-{1}-{2}-{3} x {4}", ph.CountryCode, ph.AreaCode, ph.Phone, ph.Extension);
        }

        private void Display(DeliveryScanDetails sd)
        {
            Util.Print("DeliverySignature", sd.DeliverySignature);
            // Get SignatureImage from sd.SignatureImage
            Util.Print("SignatureImageSize", sd.SignatureImageSize);
            Util.Print("SignatureImageFormat", sd.SignatureImageFormat);
            Util.Print("DeliveryAddress", sd.DeliveryAddress);
            Util.Print("DeliveryCompanyName", sd.DeliveryCompanyName);
            Util.Print("PremiumServiceText", sd.PremiumServiceText);
            Util.Print("ProductTypeText", sd.ProductTypeText);
            Util.Print("SpecialHandlingText", sd.SpecialHandlingText);
            Util.Print("PaymentTypeText", sd.PaymentTypeText);
        }

        private void Display(OnDeliveryScanDetails sd)
        {
            Util.Print("DeliveryAddress", sd.DeliveryAddress);
        }
    }
}
