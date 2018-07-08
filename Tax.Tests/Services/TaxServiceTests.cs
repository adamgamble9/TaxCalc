using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax.Calculators;
using Tax.Models;
using Tax.Services;

namespace Tax.Tests.Services
{
    [TestClass]
    public class TaxServiceTests
    {
        [TestMethod]
        public void GetRatesForLocation_Test()
        {
            TaxjarCalculator taxjarCalculator = new TaxjarCalculator();
            TaxService<TaxjarCalculator> taxService = new TaxService<TaxjarCalculator>(taxjarCalculator);

            string zip = "33408";
            LocationModel locationModel = new LocationModel
            {
                Street = "790 Juno Ocean Walk",
                City = "Juno Beach",
                State = "FL",
                Country = "US"
            };

            object result = taxService.GetRatesForLocation(zip, locationModel);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTaxForOrder_Test()
        {
            TaxjarCalculator taxjarCalculator = new TaxjarCalculator();
            TaxService<TaxjarCalculator> taxService = new TaxService<TaxjarCalculator>(taxjarCalculator);

            LocationModel fromLocation = new LocationModel
            {
                Zip = "92093",
                Street = "9500 Gilman Drive",
                City = "La Jolla",
                State = "CA",
                Country = "US"
            };
            LocationModel toLocation = new LocationModel
            {
                Zip = "90002",
                Street = "1335 E 103rd Street",
                City = "Los Angeles",
                State = "CA",
                Country = "US"
            };
            OrderModel orderModel = new OrderModel
            {
                FromLocation = fromLocation,
                ToLocation = toLocation,
                Amount = 15,
                Shipping = 1.5,
                NexusAddresses = new List<LocationModel>
                {
                    new LocationModel
                    {
                        Id = "Main Location",
                        Country = "US",
                        Zip = "92093",
                        State = "CA",
                        City = "La Jolla",
                        Street = "9500 Gilman Drive"
                    }
                },
                LineItems = new List<LineItemModel>
                {
                    new LineItemModel
                    {
                        Id = "1",
                        Quantity = 1,
                        ProductTaxCode = "20010",
                        UnitPrice = 15,
                        Discount = 0
                    }
                }
            };

            object result = taxService.GetTaxForOrder(orderModel);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
