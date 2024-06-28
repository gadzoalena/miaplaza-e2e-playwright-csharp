using System.Threading.Tasks;
using Microsoft.Playwright;

namespace MiaplazaE2EPlaywrightCsharp.PageObjects
{
    public class MiaPrepHomePage
    {
        private readonly IPage _page;

        public MiaPrepHomePage(IPage page)
        {
            _page = page;
        }

        public async Task WaitForPageLoadAsync()
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task<bool> IsHeadlineVisible()
        {
            return await _page.IsVisibleAsync("h1:has-text('High School on Your Terms')");
        }

        public async Task ClickApplyButton()
        {
            await _page.ClickAsync("a:has-text('Apply to Our School')");
        }
    }
}