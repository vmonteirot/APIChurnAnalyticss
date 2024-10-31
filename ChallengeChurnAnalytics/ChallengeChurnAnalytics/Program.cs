using ChallengeChurnAnalytics.Data;
using ChallengeChurnAnalytics.Repository;
using ChallengeChurnAnalytics.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// configuração do auth0
builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

// configuração do banco de dados e outros serviços
builder.Services.AddDbContext<DataContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddScoped<ICadastroEmpresaRepository, CadastroEmpresaRepository>();

// registrar o PaymentService
builder.Services.AddSingleton<PaymentService>();

builder.Services.AddControllers();

// configuração do swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - ChurnAnalytics",
        Version = "v1",
        Description = "API para análise de churn para empresas B2B. Permite gerenciar cadastros de empresas, feedbacks e informações adicionais",
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

// configuração de autenticação e autorização
app.UseAuthentication();  // adiciona a autenticação aqui
app.UseAuthorization();

app.MapControllers();

app.Run();
