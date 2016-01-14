using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.com.ebay.developer;

namespace Test
{
    public class OrderTest
    {
        public static void GetOrders()
        {
            string endpoint = "https://api.ebay.com/wsapi";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
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

        public static void GetOrderTransactions()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetOrderTransactions";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetOrderTransactionsRequestType request = new GetOrderTransactionsRequestType();
            request.Version = "949";
            var response = service.GetOrderTransactions(request);
            Console.WriteLine(response.Timestamp);
        }

        public static void GetItemTransactions()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetItemTransactions";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetItemTransactionsRequestType request = new GetItemTransactionsRequestType();
            request.ItemID = "121860012964";
            request.Version = "949";
            GetItemTransactionsResponseType response = service.GetItemTransactions(request);  
            Console.WriteLine(response.Timestamp);
        }

        public static void GetSellerTransactions()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetSellerTransactions";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetSellerTransactionsRequestType request = new GetSellerTransactionsRequestType();
            request.Version = "949";
            var response = service.GetSellerTransactions(request);
            Console.WriteLine(response.Timestamp);
        }

        public static void GetMyeBaySelling()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetMyeBaySelling";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetMyeBaySellingRequestType request = new GetMyeBaySellingRequestType();
            request.Version = "949";
            var response = service.GetMyeBaySelling(request);
            Console.WriteLine(response.Timestamp);
        }

        public static void GetSellingManagerSaleRecord()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetSellingManagerSaleRecord";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetSellingManagerSaleRecordRequestType request = new GetSellingManagerSaleRecordRequestType();
            request.Version = "949";
            var response = service.GetSellingManagerSaleRecord(request);
            Console.WriteLine(response.Timestamp);
        }

        public static void GetSellingManagerSoldListings()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetSellingManagerSoldListings";
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
                "AgAAAA**AQAAAA**aAAAAA**c8WOVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAlIeiCpiLoAydj6x9nY+seQ**JiADAA**AAMAAA**sPiqCG+IiAwqtalGeoTnnESc7Br2D+btBopa9arMMDNaOfglryGRHk3tn/aXAj3p2/KmDiKSmYrg51QGFNokYSGUskmH/jjsOtKgoLyTTJZ+3CtWqeAOz/cbrYadAD8l+s6xUfnTk9mWm4BjyAfYqJ1zkNHUC5YaFTk+oaDZPZ9bE7uGjfw1cvQeX6M7TalgTSygqdVV6hOVJZ3I9UPuO66HchFTPvd4n02aZ2UfsXrcYdOpstNdjLuETQIB5tmUWo6uiCwh/r+eiWt8jIycZegb/9uRHzwEy7rW9Tk7fIpIohoBtryYRLUnMJvy9Dg4l++AhFY0yakWJsWu7VHy7eCuz+OI0Pk+E+uOQhgQRzIji96K6/AnBNV9lLiOa6CiI5MdkcrF2Z4Kr4WoxAgy+4WjoUq+PRG8eDHseFWANwOVhmY9qZJq0ulR9SNcXd8FoRiinxzx3f+lO+MgfrRcea2QKKQVoYDI69jKOQ568FVQ6Zp0ClJy9ru/L9IqB87COBLFP6Ie+Zx+2nhgj+GuARYOu2z0Z7kqx+R6H19hIYoxNncQtGi2ruzqWXG+hbFWXTlqrne8IiJr1udgK1ZxJk9FTCCKQCx0s57SXuBkyaM15y2pqC+ze43ZiLGC3wk94pWEACDNRWu4rH27RZTN+ALBoGWkVdSzGxuVfMD164ak4cAJrIiT2OX77FqnLN+MGcVbfJCAu+BREtguSzW6JmM9qHXHBI+4H/jfGruvYoIt2zg7DzMFv50i+hpnLe5F";
            // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = Settings.AppId;
            service.RequesterCredentials.Credentials.DevId = Settings.DevId;
            service.RequesterCredentials.Credentials.AuthCert = Settings.CertId;
            // Make the call
            GetSellingManagerSoldListingsRequestType request = new GetSellingManagerSoldListingsRequestType();
            request.Version = "949";
            var response = service.GetSellingManagerSoldListings(request);
            Console.WriteLine(response.Timestamp);
        }
    }
}
