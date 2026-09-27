using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Profiles;

public class ProfileComponent
{
    private readonly IPage _page;

    public ProfileComponent(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetTitleAsync()
    {
        return (await _page
                .Locator(Constants.ProfileTitleSelector)
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