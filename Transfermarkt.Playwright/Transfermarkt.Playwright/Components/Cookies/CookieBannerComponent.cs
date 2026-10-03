using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Cookies;

public class CookieBannerComponent
{
    private readonly IPage _page;

    public CookieBannerComponent(IPage page)
    {
        _page = page;
    }

    public async Task AcceptAsync()
    {
        var visibilityTimeout = Constants.CookieBannerTimeoutMilliseconds;
        var acceptButton = _page
            .FrameLocator(Constants.CookiePrivacyFrameSelector)
            .Locator(Constants.CookieAcceptButtonSelector);

        while (true)
        {
            try
            {
                await acceptButton.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = visibilityTimeout
                });
            }
            catch (TimeoutException)
            {
                return;
            }

            await acceptButton.ClickAsync();
            await acceptButton.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = Constants.CookieBannerTimeoutMilliseconds
            });

            visibilityTimeout = Constants.CookieBannerRepeatQuietPeriodMilliseconds;
        }
    }
}
