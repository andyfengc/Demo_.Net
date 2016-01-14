using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.com.ebay.developer;

namespace Test
{
    public class SandboxTest
    {
        public static void GetOrders()
        {
            string endpoint = "https://api.sandbox.ebay.com/wsapi";
            string callName = "GetOrders";
            string siteId = "2";
            string version = "949";
            // Build the request URL
            string requestURL = endpoint
                                + "?callname=" + callName
                                + "&siteid=" + siteId
                                + "&appid=" + Settings.AppId
                                + "&version=" + version
                                + "&routing=default";
            // Create the service
            eBayAPIInterfaceService service = new eBayAPIInterfaceService();
            // Assign the request URL to the service locator.
            service.Url = requestURL;
            // Set credentials
            service.RequesterCredentials = new CustomSecurityHeaderType();
            service.RequesterCredentials.eBayAuthToken =
                "AgAAAA**AQAAAA**aAAAAA**Tz2NVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GhDpWLqA+dj6x9nY+seQ**kKoDAA**AAMAAA**1FzOBx5mSGbycTN2Pa7AffEQ8jbDk77g27juD4aFZeTrgQs94kRZY5Bjq1ifH088RviVP/9RZ0AVnK+qEfDi2M5uiDYT5fb1b8tJ+AaQjRfYG5I30M/rkUGolkTvwL4X9glOE147yCW5GGA8PG8SRZVjJ4myeO25v86TcJmmBmpHLbbN3V/7v6w4Gcovel1jB0QC6Ae5vDRTHt0N7SxSTTFFUQ4yQiLM5CpaVxa8Rp3mTxMS4DR5OEd0MTWyA7BlWsnN2TrLVFrYarrhKOE+6BS3lPdAdHMfB9uTl5JDR1d133zU+++P+VBLguWPO0EaGxs2sJ1NAwVHlEUslApdDIXH95XQIf0d8S+QOnmQ94lJ6eyjJfTqCyPjlFxCIA+9GzCbnOJjn1+LyrzD9zQrVPejuChy9qxYxikCoRUA0GrLLMsf77TBkNnKh5cgcyHvRG1NYeM596KEhvkvVEpKKr+ajC13SNkncl4wdvjV7AIpX+6UaGxbDjbuvGtLZU72qL1SmbZgOlp/UNFC/4bBG7aViG8+OK5UiI2dKI7wX2OuKEWy1fCOXSlUi8FwiaJCtIH4Lvj8cXYb0fhZ3ODPO+zenrK8XHXbau/4mA3YRwdPiLIuZjJUzUkaHEOQu4U5dBFz3YdK+S1K72nnBRmjuvS8LmNGSnUpf9fInLKP7o4CY9arWWTJqSmitlrYBXqfy70JsIaZNvpg6TamFAv3/TAaoFbgCsE1Um/PSY3C86ZEgy9QWrCwN7pvJs4SpW3I";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetOrdersRequestType request = new GetOrdersRequestType();
            request.CreateTimeFrom = new DateTime(2015, 12, 1);
            request.CreateTimeTo = DateTime.Now;
            request.CreateTimeFromSpecified = true;
            request.CreateTimeToSpecified = true;
            request.DetailLevel = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
            request.Version = "949";
            var response = service.GetOrders(request);
            Console.WriteLine(response.Timestamp);
            foreach (var order in response.OrderArray)
            {
                Console.WriteLine(order.TransactionArray.Length);
                foreach (var transaction in order.TransactionArray)
                {
                    Console.WriteLine("Item Id: {0}", transaction.Item.ItemID);
                    Console.WriteLine("LineItem Id: {0}", transaction.OrderLineItemID);

                }
            }
        }

        public static void GetNewOrders()
        {
            string endpoint = "https://api.sandbox.ebay.com/wsapi";
            string callName = "GetOrders";
            string siteId = "2";
            string version = "949";
            // Build the request URL
            string requestURL = endpoint
                                + "?callname=" + callName
                                + "&siteid=" + siteId
                                + "&appid=" + Settings.AppId
                                + "&version=" + version
                                + "&routing=default";
            // Create the service
            eBayAPIInterfaceService service = new eBayAPIInterfaceService();
            // Assign the request URL to the service locator.
            service.Url = requestURL;
            // Set credentials
            service.RequesterCredentials = new CustomSecurityHeaderType();
            service.RequesterCredentials.eBayAuthToken =
                "AgAAAA**AQAAAA**aAAAAA**Tz2NVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GhDpWLqA+dj6x9nY+seQ**kKoDAA**AAMAAA**1FzOBx5mSGbycTN2Pa7AffEQ8jbDk77g27juD4aFZeTrgQs94kRZY5Bjq1ifH088RviVP/9RZ0AVnK+qEfDi2M5uiDYT5fb1b8tJ+AaQjRfYG5I30M/rkUGolkTvwL4X9glOE147yCW5GGA8PG8SRZVjJ4myeO25v86TcJmmBmpHLbbN3V/7v6w4Gcovel1jB0QC6Ae5vDRTHt0N7SxSTTFFUQ4yQiLM5CpaVxa8Rp3mTxMS4DR5OEd0MTWyA7BlWsnN2TrLVFrYarrhKOE+6BS3lPdAdHMfB9uTl5JDR1d133zU+++P+VBLguWPO0EaGxs2sJ1NAwVHlEUslApdDIXH95XQIf0d8S+QOnmQ94lJ6eyjJfTqCyPjlFxCIA+9GzCbnOJjn1+LyrzD9zQrVPejuChy9qxYxikCoRUA0GrLLMsf77TBkNnKh5cgcyHvRG1NYeM596KEhvkvVEpKKr+ajC13SNkncl4wdvjV7AIpX+6UaGxbDjbuvGtLZU72qL1SmbZgOlp/UNFC/4bBG7aViG8+OK5UiI2dKI7wX2OuKEWy1fCOXSlUi8FwiaJCtIH4Lvj8cXYb0fhZ3ODPO+zenrK8XHXbau/4mA3YRwdPiLIuZjJUzUkaHEOQu4U5dBFz3YdK+S1K72nnBRmjuvS8LmNGSnUpf9fInLKP7o4CY9arWWTJqSmitlrYBXqfy70JsIaZNvpg6TamFAv3/TAaoFbgCsE1Um/PSY3C86ZEgy9QWrCwN7pvJs4SpW3I";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetOrdersRequestType request = new GetOrdersRequestType();
            request.CreateTimeFrom = new DateTime(2015, 12, 1);
            request.CreateTimeTo = DateTime.Now;
            request.CreateTimeFromSpecified = true;
            request.CreateTimeToSpecified = true;
            request.DetailLevel = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
            request.Version = "949";
            var response = service.GetOrders(request);
            Console.WriteLine("New orders: \n");
            foreach (var order in response.OrderArray)
            {
                if (order.CheckoutStatus.Status == CompleteStatusCodeType.Complete)
                {
                    Console.WriteLine("Order Id: {0}",order.OrderID);
                    Console.WriteLine("Amount Paid: {0:c}", order.AmountPaid.Value);
                    Console.WriteLine("Shipped Time: {0}", order.ShippedTime);
                    int itemIdex = 0;
                    foreach (var transaction in order.TransactionArray)
                    {
                        Console.WriteLine("Order Item {1}: {0}", transaction.Item.Title, itemIdex + 1);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
