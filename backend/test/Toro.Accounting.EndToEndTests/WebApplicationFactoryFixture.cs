namespace Toro.Accounting.EndToEndTests
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
