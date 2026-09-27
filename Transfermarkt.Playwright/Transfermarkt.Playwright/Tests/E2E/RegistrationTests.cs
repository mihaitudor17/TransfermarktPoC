using Microsoft.Playwright;
using Transfermarkt.Playwright.Components.Login;

namespace Transfermarkt.Playwright.Tests.E2E;

public class RegistrationTests : BaseTest
{
    [Test]
    public async Task Registration_ShouldDisplayCreateAccountTitle()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        var registration = new RegistrationComponent(Page);

        Assert.That(
            await registration.GetTitleAsync(),
            Is.EqualTo("Create Your Account"));
    }

    [Test]
    public async Task Registration_ShouldOpenRegistrationPage()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        var registration = new RegistrationComponent(Page);

        await registration.OpenRegistrationAsync();

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        Assert.That(
            Page.Url,
            Does.Contain("/profil/registrieren"));
    }

    [Test]
    public async Task Registration_ShouldOpenWhyRegisterPage()
    {
        var login = new LoginComponent(Page);

        await login.OpenAsync();

        var registration = new RegistrationComponent(Page);

        await registration.OpenWhyRegisterAsync();

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        Assert.That(
            Page.Url,
            Does.Contain("/profil/warumRegistrieren"));
    }
}