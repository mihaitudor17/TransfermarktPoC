using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Tests.E2E;

public class PremierLeagueTableTests : BrowserTest
{
    private const string StandingsPath = "/premier-league/tabelle/wettbewerb/GB1";

    [Test]
    public async Task PremierLeagueStandings_ShouldShowTableWithTeams()
    {
        await Page.GotoAsync(StandingsPath);

        var heading = Page.GetByText(
            "Premier League",
            new PageGetByTextOptions { Exact = true }).First;
        var standingsTable = Page.Locator("table.items").First;
        var teamRows = standingsTable.Locator("tbody tr");

        await Expect(heading).ToBeVisibleAsync();
        await Expect(standingsTable).ToBeVisibleAsync();
        await Expect(teamRows).ToHaveCountAsync(20);
    }
}
