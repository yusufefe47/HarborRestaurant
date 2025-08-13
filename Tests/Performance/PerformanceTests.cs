using System.Diagnostics;
using System.Net.Http;

namespace HarborRestaurant.Tests.Performance
{
    /// <summary>
    /// Manuel performans testleri için yardımcı sınıf
    /// dotnet run komutuyla proje çalışırken bu testleri manuel olarak çalıştırabilirsiniz
    /// </summary>
    public static class PerformanceTestHelper
    {
        public static async Task<(long firstRequest, long secondRequest, double improvement)> TestPagePerformance(string url)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5154"); // Development URL
            
            var stopwatch = Stopwatch.StartNew();
            
            // İlk istek - cache doldurmak için
            var response1 = await client.GetAsync(url);
            stopwatch.Stop();
            var firstRequestTime = stopwatch.ElapsedMilliseconds;
            
            // Kısa bekleme
            await Task.Delay(100);
            
            // İkinci istek - cache'den gelecek
            stopwatch.Restart();
            var response2 = await client.GetAsync(url);
            stopwatch.Stop();
            var secondRequestTime = stopwatch.ElapsedMilliseconds;
            
            var improvement = firstRequestTime > 0 ? 
                ((double)(firstRequestTime - secondRequestTime) / firstRequestTime * 100) : 0;
            
            Console.WriteLine($"URL: {url}");
            Console.WriteLine($"First request: {firstRequestTime}ms");
            Console.WriteLine($"Second request: {secondRequestTime}ms");
            Console.WriteLine($"Cache improvement: {improvement:F1}%");
            Console.WriteLine($"Response status: {response1.StatusCode} / {response2.StatusCode}");
            Console.WriteLine("---");
            
            return (firstRequestTime, secondRequestTime, improvement);
        }
        
        public static async Task RunAllPerformanceTests()
        {
            Console.WriteLine("🚀 Harborlights Performance Tests Starting...\n");
            
            var urls = new[] { "/", "/Menu", "/Rooms", "/About" };
            var totalImprovement = 0.0;
            var testCount = 0;
            
            foreach (var url in urls)
            {
                try
                {
                    var (first, second, improvement) = await TestPagePerformance(url);
                    totalImprovement += improvement;
                    testCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error testing {url}: {ex.Message}");
                }
                
                await Task.Delay(500); // Test'ler arası bekleme
            }
            
            if (testCount > 0)
            {
                var avgImprovement = totalImprovement / testCount;
                Console.WriteLine($"📊 Average Cache Improvement: {avgImprovement:F1}%");
                Console.WriteLine($"✅ Tested {testCount} pages successfully");
            }
            
            Console.WriteLine("\n🎯 Performance test completed!");
        }
        
        public static async Task TestCompressionSupport()
        {
            Console.WriteLine("🗜️ Testing Compression Support...\n");
            
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5154");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            
            var response = await client.GetAsync("/");
            var contentEncoding = response.Content.Headers.ContentEncoding;
            var contentLength = response.Content.Headers.ContentLength;
            
            Console.WriteLine($"Content Encoding: {string.Join(", ", contentEncoding)}");
            Console.WriteLine($"Content Length: {contentLength} bytes");
            Console.WriteLine($"Response Status: {response.StatusCode}");
            
            if (contentEncoding.Any())
            {
                Console.WriteLine("✅ Compression is working!");
            }
            else
            {
                Console.WriteLine("⚠️ No compression detected");
            }
        }
    }
}
