using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeoLib.Data;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.Tests
{
    [TestClass]
    public class ManagerTests
    {

        [TestMethod]
        public void TestZipCodeRetrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

            ZipCode zipCode = new ZipCode()
            {
                City = "LINCOLIN PARK",
                State = new State() { Abbreviation = "NJ" },
                Zip = "07035"
            };

            mockZipCodeRepository.Setup(x => x.GetByZip("07035")).Returns(zipCode);

            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

            var zipCodeData = geoService.GetZipInfo("07035");

            Assert.IsTrue(zipCodeData.City.ToUpper() == "LINCOLIN PARK");
            Assert.IsTrue(zipCodeData.State.ToUpper() == "NJ");
            Assert.IsTrue(zipCodeData.ZipCode == "07035");

        }
    }
}
