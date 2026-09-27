using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Fixtures;
    
public class ApiFixture : IAsyncDisposable
{
    public IPlaywright Playwright { get; private set; } = null!;
    public IAPIRequestContext Request { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Request = await Playwright.APIRequest.NewContextAsync(
            new APIRequestNewContextOptions
            {
                BaseURL = "https://www.transfermarkt.com"
            });
    }

    public async ValueTask DisposeAsync()
    {
        await Request.DisposeAsync();
        Playwright.Dispose();
    }
}