using System.Threading.Tasks;
using Microsoft.Playwright;

namespace MiaplazaE2EPlaywrightCsharp.PageObjects
{
    public class MiaAcademyHomePage
    {
        private readonly IPage _page;

        public MiaAcademyHomePage(IPage page)
        {
            _page = page;
        }

        public async Task GoToPage(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task<bool> IsMainHeadlineVisible()
        {
            // Check if the main headline is visible and matches the expected text
            const string expectedHeadline = "Learning, Fun & Friends for Kids!";
            var headlineElement = _page.Locator(".mia-motto");
            return await headlineElement.TextContentAsync() == expectedHeadline
                && await headlineElement.IsVisibleAsync();
        }

        public async Task ClickOnlineHighSchoolLink()
        {
            // Click the link to check out MiaPrep, the online high school
            await _page.ClickAsync("a[href='https://miaprep.com/online-school/']");
        }
    }
}
