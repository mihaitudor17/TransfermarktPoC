using Transfermarkt.Playwright.Components.TopBar;

namespace Transfermarkt.Playwright.Tests.E2E;

public class TopBarTests : BrowserTest
{
    [TestCase(TopBarDestination.Discover, "/")]
    [TestCase(TopBarDestination.TransfersAndRumours, "/navigation/transfersundgeruechte")]
    [TestCase(TopBarDestination.MarketValues, "/navigation/marktwerte")]
    [TestCase(TopBarDestination.Competitions, "/navigation/wettbewerbe")]
    [TestCase(TopBarDestination.Statistics, "/navigation/statistiken")]
    [TestCase(TopBarDestination.Forum, "/navigation/community")]
    [TestCase(TopBarDestination.Gaming, "/navigation/gaming")]
    public async Task TopBar_ShouldNavigateToDestination(
        TopBarDestination destination,
        string expectedPath)
    {
        var topBar = new TopBarComponent(Page);

        await topBar.NavigateToAsync(destination);

        await Expect(Page).ToHaveURLAsync(
            new Regex($"^https?://[^/]+{Regex.Escape(expectedPath)}(?:[?#].*)?$"));
    }
}
