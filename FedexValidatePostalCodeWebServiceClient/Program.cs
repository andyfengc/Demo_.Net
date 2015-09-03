//This code was built using Visual Studio 2005
using System;
using System.Web.Services.Protocols;
using ValidatePostalCodeWebServiceClient.CountryServiceWebReference;

namespace ValidatePostalCodeWebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidatePostalRequest request = CreateValidatePostalRequest();
            //
            CountryService service = new CountryService();
			if (usePropertyFile())
            {
                service.Url = getProperty("endpoint");
            }
            //
            try
            {
                ValidatePostalReply reply = service.validatePostal(request);
                
                if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    ShowValidatePostalReply(reply);
                }
                else
                {
                    Console.WriteLine("Postal Code Inquiry failed : {0}", reply.Notifications[0].Message);
                }
            }
            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        private static ValidatePostalRequest CreateValidatePostalRequest()
        {
            // Build the PostalCodeInquiryRequest
            ValidatePostalRequest request = new ValidatePostalRequest();
            //
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = "V4bqDWokgOVi4qsy"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = "I9VzK1WD8Ks0OT4hAEHjiMXax"; // Replace "XXX" with the Password
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.WebAuthenticationDetail.UserCredential.Key = getProperty("key");
                request.WebAuthenticationDetail.UserCredential.Password = getProperty("password");
            }
            //
            request.ClientDetail = new ClientDetail();
            request.ClientDetail.AccountNumber = "510087500"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = "118687440"; // Replace "XXX" with the client's meter number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.ClientDetail.AccountNumber = getProperty("accountnumber");
                request.ClientDetail.MeterNumber = getProperty("meternumber");
            }
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Validate Postal Service Request using VC#***"; // The client will get the same value back in the response
            //
            request.Version = new VersionId(); // Creates the Version element with all child elements populated from the wsdl
            //
            request.CarrierCode = CarrierCodeType.FDXE;
            request.CarrierCodeSpecified = true;
            request.Address = new Address();
            //request.Address.PostalCode = "32810";
            //request.Address.CountryCode = "US";
            request.Address.PostalCode = "M6K1A7";
            request.Address.City = "Toronto";
            request.Address.StateOrProvinceCode = "ON";
            request.Address.StreetLines = new string[]{ "135 Yonge St."};
            request.Address.CountryCode = "CA";
            return request;
        }

        private static void ShowValidatePostalReply(ValidatePostalReply reply)
        {
            Console.WriteLine("Postal Code Details:");
            if(reply.PostalDetail!=null)
            {
                printString(reply.PostalDetail.CityFirstInitials, "City Initials");
                printString(reply.PostalDetail.CleanedPostalCode, "Cleaned Postal Code");
                printString(reply.PostalDetail.StateOrProvinceCode, "State or Province Code");
                Console.WriteLine();
                if (reply.PostalDetail.LocationDescriptions != null)
                {
                    LocationDescription[] locations = reply.PostalDetail.LocationDescriptions;
                    for (int i = 0; i < locations.Length; i++)
                    {
                        LocationDescription location = locations[i];
                        printString(location.LocationId, "Location Id");
                        if (location.LocationNumberSpecified) { printString(location.LocationNumber.ToString(), "Location Number"); }
                        printString(location.AirportId, "Airport Id");
                        printString(location.CountryCode, "Country Code");
                        if (location.FedExEuropeFirstOriginSpecified) { printString(location.FedExEuropeFirstOrigin.ToString(), "FedEx Europe First Origin"); }
                        printString(location.PostalCode, "Postal Code");
                        printString(location.StateOrProvinceCode, "State or Province Code");
                        printString(location.ServiceArea, "Service Area");
                        if (location.RestrictedShipmentSpecialServices != null)
                        {
                            ShipmentSpecialServiceType[] shipServices = location.RestrictedShipmentSpecialServices;
                            Console.WriteLine("Shipment Special Services -");
                            for(int j=0; j<shipServices.Length; i++)
                            {
                                printString(shipServices[j].ToString(), j.ToString());
                            }
                        }
                        if (location.RestrictedPackageSpecialServices != null)
                        {
                            PackageSpecialServiceType[] packageServices = location.RestrictedPackageSpecialServices;
                            Console.WriteLine("Package Special Services -");
                            for (int j = 0; j < packageServices.Length; i++)
                            {
                                printString(packageServices[j].ToString(), j.ToString());
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        private static void printString(String value, String description)
        {
            if (value != null)
            {
                Console.WriteLine(description + ": " + value);
            }
        }
        private static bool usePropertyFile() //Set to true for common properties to be set with getProperty function.
        {
            return getProperty("usefile").Equals("True");
        }
        private static String getProperty(String propertyname) //Sets common properties for testing purposes.
        {
            try
            {
                String filename = "C:\\filepath\\filename.txt";
                if (System.IO.File.Exists(filename))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(filename);
                    do
                    {
                        String[] parts = sr.ReadLine().Split(',');
                        if (parts[0].Equals(propertyname) && parts.Length == 2)
                        {
                            return parts[1];
                        }
                    }
                    while (!sr.EndOfStream);
                }
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
            catch (Exception e)
            {
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
        }
    }
}