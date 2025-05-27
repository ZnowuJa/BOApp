using Application;
using Infrastructure;
using Persistance;
using Serilog;

using BOAppFluentUI.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Microsoft.AspNetCore.Components.Server.Circuits;


namespace BOAppFluentUI;
public class Program
{
    public static async Task Main(string[] args)
    {
        //other branch
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        // Add services to the container.
        builder.Services.AddLogging(builder =>
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.AddSerilog();
        });
        
        builder.Services.AddInfrastructure(configuration);
        builder.Services.AddPersistance(configuration);
        builder.Services.AddApplication(configuration);
        builder.Services.AddRazorPages();
        //builder.Services.AddControllers();
        builder.Services.AddRazorComponents(options => options.DetailedErrors = true)
            .AddInteractiveServerComponents();

        builder.Services.AddFluentUIComponents();
        builder.Services.AddDataGridEntityFrameworkAdapter();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseExceptionHandler("/Error");

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        // app.UseSession();
        // app.Services.GetRequiredService<CircuitHandler>();
        app.UseRouting();
        app.UseAuthentication(); // Ensure this is called before UseAuthorization
        app.UseAuthorization();  

        app.UseAntiforgery();
        //app.MapRazorPages();
        //app.MapControllers();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            var jobScheduler = services.GetRequiredService<IJobSchedulerService>();
            await jobScheduler.ScheduleJobsAsync();
        }

        await app.RunAsync();
    }
}
