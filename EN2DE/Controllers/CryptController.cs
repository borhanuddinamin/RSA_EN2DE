using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EN2DE.Models;
using EN2DE.Services;

namespace EN2DE.Controllers
{
    public class CryptController : Controller
    {
        private readonly ILogger<CryptController> _logger;
        private readonly CryptService _cryptService;

        public CryptController(ILogger<CryptController> logger)
        {
            _logger = logger;
            _cryptService = new CryptService();
        }

        public IActionResult Index()
        {
            return View(new CryptModel());
        }

        [HttpPost]
        public IActionResult Process(CryptModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsEncrypt)
                {
                    model.OutputText = _cryptService.Encrypt(model.InputText, model.Password);
                }
                else
                {
                    model.OutputText = _cryptService.Decrypt(model.InputText, model.Password);
                }
            }
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}