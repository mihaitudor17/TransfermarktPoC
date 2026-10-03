using Transfermarkt.Playwright.Components.Login;

namespace Transfermarkt.Playwright.Tests.E2E;

public class RegistrationTests : BrowserTest
{
    private LoginComponent _login = null!;
    private RegistrationComponent _registration = null!;

    [SetUp]
    public async Task SetUpRegistrationComponents()
    {
        _login = new LoginComponent(Page);
        _registration = _login.Registration;
        await _login.OpenAsync();
    }

    [Test]
    public async Task Registration_ShouldDisplayCreateAccountTitle()
    {
        await Expect(_registration.Title).ToHaveTextAsync("Create your account");
    }

    [Test]
    public async Task Registration_ShouldOpenRegistrationPage()
    {
        await _registration.OpenRegistrationAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex(@"/profil/registrieren(?:[/?#]|$)"));
    }

    [Test]
    public async Task Registration_ShouldOpenWhyRegisterPage()
    {
        await _registration.OpenWhyRegisterAsync();

        await Expect(Page).ToHaveURLAsync(
            new Regex(@"/profil/warumRegistrieren(?:[/?#]|$)"));
    }
}
