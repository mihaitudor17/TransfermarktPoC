using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.TopBar;

public static class TopBarDestinationExtensions
{
    public static string GetDisplayText(
        this TopBarDestination destination)
    {
        return destination switch
        {
            TopBarDestination.Discover => Constants.TopBarDiscoverText,
            TopBarDestination.TransfersAndRumours => Constants.TopBarTransfersAndRumoursText,
            TopBarDestination.MarketValues => Constants.TopBarMarketValuesText,
            TopBarDestination.Competitions => Constants.TopBarCompetitionsText,
            TopBarDestination.Statistics => Constants.TopBarStatisticsText,
            TopBarDestination.Forum => Constants.TopBarForumText,
            TopBarDestination.Gaming => Constants.TopBarGamingText,

            _ => throw new ArgumentOutOfRangeException(
                nameof(destination),
                destination,
                Constants.UnsupportedTopBarDestinationMessage)
        };
    }
}
