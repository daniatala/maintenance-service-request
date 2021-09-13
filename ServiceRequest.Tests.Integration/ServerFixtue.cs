using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ServiceRequest.Tests.Integration
{
    public class ServerFixtue
    {
        public HttpClient HttpClient => TestServer.CreateClient();
        public TestServer TestServer { get; }
        public IWebHostBuilder WebHostBuilder { get; }
        public IConfigurationRoot Configuration { get; private set; }

        public ServerFixtue()
        {
            WebHostBuilder = CreateWebHostBuilder();
            TestServer = new TestServer(WebHostBuilder);
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            var hostBuilder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>();

            return hostBuilder;
        }
    }
}
