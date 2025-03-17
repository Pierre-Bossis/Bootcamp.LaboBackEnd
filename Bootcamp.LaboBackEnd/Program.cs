using Bootcamp.LaboBackEnd.BLL.Services;
using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Middlewares;
using Bootcamp.LaboBackEnd.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Injection

//builder.Services.AddTransient<SqlConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<SqlConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

builder.Services.AddScoped<IProduitRepository, ProduitRepository>();
builder.Services.AddScoped<IProduitService, ProduitService>();

builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
builder.Services.AddScoped<ICategorieService, CategorieService>();

builder.Services.AddScoped<ICommandeRepository, CommandeRepository>();
builder.Services.AddScoped<ICommandeService, CommandeService>();

builder.Services.AddScoped<JwtGenerator>();

#endregion

#region Authentification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("tokenInfo").GetSection("secretKey").Value)),
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:7155",
            ValidateAudience = true,
            ValidAudience = "http://localhost:4200"
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("connectedPolicy", policy => policy.RequireAuthenticatedUser());
});

#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Front",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
#endregion

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

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseCors("Front");

app.Run();
