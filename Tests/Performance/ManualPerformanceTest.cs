using HarborRestaurant.Tests.Performance;

namespace HarborRestaurant.Tests.Performance
{
    /// <summary>
    /// Manuel performans testi sınıfı
    /// Projeyi çalıştırdıktan sonra bu metotları çağırarak performansı test edebilirsiniz
    /// </summary>
    public static class ManualPerformanceTest
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("🚀 Harborlights Hotel Management System");
            Console.WriteLine("📊 Performance Testing Tool");
            Console.WriteLine("=====================================\n");

            Console.WriteLine("Bu test aracını kullanmak için:");
            Console.WriteLine("1. dotnet run ile projeyi başlatın (başka terminal'de)");
            Console.WriteLine("2. Proje çalışmaya başladıktan sonra bu test'i çalıştırın\n");

            Console.WriteLine("Test başlatılıyor...\n");

            try
            {
                // Ana performans testleri
                await PerformanceTestHelper.RunAllPerformanceTests();
                
                // Compression testi
                await PerformanceTestHelper.TestCompressionSupport();
                
                Console.WriteLine("\n🎉 Tüm testler tamamlandı!");
                Console.WriteLine("\nBu sonuçlar cache ve compression optimizasyonlarının");
                Console.WriteLine("ne kadar etkili olduğunu göstermektedir.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Bağlantı hatası: {ex.Message}");
                Console.WriteLine("\nLütfen önce projeyi başlattığınızdan emin olun:");
                Console.WriteLine("dotnet run");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test hatası: {ex.Message}");
            }

            Console.WriteLine("\nDevam etmek için herhangi bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
