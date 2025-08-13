using System.Text;
using System.Text.Json;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly IRoomRepository _roomRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHomePageService _homePageService;
        private readonly IAboutService _aboutService;
        private readonly IConfiguration _configuration;
        
        public TranslationService(HttpClient httpClient, IRoomRepository roomRepository, 
            IMenuItemRepository menuItemRepository, IBlogPostRepository blogPostRepository,
            IHomePageService homePageService, IAboutService aboutService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _roomRepository = roomRepository;
            _menuItemRepository = menuItemRepository;
            _blogPostRepository = blogPostRepository;
            _homePageService = homePageService;
            _aboutService = aboutService;
            _configuration = configuration;
        }

        public async Task<string> TranslateAsync(string text, string fromLanguage = "tr", string toLanguage = "en")
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            try
            {
                // MyMemory API'sini kullan (ücretsiz)
                return await TranslateWithMyMemoryAsync(text, fromLanguage, toLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Translation error: {ex.Message}");
                return text; // Hata durumunda orijinal metni döndür
            }
        }

        private async Task<string> TranslateWithMyMemoryAsync(string text, string fromLanguage, string toLanguage)
        {
            try
            {
                var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair={fromLanguage}|{toLanguage}";
                
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<MyMemoryResponse>(json);
                    
                    if (result?.responseData?.translatedText != null)
                    {
                        // Rate limiting için bekleme
                        var delayMs = _configuration.GetValue<int>("Translation:RateLimitDelayMs", 100);
                        await Task.Delay(delayMs);
                        
                        return result.responseData.translatedText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MyMemory translation error: {ex.Message}");
            }
            
            return text;
        }

        public async Task<HomePage> TranslateHomePageAsync(HomePage homePage, bool translateToEnglish = true)
        {
            if (translateToEnglish)
            {
                if (!string.IsNullOrWhiteSpace(homePage.MainTitle))
                    homePage.MainTitleEn = await TranslateAsync(homePage.MainTitle);
                
                if (!string.IsNullOrWhiteSpace(homePage.Subtitle))
                    homePage.SubtitleEn = await TranslateAsync(homePage.Subtitle);
                
                if (!string.IsNullOrWhiteSpace(homePage.Description))
                    homePage.DescriptionEn = await TranslateAsync(homePage.Description);
                
                if (!string.IsNullOrWhiteSpace(homePage.ButtonText))
                    homePage.ButtonTextEn = await TranslateAsync(homePage.ButtonText);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(homePage.MainTitleEn))
                    homePage.MainTitle = await TranslateAsync(homePage.MainTitleEn, "en", "tr");
                
                if (!string.IsNullOrWhiteSpace(homePage.SubtitleEn))
                    homePage.Subtitle = await TranslateAsync(homePage.SubtitleEn, "en", "tr");
                
                if (!string.IsNullOrWhiteSpace(homePage.DescriptionEn))
                    homePage.Description = await TranslateAsync(homePage.DescriptionEn, "en", "tr");
                
                if (!string.IsNullOrWhiteSpace(homePage.ButtonTextEn))
                    homePage.ButtonText = await TranslateAsync(homePage.ButtonTextEn, "en", "tr");
            }
            
            return homePage;
        }

        public async Task<About> TranslateAboutAsync(About about, bool translateToEnglish = true)
        {
            if (translateToEnglish)
            {
                if (!string.IsNullOrWhiteSpace(about.Title))
                    about.TitleEn = await TranslateAsync(about.Title);
                
                if (!string.IsNullOrWhiteSpace(about.Subtitle))
                    about.SubtitleEn = await TranslateAsync(about.Subtitle);
                
                if (!string.IsNullOrWhiteSpace(about.Description))
                    about.DescriptionEn = await TranslateAsync(about.Description);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(about.TitleEn))
                    about.Title = await TranslateAsync(about.TitleEn, "en", "tr");
                
                if (!string.IsNullOrWhiteSpace(about.SubtitleEn))
                    about.Subtitle = await TranslateAsync(about.SubtitleEn, "en", "tr");
                
                if (!string.IsNullOrWhiteSpace(about.DescriptionEn))
                    about.Description = await TranslateAsync(about.DescriptionEn, "en", "tr");
            }
            
            return about;
        }

        public async Task<Dictionary<string, string>> TranslateBatchAsync(Dictionary<string, string> texts, string fromLanguage = "tr", string toLanguage = "en")
        {
            var results = new Dictionary<string, string>();
            
            foreach (var kvp in texts)
            {
                var translated = await TranslateAsync(kvp.Value, fromLanguage, toLanguage);
                results[kvp.Key] = translated;
                
                // API rate limiting için kısa bekleme
                await Task.Delay(100);
            }
            
            return results;
        }

        public async Task TranslateRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            
            foreach (var room in rooms)
            {
                if (!string.IsNullOrWhiteSpace(room.Name) && string.IsNullOrWhiteSpace(room.NameEn))
                {
                    room.NameEn = await TranslateAsync(room.Name);
                }
                
                if (!string.IsNullOrWhiteSpace(room.Description) && string.IsNullOrWhiteSpace(room.DescriptionEn))
                {
                    room.DescriptionEn = await TranslateAsync(room.Description);
                }
                
                await _roomRepository.UpdateAsync(room);
                await Task.Delay(200); // Rate limiting
            }
        }

        public async Task TranslateMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            
            foreach (var item in menuItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Name) && string.IsNullOrWhiteSpace(item.NameEn))
                {
                    item.NameEn = await TranslateAsync(item.Name);
                }
                
                if (!string.IsNullOrWhiteSpace(item.Description) && string.IsNullOrWhiteSpace(item.DescriptionEn))
                {
                    item.DescriptionEn = await TranslateAsync(item.Description);
                }
                
                await _menuItemRepository.UpdateAsync(item);
                await Task.Delay(200); // Rate limiting
            }
        }

        public async Task TranslateBlogPostsAsync()
        {
            var posts = await _blogPostRepository.GetAllAsync();
            
            foreach (var post in posts)
            {
                if (!string.IsNullOrWhiteSpace(post.Title) && string.IsNullOrWhiteSpace(post.TitleEn))
                {
                    post.TitleEn = await TranslateAsync(post.Title);
                }
                
                if (!string.IsNullOrWhiteSpace(post.Content) && string.IsNullOrWhiteSpace(post.ContentEn))
                {
                    post.ContentEn = await TranslateAsync(post.Content);
                }
                
                await _blogPostRepository.UpdateAsync(post);
                await Task.Delay(200); // Rate limiting
            }
        }

        public async Task TranslateAllContentAsync()
        {
            await TranslateRoomsAsync();
            await TranslateMenuItemsAsync();
            await TranslateBlogPostsAsync();
        }
    }

    // MyMemory API response model
    public class MyMemoryResponse
    {
        public ResponseData? responseData { get; set; }
    }

    public class ResponseData
    {
        public string? translatedText { get; set; }
    }
}
