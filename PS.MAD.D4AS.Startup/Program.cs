using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;

namespace PS.MAD.D4AS.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseKestrel(options =>
            {
                options.Limits.MinRequestBodyDataRate =
                    new MinDataRate(bytesPerSecond: 80, gracePeriod: TimeSpan.FromMinutes(5));
                options.Limits.MinResponseDataRate =
                    new MinDataRate(bytesPerSecond: 80, gracePeriod: TimeSpan.FromMinutes(5));
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
            });
    }
}
