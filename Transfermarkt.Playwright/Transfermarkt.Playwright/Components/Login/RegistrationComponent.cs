using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Login;

public class RegistrationComponent
{
    private readonly IPage _page;

    public RegistrationComponent(IPage page)
    {
        _page = page;
    }

    private ILocator RegisterSection =>
        _page
            .Locator(Constants.GuestDropdownSelector)
            .Locator(Constants.RegisterSectionSelector);

    public async Task<string> GetTitleAsync()
    {
        return (await RegisterSection
                .Locator(Constants.RegisterTitleSelector)
                .InnerTextAsync())
            .Trim();
    }

    public async Task OpenRegistrationAsync()
    {
        await RegisterSection
            .GetByText(
                Constants.SignUpNowText,
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
                Constants.WhyRegisterText,
                new LocatorGetByTextOptions
                {
                    Exact = true
                })
            .ClickAsync();
    }
}