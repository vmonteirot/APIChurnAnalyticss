using ChallengeChurnAnalytics.Data;
using ChallengeChurnAnalytics.Repository;
using ChallengeChurnAnalytics.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// configura��o do auth0
builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

// configura��o do banco de dados e outros servi�os
builder.Services.AddDbContext<DataContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddScoped<ICadastroEmpresaRepository, CadastroEmpresaRepository>();

// registrar o PaymentService
builder.Services.AddSingleton<PaymentService>();

builder.Services.AddControllers();

// configura��o do swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - ChurnAnalytics",
        Version = "v1",
        Description = "API para an�lise de churn para empresas B2B. Permite gerenciar cadastros de empresas, feedbacks e informa��es adicionais",
        Contact = new OpenApiContact
        {
            Name = "Kaique Toschi",  // atualizado 
            Email = "kaiquetoschi10@gmail.com",  // atualizado 
            Url = new Uri("https://github.com/KaiqueToschi")  // atualizado 
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// configura��o de autentica��o e autoriza��o
app.UseAuthentication();  // adiciona a autentica��o aqui
app.UseAuthorization();

app.MapControllers();

app.Run();
