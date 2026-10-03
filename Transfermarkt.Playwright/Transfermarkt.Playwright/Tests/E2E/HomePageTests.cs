using System.Globalization;
using Microsoft.Playwright;
using Transfermarkt.Playwright.Components.Tables;

namespace Transfermarkt.Playwright.Tests.E2E;

public class HomePageTests : BrowserTest
{
    private const int DateColumnIndex = 0;
    private const int HomeTeamColumnIndex = 2;
    private const int HomeTeamImageColumnIndex = 3;
    private const int TimeColumnIndex = 4;
    private const int AwayTeamImageColumnIndex = 5;
    private const int AwayTeamColumnIndex = 6;

    private HomeMatchesTableComponent _matches = null!;
    private IReadOnlyList<ILocator> _tables = null!;

    [SetUp]
    public async Task SetUpHomeTables()
    {
        _matches = new HomeMatchesTableComponent(Page);
        _tables = await _matches.GetTablesAsync();
    }

    private async Task<IReadOnlyList<ILocator>> GetAllRowsAsync()
    {
        var rows = new List<ILocator>();

        foreach (var table in _tables)
        {
            rows.AddRange(await _matches.GetRowsAsync(table));
        }

        return rows;
    }

    private static ILocator Cell(ILocator row, int columnIndex) =>
        row.Locator("td").Nth(columnIndex);

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidHeaders()
    {
        foreach (var table in _tables)
        {
            await Expect(_matches.Headers(table)).ToContainTextAsync(
                new[] { "Date", "Home team", "Away team" });
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveRows()
    {
        foreach (var table in _tables)
        {
            await Expect(_matches.Rows(table).First).ToBeVisibleAsync();
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidDates()
    {
        foreach (var row in await GetAllRowsAsync())
        {
            var date = (await Cell(row, DateColumnIndex).InnerTextAsync()).Trim();

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

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveHomeTeams()
    {
        foreach (var row in await GetAllRowsAsync())
        {
            var homeTeamLink = Cell(row, HomeTeamColumnIndex).Locator("a").First;

            await Expect(homeTeamLink).ToBeVisibleAsync();
            await Expect(homeTeamLink).ToHaveTextAsync(new Regex(@"\S+"));
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveAwayTeams()
    {
        foreach (var row in await GetAllRowsAsync())
        {
            var awayTeamLink = Cell(row, AwayTeamColumnIndex).Locator("a").First;

            await Expect(awayTeamLink).ToBeVisibleAsync();
            await Expect(awayTeamLink).ToHaveTextAsync(new Regex(@"\S+"));
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveValidMatchTimes()
    {
        var centralEuropeanTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
            OperatingSystem.IsWindows() ? "W. Europe Standard Time" : "Europe/Berlin");
        var centralEuropeanNow = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow, centralEuropeanTimeZone);

        foreach (var table in _tables)
        {
            var matchDate = string.Empty;

            foreach (var row in await _matches.GetRowsAsync(table))
            {
                var rowDate = (await Cell(row, DateColumnIndex).InnerTextAsync()).Trim();
                if (!string.IsNullOrEmpty(rowDate))
                    matchDate = rowDate;

                var time = (await Cell(row, TimeColumnIndex).InnerTextAsync()).Trim();

                var isFuture = DateTime.TryParseExact(
                    $"{matchDate} {time}", "ddd dd/MM/yyyy h:mm tt",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var kickoff) && kickoff > centralEuropeanNow;

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
        foreach (var row in await GetAllRowsAsync())
        {
            var image = Cell(row, HomeTeamImageColumnIndex)
                .Locator("a img")
                .First;

            await Expect(image).ToBeVisibleAsync();
        }
    }

    [Test]
    public async Task HomePage_GamesTables_ShouldHaveAwayTeamImages()
    {
        foreach (var row in await GetAllRowsAsync())
        {
            var image = Cell(row, AwayTeamImageColumnIndex)
                .Locator("a img")
                .First;

            await Expect(image).ToBeVisibleAsync();
        }
    }
}
