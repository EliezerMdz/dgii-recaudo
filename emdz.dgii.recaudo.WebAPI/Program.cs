using emdz.dgii.recaudo.Application;
using emdz.dgii.recaudo.CrossCutting.Mapper;
using emdz.dgii.recaudo.Domain.Interfaces.Application;
using emdz.dgii.recaudo.Domain.Interfaces.Repository;
using emdz.dgii.recaudo.Domain.Interfaces.Service;
using emdz.dgii.recaudo.Domain.Service;
using emdz.dgii.recaudo.Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Ignorar propiedades con valor null
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

    // (Opcional) Formatear con camelCase
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger configuration with OpenAPI info and XML comments
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DGII Recaudo API",
        Version = "v1",
        Description = "My .NET 9 API",
        Contact = new OpenApiContact { Name = "EMZ", Email = "dev@acme.com" }
    });

    // XML comments
    var xml = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    if (File.Exists(xmlPath)) config.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    // Show enums as strings
    config.DescribeAllParametersInCamelCase();
    config.CustomSchemaIds(t => t.FullName); // avoids name clashes on generics

    // Bearer/JWT support (optional)
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

// Dependency Injection for application services

builder.Services.AddScoped<IDgiiApplication, DgiiApplication>();

// Dependency Injection for infrastructure services

builder.Services.AddScoped<IDgiiService, DgiiService>();

// Dependency Injection for repositories

builder.Services.AddScoped<IDgiiRepository, DgiiRepository>();

// AutoMapper configuration
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<SignatureProfile>();

    config.AddProfile<EntityProfile>();
});

// Resolving in AutoMapper profile [taxPayerId] to [TaxPayerDto]
builder.Services.AddTransient<TaxPayerResolver>();

// Resolving in AutoMapper profile [taxPayerTypeId] to [TaxPayerTypeDto]
builder.Services.AddTransient<TaxPayerTypeResolver>();

// Resolving in AutoMapper profile [DocumentTypeId] to [DocumentTypeDto]
builder.Services.AddTransient<DocumentTypeForTaxPayerResolver>();
builder.Services.AddTransient<DocumentTypeForNaturalPersonResolver>();

// Resolving in AutoMapper profile [NaturalPersonDto] when TaxPayerType.Code == "PER"
builder.Services.AddTransient<NaturalPersonResolver>();

// Resolving in AutoMapper profile [LegalEntityDto]  when TaxPayerType.Code == "EMP"
builder.Services.AddTransient<LegalEntityResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "DGII Recaudo API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
