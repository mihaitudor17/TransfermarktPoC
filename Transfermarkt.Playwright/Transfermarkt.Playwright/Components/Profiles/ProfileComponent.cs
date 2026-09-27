using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Profiles;

public class ProfileComponent
{
    private readonly IPage _page;

    private const string TitleSelector =
        ".data-header > div:first-child h1";

    public ProfileComponent(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetTitleAsync()
    {
        return (await _page
                .Locator(TitleSelector)
                .InnerTextAsync())
            .Trim();
    }

    public async Task<bool> ContainsNameAsync(string name)
    {
        var title = await GetTitleAsync();

        return title.Contains(
            name,
            StringComparison.OrdinalIgnoreCase);
    }
}