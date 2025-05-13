using System.Reflection;
using System.Threading.RateLimiting;
using FluentValidation;
using FluentValidation.AspNetCore;
using HospitalManagement.Application.Commands.CreateDoctor;
using HospitalManagement.Application.Commands.CreateRoom;
using HospitalManagement.appsettingsModel;
using HospitalManagement.Behaviors;
using HospitalManagement.DataAccess;
using HospitalManagement.Extensions;
using HospitalManagement.RedisCacheService;
using HospitalManagement.Repository;
using HospitalManagement.Repository.Interfaces;
using HospitalManagement.Services;
using MediatR;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Odata filtering 


builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));
//end


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Caching services

builder.Services.AddMemoryCache();

var configuration = builder.Configuration;

builder.Services.AddCorsConfiguration();
builder.Services.AddJwtConfiguration();


#region RedisConfiguration    


var redisConfiguration = builder.Configuration.GetConnectionString("RedisDistributedConnection");
var redis = ConnectionMultiplexer.Connect(redisConfiguration);
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

#endregion

// Added Serilog


builder.Services.AddSerilog((serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(configuration);
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DoctorSettings>(configuration.GetSection("DoctorsSettings"));

builder.Services.Configure<AppointmentSettings>(configuration.GetSection("AppointmentSettings"));

// second way to configure

builder.Services.AddOptions<FileStorage>()
    .Bind(configuration.GetSection("FileStorage"))
    .ValidateDataAnnotations();




builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();


builder.Services.AddDbContext<HospitalContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});

// rate Limiter

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

// Fixed window rate limiter
rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
{
    options.PermitLimit = 5;
    options.Window = TimeSpan.FromSeconds(10);
    //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // FIFO
    //options.QueueLimit = 5; // Maximum number of requests in the queue

});

   // Global rate limiter
    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Request.Headers["X-Client-ID"].ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1)
                //QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                //QueueLimit = 5
            }));


    rateLimiterOptions.AddSlidingWindowLimiter("slicing", options =>
    {
        options.PermitLimit = 5;
        options.SegmentsPerWindow = 2;
        options.Window = TimeSpan.FromSeconds(10);
    });

});


//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

builder.Services.AddMediatR(cfg
    =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
 
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoomDtoValidator>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ValidationExceptionMiddleware>();



app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter(); // RateLimiter 

app.MapControllers();

app.Run();
