using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.User
{
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _aboutService.GetAll());
        }
    }
}
