using System.Text;
using DataAccess.Context;
using DataAccess.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using WebApp.Models.Users;
using WebApp.Services.Implementations;

var appBuilder = WebApplication.CreateBuilder(args);
appBuilder.Services.Configure<JwtSettings>(appBuilder.Configuration.GetSection("JwtSettings"));


var opt = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true,
    ValidIssuer = appBuilder.Configuration["JwtSettings:Issuer"],
    ValidAudience = appBuilder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(appBuilder.Configuration["JwtSettings:SecretKey"]))
};

appBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = opt);

appBuilder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
appBuilder.Services.AddControllers();
appBuilder.Services.AddSwaggerGen(o => 
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "KnowApi",
        Version = "v1",
    }));

var connString = appBuilder.Configuration
    .GetConnectionString("Default");
var connStringBuilder = new NpgsqlConnectionStringBuilder(connString);
appBuilder.Services.AddDbContext<KnowDbContext>(
    options => options.UseNpgsql(connStringBuilder.ConnectionString));


appBuilder.Services.AddIdentityCore<KnowUser>()
    .AddEntityFrameworkStores<KnowDbContext>();


appBuilder.Services.AddScoped<PostService>();
appBuilder.Services.AddScoped<TagService>();

var app = appBuilder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(o=> o.SwaggerEndpoint(
        "/swagger/v1/swagger.json","KnowApi v1"));
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


