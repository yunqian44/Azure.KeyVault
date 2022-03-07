using Azure.KeyVault.WebApp.Models;
using Azure.KeyVault.WebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IKeyVaultService _keyVaultService;

        private readonly ISecretsService _secretsService;

        public HomeController(ILogger<HomeController> logger,
            IKeyVaultService keyVaultService,
            ISecretsService secretsService)
        {
            _logger = logger;
            _keyVaultService = keyVaultService;
            _secretsService = secretsService;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<KeyValueViewModel>();
            list.Add(new KeyValueViewModel() { Key ="cnbate-name", Value = await _keyVaultService.GetSecretByKeyAsync("cnbate-name") });
            list.Add(new KeyValueViewModel() { Key = "cnbate-num", Value = await _keyVaultService.GetSecretByKeyAsync("cnbate-num") });
            list.Add(new KeyValueViewModel() { Key = "cnbate-time", Value = await _keyVaultService.GetSecretByKeyAsync("cnbate-time") });

            list.Add(new KeyValueViewModel() { Key = "cnbate-name", Value = await _secretsService.GetSecretsAsync("cnbate-name") });
            list.Add(new KeyValueViewModel() { Key = "cnbate-num", Value = await _secretsService.GetSecretsAsync("cnbate-num") });
            list.Add(new KeyValueViewModel() { Key = "cnbate-time", Value = await _secretsService.GetSecretsAsync("cnbate-time") });

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
