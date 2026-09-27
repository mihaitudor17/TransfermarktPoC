using Microsoft.Playwright;

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
        var deadline = DateTime.UtcNow.AddSeconds(10);

        while (DateTime.UtcNow < deadline)
        {
            var cookieFrame = _page.Frames
                .FirstOrDefault(frame =>
                    frame.Url.Contains("privacy-mgmt.com/index.html"));

            if (cookieFrame != null)
            {
                var acceptButton = cookieFrame.Locator(
                    "#notice button.message-component.accept-all");

                if (await acceptButton.IsVisibleAsync())
                {
                    await acceptButton.ClickAsync();
                    await _page.WaitForTimeoutAsync(500);
                    return;
                }
            }

            await _page.WaitForTimeoutAsync(250);
        }
    }
}