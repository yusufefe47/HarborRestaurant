using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface ITranslationService
    {
        Task<string> TranslateAsync(string text, string fromLanguage = "tr", string toLanguage = "en");
        Task<Dictionary<string, string>> TranslateBatchAsync(Dictionary<string, string> texts, string fromLanguage = "tr", string toLanguage = "en");
        Task<HomePage> TranslateHomePageAsync(HomePage homePage, bool translateToEnglish = true);
        Task<About> TranslateAboutAsync(About about, bool translateToEnglish = true);
        Task TranslateRoomsAsync();
        Task TranslateMenuItemsAsync();
        Task TranslateBlogPostsAsync();
        Task TranslateAllContentAsync();
    }
}
