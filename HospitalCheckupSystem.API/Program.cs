using HospitalCheckupSystem.Application.UseCases;
using HospitalCheckupSystem.Domain.Interfaces;
using HospitalCheckupSystem.Domain.Services;
using HospitalCheckupSystem.Infrastructure.Persistence;
using HospitalCheckupSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Patient services
builder.Services.AddDbContext<HospitalDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IMrnGenerator, MrnGenerator>();
builder.Services.AddScoped<CreatePatientUseCase>();


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
