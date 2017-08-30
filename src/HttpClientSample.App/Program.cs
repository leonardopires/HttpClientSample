using System;
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


        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            try
            {
                var app = Container.Resolve<SampleApplication>();
                await app.RunAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Write("FINISHED: Press ENTER to leave...");
            Console.ReadLine();
        }

    }
}
