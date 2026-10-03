using Transfermarkt.Playwright.Fixtures;

namespace Transfermarkt.Playwright.Tests.Integration;

public class ApiRequestTests : PlaywrightTest
{
    private const string PremierLeagueTableEndpoint =
        "/premier-league/tabelle/wettbewerb/GB1";

    private ApiFixture _fixture = null!;

    [SetUp]
    public async Task SetUp()
    {
        _fixture = new ApiFixture();
        await _fixture.InitializeAsync(Playwright);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _fixture.DisposeAsync();
    }

    [TestCase("/")]
    [TestCase("/premier-league/startseite/wettbewerb/GB1")]
    [TestCase(PremierLeagueTableEndpoint)]
    public async Task Endpoint_ShouldReturnSuccessfulResponse(
        string endpoint)
    {
        var response = await _fixture.Request.GetAsync(endpoint);

        await Expect(response).ToBeOKAsync();
    }

    [Test]
    public async Task PremierLeagueTable_ShouldReturnValidTablePage()
    {
        var response = await _fixture.Request.GetAsync(PremierLeagueTableEndpoint);

        var body = await response.TextAsync();

        var contentType = response.Headers.TryGetValue(
            "content-type",
            out var value)
            ? value
            : string.Empty;

        await Expect(response).ToBeOKAsync();

        Assert.Multiple(() =>
        {
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
}
