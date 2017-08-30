using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Autofac;

namespace HttpClientSample.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(h =>
            {
                var client = new HttpClient {BaseAddress = new Uri("http://localhost:55268/")};
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }).AsSelf().SingleInstance();

            builder.Register(c => Console.Out).Keyed<TextWriter>("LoggerWriter");
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<ProductRepository>().As<IRepository<Product>>().InstancePerLifetimeScope();
            builder.RegisterType<SampleApplication>().AsSelf();

            base.Load(builder);
        }
    }
}
