using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Login;

public class LoginComponent
{
    private readonly IPage _page;

    private const string LoginButtonSelector =
        "button[title='Log in']";

    private const string GuestDropdownSelector =
        "div[class*='dropdown user-guest']";

    private const string CancelButtonSelector =
        ".cancel-button";

    public LoginComponent(IPage page)
    {
        _page = page;
    }

    private ILocator GuestDropdown =>
        _page.Locator(GuestDropdownSelector);

    public async Task OpenAsync()
    {
        await _page
            .Locator(LoginButtonSelector)
            .ClickAsync();
    }

    public async Task CloseAsync()
    {
        await GuestDropdown
            .Locator(CancelButtonSelector)
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