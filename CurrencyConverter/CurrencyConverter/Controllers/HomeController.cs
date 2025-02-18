using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using CurrencyConverter.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var currencies = new List<string> { "USD", "EUR", "GBP", "INR", "AUD", "CAD", "JPY", "CNY", "BDT" };
            ViewBag.Currencies = new SelectList(currencies);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConvertCurrency(CurrencyModel model)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = $"https://v6.exchangerate-api.com/v6/8680b9d56abbb6e313b72cda/latest/{model.SourceCurrency}";

                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetStringAsync(apiUrl);
                        dynamic apiResponse = JsonConvert.DeserializeObject(response);
                        System.Diagnostics.Debug.WriteLine($"API Response: {response}");
                        if (apiResponse != null && apiResponse.conversion_rates != null)
                        {
                            var rates = apiResponse.conversion_rates;
                            if (rates[model.DestinationCurrency] != null)
                            {
                                decimal conversionRate = (decimal)rates[model.DestinationCurrency];
                                model.ConvertedAmount = model.Amount * conversionRate;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Error: Destination currency not found.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error: API response does not contain conversion rates.");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        ModelState.AddModelError("", $"API Error: {ex.Message}");
                    }
                }
            }

            var currencies = new List<string> { "USD", "EUR", "GBP", "INR", "AUD", "CAD", "JPY", "CNY", "BDT" };
            ViewBag.Currencies = new SelectList(currencies);
            return View("Index", model);
        }
    }
}
