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

    public ILocator Headers =>
        GetTable().Locator(Constants.SearchResultHeadersSelector);

    public ILocator NameLink(string name, int columnIndex) =>
        GetTable()
            .Locator($"tbody tr td:nth-child({columnIndex}) a")
            .GetByText(name)
            .First;

    public async Task OpenByNameAsync(
        string name,
        int columnIndex)
    {
        await NameLink(name, columnIndex).ClickAsync();
    }
}
