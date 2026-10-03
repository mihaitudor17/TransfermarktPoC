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

    public ILocator Title => _page.Locator(Constants.ProfileTitleSelector).First;

}
