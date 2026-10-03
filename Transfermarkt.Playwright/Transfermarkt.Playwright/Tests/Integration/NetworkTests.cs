using Transfermarkt.Playwright.Components.Search;

namespace Transfermarkt.Playwright.Tests.Integration;

public class NetworkTests : BrowserTest
{
    [TestCase("Liverpool")]
    [TestCase("Manchester United")]
    [TestCase("Real Madrid")]
    public async Task Search_ShouldInterceptSearchRequest(
        string searchTerm)
    {
        var intercepted = false;

        await Page.RouteAsync(
            "**/schnellsuche/ergebnis/schnellsuche**",
            async route =>
            {
                intercepted = true;

                Assert.That(
                    route.Request.Method,
                    Is.EqualTo("GET"));

                var uri = new Uri(route.Request.Url);

                var query = System.Web.HttpUtility.ParseQueryString(
                    uri.Query);

                Assert.That(
                    query["query"],
                    Is.EqualTo(searchTerm));

                await route.ContinueAsync();
            });

        var search = new SearchComponent(Page);

        await search.SearchAsync(searchTerm);

        Assert.That(
            intercepted,
            Is.True,
            "The search request was not intercepted.");
    }
}
