# 🏨 Harborlights - Dinamik Otel Yönetim Sistemi

## 📋 Proje Hakkında

Harborlights, modern web teknolojileri kullanılarak geliştirilmiş tam özellikli bir otel yönetim sistemidir. Bu proje, ASP.NET Core MVC ile geliştirilmiş olup, otellerin ihtiyaç duyduğu oda/rezervasyon yönetimi, içerik yönetimi, blog ve çoklu dil desteğini içerir.

### 🎯 Proje Amacı
- Oteller için kapsamlı bir yönetim sistemi geliştirmek
- Modern web teknolojilerini öğrenmek ve uygulamak
- Katmanlı mimari ve tasarım desenlerini kullanmak
- Gerçek dünya problemlerine teknolojik çözümler üretmek

## 🛠️ Kullanılan Teknolojiler

### Backend Technologies
- **ASP.NET Core MVC 9.0** - Ana framework
- **Entity Framework Core** - ORM (Object-Relational Mapping)
- **ASP.NET Identity** - Kimlik doğrulama ve yetkilendirme
- **SQL Server Express** - Veritabanı
- **SignalR** - Gerçek zamanlı iletişim
- **Localization (.resx)** - Türkçe/İngilizce çoklu dil

### Frontend Technologies
- **HTML5/CSS3** - Temel yapı
- **Bootstrap 5** - Responsive tasarım
- **JavaScript/jQuery** - İnteraktif özellikler
- **AdminLTE** - Admin panel tema
- **Chart.js** - Grafik ve istatistikler
- **DataTables** - Gelişmiş tablo özellikleri
- **SweetAlert2** - Modern alert mesajları
- **TinyMCE** - Zengin metin editörü

### Ek Kütüphaneler
- **iTextSharp** - PDF rapor üretimi
- **EPPlus** - Excel rapor üretimi
- **QRCoder** - QR kod üretimi
- **Font Awesome** - İkon kütüphanesi
- **MyMemory API** - Ücretsiz otomatik çeviri (opsiyonel)

## 🏗️ Proje Mimarisi

### Katmanlı Mimari Yapısı

```
HarborRestaurant/
├── 📁 Areas/
│   └── Admin/              # Admin paneli area'sı
│       ├── Controllers/    # Admin controller'ları
│       └── Views/         # Admin view'ları
├── 📁 Business/           # İş mantığı katmanı
│   ├── Abstract/          # Interface'ler
│   └── Concrete/          # Implementasyonlar
├── 📁 DataAccess/         # Veri erişim katmanı
│   ├── Abstract/          # Repository interface'leri
│   ├── Concrete/          # Repository implementasyonları
│   └── Context/           # Veritabanı context
├── 📁 Entities/           # Varlık modelleri
│   ├── Concrete/          # Entity sınıfları
│   └── Enums/            # Enum'lar
├── 📁 Controllers/        # MVC Controller'lar
├── 📁 Views/              # Razor View'lar
├── 📁 Models/             # ViewModel'ler
├── 📁 wwwroot/            # Statik dosyalar
├── 📁 Resources/          # Çoklu dil kaynakları
└── 📁 Migrations/         # EF Migration'ları
```

### Tasarım Desenleri
- **Repository Pattern** - Veri erişim soyutlaması
- **Unit of Work** - Transaction yönetimi
- **Dependency Injection** - Bağımlılık enjeksiyonu
- **MVC Pattern** - Model-View-Controller

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- ✅ Windows 10/11
- ✅ .NET 9.0 SDK
- ✅ Visual Studio 2022 veya VS Code
- ✅ SQL Server Express LocalDB

### Adım 1: Projeyi İndirin
```bash
git clone [repo-url]
cd HarborRestaurant
```

### Adım 2: NuGet Paketlerini Yükleyin
```bash
dotnet restore
```

### Adım 3: Veritabanını Oluşturun
```bash
dotnet ef database update
```

### Adım 4: Projeyi Çalıştırın
```bash
dotnet run
```

## 🚀 Performans Optimizasyonları

**Harborlights** enterprise-grade performans için optimize edilmiştir:

### ⚡ Hız Özellikleri:
- **Smart Caching**: Memory cache, response cache, output cache
- **Compression**: Brotli & Gzip ile %70 daha küçük dosyalar
- **HTTP/2-3**: Modern protokol desteği
- **Database Optimization**: Connection pooling, compiled queries
- **Frontend Optimization**: Lazy loading, async CSS/JS, critical CSS
- **CDN Ready**: Statik dosyalar için 1 yıl browser cache

### 📊 Performans Metrikleri:
- **İlk ziyaret**: Normal hız
- **Sonraki ziyaretler**: %50-80 daha hızlı
- **Sayfa boyutu**: %60-70 küçülme
- **Database yükü**: %30-50 azalma

Detaylı performans raporu için: `PERFORMANCE_REPORT.md`

### Adım 5: Tarayıcıda Açın
- Development: `http://localhost:5154` (HTTPS yönlendirme Development'ta kapalı)
- Production: HTTPS yönlendirme otomatik aktif
- Admin Panel: `http://localhost:5154/Admin`

### Dil Seçimi (TR/EN)
- Varsayılan dil: Türkçe (tr)
- Desteklenen diller: Türkçe (tr), İngilizce (en)
- URL ile geçici değişim: `?culture=en` veya `?culture=tr`
- Cookie tabanlı tercih de desteklenir (AspNetCore kültür cookie)

## 👤 Kullanıcı Yönetimi

### İlk Admin Hesabı Oluşturma

**Yöntem 1: Yeni Kayıt**
1. `http://localhost:5154/Account/Register` adresine gidin
2. Formu doldurun
3. Otomatik olarak Admin rolü atanır

**Yöntem 2: Mevcut Kullanıcıyı Admin Yap**
1. Bir kullanıcı ile giriş yapın
2. `http://localhost:5154/Admin/Setup/MakeAdmin` adresine gidin
3. Kullanıcıya Admin rolü atanır (rol yoksa otomatik oluşturulur)

## 📊 Özellikler ve Modüller

### 🏠 Frontend (Müşteri Paneli)
- **Anasayfa**: Hero section, özel yemekler, son blog yazıları
- **Hakkımızda**: Mekan bilgileri ve hikayesi
- **Menü**: Kategorili yemek listesi, filtreleme
- **Rezervasyon**: Online masa rezervasyonu
- **Odalar**: Özel etkinlik alanları
- **Blog**: Mekan haberleri ve yazıları
- **İletişim**: İletişim formu ve harita

### 🔧 Backend (Admin Paneli)
- **Dashboard**: İstatistikler, grafikler, özet bilgiler
- **Kullanıcı Yönetimi**: CRUD işlemleri, rol yönetimi
- **Menü Yönetimi**: Yemek ve kategori yönetimi
- **Rezervasyon Yönetimi**: Onay, red, düzenleme
- **Masa Yönetimi**: Masa kapasitesi, durum yönetimi
- **Blog Yönetimi**: Yazı ve kategori yönetimi
- **İletişim Mesajları**: Müşteri mesajlarını görüntüleme
- **Sayfa Yönetimi**: Dinamik sayfa içerikleri

Not: Backend’i olmayan eski “Ayarlar/Settings” sayfaları kaldırılmıştır.

### 🎯 Ekstra Özellikler (Bonus Puanlar)
- **SignalR**: Gerçek zamanlı bildirimler
- **Dosya Yükleme**: Resim ve döküman yönetimi
- **PDF Raporlama**: Rezervasyon, menü raporları
- **Excel Raporlama**: Veri dışa aktarımı
- **QR Kod**: Menü için QR kod üretimi
- **Çoklu Dil**: Türkçe/İngilizce dil desteği
- **E-posta**: SMTP ile otomatik e-posta gönderimi

### Çoklu Dil ve Otomatik Çeviri
- Kaynaklar `Resources/SharedResource.tr.resx` ve `Resources/SharedResource.en.resx` dosyalarındadır.
- Varsayılan kültür Türkçe’dir; URL parametresi ve cookie ile değiştirilebilir.
- Admin panelde içerik girerken İngilizce alanlar desteklenir.
- Otomatik çeviri için MyMemory (ücretsiz) entegrasyonu mevcuttur (ek API anahtarı gerektirmez; ücretsiz limitlere tabidir).

## 📝 Veritabanı Yapısı

### Ana Tablolar
- **AspNetUsers**: Kullanıcı bilgileri
- **MenuCategories**: Menü kategorileri
- **MenuItems**: Yemek öğeleri
- **Reservations**: Rezervasyonlar
- **Tables**: Masa bilgileri
- **BlogCategories**: Blog kategorileri
- **BlogPosts**: Blog yazıları
- **ContactMessages**: İletişim mesajları
- **HomePages**: Anasayfa içerikleri
- **Abouts**: Hakkımızda sayfa içeriği
- **Rooms**: Özel alanlar

### İlişkiler
- Kullanıcılar ↔ Roller (Çoka-Çok)
- Menü Kategorileri ↔ Menü Öğeleri (Bir-Çok)
- Masalar ↔ Rezervasyonlar (Bir-Çok)
- Blog Kategorileri ↔ Blog Yazıları (Bir-Çok)

## 🔐 Güvenlik Özellikleri

- **Authentication**: ASP.NET Identity
- **Authorization**: Role-based yetkilendirme
- **CSRF Protection**: Anti-forgery token
- **SQL Injection**: Entity Framework parameterized queries
- **XSS Protection**: Razor view encoding

## 📱 Responsive Tasarım

- **Mobile First**: Mobil cihazlar öncelikli
- **Bootstrap Grid**: Flexible layout sistemi
- **Cross-browser**: Tüm modern tarayıcılar
- **Touch Friendly**: Dokunmatik ekran uyumlu

## 🧪 Test ve Debug

### Test Verileri
Proje ilk çalıştırıldığında örnek veriler eklenebilir ve bazı alanlar İngilizce olarak doldurulur (seed mantığı Program.cs içinde korunur):
- Menü kategorileri ve yemekleri
- Blog kategorileri ve yazıları
- Örnek masalar ve odalar

### Debug Modları
- **Development**: Geliştirme ortamı
- **Production**: Canlı ortam
- **Staging**: Test ortamı

## 📈 Performans Optimizasyonları

- **Async/Await**: Asenkron programlama
- **Lazy Loading**: İhtiyaç anında yükleme
- **Caching**: Veri önbellekleme
- **Compression**: Dosya sıkıştırma
- **Minification**: CSS/JS küçültme

## 🔄 CI/CD ve Deployment

### Deployment Seçenekleri
1. **IIS**: Windows sunucu
2. **Azure App Service**: Cloud hosting
3. **Docker**: Container deployment

### Environment Ayarları
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HarborRestaurant;Trusted_Connection=true"
  }
}
```

## 🗂️ Dosya Yükleme Politikaları
- Merkezi servis: `FileService`
- Desteklenen uzantılar: .jpg, .jpeg, .png, .gif, .webp
- Maksimum boyut: 5 MB
- Kayıt yeri: `wwwroot/images/{folder}` (ör. blog, uploads)
- Silme işlemleri `DeleteImageAsync` ile yapılır

## 📚 Kullanım Kılavuzu

### Yönetici Olarak İlk Adımlar

1. **Giriş Yapın**
   - Admin paneline erişin
   - Dashboard'u inceleyin

2. **Temel Verileri Ekleyin**
   - Menü kategorileri oluşturun
   - Yemekleri ekleyin
   - Masa bilgilerini girin

3. **İçerikleri Düzenleyin**
   - Anasayfa içeriğini güncelleyin
   - Hakkımızda sayfasını düzenleyin
   - İletişim bilgilerini ekleyin
 - Çoklu dil alanlarını (EN) doldurun; müşteri-facing sayfalar dil seçimine göre doğru alanı gösterir

4. **Blog Yazıları**
   - Kategoriler oluşturun
   - İlk blog yazınızı ekleyin

### Müşteri Deneyimi

1. **Anasayfa**: İlk izlenim
2. **Menü İnceleme**: Yemek seçimi
3. **Rezervasyon**: Masa ayırtma
4. **İletişim**: Geri bildirim

## 🐛 Bilinen Sorunlar ve Çözümler

### Yaygın Hatalar

**1. Veritabanı Bağlantı Hatası**
```bash
# Çözüm:
dotnet ef database update
```

**2. Admin Erişim Sorunu**
```bash
# Çözüm 1: Yeni kullanıcı kaydı yapın (otomatik admin rolü)
# Çözüm 2: Giriş yapıp /Admin/Setup/MakeAdmin adresine gidin
```

**3. NuGet Paket Hatası**
```bash
# Çözüm:
dotnet clean
dotnet restore
dotnet build
```

**4. Otomatik Çeviri Limitleri**
```text
MyMemory ücretsiz servistir ve hız/limit kısıtları olabilir. Yoğun kullanımda bekleme ya da manuel çeviri tercih edin.
```

## 📞 Destek ve İletişim

### Geliştirici Notları
- Kod içerisinde yeterli yorum satırı eklenmiştir
- Her modül ayrı ayrı test edilmiştir
- Best practice'lere uygun geliştirilmiştir

### Gelecek Güncellemeler
- [ ] Mobil uygulama API'si
- [ ] Sipariş takip sistemi
- [ ] Online ödeme entegrasyonu
- [ ] Sosyal medya entegrasyonu

## 🏆 Proje Başarıları

### Teknik Başarılar
- ✅ Katmanlı mimari implementasyonu
- ✅ Design pattern kullanımı
- ✅ Modern web teknolojileri
- ✅ Responsive tasarım
- ✅ Güvenlik best practice'leri

### İş Değeri
- ✅ Gerçek dünya problemi çözümü
- ✅ Kullanıcı dostu arayüz
- ✅ Ölçeklenebilir yapı
- ✅ Maintainable kod

## 📋 Lisans ve Kullanım

Bu proje eğitim amaçlı geliştirilmiştir. Ticari kullanım için gerekli lisanslar alınmalıdır.

---

**Geliştirici**: [Öğrenci Adı]  
**Proje Tarihi**: Ağustos 2025  
**Versiyon**: 1.0.0  
**Framework**: ASP.NET Core MVC 9.0

---

*Bu README dosyası projenin tüm özelliklerini, kurulum adımlarını ve kullanım kılavuzunu içermektedir. Herhangi bir sorun yaşadığınızda bu dokümana başvurabilirsiniz.*
