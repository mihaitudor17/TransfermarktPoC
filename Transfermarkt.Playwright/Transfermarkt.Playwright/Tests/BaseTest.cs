using Microsoft.Playwright;
using Transfermarkt.Playwright.Components.Cookies;
using Transfermarkt.Playwright.Fixtures;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Tests;

public abstract class BaseTest
{
    protected PlaywrightFixture Fixture = null!;
    protected IPage Page = null!;
    protected HttpErrorMonitor HttpErrors = null!;

    [SetUp]
    public async Task SetUp()
    {
        Fixture = new PlaywrightFixture();

        await Fixture.InitializeAsync();

        Page = Fixture.Page;

        // Start monitoring before the first page navigation
        HttpErrors = new HttpErrorMonitor();
        HttpErrors.Attach(Page);

        await Page.GotoAsync("/");

        var cookieBanner = new CookieBannerComponent(Page);
        await cookieBanner.AcceptAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        Assert.That(
            HttpErrors.Errors,
            Is.Empty,
            "HTTP 5xx responses were detected:\n" +
            string.Join(
                "\n",
                HttpErrors.Errors.Select(e => $"{e.Status} - {e.Url}")));

        await Fixture.DisposeAsync();
    }
}