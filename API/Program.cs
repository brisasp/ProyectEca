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
using API.Configuration;

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
//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddSingleton<EmailService>();


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

async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var context = serviceProvider.GetRequiredService<TimeSpanConverter>(); //


    string[] roles = { "admin", "profesor" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var adminEmail = "admin@ateca.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Administrador"
        };
        var result = await userManager.CreateAsync(newUser, "Admin123*");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newUser, "admin");
        }
    }
    //para hacer seed de DiasNoLectivos tambien
    var diasPredeterminados = new List<DiaNoLectivoEntity>
{
    
    new() { Fecha = new DateTime(2025, 12, 25), Motivo = "Navidad" },
    new() { Fecha = new DateTime(2026, 1, 6), Motivo = "Día de Reyes" },
    new() { Fecha = new DateTime(2026, 4, 2), Motivo = "Jueves Santo" },
};

    bool hayCambios = false;

    foreach (var dia in diasPredeterminados)
    {
        if (!context.DiasNoLectivos.Any(d => d.Fecha == dia.Fecha))
        {
            context.DiasNoLectivos.Add(dia);
            hayCambios = true;
        }
    }

    if (hayCambios)
    {
        await context.SaveChangesAsync();
        Console.WriteLine("Días no lectivos añadidos.");
    }
    else
    {
        Console.WriteLine("No se añadieron días no lectivos porque ya existían.");
    }
}





using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndAdminAsync(services);
}



app.Run();
