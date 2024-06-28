using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace MiaplazaE2EPlaywrightCsharp.PageObjects
{
    public class MOHSInitialApplication
    {
        private readonly IPage _page;

        public MOHSInitialApplication(IPage page)
        {
            _page = page;
        }

        public async Task FillParentInformation(string firstName, string lastName, string email, string phone)
        {
            await _page.FillAsync("input[elname='First']", firstName);
            await _page.FillAsync("input[elname='Last']", lastName);
            await _page.FillAsync("input[name='Email']", email);
            await _page.FillAsync("input[name='PhoneNumber']", phone);
        }

        public async Task FillSecondParentInformation(string firstName, string email, string phone)
        {
            await _page.FillAsync("input[name='Name1']", firstName);
            await _page.FillAsync("input[name='Email1']", email);
            await _page.FillAsync("input[name='PhoneNumber1']", phone);
        }

        public async Task SelectSecondParentInformation(bool addSecondParent)
        {
            var option = addSecondParent ? "Yes" : "No";
            await _page.SelectOptionAsync("select[name='Dropdown']", new[] { option });
        }

        public async Task SelectGoogleCheckboxOption()
        {
            await _page.ClickAsync("label:has-text('Google')");
        }

        public async Task SetRandomAvailableDate()
        {
            await _page.ClickAsync("input[name='Date']");
            await _page.WaitForSelectorAsync("#ui-datepicker-div", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var availableDates = await _page.QuerySelectorAllAsync("#ui-datepicker-div td[data-handler='selectDay']");
            if (availableDates.Count == 0)
            {
                throw new InvalidOperationException("No available dates to select.");
            }
            var random = new Random();
            var randomDate = availableDates[random.Next(0, availableDates.Count)];
            await randomDate.ClickAsync();
        }

        public async Task ClickNext()
        {
            await _page.ClickAsync("button[elname='next']");
        }

        public async Task ScrollIntoView(string selector)
        {
            var element = await _page.QuerySelectorAsync(selector);
            if (element != null)
            {
                await element.ScrollIntoViewIfNeededAsync();
            }
        }

        // Random Data Generators
        public string GenerateRandomEmail()
        {
            return $"test{Guid.NewGuid()}@example.com";
        }

        public string GenerateRandomPhone()
        {
            var random = new Random();
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        public string GenerateRandomFirstName()
        {
            var firstNames = new[] { "John", "Jane", "Michael", "Emily", "Chris", "Sarah" };
            var random = new Random();
            return firstNames[random.Next(firstNames.Length)];
        }

        public string GenerateRandomLastName()
        {
            var lastNames = new[] { "Doe", "Smith", "Johnson", "Lee", "Brown", "Davis" };
            var random = new Random();
            return lastNames[random.Next(lastNames.Length)];
        }
    }
}