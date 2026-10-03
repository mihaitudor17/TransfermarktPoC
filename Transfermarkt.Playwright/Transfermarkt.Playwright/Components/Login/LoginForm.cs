using Microsoft.Playwright;
using Transfermarkt.Playwright.Helpers;

namespace Transfermarkt.Playwright.Components.Login;

public class LoginForm
{
    private readonly IPage _page;

    public LoginForm(IPage page)
    {
        _page = page;
    }

    private ILocator LoginSection =>
        _page
            .Locator(Constants.GuestDropdownSelector)
            .Locator(Constants.LoginSectionSelector);

    private ILocator Form =>
        LoginSection.Locator(Constants.LoginFormSelector);

    public ILocator UsernameInput =>
        Form.Locator(Constants.UsernameSelector);

    public ILocator PasswordInput =>
        Form.Locator(Constants.PasswordSelector);

    public ILocator RememberMeCheckbox =>
        Form.Locator(Constants.RememberMeSelector);

    public ILocator LoginButton =>
        Form.Locator(Constants.SubmitButtonSelector);

    public ILocator UsernameError =>
        Form.Locator(Constants.ErrorListSelector);

    private ILocator PasswordToggle =>
        PasswordInput
            .Locator(Constants.PasswordToggleContainerSelector)
            .Locator(Constants.PasswordToggleButtonSelector);

    public async Task FillUsernameAsync(string username)
    {
        await UsernameInput.FillAsync(username);
    }

    public async Task FillPasswordAsync(string password)
    {
        await PasswordInput.FillAsync(password);
    }

    public async Task SetRememberMeAsync(bool remember)
    {
        if (remember)
        {
            await RememberMeCheckbox.CheckAsync();
        }
        else
        {
            await RememberMeCheckbox.UncheckAsync();
        }
    }

    public async Task TogglePasswordVisibilityAsync()
    {
        await PasswordToggle.ClickAsync();
    }

    public async Task OpenForgotLoginDetailsAsync()
    {
        await Form
            .Locator(Constants.ForgotLoginDetailsSelector)
            .ClickAsync();
    }

}
