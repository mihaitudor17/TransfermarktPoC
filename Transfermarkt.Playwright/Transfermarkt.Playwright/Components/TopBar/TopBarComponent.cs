using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.TopBar;

public class TopBarComponent
{
    private readonly IPage _page;

    public TopBarComponent(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToAsync(TopBarDestination destination)
    {
        var expectedText = destination.GetDisplayText();

        await _page
            .Locator(Constants.TopBarLinkSelector)
            .Filter(new LocatorFilterOptions
            {
                HasTextString = expectedText
            })
            .ClickAsync();
    }
}
