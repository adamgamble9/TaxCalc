using System.Web.Mvc;
using Tax.Calculators;
using Tax.Services;

namespace Tax.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Example of use
            TaxjarCalculator taxjarCalculator = new TaxjarCalculator();
            TaxService<TaxjarCalculator> taxService = new TaxService<TaxjarCalculator>(taxjarCalculator);

            object ratesForLocation = taxService.GetRatesForLocation("33408");
            
            return View();
        }
    }
}