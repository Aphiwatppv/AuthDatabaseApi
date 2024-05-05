using AuthApiWebAppMinimal;
using System.Text.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ISqlAccess ,SqlAccess>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigurationAPI();

app.Run();








