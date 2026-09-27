using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

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
            Constants.ClubsResultType => Constants.ClubsResultBoxXPath,
            Constants.PlayersResultType => Constants.PlayersResultBoxXPath,

            _ => throw new ArgumentException(
                string.Format(Constants.UnsupportedSearchResultTypeMessage, _resultType),
                nameof(_resultType))
        };

        return _page
            .Locator($"xpath={boxXPath}")
            .Locator(Constants.SearchResultsTableSelector)
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
            .Locator(Constants.SearchResultHeadersSelector)
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
