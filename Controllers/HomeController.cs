using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OmdbApi.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace OmdbApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieSearch(Movie model)
        {
            if (ModelState.IsValid)
            {
                string apiKey = "5f212cf8";
                string apiUrl = $"http://www.omdbapi.com/?apikey={apiKey}&t={model.Title}";

                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(apiUrl).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        Movie movie = JsonConvert.DeserializeObject<Movie>(json);
                        return View(movie);
                    }
                }
            }

            return View(model);
        }
    }
}
