using Microsoft.EntityFrameworkCore;
using Vendas.Orders.Application.Interfaces.Services;
using Vendas.Orders.Application.Services;
using Vendas.Orders.Domain.Interfaces.Repository;
using Vendas.Orders.Infrastructure.Data.Context;
using Vendas.Orders.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrdersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//To-do: Centralizar instâncias em application e infra
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();