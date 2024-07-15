using WebsiteStatus;
using Serilog;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft",Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"D:\Projects\WebsiteStatus\logFile.txt")
    .CreateLogger();
try
{
    Log.Information("Starting up the service");
    var builder = Host.CreateDefaultBuilder(args)
        .UseWindowsService()//register service ...to deploy it as a windows service
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
        })
        .UseSerilog();

    var host = builder.Build();
    host.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex,"There was a problem starting the service");
    return;
}
finally
{
    Log.CloseAndFlush();
   
}
 

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args).Build().Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureServices((hostContext, services) =>
//            {
//                //services.AddHostedService<MonitoringSomethin>();
//            });
//}
