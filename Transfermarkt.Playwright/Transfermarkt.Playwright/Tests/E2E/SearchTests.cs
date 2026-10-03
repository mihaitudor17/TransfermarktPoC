using Transfermarkt.Playwright.Components.Profiles;
using Transfermarkt.Playwright.Components.Search;
using Transfermarkt.Playwright.Components.Tables;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Tests.E2E;

public class SearchTests : BrowserTest
{
    [TestCase("Manchester United")]
    [TestCase("Liverpool FC")]
    [TestCase("Real Madrid")]
    [TestCase("FC Barcelona")]
    public async Task Search_ShouldFindAndOpenClub(string searchTerm)
    {
        var expectedHeaders = new[]
        {
            "Club",
            "Country",
            "Squad",
            "Total Market Value",
            "Transfers",
            "Stadium",
            "forum"
        };

        await SearchAndOpenProfileAsync(
            searchTerm,
            Constants.ClubsResultType,
            2,
            expectedHeaders);
    }

    [TestCase("Ianis Hagi")]
    [TestCase("Erling Haaland")]
    [TestCase("Kylian Mbappé")]
    [TestCase("Lionel Messi")]
    public async Task Search_ShouldFindAndOpenPlayer(string searchTerm)
    {
        var expectedHeaders = new[]
        {
            "Position",
            "Club",
            "Age",
            "Nat.",
            "Market Value",
            "Agents"
        };

        await SearchAndOpenProfileAsync(
            searchTerm,
            Constants.PlayersResultType,
            1,
            expectedHeaders);
    }

    private async Task SearchAndOpenProfileAsync(
        string searchTerm,
        string resultType,
        int nameColumnIndex,
        string[] expectedHeaders)
    {
        var search = new SearchComponent(Page);
        await search.SearchAsync(searchTerm);

        var resultsTable = new SearchResultsTableComponent(Page, resultType);
        await Expect(resultsTable.Headers).ToHaveTextAsync(expectedHeaders);
        await Expect(resultsTable.NameLink(searchTerm, nameColumnIndex))
            .ToBeVisibleAsync();

        await resultsTable.OpenByNameAsync(searchTerm, nameColumnIndex);

        var profile = new ProfileComponent(Page);
        await Expect(profile.Title).ToContainTextAsync(searchTerm);
    }
}
