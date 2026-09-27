using Microsoft.Playwright;
using Transfermarkt.Playwright.Components.Login;

namespace Transfermarkt.Playwright.Tests.E2E;

public class LoginTests : BaseTest
{
    [Test]
    public async Task Login_ShouldOpenGuestDropdown()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        Assert.That(
            await login.IsOpenAsync(),
            Is.True,
            "Login dropdown should be visible.");
    }

    [Test]
    public async Task Login_ShouldCloseGuestDropdown()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();
        await login.CloseAsync();

        Assert.That(
            await login.IsOpenAsync(),
            Is.False,
            "Login dropdown should be closed.");
    }

    [Test]
    public async Task Login_ShouldAllowEnteringCredentials()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillUsernameAsync("test-user");
        await login.Form.FillPasswordAsync("test-password");

        Assert.That(
            await Page.Locator("#username").InputValueAsync(),
            Is.EqualTo("test-user"));

        Assert.That(
            await Page.Locator("#password").InputValueAsync(),
            Is.EqualTo("test-password"));
    }

    [Test]
    public async Task Login_ShouldRememberUserSelection()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.SetRememberMeAsync(true);

        Assert.That(
            await login.Form.IsRememberMeCheckedAsync(),
            Is.True);
    }

    [Test]
    public async Task Login_ShouldAllowUncheckingRememberMe()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.SetRememberMeAsync(true);
        await login.Form.SetRememberMeAsync(false);

        Assert.That(
            await login.Form.IsRememberMeCheckedAsync(),
            Is.False);
    }

    [Test]
    public async Task Login_ShouldOpenForgotLoginDetailsPage()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.OpenForgotLoginDetailsAsync();

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        Assert.That(
            Page.Url,
            Does.Contain("/profil/loginDetails"));
    }
    
    [Test]
    public async Task Login_ShouldTogglePasswordVisibility()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillPasswordAsync("test-password");

        Assert.That(
            await login.Form.GetPasswordInputTypeAsync(),
            Is.EqualTo("password"));

        await login.Form.TogglePasswordVisibilityAsync();

        Assert.That(
            await login.Form.GetPasswordInputTypeAsync(),
            Is.EqualTo("text"));

        await login.Form.TogglePasswordVisibilityAsync();

        Assert.That(
            await login.Form.GetPasswordInputTypeAsync(),
            Is.EqualTo("password"));
    }
    
    [Test]
    public async Task Login_ShouldBeDisabledWhenUsernameIsEmpty()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillPasswordAsync("test-password");

        Assert.That(
            await login.Form.IsLoginButtonEnabledAsync(),
            Is.False,
            "Login button should be disabled when username is empty.");
    }

    [Test]
    public async Task Login_ShouldBeDisabledWhenPasswordIsEmpty()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillUsernameAsync("test-user");

        Assert.That(
            await login.Form.IsLoginButtonEnabledAsync(),
            Is.False,
            "Login button should be disabled when password is empty.");
    }

    [Test]
    public async Task Login_ShouldBeDisabledWhenUsernameAndPasswordAreEmpty()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        Assert.That(
            await login.Form.IsLoginButtonEnabledAsync(),
            Is.False,
            "Login button should be disabled when username and password are empty.");
    }

    [Test]
    public async Task Login_ShouldShowUsernameErrorWhenUsernameContainsAt()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillUsernameAsync("test@example.com");

        await login.Form.WaitForUsernameErrorToAppearAsync();

        Assert.That(
            await login.Form.HasUsernameErrorAsync(),
            Is.True,
            "Username validation error should be displayed.");

        Assert.That(
            await login.Form.GetUsernameErrorAsync(),
            Does.Contain(
                "@ Zeichen ist im Benutzernamen nicht erlaubt"));
    }

    [Test]
    public async Task Login_ShouldRemoveUsernameErrorWhenAtIsRemoved()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        await login.Form.FillUsernameAsync("test@example.com");

        await login.Form.WaitForUsernameErrorToAppearAsync();

        Assert.That(
            await login.Form.HasUsernameErrorAsync(),
            Is.True);

        await login.Form.FillUsernameAsync("testexample.com");

        await login.Form.WaitForUsernameErrorToDisappearAsync();

        Assert.That(
            await login.Form.HasUsernameErrorAsync(),
            Is.False,
            "Username validation error should disappear after removing '@'.");
    }
}