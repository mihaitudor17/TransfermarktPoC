# Transfermarkt Playwright POC

A Playwright test suite for [Transfermarkt](https://www.transfermarkt.com) using C#, .NET and NUnit.

The project covers the main UI flows requested in the assignment, as well as API, network and unit tests.

## Requirements

* .NET 8 SDK
* PowerShell (`pwsh`) to install Playwright's Chromium browser

## Running the Tests

From the repository root, restore and build the solution, install Chromium, and run the tests:

```bash
cd Transfermarkt.Playwright
dotnet restore Transfermarkt.Playwright.sln
dotnet build Transfermarkt.Playwright.sln
pwsh Transfermarkt.Playwright/bin/Debug/net8.0/playwright.ps1 install chromium
dotnet test Transfermarkt.Playwright.sln --no-build
```

The browser tests run in headless Chromium by default. The Playwright NuGet package is restored with the solution; the install command downloads the browser binary.

To capture the complete console output as submission evidence, run this from the solution directory after the final changes:

```bash
dotnet test Transfermarkt.Playwright.sln --no-build --logger "console;verbosity=normal" > Transfermarkt.Playwright/TestResults/test-output.txt 2>&1
```

This writes `Transfermarkt.Playwright/Transfermarkt.Playwright/TestResults/test-output.txt` from the repository root. This console evidence file is kept in Git; other generated test results remain ignored.

## Test Coverage

The test suite covers:

### E2E tests

The E2E tests run through the Transfermarkt UI.

They cover:

* Dynamic match tables displayed on the home page
* Premier League standings table in the browser
* Search
* Main navigation
* Login form behaviour
* Registration links
* HTTP 5xx monitoring during browser flows

### Integration tests

The integration tests check HTTP and network behaviour without testing the full UI flow.

They cover:

* Transfermarkt HTTP endpoints
* Premier League standings endpoint response
* Search request interception
* Search request parameters

### Unit tests

The unit tests check small pieces of logic without starting a browser or making network requests.

They cover the mapping between `TopBarDestination` values and the text used by the top navigation.

## Test Strategy

The project uses three test levels.

### Unit

Unit tests check isolated logic.

The `TopBarDestinationExtensions` class maps each `TopBarDestination` value to the text shown in the navigation bar.

This logic does not need a browser or a network connection, so it is tested as a unit.

The tests also check that every destination has a non-empty and unique display value.

### Integration

Integration tests check communication with Transfermarkt.

The API tests use Playwright's `APIRequestContext` to send requests directly to Transfermarkt.

The network tests use a browser context and Playwright route interception to check the search request and its query parameter.

These tests were chosen because they test API and network behaviour without the cost of a full browser flow.

### E2E

E2E tests use Chromium and interact with Transfermarkt as a user would.

They cover the main flows from the assignment:

* Home page
* Search
* Navigation
* Login
* Registration

The E2E tests also monitor HTTP responses and fail if a request returns a 5xx status during the test flow.

## Design

The project uses small components to keep browser actions and selectors outside the tests.

Examples:

* `TopBarComponent` handles the main navigation.
* `SearchComponent` handles search.
* `LoginComponent` handles the login dropdown.
* `LoginForm` handles login form actions and validation.
* `RegistrationComponent` handles registration links.
* `SearchResultsTableComponent` handles search result tables.
* `HomeMatchesTableComponent` exposes the match tables on the home page.
* `ProfileComponent` handles profile page checks.
* `CookieBannerComponent` handles the cookie consent UI.

The browser tests inherit from a shared `BrowserTest`, which uses Playwright's NUnit `PageTest` base class. It configures the base URL, opens the home page, accepts cookies and checks for HTTP 5xx responses. Playwright manages the browser, context and page lifecycle.

The tests contain the assertions. The components contain the browser actions and selectors.

This keeps the tests focused on behaviour and makes selectors easier to maintain.

## Technology Choices

### C#

C# was chosen for the test suite because it is well suited to UI automation and provides strong support for async browser operations.

### .NET

The project targets .NET 8.

### Playwright

Playwright was chosen because it provides:

* Browser automation
* Locator-based element handling
* Automatic waiting
* Network interception
* API requests
* Headless browser support

These features cover both the E2E and integration test requirements.

### NUnit

NUnit was chosen for its simple test structure, assertions, setup and teardown support, and test case support.

## Test Stability

The tests are designed to work with Transfermarkt's changing content.

They avoid hardcoding the full contents of pages where possible.

For example:

* Home page tests validate table structure, team names, dates and match times.
* Search tests use different search terms and validate the returned content.
* Navigation tests verify the expected URL for each destination.
* Home page tests validate the match tables currently displayed instead of expecting a fixed set of fixtures.
* The Premier League standings UI test checks the table structure and row count without depending on which clubs occupy particular positions.
* Login tests validate form behaviour rather than depending on a successful login.

The tests use Playwright locators and assertions to wait for page elements.

Static waits are avoided in the test flows where possible.

## Dynamic Content and External Limitations

Transfermarkt is a live external website. Fixtures, search results and other page content can change over time.

The tests therefore validate the structure and behaviour of the page instead of relying on a fixed set of dynamic data.

The cookie consent UI is loaded inside a third-party iframe and can appear after the page loads. `CookieBannerComponent` waits for the relevant frame and consent button before continuing.

The login tests do not perform a real authenticated login. A real login would require valid external account credentials. Instead, the tests cover the form behaviour, validation, password visibility, remember-me option and login-related links.

The HTTP 5xx monitor checks responses generated during the tested browser flows. It does not guarantee that Transfermarkt has no server errors outside those flows.

## Test Execution Evidence

Save the complete output from the final full test run at:

```text
Transfermarkt.Playwright/Transfermarkt.Playwright/TestResults/test-output.txt
```

The file contains the complete output from the final test run. To regenerate it from the solution directory, run:

```bash
dotnet test Transfermarkt.Playwright.sln --no-build --logger "console;verbosity=normal" > Transfermarkt.Playwright/TestResults/test-output.txt 2>&1
```

This provides evidence that the test suite was executed locally.

## Continuous Integration

The GitHub Actions workflow in `.github/workflows/playwright.yml` builds the solution, installs Chromium, runs the test suite, and uploads the NUnit test results. The tests use the live Transfermarkt website, so workflow results depend on that external site being reachable and allowing requests from the runner.

## Assumptions

* The tests run against the live Transfermarkt website.
* The test suite does not create or modify a real Transfermarkt account.
* Test data can change because Transfermarkt is a live website.
* A successful authenticated login is outside the scope of this POC.
* Third-party cookie consent is handled as part of the test setup.

## Project Structure

```text
Transfermarkt.Playwright/
├── Components/
│   ├── Cookies/
│   ├── Login/
│   ├── Profiles/
│   ├── Search/
│   ├── Tables/
│   └── TopBar/
│
├── Fixtures/
│   └── ApiFixture.cs
│
├── Helpers/
│   └── HttpErrorMonitor.cs
│
├── Tests/
│   ├── BrowserTest.cs
│   ├── E2E/
│   ├── Integration/
│   └── Unit/
│
├── README.md
└── Transfermarkt.Playwright.csproj
```

## Summary

The solution separates unit, integration and E2E tests based on what they need to verify.

* Unit tests verify isolated logic.
* Integration tests verify API and network behaviour.
* E2E tests verify user-facing browser flows.

This keeps each test level focused on the type of behaviour it is intended to check.
