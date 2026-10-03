using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Tests.Integration;

public class ApiRequestTests : BrowserTest
{
    private const string PremierLeagueTableEndpoint =
        "/premier-league/tabelle/wettbewerb/GB1";

    private Task<IAPIResponse> GetAsync(string endpoint)
    {
        var url = new Uri(new Uri(Constants.TransfermarktBaseUrl), endpoint);
        return Page.APIRequest.GetAsync(url.ToString());
    }

    [TestCase("/")]
    [TestCase("/premier-league/startseite/wettbewerb/GB1")]
    [TestCase(PremierLeagueTableEndpoint)]
    public async Task Endpoint_ShouldReturnSuccessfulResponse(
        string endpoint)
    {
        var response = await GetAsync(endpoint);

        await Expect(response).ToBeOKAsync();
    }

    [Test]
    public async Task PremierLeagueTable_ShouldReturnValidTablePage()
    {
        var response = await GetAsync(PremierLeagueTableEndpoint);
        var body = await response.TextAsync();
        var contentType = response.Headers.TryGetValue(
            "content-type",
            out var value)
            ? value
            : string.Empty;

        await Expect(response).ToBeOKAsync();

        var responseDetails =
            $"Status: {response.Status}; URL: {response.Url}; " +
            $"Content-Type: {contentType}; Body length: {body.Length}.";

        Assert.That(
            contentType,
            Does.Contain("text/html"),
            $"Response should be an HTML page. {responseDetails}");
        Assert.That(
            body,
            Does.Contain("Premier League"),
            $"Response should contain the Premier League. {responseDetails}");
        Assert.That(
            body,
            Does.Contain("table"),
            $"Response should contain table markup. {responseDetails}");
    }
}
