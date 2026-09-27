using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Search;

public class SearchComponent
{
    private readonly IPage _page;

    public SearchComponent(IPage page)
    {
        _page = page;
    }

    public async Task SearchAsync(string searchTerm)
    {
        await _page
            .Locator(Constants.SearchInputSelector)
            .FillAsync(searchTerm);

        await _page
            .Locator(Constants.SearchButtonSelector)
            .ClickAsync();
    }
}
