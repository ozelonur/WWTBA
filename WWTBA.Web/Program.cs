using FluentValidation;
using FluentValidation.AspNetCore;
using WWTBA.Service.Validations;
using WWTBA.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LessonCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubjectCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubjectUpdateDtoValidator>();

builder.Services.AddHttpClient<LessonApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<SubjectApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

var app = builder.Build();
app.UseExceptionHandler("/Home/Error");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();