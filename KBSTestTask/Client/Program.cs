using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KBSTestTask.Client
{
    public class Program
    {
        private const string broadcastUri = "https://localhost:44304/broadcastHub";
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton(sp =>
            {
                var hubConnection = new HubConnectionBuilder().WithUrl(broadcastUri).Build();
                return hubConnection;
            });


            await builder.Build().RunAsync();
        }
    }
}
