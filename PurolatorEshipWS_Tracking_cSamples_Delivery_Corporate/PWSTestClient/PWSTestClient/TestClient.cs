using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using TrackingTestClient = PWSTestClient.TrackingTestClient;

namespace PWSTestClient
{
    class TestClients
    {
        static void Main(string[] args)
        {
            TrackingTestClient.TestClient trackingClient = new TrackingTestClient.TestClient();
            trackingClient.CallGetDeliveryDetails();

            Console.WriteLine();
            trackingClient.CallTrackPackagesByPin();

            Console.WriteLine();
            trackingClient.CallTrackPackagesByReference();

            Console.ReadLine();
        }
    }
}
