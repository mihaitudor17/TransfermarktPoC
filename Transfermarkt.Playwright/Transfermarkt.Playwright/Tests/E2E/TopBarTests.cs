using Transfermarkt.Playwright.Components.TopBar;

namespace Transfermarkt.Playwright.Tests.E2E;

public class TopBarTests : BaseTest
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
        await Page.GotoAsync("/");

        var topBar = new TopBarComponent(Page);

        await topBar.NavigateToAsync(destination);

        Assert.That(new Uri(Page.Url).AbsolutePath, Is.EqualTo(expectedPath));
    }
}