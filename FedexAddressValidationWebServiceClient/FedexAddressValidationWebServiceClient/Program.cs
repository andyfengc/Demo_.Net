using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using FedexAddressValidationWebServiceClient.AddressValidationServiceWebReference;

namespace FedexAddressValidationWebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressValidationRequest request = CreateAddressValidationRequest();
            //
            AddressValidationService service = new AddressValidationService();
            if (usePropertyFile())
            {
                service.Url = getProperty("endpoint");
            }
            //
            try
            {
                // Call the AddressValidationService passing in an AddressValidationRequest and returning an AddressValidationReply
                AddressValidationReply reply = service.addressValidation(request);
                //
                if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
                {
                    ShowAddressValidationReply(reply);
                }
                else
                {
                    foreach (Notification notification in reply.Notifications)
                        Console.WriteLine(notification.Message);
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

        private static AddressValidationRequest CreateAddressValidationRequest()
        {
            // Build the AddressValidationRequest
            AddressValidationRequest request = new AddressValidationRequest();
            //
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            //request.WebAuthenticationDetail.UserCredential.Key = "Cr0nkpIax2HSog22"; // Replace "XXX" with the Key
            //request.WebAuthenticationDetail.UserCredential.Password = "qX1UFuVRLZ2RDYQY9gRO3DIPg"; // Replace "XXX" with the Password
            request.WebAuthenticationDetail.UserCredential.Key = "V4bqDWokgOVi4qsy"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = "I9VzK1WD8Ks0OT4hAEHjiMXax"; // Replace "XXX" with the Password
            request.WebAuthenticationDetail.ParentCredential = new WebAuthenticationCredential();
            //request.WebAuthenticationDetail.ParentCredential.Key = "Cr0nkpIax2HSog22"; // Replace "XXX" with the Key
            //request.WebAuthenticationDetail.ParentCredential.Password = "qX1UFuVRLZ2RDYQY9gRO3DIPg"; // Replace "XXX"
            request.WebAuthenticationDetail.ParentCredential.Key = "V4bqDWokgOVi4qsy"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.ParentCredential.Password = "I9VzK1WD8Ks0OT4hAEHjiMXax"; // Replace "XXX"            
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.WebAuthenticationDetail.UserCredential.Key = getProperty("key");
                request.WebAuthenticationDetail.UserCredential.Password = getProperty("password");
                request.WebAuthenticationDetail.ParentCredential.Key = getProperty("parentkey");
                request.WebAuthenticationDetail.ParentCredential.Password = getProperty("parentpassword");
            }
            //
            request.ClientDetail = new ClientDetail();
            //request.ClientDetail.AccountNumber = "287737831"; // Replace "XXX" with the client's account number
            //request.ClientDetail.MeterNumber = "106578628"; // Replace "XXX" with the client's meter number
            request.ClientDetail.AccountNumber = "510087500"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = "118687440"; // Replace "XXX" with the client's meter number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.ClientDetail.AccountNumber = getProperty("accountnumber");
                request.ClientDetail.MeterNumber = getProperty("meternumber");
            }
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Address Validation Request using VC#***"; // The client will get the same value back in the reply
            //
            request.Version = new VersionId(); // Creates the Version element with all child elements populated
            //
            request.InEffectAsOfTimestamp = DateTime.Now;
            request.InEffectAsOfTimestampSpecified = true;
            //
            SetAddress(request);
            //
            return request;
        }

        private static void SetAddress(AddressValidationRequest request)
        {
            request.AddressesToValidate = new AddressToValidate[4];
            request.AddressesToValidate[0] = new AddressToValidate();
            request.AddressesToValidate[0].ClientReferenceId = "ClientReferenceId1";
            request.AddressesToValidate[0].Address = new Address();
            request.AddressesToValidate[0].Address.StreetLines = new String[1] { "100 Nickerson RD" };
            request.AddressesToValidate[0].Address.PostalCode = "01752";
            request.AddressesToValidate[0].Address.City = "Marlborough";
            request.AddressesToValidate[0].Address.StateOrProvinceCode = "MA";
            request.AddressesToValidate[0].Address.CountryCode = "US";
            //
            request.AddressesToValidate[1] = new AddressToValidate();
            request.AddressesToValidate[1].ClientReferenceId = "ClientReferenceId2";
            request.AddressesToValidate[1].Address = new Address();
            request.AddressesToValidate[1].Address.StreetLines = new String[1] { "167 PROSPECT HIGHWAY" };
            request.AddressesToValidate[1].Address.PostalCode = "2147";
            request.AddressesToValidate[1].Address.City = "New SOUTH WALES";
            request.AddressesToValidate[1].Address.CountryCode = "AU";

            request.AddressesToValidate[2] = new AddressToValidate();
            request.AddressesToValidate[2].ClientReferenceId = "ClientReferenceId3";
            request.AddressesToValidate[2].Address = new Address();
            request.AddressesToValidate[2].Address.StreetLines = new String[1] { "20 Tuxedo Court" };
            request.AddressesToValidate[2].Address.PostalCode = "M1G3S5";
            //request.AddressesToValidate[2].Address.City = "Scarborough";
            request.AddressesToValidate[2].Address.StateOrProvinceCode = "ON";
            request.AddressesToValidate[2].Address.CountryCode = "CA";


            request.AddressesToValidate[3] = new AddressToValidate();
            request.AddressesToValidate[3].ClientReferenceId = "ClientReferenceId4";
            request.AddressesToValidate[3].Address = new Address();
            request.AddressesToValidate[3].Address.StreetLines = new String[1] { "135 Liberty St."};
            request.AddressesToValidate[3].Address.PostalCode = "M6K 1A7";
            request.AddressesToValidate[3].Address.City = "Toronto";
            request.AddressesToValidate[3].Address.StateOrProvinceCode = "ON";
            request.AddressesToValidate[3].Address.CountryCode = "CA";
        }

        private static void ShowAddressValidationReply(AddressValidationReply reply)
        {
            Console.WriteLine("AddressValidationReply details:");
            Console.WriteLine("*****************************************************");
            foreach (AddressValidationResult result in reply.AddressResults)
            {
                Console.WriteLine();
                Console.WriteLine("Address Id : " + result.ClientReferenceId);
                if (result.ClassificationSpecified) { Console.WriteLine("Classification: " + result.Classification); }
                if (result.StateSpecified) { Console.WriteLine("State: " + result.State); }
                Console.WriteLine("Proposed Address--");
                Address address = result.EffectiveAddress;
                foreach (String street in address.StreetLines)
                {
                    Console.WriteLine("   Street: " + street);
                }
                Console.WriteLine("     City: " + address.City);
                Console.WriteLine("    ST/PR: " + address.StateOrProvinceCode);
                Console.WriteLine("   Postal: " + address.PostalCode);
                Console.WriteLine("  Country: " + address.CountryCode);
                Console.WriteLine();
                Console.WriteLine("Address Attributes:");
                foreach (AddressAttribute attribute in result.Attributes)
                {
                    Console.WriteLine("  " + attribute.Name + ": " + attribute.Value);
                }
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
