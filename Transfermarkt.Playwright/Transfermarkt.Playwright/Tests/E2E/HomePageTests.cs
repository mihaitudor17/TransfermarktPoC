using System.Globalization;
using Transfermarkt.Playwright.Components.Tables;

namespace Transfermarkt.Playwright.Tests.E2E;

public class HomePageTests : BaseTest
{
    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidHeaders()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        Assert.That(
            tables,
            Is.Not.Empty,
            "Home page should contain games tables.");

        foreach (var table in tables)
        {
            var headers = await matches.GetHeadersAsync(table);

            Assert.Multiple(() =>
            {
                Assert.That(
                    headers,
                    Does.Contain("Date"),
                    "Games table should contain a Date header.");

                Assert.That(
                    headers,
                    Does.Contain("Home team"),
                    "Games table should contain a Home team header.");

                Assert.That(
                    headers,
                    Does.Contain("Away team"),
                    "Games table should contain an Away team header.");
            });
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveRows()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            Assert.That(
                rows,
                Is.Not.Empty,
                "Games table should contain match rows.");
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidDates()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            foreach (var row in rows)
            {
                var date = (
                    await row
                        .Locator("td")
                        .Nth(0)
                        .InnerTextAsync())
                    .Trim();
                
                if (string.IsNullOrEmpty(date))
                    continue;

                Assert.That(
                    DateTime.TryParseExact(
                        date,
                        "ddd dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out _),
                    Is.True,
                    $"Invalid match date: '{date}'.");
            }
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveHomeTeams()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            foreach (var row in rows)
            {
                var homeTeamLink = row
                    .Locator("td")
                    .Nth(2)
                    .Locator("a")
                    .First;

                Assert.That(
                    await homeTeamLink.CountAsync(),
                    Is.GreaterThan(0),
                    "Home team link should be present.");

                var homeTeam = (
                    await homeTeamLink
                        .InnerTextAsync())
                    .Trim();

                Assert.That(
                    homeTeam,
                    Is.Not.Empty,
                    "Home team name should not be empty.");
            }
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveAwayTeams()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            foreach (var row in rows)
            {
                var awayTeamLink = row
                    .Locator("td")
                    .Nth(6)
                    .Locator("a")
                    .First;

                Assert.That(
                    await awayTeamLink.CountAsync(),
                    Is.GreaterThan(0),
                    "Away team link should be present.");

                var awayTeam = (
                    await awayTeamLink
                        .InnerTextAsync())
                    .Trim();

                Assert.That(
                    awayTeam,
                    Is.Not.Empty,
                    "Away team name should not be empty.");
            }
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidMatchTimes()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);
            var matchDate = string.Empty;

            foreach (var row in rows)
            {
                var rowDate = (await row.Locator("td").Nth(0).InnerTextAsync()).Trim();
                if (!string.IsNullOrEmpty(rowDate))
                    matchDate = rowDate;

                var time = (
                    await row
                        .Locator("td")
                        .Nth(4)
                        .InnerTextAsync())
                    .Trim();

                var isFuture = DateTime.TryParseExact(
                    $"{matchDate} {time}", "ddd dd/MM/yyyy h:mm tt",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var kickoff) && kickoff > DateTime.Now;

                Assert.That(
                    isFuture
                        ? DateTime.TryParseExact(
                            time, "h:mm tt", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out _)
                        : Regex.IsMatch(time, @"^\d+:\d+$"),
                    Is.True,
                    $"Invalid match time or score: '{time}' for '{matchDate}'.");
            }
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveHomeTeamImages()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            foreach (var row in rows)
            {
                var image = row
                    .Locator("td")
                    .Nth(3)
                    .Locator("a img")
                    .First;

                Assert.That(
                    await image.CountAsync(),
                    Is.GreaterThan(0),
                    "Home team image should be present.");
            }
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveAwayTeamImages()
    {
        var matches = new HomeMatchesTableComponent(Page);
        var tables = await matches.GetTablesAsync();

        foreach (var table in tables)
        {
            var rows = await matches.GetRowsAsync(table);

            foreach (var row in rows)
            {
                var image = row
                    .Locator("td")
                    .Nth(5)
                    .Locator("a img")
                    .First;

                Assert.That(
                    await image.CountAsync(),
                    Is.GreaterThan(0),
                    "Away team image should be present.");
            }
        }
    }
}
