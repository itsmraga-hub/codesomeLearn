using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using codesome.Server.Data;
using NSwag;
using System.Text;
using Asp.Versioning;
using codesome.Server.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["key"]));


builder.Services.AddAuthorization();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });


// Swagger
NSwag.OpenApiSecurityScheme openApiSecurityScheme = new NSwag.OpenApiSecurityScheme
{
    Type = OpenApiSecuritySchemeType.ApiKey,
    Name = "Authorization",
    In = OpenApiSecurityApiKeyLocation.Header
};


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
/*builder.Services.AddDbContext<codesomeServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("codesomeServerContext") ?? throw new InvalidOperationException("Connection string 'codesomeServerContext' not found.")));
*/


builder.Services.AddDbContextPool<codesomeServerContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("codesome");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'CodesomeELearningAppContext' not found.");
    }

    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 27)))
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});


builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllIPs", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});


// API Versioning

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

/*
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
*/

List<string> token = new List<string>();

builder.Services.AddSwaggerDocument(config =>
{
    config.AddSecurity("Bearer", token, openApiSecurityScheme);
    config.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Codesome";
        document.Info.Description = "Codesome Api";
        document.Info.TermsOfService = "https://codesome.com";
        document.Info.Contact = new NSwag.OpenApiContact
        {
            Name = "Teleeza Dev",
            Email = "codesome.com",
            Url = "https://codesome.com"
        };
        document.Info.License = new NSwag.OpenApiLicense
        {
            Name = "USE UNDER CODESOME LICENSE API RULES AND REGULATIONS",
            Url = "https://codesome.com"
        };
    };
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddScoped<IUrlHelper>(x => {
    var actioncontext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actioncontext);
});

var app = builder.Build();


app.UseOpenApi();
app.UseSwaggerUi3();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAllIPs");

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
