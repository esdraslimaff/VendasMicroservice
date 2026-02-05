using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Vendas.EventBus.Abstractions;
using Vendas.EventBus.RabbitMQ;
using Vendas.Orders.Application.Interfaces.Services;
using Vendas.Orders.Application.Services;
using Vendas.Orders.Domain.Interfaces.Repository;
using Vendas.Orders.Infrastructure.Data.Context;
using Vendas.Orders.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


//To-do: Centralizar instâncias em application e infra
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    Port = 5672
};

var connection = await factory.CreateConnectionAsync();
builder.Services.AddSingleton(connection);

builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

builder.Services.AddDbContext<OrdersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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