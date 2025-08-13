# 🔧 Harbor Restaurant - Detaylı Kurulum Kılavuzu

## 📋 Başlamadan Önce

Bu kılavuz, Harbor Restaurant projesini sıfırdan kurmak isteyen öğrenciler için hazırlanmıştır. Her adım detaylı şekilde anlatılmıştır.

## 💻 Sistem Gereksinimleri

### Minimum Gereksinimler
- **İşletim Sistemi**: Windows 10/11 (64-bit)
- **RAM**: 8 GB (16 GB önerilen)
- **Disk Alanı**: 5 GB boş alan
- **İnternet**: İndirmeler için gerekli

### Gerekli Yazılımlar
1. **.NET 9.0 SDK**
2. **Visual Studio 2022** veya **VS Code**
3. **SQL Server Express LocalDB**
4. **Git** (opsiyonel)

---

## 🚀 Adım Adım Kurulum

### ADIM 1: .NET 9.0 SDK Kurulumu

#### 1.1 İndirme
1. [Microsoft .NET İndirme Sayfası](https://dotnet.microsoft.com/download)na gidin
2. **.NET 9.0 SDK** bölümünü bulun
3. **Windows x64** versiyonunu indirin

#### 1.2 Kurulum
1. İndirilen dosyayı çift tıklayın
2. Kurulum sihirbazını takip edin
3. "Add to PATH" seçeneğini işaretleyin
4. Kurulumu tamamlayın

#### 1.3 Doğrulama
PowerShell'i açın ve şu komutu çalıştırın:
```powershell
dotnet --version
```
Çıktı: `9.0.xxx` şeklinde olmalı

---

### ADIM 2: Visual Studio 2022 Kurulumu

#### 2.1 İndirme
1. [Visual Studio İndirme Sayfası](https://visualstudio.microsoft.com/downloads/)na gidin
2. **Visual Studio Community 2022** (ücretsiz) indirin

#### 2.2 Kurulum ve Workload Seçimi
1. Kurulum dosyasını çalıştırın
2. **Visual Studio Installer** açılacak
3. Şu workload'ları seçin:
   - ✅ **ASP.NET and web development**
   - ✅ **.NET desktop development**
   - ✅ **Data storage and processing**

#### 2.3 Ek Bileşenler
**Individual Components** sekmesinde:
- ✅ **SQL Server Express 2019 LocalDB**
- ✅ **Entity Framework 6 tools**
- ✅ **NuGet package manager**

---

### ADIM 3: Proje Dosyalarını Hazırlama

#### 3.1 Klasör Oluşturma
1. `C:\` driv'ında `Projects` klasörü oluşturun
2. İçinde `HarborRestaurant` klasörü oluşturun

#### 3.2 Dosyaları Kopyalama
Proje dosyalarını `C:\Projects\HarborRestaurant\` klasörüne kopyalayın

---

### ADIM 4: Proje Açma ve NuGet Paketleri

#### 4.1 Visual Studio'da Açma
1. Visual Studio 2022'yi açın
2. **File → Open → Project/Solution**
3. `HarborRestaurant.sln` dosyasını seçin

#### 4.2 NuGet Paketlerini Geri Yükleme
1. **Solution Explorer**'da projeye sağ tık
2. **Restore NuGet Packages** seçin
3. Veya **Package Manager Console**'da:
```powershell
dotnet restore
```

#### 4.3 Paket Listesi Kontrolü
Şu paketlerin yüklü olduğunu kontrol edin:
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- EPPlus
- iTextSharp
- QRCoder
- Microsoft.AspNetCore.SignalR

---

### ADIM 5: Veritabanı Kurulumu

#### 5.1 Connection String Kontrolü
`appsettings.json` dosyasını açın:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HarborRestaurant;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### 5.2 Migration Çalıştırma
**Package Manager Console**'da:
```powershell
dotnet ef database update
```

#### 5.3 Veritabanı Kontrolü
1. **View → SQL Server Object Explorer**
2. **(localdb)\mssqllocaldb** bağlantısını genişletin
3. **Databases → HarborRestaurant** olmalı

---

### ADIM 6: İlk Çalıştırma

#### 6.1 Projeyi Build Etme
```powershell
dotnet build
```

#### 6.2 Projeyi Çalıştırma
```powershell
dotnet run
```

#### 6.3 Tarayıcıda Açma
- Ana sayfa: `http://localhost:5154`
- Admin panel: `http://localhost:5154/Admin`

---

## 🔑 İlk Admin Hesabı Oluşturma

### Yöntem 1: Otomatik Kayıt
1. `http://localhost:5154/Account/Register` adresine gidin
2. Formu doldurun:
   - **Ad**: John
   - **Soyad**: Doe
   - **E-posta**: admin@test.com
   - **Şifre**: Admin123!
3. **Kayıt Ol** butonuna tıklayın
4. Otomatik olarak Admin rolü atanır

### Yöntem 2: Varsayılan Admin
1. `http://localhost:5154/Account/CreateAdmin` adresine gidin
2. Varsayılan admin oluşturulur:
   - **E-posta**: admin@harborrestaurant.com
   - **Şifre**: Admin123!

---

## 🧪 Test ve Doğrulama

### Temel Özellik Testleri

#### 1. Anasayfa Testi
- ✅ Sayfa yükleniyor mu?
- ✅ Hero section görünüyor mu?
- ✅ Menü öğeleri listeleniyor mu?

#### 2. Admin Panel Testi
- ✅ Giriş yapabiliyor musunuz?
- ✅ Dashboard açılıyor mu?
- ✅ Menü linkleri çalışıyor mu?

#### 3. Veritabanı Testi
- ✅ Kullanıcı oluşturabiliyor musunuz?
- ✅ Veriler kaydediliyor mu?
- ✅ İlişkiler çalışıyor mu?

---

## ❌ Yaygın Hatalar ve Çözümleri

### HATA 1: "dotnet command not found"
**Neden**: .NET SDK yüklü değil veya PATH'e eklenmemiş  
**Çözüm**:
1. .NET SDK'yı yeniden kurun
2. Sistem yeniden başlatın
3. PowerShell'i yönetici olarak açın

### HATA 2: "Cannot connect to LocalDB"
**Neden**: SQL Server Express LocalDB yüklü değil  
**Çözüm**:
1. Visual Studio Installer'ı açın
2. **Modify** → **Individual Components**
3. **SQL Server Express 2019 LocalDB** seçin
4. Install edin

### HATA 3: "Package restore failed"
**Neden**: İnternet bağlantısı veya NuGet sorunu  
**Çözüm**:
```powershell
dotnet nuget locals all --clear
dotnet restore
```

### HATA 4: "Migration already applied"
**Neden**: Veritabanı zaten güncel  
**Çözüm**:
```powershell
dotnet ef database drop
dotnet ef database update
```

### HATA 5: "Access Denied" Admin Panelinde
**Neden**: Kullanıcının Admin rolü yok  
**Çözüm**:
1. Yeni kullanıcı kaydı yapın (otomatik admin)
2. Veya mevcut kullanıcıya manuel rol verin

---

## 🔧 Geliştirme Ortamı Ayarları

### Visual Studio Extensions (Önerilen)
1. **Web Essentials**
2. **Productivity Power Tools**
3. **CodeMaid**
4. **SonarLint**

### VS Code Extensions (Alternatif)
1. **C# for Visual Studio Code**
2. **ASP.NET Core Snippets**
3. **Auto Rename Tag**
4. **Bracket Pair Colorizer**

---

## 📊 Proje Yapısı Detayları

### Dosya ve Klasör Açıklamaları

```
HarborRestaurant/
├── 📁 Areas/Admin/              # Admin paneli
│   ├── Controllers/             # Admin controller'ları
│   │   ├── DashboardController.cs    # Ana dashboard
│   │   ├── UsersController.cs        # Kullanıcı yönetimi
│   │   ├── MenuItemsController.cs    # Menü yönetimi
│   │   └── ...
│   └── Views/                   # Admin view'ları
│       ├── Dashboard/           # Dashboard sayfaları
│       ├── Users/              # Kullanıcı sayfaları
│       └── Shared/             # Ortak layout'lar
├── 📁 Business/                 # İş mantığı katmanı
│   ├── Abstract/               # Servis interface'leri
│   │   ├── IMenuService.cs     # Menü servisi interface
│   │   ├── IUserService.cs     # Kullanıcı servisi interface
│   │   └── ...
│   └── Concrete/               # Servis implementasyonları
│       ├── MenuService.cs      # Menü işlemleri
│       ├── UserService.cs      # Kullanıcı işlemleri
│       └── ...
├── 📁 DataAccess/              # Veri erişim katmanı
│   ├── Abstract/               # Repository interface'leri
│   ├── Concrete/               # Repository implementasyonları
│   └── Context/                # Veritabanı context
├── 📁 Entities/                # Varlık modelleri
│   ├── Concrete/               # Entity sınıfları
│   │   ├── MenuItem.cs         # Menü öğesi
│   │   ├── User.cs            # Kullanıcı
│   │   └── ...
│   └── Enums/                  # Enum'lar
├── 📁 Controllers/             # Ana controller'lar
├── 📁 Views/                   # Ana view'lar
├── 📁 wwwroot/                 # Statik dosyalar
│   ├── css/                   # CSS dosyaları
│   ├── js/                    # JavaScript dosyaları
│   ├── images/                # Resim dosyaları
│   └── lib/                   # Kütüphaneler
└── 📁 Resources/               # Çoklu dil kaynakları
```

---

## 🎯 İleri Seviye Konfigürasyonlar

### SMTP E-posta Ayarları
`appsettings.json`'a ekleyin:
```json
{
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "DisplayName": "Harbor Restaurant"
  }
}
```

### SignalR Hub Ayarları
```csharp
// Program.cs'de
builder.Services.AddSignalR();
app.MapHub<NotificationHub>("/notificationHub");
```

### Dosya Yükleme Ayarları
```json
{
  "FileUploadSettings": {
    "MaxFileSize": 5242880,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".gif"],
    "UploadPath": "wwwroot/uploads/"
  }
}
```

---

## 📱 Mobil Test

### Responsive Test
1. **Chrome DevTools**: F12 → Device Toggle
2. **Different Screen Sizes**: Phone, Tablet, Desktop
3. **Touch Events**: Gesture simulation

### Performance Test
1. **Lighthouse**: Chrome → F12 → Lighthouse
2. **Network Speed**: Slow 3G simulation
3. **Accessibility**: WCAG guidelines

---

## 🔄 Backup ve Yedekleme

### Veritabanı Yedekleme
```sql
BACKUP DATABASE HarborRestaurant 
TO DISK = 'C:\Backup\HarborRestaurant.bak'
```

### Proje Dosyalarını Yedekleme
```powershell
# PowerShell ile
Compress-Archive -Path "C:\Projects\HarborRestaurant" -DestinationPath "C:\Backup\HarborRestaurant_$(Get-Date -Format 'yyyyMMdd').zip"
```

---

## 📞 Destek ve Yardım

### Öğrenme Kaynakları
1. **Microsoft Docs**: https://docs.microsoft.com/aspnet/core
2. **Entity Framework Docs**: https://docs.microsoft.com/ef/core
3. **Bootstrap Docs**: https://getbootstrap.com/docs

### Video Eğitimler
1. **ASP.NET Core MVC**: YouTube'da arayın
2. **Entity Framework Core**: Microsoft Learn
3. **Bootstrap**: Bootstrap resmi kanalı

### Forum ve Topluluklar
1. **Stack Overflow**: Hata çözümleri
2. **GitHub Issues**: Açık kaynak projeleri
3. **Reddit r/dotnet**: Topluluk desteği

---

## ✅ Teslim Öncesi Kontrol Listesi

### Fonksiyonel Testler
- [ ] Anasayfa açılıyor
- [ ] Kullanıcı kaydı çalışıyor
- [ ] Admin panel erişilebilir
- [ ] Menü CRUD işlemleri
- [ ] Rezervasyon sistemi
- [ ] Blog yönetimi
- [ ] Dosya yükleme
- [ ] Raporlama özellikleri

### Kod Kalitesi
- [ ] Build hatasız
- [ ] Warning'ler minimum
- [ ] Yeterli yorum satırı
- [ ] Clean code principles
- [ ] Exception handling

### Dokümantasyon
- [ ] README.md güncel
- [ ] Kod yorumları eksiksiz
- [ ] API dokümantasyonu
- [ ] Kullanıcı kılavuzu

---

**🎓 Bu kılavuz size başarılı bir proje teslimi için gerekli tüm adımları sağlar. Herhangi bir sorunda bu dokümana geri dönebilirsiniz.**

---
*Son güncelleme: Ağustos 2025*
