using NoteService.Domain.Repositories;
using NoteService.Services.Abstraction;
using System.Runtime.CompilerServices;

namespace NoteService.Services
{
    public class ServiceManager : IServiceManager
    {
        public IUserNoteService UserNoteService => _userNoteService.Value;

        private readonly Lazy<IUserNoteService> _userNoteService;

        public ServiceManager(IRepositoryManager repository)
        {
               _userNoteService =new Lazy<IUserNoteService>(()=> new  UserNoteService(repository));
        }
    }
}
