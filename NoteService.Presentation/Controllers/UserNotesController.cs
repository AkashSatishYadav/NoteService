using Microsoft.AspNetCore.Mvc;
using NoteService.Services.Abstraction;

namespace NoteService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class UserNotesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserNotesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserNotes(string userId)
        {
            return Ok(_service.UserNoteService.GetNotesByUserId(userId, false));
        }
    }
}
