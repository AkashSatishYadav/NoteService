using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoteService.Services.Abstraction;
using NoteService.Shared.DataTransferObjects;

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

        [HttpGet("{userId}", Name = "GetNotesByUserId")]
        public IActionResult GetUserNotes(string userId)
        {
            return Ok(_service.UserNoteService.GetNotesByUserId(userId, false));
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] UserNoteDto note)
        {
            _service.UserNoteService.CreateNote(note);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
