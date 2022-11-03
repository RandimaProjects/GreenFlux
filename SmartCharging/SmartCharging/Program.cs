using Microsoft.EntityFrameworkCore;
using SmartCharging.Application.Contracts;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Application.Services;
using SmartCharging.Infrastructure.Context;
using SmartCharging.Infrastructure.Repositories;
using AutoMapper;
using SmartCharging.Application.Contracts.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SmartChargingDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IGroupService,GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IChargeStationService, ChargeStationService>();
builder.Services.AddScoped<IChargeStationRepository, ChargeStationRepository>();
builder.Services.AddScoped<IConnectorService, ConnectorService>();
builder.Services.AddScoped<IConnectorRepository, ConnectorRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
