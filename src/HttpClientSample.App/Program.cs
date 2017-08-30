using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using HttpClientSample.Core;
using HttpClientSample.Core.Net;

namespace HttpClientSample.App
{
    public static class Program
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(
            () =>
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<CoreModule>();
                return builder.Build();
            }
        );

        private static IContainer Container => container.Value;


        static int Main()
        {
            var result = RunAsync().Result;
            Console.ReadLine();
            return result;
        }

        static async Task<int> RunAsync()
        {
            var result = 0;
            var log = Container.Resolve<ILogger>();
            try
            {
                var app = Container.Resolve<SampleApplication>();
                await app.RunAsync();
            }
            catch (Exception e)
            {
                log.WriteLine(e.Message);
                result = e.HResult;
            }

            log.WriteLine("FINISHED: Press ENTER to leave...");
            return result;
        }

    }
}
