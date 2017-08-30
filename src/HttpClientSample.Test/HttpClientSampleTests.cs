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

            var rootScopeClient = container.Resolve<HttpClient>();

            using (container.BeginLifetimeScope())
            {
                var innerScopeClient = container.Resolve<HttpClient>();

                innerScopeClient.Should().BeSameAs(rootScopeClient, "There must be only one instance of HttpClient in order to keep the socket count low");
            }
        }
    }
}
