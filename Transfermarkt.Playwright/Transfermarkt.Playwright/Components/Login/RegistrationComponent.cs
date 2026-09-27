using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Login;

public class RegistrationComponent
{
    private readonly IPage _page;

    private const string GuestDropdownSelector =
        "div[class*='dropdown user-guest']";

    private const string RegisterSectionSelector =
        "div[class*='register']";

    private const string RegisterTitleSelector =
        "h3[class*='register-title']";

    public RegistrationComponent(IPage page)
    {
        _page = page;
    }

    private ILocator RegisterSection =>
        _page
            .Locator(GuestDropdownSelector)
            .Locator(RegisterSectionSelector);

    public async Task<string> GetTitleAsync()
    {
        return (await RegisterSection
                .Locator(RegisterTitleSelector)
                .InnerTextAsync())
            .Trim();
    }

    public async Task OpenRegistrationAsync()
    {
        await RegisterSection
            .GetByText(
                "Sign up now",
                new LocatorGetByTextOptions
                {
                    Exact = true
                })
            .ClickAsync();
    }

    public async Task OpenWhyRegisterAsync()
    {
        await RegisterSection
            .GetByText(
                "Why register?",
                new LocatorGetByTextOptions
                {
                    Exact = true
                })
            .ClickAsync();
    }
}