using Hangfire;
using NoteService;
using NoteService.Infrastructure.QueueService;
using NoteService.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureMessageQueue();
await builder.Services.ConfigureRabbitMq();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureAuthorization();
//builder.Services.ConfigureCors();
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(NoteService.Presentation.AssemblyReference).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
var rabbitConnection = app.Services.GetRequiredService<IRabbitMqConnection>() as RabbitMqConnection;
app.Lifetime.ApplicationStopping.Register(() => rabbitConnection?.Dispose());
app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
//app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<INoteCleanupService>(
    "cleanup-deleted-notes",
    service => service.CleanupOldDeletedNotesAsync(),
    Cron.Daily());
app.MapControllers();
app.Run();
