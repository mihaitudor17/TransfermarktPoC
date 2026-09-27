using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Search;

public class SearchComponent
{
    private readonly IPage _page;

    private const string SearchFormSelector = "#schnellsuche";
    private const string SearchInputSelector = "#schnellsuche input[type='text']";
    private const string SearchButtonSelector = "#schnellsuche button[type='submit']";

    public SearchComponent(IPage page)
    {
        _page = page;
    }

    public async Task SearchAsync(string searchTerm)
    {
        await _page
            .Locator(SearchInputSelector)
            .FillAsync(searchTerm);

        await _page
            .Locator(SearchButtonSelector)
            .ClickAsync();
    }
}