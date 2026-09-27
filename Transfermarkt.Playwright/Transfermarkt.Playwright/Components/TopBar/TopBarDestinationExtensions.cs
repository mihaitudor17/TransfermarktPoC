namespace Transfermarkt.Playwright.Components.TopBar;

public static class TopBarDestinationExtensions
{
    public static string GetDisplayText(
        this TopBarDestination destination)
    {
        return destination switch
        {
            TopBarDestination.Discover => "DISCOVER",
            TopBarDestination.TransfersAndRumours => "TRANSFERS & RUMOURS",
            TopBarDestination.MarketValues => "MARKET VALUES",
            TopBarDestination.Competitions => "COMPETITIONS",
            TopBarDestination.Statistics => "STATISTICS",
            TopBarDestination.Forum => "FORUM",
            TopBarDestination.Gaming => "GAMING",

            _ => throw new ArgumentOutOfRangeException(
                nameof(destination),
                destination,
                "Unsupported top bar destination.")
        };
    }
}