using AuthJwt.Services;
using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DatabaseContext;

//using Customer.API.Infrastructures.DatabaseContext;
using Framework.ASP.Extensions.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJson(true, true);
builder.Services.AddGzipResponseCompression(System.IO.Compression.CompressionLevel.Fastest);
builder.Services.AddJwtToken(builder.Configuration.GetValue<string>("SecretKey"));

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidationFilter(typeof(Program));

builder.Services.AddDbContext<CustomersContext>((options) =>
{
    string connectionString = builder.Configuration.GetSecretConnectionString("CustomerDB");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IHashPasswordRule, HashPasswordRule>();

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