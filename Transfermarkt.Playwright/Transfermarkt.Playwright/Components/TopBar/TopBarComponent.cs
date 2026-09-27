using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.TopBar;

public class TopBarComponent
{
    private readonly IPage _page;

    private const string LinkSelector = "a.main-navbar__lp-link";
    private const string ActiveLinkSelector = "a.main-navbar__lp-link.active";

    public TopBarComponent(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToAsync(TopBarDestination destination)
    {
        var expectedText = destination.GetDisplayText();

        await _page
            .Locator(LinkSelector)
            .Filter(new LocatorFilterOptions
            {
                HasTextString = expectedText
            })
            .ClickAsync();
    }

    public async Task<string> GetActiveDestinationTextAsync()
    {
        return (await _page
                .Locator(ActiveLinkSelector)
                .InnerTextAsync())
            .Trim();
    }
}