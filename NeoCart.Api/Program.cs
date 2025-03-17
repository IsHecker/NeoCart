using NeoCart.Application;
using NeoCart.Infrastructure;

namespace NeoCart.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);
        

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        // using (var scope = app.Services.CreateScope())
        // {
        //     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //     dbContext.Database.Migrate(); // Auto-run migrations
        // }

        app.Run();
    }
}