using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Login;

public class LoginComponent
{
    private readonly IPage _page;

    public LoginComponent(IPage page)
    {
        _page = page;
    }

    private ILocator GuestDropdown =>
        _page.Locator(Constants.GuestDropdownSelector);

    public async Task OpenAsync()
    {
        await _page
            .Locator(Constants.LoginButtonSelector)
            .ClickAsync();
    }

    public async Task CloseAsync()
    {
        await GuestDropdown
            .Locator(Constants.CancelButtonSelector)
            .ClickAsync();

        await GuestDropdown.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = 10000
            });
    }

    public async Task<bool> IsOpenAsync()
    {
        return await GuestDropdown.IsVisibleAsync();
    }

    public LoginForm Form =>
        new LoginForm(_page);

    public RegistrationComponent Registration =>
        new RegistrationComponent(_page);
}