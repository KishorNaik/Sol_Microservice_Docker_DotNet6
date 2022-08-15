using AuthJwt.Services;
using Framework.ASP.Extensions.Extensions;
using Framework.Model.Response;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wallets.API.Applications.Features.Event.Integration.Consumer;
using Wallets.API.Extensions;

//using Wallets.API.Applications.Features.Event.Integration.Consumer;
using Wallets.API.Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRabbitMQ();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((opt) =>
{
    opt.AddJwtInSwagger();
});

builder.Services.AddJson(true, true);
builder.Services.AddGzipResponseCompression(System.IO.Compression.CompressionLevel.Fastest);

builder.Services.Configure<AppSettingModel>(builder.Configuration.GetSection("Jwt"));
var getSecretKey = builder.Configuration.GetSection("Jwt").Get<AppSettingModel>();
builder.Services.AddJwtToken(getSecretKey.SecretKey);

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidationFilter(typeof(Program));

builder.Services.AddDbContext<WalletContext>((options) =>
{
    string connectionString = builder.Configuration.GetSecretConnectionString("WalletDB");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();