using API.Helpers;
using API.Middleware;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
//Database connection
builder.Services.AddDbContext<StoreContext>(x=>x.UseSqlite(connectionString));
//Extensions
builder.Services.AddApplicationServices();
//Extensions/Swagger
builder.Services.AddSwaggerDocumentation();
//CORS
builder.Services.AddCors(opt=>{
    opt.AddPolicy("CorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
});


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
app.UseMiddleware<ExceptionMiddleware>();

//Errorhandling
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
//Middleware serve images from api
app.UseStaticFiles();
//Middleware cors
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseSwaggerDocumentaion();
app.MapControllers();

app.Run();
