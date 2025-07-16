﻿using BankaAPI.Data; // DbContext sınıfının bulunduğu namespace                         
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 🔹 DbContext'i DI konteynerine ekle
builder.Services.AddDbContext<BankaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankaDb")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
