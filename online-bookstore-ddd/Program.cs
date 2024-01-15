using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using Bookstore.Domain.Services;
using Bookstore.Infrastructure.Logging;
using Bookstore.Infrastructure.Persistance;
using Bookstore.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using online_bookstore_ddd.Events;
using online_bookstore_ddd.Events.EventHandlers;
using online_bookstore_ddd.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>(); 
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<IDomainEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<ItemRemovedFromCartEvent>, ItemRemovedFromCartEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<ItemAddedtoCartEvent>, ItemAddedtoCartEventHandler>(); 
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("BookstoreDb"));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();
