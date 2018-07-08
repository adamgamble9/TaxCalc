using System;
using System.Web.Configuration;
using Tax.Models;
using Taxjar;

namespace Tax.Calculators
{
    public class TaxjarCalculator : ITaxCalculator
    {
        #region Private Fields

        private readonly TaxjarApi _taxjarApi;

        #endregion

        #region Constructor

        public TaxjarCalculator()
        {
            try
            {
                string apiKey = WebConfigurationManager.AppSettings["TaxJarApiKey"];
                _taxjarApi = new TaxjarApi(apiKey);
            }
            catch (Exception e)
            {
                throw new Exception("Could not instantiate TaxjarApi.", e.InnerException);
            }
        }
        
        #endregion
        
        #region Public Methods

        public object GetRatesForLocation(string zip, LocationModel model = null)
        {
            if (_taxjarApi == null)
                return null;

            object apiParameters = LocationModelToApiParameters(model);

            return _taxjarApi.RatesForLocation(zip, apiParameters);
        }

        public object GetTaxForOrder(OrderModel model)
        {
            if (_taxjarApi == null)
                return null;

            object apiParameters = OrderModelToApiParameters(model);
            
            return _taxjarApi.TaxForOrder(apiParameters);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts domain model to object to be consumed by TaxjarApi.RatesForLocation
        /// </summary>
        private object LocationModelToApiParameters(LocationModel model)
        {
            if (model == null)
                return null;

            return new
            {
                street = model.Street ?? string.Empty,
                city = model.City ?? string.Empty,
                state = model.State ?? string.Empty,
                country = model.Country ?? string.Empty
            };
        }

        /// <summary>
        /// Converts domain model to object to be consumed by TaxjarApi.TaxForOrder
        /// </summary>
        private object OrderModelToApiParameters(OrderModel model)
        {
            if (model == null)
                return null;

            int nexusAddressesCount = model.NexusAddresses?.Count ?? 0;
            int lineItemsCount = model.LineItems?.Count ?? 0;

            object[] nexusAddresses = new object[nexusAddressesCount];
            object[] lineItems = new object[lineItemsCount];
            
            for (int i = 0; i < nexusAddressesCount; i++)
            {
                LocationModel locationModel = model.NexusAddresses[i];
                if (locationModel == null)
                    continue;

                nexusAddresses[i] = new
                {
                    id = locationModel.Id ?? string.Empty,
                    country = locationModel.Country ?? string.Empty,
                    zip = locationModel.Zip ?? string.Empty,
                    state = locationModel.State ?? string.Empty,
                    city = locationModel.City ?? string.Empty,
                    street = locationModel.Street ?? string.Empty
                };
            }

            for (int i = 0; i < lineItemsCount; i++)
            {
                LineItemModel lineItemModel = model.LineItems[i];
                if (lineItemModel == null)
                    continue;

                lineItems[i] = new
                {
                    id = lineItemModel.Id ?? string.Empty,
                    quantity = lineItemModel.Quantity,
                    product_tax_code = lineItemModel.ProductTaxCode ?? string.Empty,
                    unit_price = lineItemModel.UnitPrice,
                    discount = lineItemModel.Discount
                };
            }
            
            return new
            {
                from_country = model.FromLocation.Country ?? string.Empty,
                from_zip = model.FromLocation.Zip ?? string.Empty,
                from_state = model.FromLocation.State ?? string.Empty,
                from_city = model.FromLocation.City ?? string.Empty,
                from_street = model.FromLocation.Street ?? string.Empty,
                to_country = model.ToLocation.Country ?? string.Empty,
                to_zip = model.ToLocation.Zip ?? string.Empty,
                to_state = model.ToLocation.State ?? string.Empty,
                to_city = model.ToLocation.City ?? string.Empty,
                to_street = model.ToLocation.Street ?? string.Empty,
                amount = model.Amount,
                shipping = model.Shipping,
                customer_id = model.CustomerId,
                nexus_addresses = nexusAddresses,
                line_items = lineItems
            };
        }

        #endregion
    }
}