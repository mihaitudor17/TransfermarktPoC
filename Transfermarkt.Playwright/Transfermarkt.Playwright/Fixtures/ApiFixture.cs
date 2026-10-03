using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Fixtures;

public class ApiFixture : IAsyncDisposable
{
    public IAPIRequestContext Request { get; private set; } = null!;

    public async Task InitializeAsync(IPlaywright playwright)
    {
        Request = await playwright.APIRequest.NewContextAsync(
            new APIRequestNewContextOptions
            {
                BaseURL = Constants.TransfermarktBaseUrl
            });
    }

    public async ValueTask DisposeAsync()
    {
        await Request.DisposeAsync();
    }
}
