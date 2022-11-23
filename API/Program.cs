using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x=>x.UseSqlite(connectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Repositorys
builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();

//Migrations and DB Update
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>(); 
    try
    {
        var context=services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        //Dataseeding (Data/StoreContextSeed)
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }   
    catch(Exception ex)
    {
        var logger=loggerFactory.CreateLogger<Program>();
        logger.LogError(ex,"An error occured during migration");
    }
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
