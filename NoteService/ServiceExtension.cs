using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Repositories;
using NoteService.Infrastructure;
using NoteService.Services;
using NoteService.Services.Abstraction;

namespace NoteService
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
    services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
