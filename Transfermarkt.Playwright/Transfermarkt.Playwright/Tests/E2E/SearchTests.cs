using Transfermarkt.Playwright.Components.Profiles;
using Transfermarkt.Playwright.Components.Search;
using Transfermarkt.Playwright.Components.Tables;

namespace Transfermarkt.Playwright.Tests.E2E;

public class SearchTests : BaseTest
{
    [TestCase("Manchester United")]
    [TestCase("Liverpool FC")]
    [TestCase("Real Madrid")]
    [TestCase("FC Barcelona")]
    public async Task Search_ShouldFindAndOpenClub(string searchTerm)
    {
        var search = new SearchComponent(Page);

        var profile = new ProfileComponent(Page);

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

        await search.SearchAsync(searchTerm);
        
        var resultsTable = new SearchResultsTableComponent(
            Page,
            "clubs");

        var headers = await resultsTable.GetHeadersAsync();

        Assert.That(headers, Is.EqualTo(expectedHeaders));

        var names = await resultsTable.GetNamesAsync(2);

        Assert.That(
            names,
            Does.Contain(searchTerm),
            $"Search results should contain '{searchTerm}'.");

        await resultsTable.OpenByNameAsync(
            searchTerm,
            2);

        Assert.That(
            await profile.ContainsNameAsync(searchTerm),
            Is.True,
            $"Profile header should contain '{searchTerm}'.");
    }

    [TestCase("Ianis Hagi")]
    [TestCase("Erling Haaland")]
    [TestCase("Kylian Mbappé")]
    [TestCase("Lionel Messi")]
    public async Task Search_ShouldFindAndOpenPlayer(string searchTerm)
    {
        var search = new SearchComponent(Page);

        var resultsTable = new SearchResultsTableComponent(
            Page,
            "players");

        var profile = new ProfileComponent(Page);

        var expectedHeaders = new[]
        {
            "Position",
            "Club",
            "Age",
            "Nat.",
            "Market Value",
            "Agents"
        };

        await search.SearchAsync(searchTerm);

        var headers = await resultsTable.GetHeadersAsync();

        Assert.That(headers, Is.EqualTo(expectedHeaders));

        var names = await resultsTable.GetNamesAsync(1);

        Assert.That(
            names,
            Does.Contain(searchTerm),
            $"Search results should contain '{searchTerm}'.");

        await resultsTable.OpenByNameAsync(
            searchTerm,
            1);

        Assert.That(
            await profile.ContainsNameAsync(searchTerm),
            Is.True,
            $"Profile header should contain '{searchTerm}'.");
    }
}