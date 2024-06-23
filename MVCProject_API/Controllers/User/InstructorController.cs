using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Controllers.User
{
    public class InstructorController : BaseController
    {
        private readonly IInstructorService _instructorService;
        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _instructorService.GetAll());
        }
    }
}
