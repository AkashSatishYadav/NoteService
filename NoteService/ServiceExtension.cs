using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteService.Domain.Repositories;
using NoteService.Infrastructure;
using NoteService.Infrastructure.QueueService;
using NoteService.Services;
using NoteService.Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

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

        public static void ConfigureAuthentication(this IServiceCollection services)
		{
			// Before configuring authentication:
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // clear legacy mappings

 services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.MapInboundClaims = false; // ensure claims aren't remapped
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "notesAPI",
            NameClaimType = "sub" // map the Name to the 'sub' claim
        };
    });

            // services.AddAuthentication("Bearer")
    // .AddJwtBearer("Bearer", options =>
    // {
        // options.Authority = "https://localhost:5001"; // your IDP
        // options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        // {
            // ValidateAudience = true,
            // ValidAudience = "notesAPI",
			// NameClaimType = "sub"
        // };
    // });
		}

        public static void ConfigureAuthorization(this IServiceCollection services) =>
            services.AddAuthorization(options =>
            {
                options.AddPolicy("NotesApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "notesAPI");
                });
            });
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
    config.UseSqlServerStorage(configuration.GetConnectionString("hangfireConnection")));
           services.AddHangfireServer();
           services.AddScoped<INoteCleanupService, NoteCleanupService>();
        }

        public static void ConfigureMessageQueue(this IServiceCollection service) =>
            service.AddScoped<IMessageQueueService, RabbitMqService>();

        public static async Task ConfigureRabbitMq(this IServiceCollection services)
        {
            RabbitMqConnection rabbitMqConnection = new RabbitMqConnection();
            await rabbitMqConnection.InitializeConnection();
            services.AddSingleton<IRabbitMqConnection>(rabbitMqConnection);
        }
    }
}
