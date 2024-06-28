using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using MiaplazaE2EPlaywrightCsharp.PageObjects;

namespace MiaplazaE2EPlaywrightCsharp.Tests
{
    public class MiaAcademyTests
    {
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IPage _page = null!;
        private MiaAcademyHomePage _miaAcademyHomePage = null!;
        private MiaPrepHomePage _miaPrepHomePage = null!;
        private MOHSInitialApplication _mohsInitialApplication = null!;

        [SetUp]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            _miaAcademyHomePage = new MiaAcademyHomePage(_page);
            _miaPrepHomePage = new MiaPrepHomePage(_page);
            _mohsInitialApplication = new MOHSInitialApplication(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task TestMiaAcademyNavigationToMiaPrep()
        {
            // Navigate to the MiaAcademy Home Page
            string miaAcademyUrl = "https://miacademy.co/";
            await _miaAcademyHomePage.GoToPage(miaAcademyUrl);

            // Assert that the main headline on the MiaAcademy home page is visible and correct
            bool isMiaAcademyHeadlineVisible = await _miaAcademyHomePage.IsMainHeadlineVisible();
            Assert.IsTrue(isMiaAcademyHeadlineVisible, "Main headline should be visible and match the expected text.");

            // Click the link to navigate to the MiaPrep Home Page
            await _miaAcademyHomePage.ClickOnlineHighSchoolLink();

            // Assert that the URL has changed to the MiaPrep home page URL
            await _page.WaitForURLAsync("https://miaprep.com/online-school/");
            Assert.AreEqual("https://miaprep.com/online-school/", _page.Url, "URL should match the MiaPrep online school.");

            // Wait for the MiaPrep Home Page to load
            await _miaPrepHomePage.WaitForPageLoadAsync();

            // Assert that the main headline on the MiaPrep home page is visible and correct
            bool isMiaPrepHeadlineVisible = await _miaPrepHomePage.IsHeadlineVisible();
            Assert.IsTrue(isMiaPrepHeadlineVisible, "MiaPrep page headline should be visible and match the expected text.");

            // Click the "Apply to Our School" Button
            await _miaPrepHomePage.ClickApplyButton();

            // Wait for the application form page to load
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Assert that the Parent Information form section is visible
            var parentInfoSection = await _page.QuerySelectorAsync("h2:has-text('Parent Information')");
            Assert.IsNotNull(parentInfoSection, "Parent Information section should be visible.");

            // Fill out the Parent Information form with random data
            string firstName = _mohsInitialApplication.GenerateRandomFirstName();
            string lastName = _mohsInitialApplication.GenerateRandomLastName();
            string email = _mohsInitialApplication.GenerateRandomEmail();
            string phone = _mohsInitialApplication.GenerateRandomPhone();
            await _mohsInitialApplication.FillParentInformation(firstName, lastName, email, phone);

            // Select the option to add information for a second parent
            await _mohsInitialApplication.SelectSecondParentInformation(true);

            // Assert that the second parent information section is visible
            var secondParentSection = await _page.QuerySelectorAsync("h2:has-text('Second Parent Information')");
            Assert.IsNotNull(secondParentSection, "Second Parent Information section should be visible.");

            // Fill out the Second Parent Information form with random data
            string secondFirstName = _mohsInitialApplication.GenerateRandomFirstName();
            string secondLastName = _mohsInitialApplication.GenerateRandomLastName();
            string secondEmail = _mohsInitialApplication.GenerateRandomEmail();
            string secondPhone = _mohsInitialApplication.GenerateRandomPhone();
            await _mohsInitialApplication.FillSecondParentInformation(secondFirstName, secondEmail, secondPhone);

            // Select the checkbox option for "How did you hear about us?" containing "Google"
            await _mohsInitialApplication.SelectGoogleCheckboxOption();

            // Assert that at least one checkbox is selected
            var selectedCheckbox = await _page.QuerySelectorAsync("div[role='group'] input[type='checkbox']:checked");
            Assert.IsNotNull(selectedCheckbox, "At least one 'How did you hear about us?' option should be selected.");

            // Set a random available date
            await _mohsInitialApplication.SetRandomAvailableDate();

            // Assert that the start date is correctly set
            var filledStartDate = await _page.Locator("input[name='Date']").InputValueAsync();
            Assert.IsFalse(string.IsNullOrEmpty(filledStartDate), "A start date should be selected.");

            // Proceed to the next page
            await _mohsInitialApplication.ClickNext();

            // Assert that the Student Information form section is visible
            var studentInfoSection = await _page.QuerySelectorAsync("h2:has-text('Student Information')");
            Assert.IsNotNull(studentInfoSection, "Student Information section should be visible.");

            await Task.Delay(5000); // Visual observation of the test run (not required for actual tests)
        }
    }
}