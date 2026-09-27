using Transfermarkt.Playwright.Fixtures;

namespace Transfermarkt.Playwright.Tests.Integration;

public class ApiRequestTests
{
    private ApiFixture _fixture = null!;

    [SetUp]
    public async Task SetUp()
    {
        _fixture = new ApiFixture();
        await _fixture.InitializeAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _fixture.DisposeAsync();
    }

    [TestCase("/")]
    [TestCase("/premier-league/startseite/wettbewerb/GB1")]
    [TestCase("/premier-league/tabelle/wettbewerb/GB1")]
    public async Task Endpoint_ShouldReturnSuccessfulResponse(
        string endpoint)
    {
        var response = await _fixture.Request.GetAsync(endpoint);

        Assert.That(
            response.Status,
            Is.EqualTo(200),
            $"Endpoint '{endpoint}' should return HTTP 200.");
    }

    [Test]
    public async Task PremierLeagueTable_ShouldReturnValidTablePage()
    {
        var response = await _fixture.Request.GetAsync(
            "/premier-league/tabelle/wettbewerb/GB1");

        var body = await response.TextAsync();

        var contentType = response.Headers.TryGetValue(
            "content-type",
            out var value)
            ? value
            : string.Empty;

        Assert.Multiple(() =>
        {
            Assert.That(
                response.Status,
                Is.EqualTo(200),
                "Premier League table endpoint should return HTTP 200.");

            Assert.That(
                contentType,
                Does.Contain("text/html"),
                "Response should be an HTML page.");

            Assert.That(
                body,
                Does.Contain("Premier League"),
                "Response should contain the Premier League.");

            Assert.That(
                body,
                Does.Contain("table"),
                "Response should contain table markup.");
        });
    }

    [Test]
    public async Task PremierLeagueTable_ShouldContainExpectedTeams()
    {
        var response = await _fixture.Request.GetAsync(
            "/premier-league/tabelle/wettbewerb/GB1");

        var body = await response.TextAsync();

        var expectedTeams = new[]
        {
            "Arsenal",
            "Liverpool",
            "Manchester City",
            "Chelsea"
        };

        Assert.That(
            response.Ok,
            Is.True,
            "Premier League table request should succeed.");

        foreach (var team in expectedTeams)
        {
            Assert.That(
                body,
                Does.Contain(team),
                $"Premier League table should contain '{team}'.");
        }
    }
}