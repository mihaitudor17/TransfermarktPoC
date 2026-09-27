namespace Transfermarkt.Playwright.Helpers;

public static class Constants
{
    public const string TransfermarktBaseUrl = "https://www.transfermarkt.com";
    public const string CookiePrivacyFrameSelector = "iframe[src*='privacy-mgmt.com/index.html']";
    public const string CookieAcceptButtonSelector = "#notice button.message-component.accept-all";
    public const int CookieBannerTimeoutMilliseconds = 10000;
    public const int CookieBannerRepeatQuietPeriodMilliseconds = 2000;

    public const string GuestDropdownSelector = "div[class*='dropdown user-guest']";
    public const string LoginButtonSelector = "button[title='Log in']";
    public const string LoginSectionSelector = "div[class*='login']";
    public const string LoginFormSelector = "form[class*='login-form']";
    public const string UsernameSelector = "#username";
    public const string PasswordSelector = "#password";
    public const string RememberMeSelector = "input[type='checkbox']";
    public const string SubmitButtonSelector = "button[type='submit']";
    public const string ForgotLoginDetailsSelector = "a[href='/profil/loginDetails']";
    public const string CancelButtonSelector = ".cancel-button";
    public const string RegisterSectionSelector = "div[class*='register']";
    public const string RegisterTitleSelector = "h3[class*='register-title']";
    public const string SignUpNowText = "Sign up now";
    public const string WhyRegisterText = "Why register?";
    public const string ProfileTitleSelector = ".data-header > div:first-child h1";

    public const string SearchFormSelector = "#schnellsuche";
    public const string SearchInputSelector = "#schnellsuche input[type='text']";
    public const string SearchButtonSelector = "#schnellsuche button[type='submit']";
    public const string HomeMatchesTableSelector = "table.startseite";
    public const string TableHeadersSelector = "thead tr th";
    public const string TableRowsSelector = "tbody tr";
    public const string ErrorListSelector = "div[class*='error-list']";
    public const string PasswordToggleContainerSelector = "..";
    public const string PasswordToggleButtonSelector = "button";
    public const string PasswordTypeAttribute = "type";
    public const string TopBarLinkSelector = "a.main-navbar__lp-link";
    public const string ActiveTopBarLinkSelector = "a.main-navbar__lp-link.active";
    public const string SearchResultsTableSelector = ".responsive-table table.items";
    public const string SearchResultHeadersSelector = "thead tr th:not(:first-child)";
    public const string ClubsResultType = "clubs";
    public const string PlayersResultType = "players";
    public const string ClubsResultBoxXPath = "//div[contains(@class,'box')][.//h2[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'clubs')]]";
    public const string PlayersResultBoxXPath = "//div[contains(@class,'box')][.//h2[contains(translate(normalize-space(.),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'players')]]";
    public const string UnsupportedSearchResultTypeMessage = "Unsupported search result type: {0}";
    public const string UnsupportedTopBarDestinationMessage = "Unsupported top bar destination.";
    public const string TopBarDiscoverText = "DISCOVER";
    public const string TopBarTransfersAndRumoursText = "TRANSFERS & RUMOURS";
    public const string TopBarMarketValuesText = "MARKET VALUES";
    public const string TopBarCompetitionsText = "COMPETITIONS";
    public const string TopBarStatisticsText = "STATISTICS";
    public const string TopBarForumText = "FORUM";
    public const string TopBarGamingText = "GAMING";
}
