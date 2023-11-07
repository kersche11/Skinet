using API.Helpers;
using API.Middleware;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);
//Extensions
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();
  //Extensions/Swagger
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentaion();
//Errorhandling
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
//Middleware serve images from api
app.UseStaticFiles();
//Middleware cors
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//Migrations and DB Update
using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;
    var context=services.GetRequiredService<StoreContext>();
    var identityContext=services.GetRequiredService<AppIdentityDbContext>();
    var userManager=services.GetRequiredService<UserManager<AppUser>>();
    var logger = services.GetRequiredService<ILogger<Program>>(); 
    try
    {
       
        await context.Database.MigrateAsync();
        await identityContext.Database.MigrateAsync();
        //Dataseeding (Data/StoreContextSeed)
        await StoreContextSeed.SeedAsync(context);
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
    }   
    catch(Exception ex)
    {
        logger.LogError(ex,"An error occured during migration");
    }
    


app.Run();
