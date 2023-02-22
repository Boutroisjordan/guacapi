using System.Text;
using GuacAPI.Authorization;
using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using GuacAPI.Context;
using GuacAPI.ExtensionMethods;
using GuacAPI.Helpers;
using GuacAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Db context
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});


//Instancie la class DefaultProductRepository à chaque fois qu'il rencontre une Interface IProductRepository (interface ne s'intanscie pas toute seule)
//builder.Services.AddScoped<IProductRepository, DefaultProductRepository>();

builder.Services.AddInjections(); //Inject all injection depandencies

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMemoryCache();

//permet d'ajouter du context http 
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           var value = builder.Configuration.GetSection("AppSettings:Secret").Value;
           if (value != null)
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey =
                       new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(value)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
       });


builder.Services.AddCors(options =>
    options.AddDefaultPolicy(cors =>
    {
        cors
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
}
// ---------------

app.UseStaticFiles();// For the wwwroot folder

app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Images")),
            RequestPath = "/Images"
        });



//---------------
app.UseCors();
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

// app.UseMiddleware<APIKeyMiddleware>(builder.Configuration.GetSection("ApiKey:Key").Value);

app.MapControllers();

app.Run();