

using Microsoft.OpenApi.Models;
using Post.Application;
using Post.Implementation.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddAutoMapperNMediatRServices();

 builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( o => 
{
    o.IncludeXmlComments("Docs.Api.xml");
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authoriztion header using bearer schema. " +
                      "Exm: \"Authoriztion: Bearer {token}\"",
        Name= "Authoriztion",
        In= ParameterLocation.Header,
        Type= SecuritySchemeType.Http,
        Scheme="bearer",
        Reference = new 
        OpenApiReference { Type = ReferenceType.SecurityScheme,Id="Bearer"}
    };
    o.AddSecurityDefinition("Bearer", securitySchema);
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema,new[]{"Bearer"} }
    });
    } );

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
