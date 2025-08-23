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


        [HttpGet]
        public async Task<IActionResult> GetUserNotes()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }
            var userNotes = await _service.UserNoteService.GetNotesByUserIdAsync(userId, false);
            return Ok(userNotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] UserNoteDto note)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            note.UserID = userId;
            await _service.UserNoteService.CreateNoteAsync(note);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] UserNoteDto note)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            note.UserID = userId;
            await _service.UserNoteService.UpdateNoteAsync(note);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote([FromBody] UserNoteForDeleteDto note)
        {
            //var userId = User.FindFirst("sub")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized("User ID not found in token.");
            //}

            //note.UserId = userId;
            await _service.UserNoteService.DeleteNoteAsync(note);

            return NoContent();
        }
    }
}
