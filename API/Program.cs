using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DesignAPI.Data;
using DesignAPI.Models.Entity;
using DesignAPI.Repository.IRepository;
using DesignAPI.Repository;
using System.Text;
using DesignAPI.AutoMapper;
using RestAPI.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

// Db connection config
builder.Services.AddDbContext<TimeSpanConverter>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

// Registrar Identity para AppUser (eliminamos la segunda configuración)
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<TimeSpanConverter>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IDiaNoLectivoRepository, DiaNoLectivoRepository>();
builder.Services.AddScoped<IFranjaHorarioRepository, FranjaHorarioRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

// Logger setup
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Memory Cache setup
builder.Services.AddMemoryCache();
// Configuración de Google Authentication
//builder.Services.AddAuthentication()
   // .AddGoogle(options =>
   // {
     //   options.ClientId = builder.Configuration["GoogleAuth:ClientId"]; // Tu Client ID
      //  options.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"]; // Tu Client Secret
  //  });



// Authentication setup (JWT)
var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Required for Authorization
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Token \r\n\r\n" +
        "Insert The token with the following format: Bearer thgashqkssuqj",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer"
            },
            new List<string>()
        }
    });
});

// CORS Policy
builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Llamar al Seeder de datos
//using (var scope = app.Services.CreateScope())
//{
//  var services = scope.ServiceProvider;
//  var context = services.GetRequiredService<ApplicationDbContext>();
//   var userManager = services.GetRequiredService<UserManager<AppUser>>();
//   DataSeeder.Seed(context, userManager);
//}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
