using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.com.ebay.developer;

namespace Test
{
    internal class GeteBayOfficialTime
    {
        
        public static void TestSandbox()
        {
            string endpoint = "https://api.sandbox.ebay.com/wsapi";
            string callName = "GeteBayOfficialTime";
            string siteId = "0";
            string appId = "KoboIncab-650b-4637-abcd-a6792d80993"; // use your app ID
            string devId = "c81af250-3212-4e28-8d7b-bee0826266e1"; // use your dev ID
            string certId = "d82f3fcb-303c-409b-9446-235e799f8589"; // use your cert ID
            string version = "405";
            // Build the request URL
            string requestURL = endpoint
                                + "?callname=" + callName
                                + "&siteid=" + siteId
                                + "&appid=" + appId
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
            service.RequesterCredentials.Credentials.AppId = appId;
            service.RequesterCredentials.Credentials.DevId = devId;
            service.RequesterCredentials.Credentials.AuthCert = certId;
            // Make the call to GeteBayOfficialTime
            GeteBayOfficialTimeRequestType request = new GeteBayOfficialTimeRequestType();
            request.Version = "405";
            GeteBayOfficialTimeResponseType response = service.GeteBayOfficialTime(request);
            Console.WriteLine("The time at eBay headquarters in San Jose, California, USA, is:");
            Console.WriteLine(response.Timestamp);
        }

        public static void TestProduction()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GeteBayOfficialTime";
            string siteId = "0";
            string appId = "c81af250-3212-4e28-8d7b-bee0826266e1"; // use your app ID
            string devId = "KoboInccf-4223-4880-9d21-c0b48323836"; // use your dev ID
            string certId = "c6a81e6d-d0ee-45e4-a7e8-3a1a9df2dcfd"; // use your cert ID
            string version = "405";
            // Build the request URL
            string requestURL = endpoint
                                + "?callname=" + callName
                                + "&siteid=" + siteId
                                + "&appid=" + appId
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
            service.RequesterCredentials.Credentials.AppId = appId;
            service.RequesterCredentials.Credentials.DevId = devId;
            service.RequesterCredentials.Credentials.AuthCert = certId;
            // Make the call to GeteBayOfficialTime
            GeteBayOfficialTimeRequestType request = new GeteBayOfficialTimeRequestType();
            request.Version = "405";
            GeteBayOfficialTimeResponseType response = service.GeteBayOfficialTime(request);
            Console.WriteLine("The time at eBay headquarters in San Jose, California, USA, is:");
            Console.WriteLine(response.Timestamp);
        }
    }
}
