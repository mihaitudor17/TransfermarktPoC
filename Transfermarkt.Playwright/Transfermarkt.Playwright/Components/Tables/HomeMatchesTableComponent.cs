using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Tables;

public class HomeMatchesTableComponent
{
    private readonly IPage _page;

    public HomeMatchesTableComponent(IPage page)
    {
        _page = page;
    }

    public async Task<IReadOnlyList<ILocator>> GetTablesAsync()
    {
        var tables = _page.Locator(Constants.HomeMatchesTableSelector);
        await tables.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        var count = await tables.CountAsync();
        return Enumerable
            .Range(0, count)
            .Select(tables.Nth)
            .ToList();
    }

    public ILocator Headers(ILocator table) =>
        table.Locator(Constants.TableHeadersSelector);

    public ILocator Rows(ILocator table) =>
        table.Locator(Constants.TableRowsSelector);

    public async Task<IReadOnlyList<ILocator>> GetRowsAsync(
        ILocator table)
    {
        var rows = Rows(table);
        await rows.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        var count = await rows.CountAsync();

        return Enumerable
            .Range(0, count)
            .Select(rows.Nth)
            .ToList();
    }
}
