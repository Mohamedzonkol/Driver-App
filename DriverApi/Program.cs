using Driver.DataServicers.Data;
using Driver.DataServices.Repositories;
using Driver.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Inject The MediatR
builder.Services.AddMediatR(con=>con.RegisterServicesFromAssembly(typeof(Program).Assembly));
//Add Redis Connection
builder.Services.AddSingleton<ConnectionMultiplexer>(provider =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisDb"));
});
//Add Watch Dog
builder.Services.AddWatchDogServices(options =>
{
    options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
    options.IsAutoClear = false;
    options.SetExternalDbConnString = builder.Configuration.GetConnectionString("LoggingConnection");
    options.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Inject WatchDog exception logger middleware
app.UseWatchDogExceptionLogger();

// Configure WatchDog for security
app.UseWatchDog(options =>
{
   options.WatchPageUsername = builder.Configuration["Secrets:UserName"];
   options.WatchPagePassword = builder.Configuration["Secrets:Password"];
});
app.Run();
