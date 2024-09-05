using MovieStore.DbOperations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using MovieStore.MiddleWares;
using MovieStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero,

    };
}); // authentication servis ekleme

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    options.JsonSerializerOptions.IgnoreNullValues = true;

    // Yeni eklenen ayar
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Döngüsel referansları yok sayma
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "MovieStoreDb"));
builder.Services.AddScoped<IMovieStoreDbContext>(provider =>provider.GetService<MovieStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IloggerService, ConsoleLogger>(); // builder.Services.AddSingleton<IloggerService, DbLogger>(); yaparak db loglamýþ olruz tek bir hamlede baðýmlýklardan kurtulduk

var app = builder.Build();

// Create a scope to get the DbContext and initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // authentication yapýlmal önce önce kimlik doprulama sonra yetkilendirme yapýlýr  INTERNAL MIDDLAWAREDE AKTIFLESTIRILDI
app.UseHttpsRedirection();

app.UseAuthorization();

app.useCustomExceptionMiddleWare(); // costum oluþturduðumuz middleWare calistirdik.

app.MapControllers();

app.Run();
