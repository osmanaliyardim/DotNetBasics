using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Email;
using System;
using System.Net;

namespace BrainstormSessions
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(formatter: new JsonFormatter(),
                path: @"c:\temp\ali.log",
                shared: true)
                .WriteTo
                .Email(new EmailConnectionInfo
                    {
                        FromEmail = "",
                        ToEmail = "",
                        MailServer = "smtp.gmail.com",
                        NetworkCredentials = new NetworkCredential
                        {
                            UserName = "",
                            Password = ""
                        },
                        EnableSsl = true,
                        Port = 465,
                        EmailSubject = "Logs"
                    },
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    batchPostingLimit: 10,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug
                ).CreateLogger();

            try
            {
                Log.Information("Starting web host");

                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
