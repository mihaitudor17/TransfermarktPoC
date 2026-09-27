using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Fixtures;

public class PlaywrightFixture : IAsyncDisposable
{
    public IPlaywright Playwright { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public IBrowserContext Context { get; private set; } = null!;
    public IPage Page { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });

        Context = await Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                BaseURL = "https://www.transfermarkt.com"
            });

        Page = await Context.NewPageAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.CloseAsync();
        await Browser.CloseAsync();
        Playwright.Dispose();
    }
}