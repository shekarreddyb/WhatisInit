using Microsoft.AspNetCore.Mvc;
using WhatIsInIt.Models;

namespace WhatIsInIt.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        [Route("LoadInteractionById")]
        public async Task<IActionResult> LoadInteractionById(Guid id)
        {
            await Task.Delay(5000);
            _logger.LogInformation("Loading interaction by Id {InteractionId}", id);

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        [Route("SendPrompt")]
        public IActionResult SendPrompt([FromBody] PromptPayload prompt)
        {
            _logger.LogInformation("sending prompt");
            return Ok(prompt);
        }
    }
}