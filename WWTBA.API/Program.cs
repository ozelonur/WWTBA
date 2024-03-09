using System.Reflection;
using System.Text;
using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WWTBA.API.Filters;
using WWTBA.API.Middlewares;
using WWTBA.API.Modules;
using WWTBA.Core.Models;
using WWTBA.Repository;
using WWTBA.Service.Mapping;
using WWTBA.Service.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); // Bu satır önemli!
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LessonValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LessonCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LessonUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubjectCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubjectUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<QuestionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<QuestionCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<QuestionUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AnswerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AnswerCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AnswerUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserAnswerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserAnswerCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserAnswerUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserTestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserTestCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserTestUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RefreshTokenValidator>();

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.Configure<SMTPSettings>(builder.Configuration.GetSection("SmtpSettings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<,>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
        option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCustomException();

app.MapControllers();
app.UseIpRateLimiting();

app.Run();