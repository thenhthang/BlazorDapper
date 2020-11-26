using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorDapperUI.Services;
using BlazorDapperUI.IHttpClient;

namespace BlazorDapperUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:34224/")});

            //VideoHttpClient videoHttpClient = new VideoHttpClient("https://localhost:34224/");
            //MyHttpClient myHttpClient = new MyHttpClient(builder.HostEnvironment.BaseAddress);
            //builder.Services.AddSingleton(videoHttpClient);
            builder.Services.AddScoped(sp => new VideoHttpClient("https://localhost:34224/"));
            builder.Services.AddScoped(sp => new MyHttpClient(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped<IVideoService, VideoService>();
           
            await builder.Build().RunAsync();
        }
    }
}
