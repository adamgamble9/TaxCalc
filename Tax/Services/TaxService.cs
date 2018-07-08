using Tax.Calculators;
using Tax.Models;

namespace Tax.Services
{
    public class TaxService<TTaxCalculator> : ITaxCalculator
        where TTaxCalculator : ITaxCalculator
    {
        #region Private Fields
        
        private readonly TTaxCalculator _taxCalculator;

        #endregion

        #region Constructor
        
        public TaxService(TTaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the tax rates for a location
        /// </summary>
        public object GetRatesForLocation(string zip, LocationModel model = null)
        {
            if (_taxCalculator == null)
                return null;

            return _taxCalculator.GetRatesForLocation(zip, model);
        }

        /// <summary>
        /// Calculate the taxes for an order
        /// </summary>
        public object GetTaxForOrder(OrderModel model)
        {
            if (_taxCalculator == null)
                return null;

            return _taxCalculator.GetTaxForOrder(model);
        }

        #endregion
    }
}