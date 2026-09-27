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

    private ILocator PasswordInput =>
        Form.Locator(Constants.PasswordSelector);

    private ILocator PasswordToggle =>
        PasswordInput
            .Locator(Constants.PasswordToggleContainerSelector)
            .Locator(Constants.PasswordToggleButtonSelector);

    public async Task FillUsernameAsync(string username)
    {
        await Form
            .Locator(Constants.UsernameSelector)
            .FillAsync(username);
    }

    public async Task FillPasswordAsync(string password)
    {
        await PasswordInput.FillAsync(password);
    }

    public async Task SetRememberMeAsync(bool remember)
    {
        var checkbox = Form
            .Locator(Constants.RememberMeSelector);

        if (remember)
        {
            await checkbox.CheckAsync();
        }
        else
        {
            await checkbox.UncheckAsync();
        }
    }

    public async Task TogglePasswordVisibilityAsync()
    {
        await PasswordToggle.ClickAsync();
    }

    public async Task<string> GetPasswordInputTypeAsync()
    {
        return await PasswordInput.GetAttributeAsync(Constants.PasswordTypeAttribute)
               ?? string.Empty;
    }

    public async Task LoginAsync()
    {
        await Form
            .Locator(Constants.SubmitButtonSelector)
            .ClickAsync();
    }

    public async Task<bool> IsRememberMeCheckedAsync()
    {
        return await Form
            .Locator(Constants.RememberMeSelector)
            .IsCheckedAsync();
    }

    public async Task OpenForgotLoginDetailsAsync()
    {
        await Form
            .Locator(Constants.ForgotLoginDetailsSelector)
            .ClickAsync();
    }
    
    public async Task<bool> IsLoginButtonEnabledAsync()
    {
        return await Form
            .Locator(Constants.SubmitButtonSelector)
            .IsEnabledAsync();
    }

    public async Task<bool> HasUsernameErrorAsync()
    {
        var error = Form.Locator(Constants.ErrorListSelector);

        return await error.IsVisibleAsync();
    }

    public async Task<string> GetUsernameErrorAsync()
    {
        var error = Form.Locator(Constants.ErrorListSelector);

        await error.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        return (await error.InnerTextAsync()).Trim();
    }

    public async Task WaitForUsernameErrorToAppearAsync()
    {
        var error = Form.Locator(Constants.ErrorListSelector);

        await error.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
    }

    public async Task WaitForUsernameErrorToDisappearAsync()
    {
        var error = Form.Locator(Constants.ErrorListSelector);

        await Assertions.Expect(error).ToHaveCountAsync(0);
    }
}
