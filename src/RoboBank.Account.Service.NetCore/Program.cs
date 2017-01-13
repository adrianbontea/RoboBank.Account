using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Linq;

namespace RoboBank.Account.Service.NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = 5000;

            if (args.Any())
            {
                int.TryParse(args.First(), out port);
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls($"http://*:{port}")
                .Build();

            host.Run();
        }
    }
}
