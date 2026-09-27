using Transfermarkt.Playwright.Components.TopBar;

namespace Transfermarkt.Playwright.Tests.Unit;

public class TopBarDestinationExtensionsTests
{
    [TestCase(TopBarDestination.Discover, "DISCOVER")]
    [TestCase(
        TopBarDestination.TransfersAndRumours,
        "TRANSFERS & RUMOURS")]
    [TestCase(TopBarDestination.MarketValues, "MARKET VALUES")]
    [TestCase(TopBarDestination.Competitions, "COMPETITIONS")]
    [TestCase(TopBarDestination.Statistics, "STATISTICS")]
    [TestCase(TopBarDestination.Forum, "FORUM")]
    [TestCase(TopBarDestination.Gaming, "GAMING")]
    public void GetDisplayText_ShouldReturnExpectedText(
        TopBarDestination destination,
        string expectedText)
    {
        var result = destination.GetDisplayText();

        Assert.That(result, Is.EqualTo(expectedText));
    }

    [Test]
    public void GetDisplayText_ShouldReturnNonEmptyTextForEveryDestination()
    {
        foreach (var destination in Enum.GetValues<TopBarDestination>())
        {
            var result = destination.GetDisplayText();

            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }
    }

    [Test]
    public void GetDisplayText_ShouldReturnUniqueTextForEveryDestination()
    {
        var texts = Enum.GetValues<TopBarDestination>()
            .Select(destination => destination.GetDisplayText())
            .ToList();

        Assert.That(
            texts,
            Is.Unique);
    }

    [Test]
    public void GetDisplayText_ShouldMatchDestinationCount()
    {
        var destinations = Enum.GetValues<TopBarDestination>();

        var texts = destinations
            .Select(destination => destination.GetDisplayText())
            .ToList();

        Assert.That(
            texts.Count,
            Is.EqualTo(destinations.Length));
    }
}