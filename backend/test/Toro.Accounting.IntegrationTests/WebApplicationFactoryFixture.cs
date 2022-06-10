namespace Toro.Accounting.IntegrationTests
{
    public class WebApplicationFactoryFixture : IDisposable
    {
        public CustomWebApplicationFactory WebApplicationFactory { get; }

        public WebApplicationFactoryFixture()
        {
            WebApplicationFactory = new CustomWebApplicationFactory();
        }

        public void Dispose() => WebApplicationFactory?.Dispose();
    }
}
