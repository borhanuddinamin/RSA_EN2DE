using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EN2DE.Models;
using EN2DE.Services;
using System.Numerics;

namespace EN2DE.Controllers
{
    public class RSACryptController : Controller
    {
        private readonly ILogger<RSACryptController> _logger;
        private readonly RSACryptService _rsaCryptService;

        public RSACryptController(ILogger<RSACryptController> logger, RSACryptService rsaCryptService)
        {
            _logger = logger;
            _rsaCryptService = rsaCryptService;
        }

        public IActionResult Index()
        {
            return View(new RSACryptModel());
        }

        [HttpPost]
        public IActionResult GenerateKeys()
        {
            var keyPair = _rsaCryptService.GenerateKeyPair();
            return Json(keyPair);
        }

        [HttpPost]
        public IActionResult Encrypt(string plaintext, string keyPair)
        {
            var encryptedText = _rsaCryptService.Encrypt(plaintext, keyPair);
            return Json(new { encryptedText });
        }

        [HttpPost]
        public IActionResult Decrypt(string cipherText, string keyPair)
        {
            var decryptedText = _rsaCryptService.Decrypt(cipherText, keyPair);
            return Json(new { decryptedText });
        }
    }
}