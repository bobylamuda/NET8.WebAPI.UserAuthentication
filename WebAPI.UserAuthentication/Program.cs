using ImTools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.UserAuthentication.Data;
using WebAPI.UserAuthentication.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Add Authentication
        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        //Add Authorization
        builder.Services.AddAuthorizationBuilder();

        //Configure DbContext
        builder.Services
            .AddDbContext<AppDbContext>(opt => 
                opt.UseSqlite(connectionString: "DataSource=appdata.db"));

        builder.Services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        var app = builder.Build();

        app.MapIdentityApi<AppUser>();

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
    }
}