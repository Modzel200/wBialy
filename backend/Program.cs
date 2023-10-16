using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wBialy.Entities;
using wBialy.Models.Validators;
using wBialy.Models;
using FluentValidation.AspNetCore;
using NLog.Web;
using wBialy.Middleware;
using wBialy;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using wBialy.Services;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using wBialy.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var authenticationSetting = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);
builder.Services.AddSingleton(authenticationSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("appDb")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<wBialySeeder>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<EditLFPostDto>, EditLFPostDtoValidator>();
builder.Services.AddScoped<IValidator<CreateLFPostDto>, CreateLFPostDtoValidator>();
builder.Services.AddScoped<IValidator<CreateEventPostDto>, CreateEventPostDtoValidator>();
builder.Services.AddScoped<IValidator<CreateGastroPostDto>, CreateGastroPostDtoValidator>();
builder.Services.AddScoped<IValidator<LFTagDto>, LFTagDtoValidator>();
builder.Services.AddScoped<IValidator<EventTagDto>, EventTagDtoValidator>();
builder.Services.AddScoped<IValidator<GastroTagDto>, GastroTagDtoValidator>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(builder.Configuration["AllowedOrigins"])
        );
});




var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<wBialySeeder>();
seeder.Seed();
app.UseCors("FrontEndClient");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
