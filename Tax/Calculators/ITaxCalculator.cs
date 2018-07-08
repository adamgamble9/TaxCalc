using Tax.Models;

namespace Tax.Calculators
{
    public interface ITaxCalculator
    {
        object GetRatesForLocation(string zip, LocationModel model = null);
        object GetTaxForOrder(OrderModel model);
    }
}
