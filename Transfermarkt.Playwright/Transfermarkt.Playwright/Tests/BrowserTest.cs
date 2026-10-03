using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Transfermarkt.Playwright.Components.Cookies;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Tests;

public abstract class BrowserTest : PageTest
{
    protected HttpErrorMonitor HttpErrors = null!;

    public override BrowserNewContextOptions ContextOptions() => new()
    {
        BaseURL = Constants.TransfermarktBaseUrl
    };

    [SetUp]
    public async Task SetUp()
    {
        // Start monitoring before the first page navigation
        HttpErrors = new HttpErrorMonitor();
        HttpErrors.Attach(Page);

        await Page.GotoAsync("/");

        var cookieBanner = new CookieBannerComponent(Page);
        await cookieBanner.AcceptAsync();
    }

    [TearDown]
    public void VerifyNoServerErrors()
    {
        Assert.That(
            HttpErrors.Errors,
            Is.Empty,
            "HTTP 5xx responses were detected:\n" +
            string.Join(
                "\n",
                HttpErrors.Errors.Select(e => $"{e.Status} - {e.Url}")));
    }
}
