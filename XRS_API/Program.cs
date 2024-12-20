using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using XRS_API.DbContexts;
using XRS_API.Models.ViewModels;
using XRS_API.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File("C:/Users/alkay/Desktop/Repository/AhmedAlkurdi/applog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
})
.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<XRS_APPContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Connection"])
);

builder.Services.AddTransient<IHotelRepository, HotelRepository>(); 

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<IRoomRepository, RoomRepository>();

builder.Services.AddTransient<IGuestRepository, GuestRepository>();

builder.Services.AddTransient<IBookingRepository, BookingRepository>();

builder.Services.AddTransient<ILoginRepository, LoginRepository>();





builder.Host.UseSerilog();

builder.Services.AddScoped<HotelWithOutListsInfo>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
