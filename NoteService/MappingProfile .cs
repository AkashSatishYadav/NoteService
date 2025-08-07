using AutoMapper;
using NoteService.Domain.Models;
using NoteService.Shared.DataTransferObjects;

namespace NoteService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserNote, UserNoteDto>();
        }
    }
}
