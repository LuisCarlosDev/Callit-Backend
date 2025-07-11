using System.Text;
using Callit.Api.Filters;
using Callit.Api.Token;
using Callit.Application;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Security.Tokens;
using Callit.Infrastructure;
using Callit.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var secretKet = builder.Configuration.GetValue<string>("Settings:Jwt:Secret");

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(config =>
{
  config.AddSecurityDefinition(
    "Bearer",
    new OpenApiSecurityScheme
    {
      Name = "Authorization",
      Description =
        @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
      In = ParameterLocation.Header,
      Scheme = "Bearer",
      Type = SecuritySchemeType.ApiKey,
    }
  );

  config.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
          Scheme = "oauth2",
          Name = "Bearer",
          In = ParameterLocation.Header,
        },
        new List<string>()
      },
    }
  );
});
builder.Services.AddCors(options =>
{
  options.AddPolicy("all", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder
  .Services.AddAuthentication(config =>
  {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(config =>
  {
    config.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = false,
      ValidateAudience = false,
      ClockSkew = new TimeSpan(0),
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKet!)),
    };
  });
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(config =>
{
  config.AddPolicy("all", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("all");

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
  await using var scope = app.Services.CreateAsyncScope();

  await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);
}
