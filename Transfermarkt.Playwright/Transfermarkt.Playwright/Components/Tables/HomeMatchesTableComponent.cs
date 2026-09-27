using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Tables;

public class HomeMatchesTableComponent
{
    private readonly IPage _page;

    private const string TableSelector = "table.startseite";

    public HomeMatchesTableComponent(IPage page)
    {
        _page = page;
    }

    public async Task<IReadOnlyList<ILocator>> GetTablesAsync()
    {
        var tables = _page.Locator(TableSelector);
        var count = await tables.CountAsync();

        var result = new List<ILocator>();

        for (var i = 0; i < count; i++)
        {
            result.Add(tables.Nth(i));
        }

        return result;
    }

    public async Task<IReadOnlyList<string>> GetHeadersAsync(
        ILocator table)
    {
        return (await table
                .Locator("thead tr th")
                .AllInnerTextsAsync())
            .Select(x => x.Trim())
            .ToList();
    }

    public async Task<IReadOnlyList<ILocator>> GetRowsAsync(
        ILocator table)
    {
        var rows = table.Locator("tbody tr");
        var count = await rows.CountAsync();

        return Enumerable
            .Range(0, count)
            .Select(rows.Nth)
            .ToList();
    }
}