using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _environment)
        {
            _logger = logger;
            Environment = _environment;
        }

        public IActionResult Index()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Data/data.json");
            string json = System.IO.File.ReadAllText(path);
            var urls = JsonConvert.DeserializeObject<List<UrlTarget>>(json);

            Random rnd = new Random();
            var t = rnd.Next(1, urls.Count);
            var uri2Go = (urls.Skip(t-1).Take(1).FirstOrDefault().Uri);
            return Redirect(uri2Go);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
