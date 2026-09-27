using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Components.Login;

public class LoginForm
{
    private readonly IPage _page;

    private const string GuestDropdownSelector =
        "div[class*='dropdown user-guest']";

    private const string LoginSectionSelector =
        "div[class*='login']";

    private const string LoginFormSelector =
        "form[class*='login-form']";

    private const string UsernameSelector =
        "#username";

    private const string PasswordSelector =
        "#password";

    private const string RememberMeSelector =
        "input[type='checkbox']";

    private const string SubmitButtonSelector =
        "button[type='submit']";

    private const string ForgotLoginDetailsSelector =
        "a[href='/profil/loginDetails']";

    public LoginForm(IPage page)
    {
        _page = page;
    }

    private ILocator LoginSection =>
        _page
            .Locator(GuestDropdownSelector)
            .Locator(LoginSectionSelector);

    private ILocator Form =>
        LoginSection.Locator(LoginFormSelector);

    private ILocator PasswordInput =>
        Form.Locator(PasswordSelector);

    private ILocator PasswordToggle =>
        PasswordInput
            .Locator("..")
            .Locator("button");

    public async Task FillUsernameAsync(string username)
    {
        await Form
            .Locator(UsernameSelector)
            .FillAsync(username);
    }

    public async Task FillPasswordAsync(string password)
    {
        await PasswordInput.FillAsync(password);
    }

    public async Task SetRememberMeAsync(bool remember)
    {
        var checkbox = Form
            .Locator(RememberMeSelector);

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
        return await PasswordInput.GetAttributeAsync("type")
               ?? string.Empty;
    }

    public async Task LoginAsync()
    {
        await Form
            .Locator(SubmitButtonSelector)
            .ClickAsync();
    }

    public async Task<bool> IsRememberMeCheckedAsync()
    {
        return await Form
            .Locator(RememberMeSelector)
            .IsCheckedAsync();
    }

    public async Task OpenForgotLoginDetailsAsync()
    {
        await Form
            .Locator(ForgotLoginDetailsSelector)
            .ClickAsync();
    }
    
    public async Task<bool> IsLoginButtonEnabledAsync()
    {
        return await Form
            .Locator(SubmitButtonSelector)
            .IsEnabledAsync();
    }

    public async Task<bool> HasUsernameErrorAsync()
    {
        var error = Form.Locator("div[class*='error-list']");

        return await error.IsVisibleAsync();
    }

    public async Task<string> GetUsernameErrorAsync()
    {
        var error = Form.Locator("div[class*='error-list']");

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
        var error = Form.Locator("div[class*='error-list']");

        await error.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
    }

    public async Task WaitForUsernameErrorToDisappearAsync()
    {
        var error = Form.Locator("div[class*='error-list']");

        await Assertions.Expect(error).ToHaveCountAsync(0);
    }
}