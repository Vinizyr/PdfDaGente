//using EmissorDePdf.App.Features.Handlers;
using EmissorDePdf.App.Validations;
using EmissorDePdf.Domain.IRepository;
using EmissorDePdf.Email;
using EmissorDePdf.Infra.Repository;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Emissor de Pdf",
        Version = "v1",
        Description = "Emite pdf de compras e manda para o email do cliente"
        // Adicione mais informações, se necessário
    });
});

//Repositories
builder.Services.AddScoped<IPdfRepository, PdfRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

//Validations
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GerarPdfCommandValidator>());


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Emissor de PDF´s");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
string wwwroot = app.Environment.WebRootPath;
RotativaConfiguration.Setup(wwwroot, "Rotativa");

app.Run();
