using Transfermarkt.Playwright.Components.Login;

namespace Transfermarkt.Playwright.Tests.E2E;

public class LoginTests : BrowserTest
{
    private const string TestUsername = "test-user";
    private const string TestPassword = "test-password";

    private LoginComponent _login = null!;
    private LoginForm _form = null!;

    [SetUp]
    public void SetUpLoginComponents()
    {
        _login = new LoginComponent(Page);
        _form = _login.Form;
    }

    [Test]
    public async Task Login_ShouldOpenGuestDropdown()
    {
        await _login.OpenAsync();

        await Expect(_login.GuestDropdown).ToBeVisibleAsync();
    }

    [Test]
    public async Task Login_ShouldCloseGuestDropdown()
    {
        await _login.OpenAsync();
        await _login.CloseAsync();

        await Expect(_login.GuestDropdown).ToBeHiddenAsync();
    }

    [Test]
    public async Task Login_ShouldAllowEnteringCredentials()
    {
        await _login.OpenAsync();

        await _form.FillUsernameAsync(TestUsername);
        await _form.FillPasswordAsync(TestPassword);

        await Expect(_form.UsernameInput).ToHaveValueAsync(TestUsername);
        await Expect(_form.PasswordInput).ToHaveValueAsync(TestPassword);
    }

    [Test]
    public async Task Login_ShouldRememberUserSelection()
    {
        await _login.OpenAsync();

        await _form.SetRememberMeAsync(true);

        await Expect(_form.RememberMeCheckbox).ToBeCheckedAsync();
    }

    [Test]
    public async Task Login_ShouldAllowUncheckingRememberMe()
    {
        await _login.OpenAsync();

        await _form.SetRememberMeAsync(true);
        await _form.SetRememberMeAsync(false);

        await Expect(_form.RememberMeCheckbox).Not.ToBeCheckedAsync();
    }

    [Test]
    public async Task Login_ShouldOpenForgotLoginDetailsPage()
    {
        await _login.OpenAsync();

        await _form.OpenForgotLoginDetailsAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex(@"/profil/loginDetails(?:[/?#]|$)"));
    }

    [Test]
    public async Task Login_ShouldTogglePasswordVisibility()
    {
        await _login.OpenAsync();

        await _form.FillPasswordAsync(TestPassword);

        await Expect(_form.PasswordInput).ToHaveAttributeAsync("type", "password");

        await _form.TogglePasswordVisibilityAsync();

        await Expect(_form.PasswordInput).ToHaveAttributeAsync("type", "text");

        await _form.TogglePasswordVisibilityAsync();

        await Expect(_form.PasswordInput).ToHaveAttributeAsync("type", "password");
    }

    [Test]
    public async Task Login_ShouldBeDisabledWhenUsernameIsEmpty()
    {
        await _login.OpenAsync();

        await _form.FillPasswordAsync(TestPassword);

        await Expect(_form.LoginButton).ToBeDisabledAsync();
    }

    [Test]
    public async Task Login_ShouldBeDisabledWhenPasswordIsEmpty()
    {
        await _login.OpenAsync();

        await _form.FillUsernameAsync(TestUsername);

        await Expect(_form.LoginButton).ToBeDisabledAsync();
    }

    [Test]
    public async Task Login_ShouldBeDisabledWhenUsernameAndPasswordAreEmpty()
    {
        await _login.OpenAsync();

        await Expect(_form.LoginButton).ToBeDisabledAsync();
    }

    [Test]
    public async Task Login_ShouldShowUsernameErrorWhenUsernameContainsAt()
    {
        await _login.OpenAsync();

        await _form.FillUsernameAsync("test@example.com");

        await Expect(_form.UsernameError).ToBeVisibleAsync();
        await Expect(_form.UsernameError)
            .ToContainTextAsync("@ Zeichen ist im Benutzernamen nicht erlaubt");
    }

    [Test]
    public async Task Login_ShouldRemoveUsernameErrorWhenAtIsRemoved()
    {
        await _login.OpenAsync();

        await _form.FillUsernameAsync("test@example.com");

        await Expect(_form.UsernameError).ToBeVisibleAsync();

        await _form.FillUsernameAsync("testexample.com");

        await Expect(_form.UsernameError).ToBeHiddenAsync();
    }
}
