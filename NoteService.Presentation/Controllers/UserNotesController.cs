using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteService.Services.Abstraction;
using NoteService.Shared.DataTransferObjects;

namespace NoteService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "NotesApiScope")]
    public class UserNotesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserNotesController(IServiceManager service)
        {
            _service = service;
        }


        [HttpGet("{userId}", Name = "GetNotesByUserId")]
        public async Task<IActionResult> GetUserNotes(string userId)
        {
            var userNotes = await _service.UserNoteService.GetNotesByUserIdAsync(userId, false);
            return Ok(userNotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] UserNoteDto note)
        {
            await _service.UserNoteService.CreateNoteAsync(note);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] UserNoteDto note)
        {
            await _service.UserNoteService.UpdateNoteAsync(note);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote([FromBody] UserNoteForDeleteDto note)
        {
            await _service.UserNoteService.DeleteNoteAsync(note);

            return NoContent();
        }
    }
}
