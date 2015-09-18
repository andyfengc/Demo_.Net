using System;
using System.Collections.Generic;
using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class AddressValidationTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";
        }

        [TestMethod]
        public void TestMethod1()
        {
            Address caAddress = new Address()
            {
                company = "Koob",
                street1 = "1111 TUXEDO court",
                street2 = "Unit 1",
                city = "toronto",
                state = "ON",
                country = "CA",
                zip = "M1G3S5"
            };

            Address usAddress = new Address()
            {
                company = "Simpler Postage Inc",
                street1 = "1645 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };

            Address ukAddress = new Address()
            {
                company = "Simpler Postage Inc",
                street1 = "59 GREAT MARLBOROUGH ST BIBIGO K",
                street2 = "Unit 1",
                city = "LONDON",
                country = "GB",
                zip = "W1F7JY"
            };

            caAddress.Create();

            caAddress.Verify();

            int a = 1;
        }
    }
}
