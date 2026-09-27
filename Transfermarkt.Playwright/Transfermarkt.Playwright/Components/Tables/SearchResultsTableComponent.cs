using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Tables;

public class SearchResultsTableComponent
{
    private readonly IPage _page;
    private readonly string _resultType;

    public SearchResultsTableComponent(
        IPage page,
        string resultType)
    {
        _page = page;
        _resultType = resultType;
    }

    private ILocator GetTable()
    {
        var resultType = _resultType.Trim().ToLowerInvariant();

        var boxXPath = resultType switch
        {
            "clubs" =>
                "//div[contains(@class,'box')]" +
                "[.//h2[contains(" +
                "translate(normalize-space(.)," +
                "'ABCDEFGHIJKLMNOPQRSTUVWXYZ'," +
                "'abcdefghijklmnopqrstuvwxyz')," +
                "'clubs')]]",

            "players" =>
                "//div[contains(@class,'box')]" +
                "[.//h2[contains(" +
                "translate(normalize-space(.)," +
                "'ABCDEFGHIJKLMNOPQRSTUVWXYZ'," +
                "'abcdefghijklmnopqrstuvwxyz')," +
                "'players')]]",

            _ => throw new ArgumentException(
                $"Unsupported search result type: {_resultType}",
                nameof(_resultType))
        };

        return _page
            .Locator($"xpath={boxXPath}")
            .Locator(".responsive-table table.items")
            .First;
    }

    public async Task<IReadOnlyList<string>> GetHeadersAsync()
    {
        var table = GetTable();

        await table.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        return await table
            .Locator("thead tr th:not(:first-child)")
            .AllInnerTextsAsync();
    }

    public async Task<IReadOnlyList<string>> GetNamesAsync(
        int columnIndex)
    {
        var table = GetTable();

        var links = table.Locator(
            $"tbody tr td:nth-child({columnIndex}) a");

        return (await Task.WhenAll(
                (await links.AllAsync())
                .Select(link =>
                    link.GetAttributeAsync("title"))))
            .Where(title => !string.IsNullOrEmpty(title))
            .Select(title => title!)
            .ToList();
    }

    public async Task OpenByNameAsync(
        string name,
        int columnIndex)
    {
        var table = GetTable();

        var link = table
            .Locator(
                $"tbody tr td:nth-child({columnIndex}) a[title='{name}']")
            .First;

        await link.ClickAsync();
    }
}