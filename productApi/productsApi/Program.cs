using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using productsApi.Interfaces;
using productsApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//    .AddNewtonsoftJson(options =>
//{
//    // Ignora os loopings nas consultas
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
// Ignora valores nulos ao fazer junções nas consultas
//    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultChallengeScheme = "JwtBearer";
//    options.DefaultAuthenticateScheme = "JwtBearer";
//})

//.AddJwtBearer("JwtBearer", options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        //valida quem está solicitando
//        ValidateIssuer = true,

//        //valida quem está recebendo
//        ValidateAudience = true,

//        //define se o tempo de expiração será validado
//        ValidateLifetime = true,

//        //forma de criptografia e valida a chave de autenticação
//        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("products-webapi-chave-symmetricsecuritykey")),

//        //valida o tempo de expiração do token
//        ClockSkew = TimeSpan.FromMinutes(30),

//        //nome do issuer (de onde está vindo)
//        ValidIssuer = "Products-WebAPI",

//        //nome do audience (para onde está indo)
//        ValidAudience = "Products-WebAPI"
//    };
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Adicione o gerador do Swagger à coleção de serviços
builder.Services.AddSwaggerGen(options =>
{
    //Adiciona informações sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API for Products",
        Description = "Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Informática"
        }
    });



    //Usando a autenticaçao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
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
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();