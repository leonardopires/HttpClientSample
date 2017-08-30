using System;
using System.Net.Http;
using Autofac;
using FluentAssertions;
using HttpClientSample.Core;
using Xunit;

namespace HttpClientSample.Test
{
    public class HttpClientSampleTests
    {
        [Fact]
        public void ContainerShouldAlwaysReturnTheSameHttpClient()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            var container = builder.Build();

            var client1 = container.Resolve<HttpClient>();

            using (container.BeginLifetimeScope())
            {
                var client2 = container.Resolve<HttpClient>();

                client2.Should().BeSameAs(client1, "There must be only one instance of HttpClient in order to keep the socket count low");
            }
        }
    }
}
