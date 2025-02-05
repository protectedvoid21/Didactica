using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Didactica.Tests.PlaywrightTests;

/// <summary>
/// A test class for web application functionalities using Playwright and NUnit.
/// </summary>
/// <remarks>
/// This class is derived from the Playwright NUnit <see cref="PageTest"/> class
/// and includes automated tests for verifying login behavior in a web application.
/// </remarks>
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlayWrightTests : PageTest
{
    /// <summary>
    /// Defines the URL of the login page utilized in automated Playwright tests for validating authentication workflows.
    /// </summary>
    /// <remarks>
    /// This URL is essential for test cases where navigation to the login screen is required,
    /// enabling the testing of functionalities such as user credential input, login button interactions,
    /// and the handling of success or error scenarios.
    /// </remarks>
    private const string LoginUrl = "http://localhost:5173/login";

    /// <summary>
    /// Represents a valid username for authentication in the Playwright tests.
    /// </summary>
    /// <remarks>
    /// This constant is used to simulate a successful login scenario within the test suite.
    /// The associated password for this username is expected to be valid as well.
    /// </remarks>
    private const string ValidUsername = "test1234";

    /// <summary>
    /// Represents a valid password used for authentication purposes in test cases.
    /// </summary>
    /// <remarks>
    /// This constant is employed in Playwright-based NUnit tests to test login functionalities
    /// by providing a valid password that meets standard complexity requirements, including
    /// uppercase, lowercase, numbers, and special characters.
    /// </remarks>
    private const string ValidPassword = "Test1234!";

    /// <summary>
    /// Represents an invalid username used for testing login failure scenarios in Playwright-based tests.
    /// </summary>
    /// <remarks>
    /// This constant is used in test cases to simulate unsuccessful login attempts
    /// by providing credentials that do not correspond to a valid user account.
    /// </remarks>
    private const string InvalidUsername = "invaliduser";

    /// <summary>
    /// Represents an intentionally incorrect password utilized to simulate and validate login failure cases in automated tests.
    /// </summary>
    /// <remarks>
    /// This constant is employed in negative test scenarios to verify the system's response
    /// to invalid authentication attempts by providing an incorrect password.
    /// </remarks>
    private const string InvalidPassword = "wrongpassword";

    /// Verifies if a valid login attempt redirects the user to the appropriate URL.
    /// This test navigates to the login page, fills in the required fields with valid credentials,
    /// attempts to log in, and checks whether the navigation to the expected URL is successful.
    /// <return>Returns true if the navigation URL matches the expected URL after login.</return>
    [Test]
    public async Task ValidLoginRedirect_returnsTrue()
    {
        await Page.GotoAsync(LoginUrl);

        await Page.GetByLabel("Nazwa użytkownika").FillAsync(ValidUsername);
        await Page.GetByLabel("Hasło").FillAsync(ValidPassword);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Zaloguj" }).ClickAsync();

        await Page.WaitForNavigationAsync();
        Xunit.Assert.Equal("http://localhost:5173/", Page.Url); 
    }

    /// Tests that an error message is displayed when a login attempt fails due to invalid credentials being provided.
    /// This test navigates to the login page, inputs invalid username and password, submits the form, and verifies
    /// that the appropriate error message is displayed.
    /// <returns>A task representing the asynchronous operation, ensuring the appropriate error message is displayed for failed login attempts.</returns>
    [Test]
    public async Task FailedLoginShowsErrorMessage()
    {
        await Page.GotoAsync(LoginUrl);

        await Page.GetByLabel("Nazwa użytkownika").FillAsync(InvalidUsername);
        await Page.GetByLabel("Hasło").FillAsync(InvalidPassword);
        
        await Page.GetByRole(AriaRole.Button, new() { Name = "Zaloguj" }).ClickAsync();
        
        var errorMessage = await Page.GetByText("Could not log in. Invalid username or password.").TextContentAsync();
        Xunit.Assert.Equal("Could not log in. Invalid username or password.", errorMessage);
    }
}