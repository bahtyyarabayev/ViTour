# 🌍 ViTour - Modern Tur Rezervasyon & Yönetim Platformu

ViTour, kullanıcıların hayallerindeki rotaları keşfetmelerini sağlayan, **ASP.NET Core** ve **MongoDB** tabanlı, modern UI prensipleriyle geliştirilmiş kapsamlı bir turizm portalıdır.

---

## 🚀 Proje Öne Çıkan Özellikleri

### 🧭 Tur Yönetimi (Archive Tour)
* 📥 **Dinamik Veri:** MongoDB üzerinden anlık çekilen tur bilgileri.
* 📄 **Akıllı Pagination:** Her sayfada kendinize uygun şekilde optimize edilmiş sayfalama.
* 🔍 **Gelişmiş Filtreleme:** Kategori ve lokasyona göre hızlı arama seçenekleri.

### 🗺️ Dinamik Tur Detayları
Tur detay sayfasında kullanıcı deneyimini artıran 5 farklı dinamik sekme bulunur:
* 📌 **Information:** Kapasite, gün sayısı ve fiyat gibi temel bilgiler.
* 📅 **Tour Planning:** MongoDB `TourPlanning` koleksiyonundan çekilen günlük programlar.
* 📍 **Location Share:** Google Maps yerine Pixar tarzı harita görselleri.
* ⭐ **Reviews:** 5 yıldızlı değerlendirme sistemi ve otomatik ortalama puan hesaplama.
* 🖼️ **Gallery:** `TourImages` koleksiyonundan çekilen dinamik tur görselleri.

### 🧾 Rezervasyon Sistemi
Sıfırdan tasarlanan rezervasyon modülü şu yeteneklere sahiptir:
* 🎨 **Modern UI:** Kullanıcıyı yormayan, temiz ve şık arayüz.
* 🧮 **Otomatik Hesaplama:** Kişi sayılarına ve yaş kategorilerine göre anlık fiyat hesaplama.
* 📉 **Kontenjan Kontrolü:** Tur kapasitesine göre dinamik yer takibi.
* 💾 **MongoDB Entegrasyonu:** Tüm kayıtların asenkron olarak NoSQL veritabanına işlenmesi.

### 🧑‍💻 Gelişmiş Admin Paneli
Tamamen **custom** geliştirilen panel ile tüm sistem parmaklarınızın ucunda:
* 🛠️ **Modüler Yönetim:** Tours, Categories, Location, Reviews ve Reservations modülleri.
* 📝 **Tam CRUD:** Tüm veriler üzerinde Ekleme/Silme/Güncelleme yetkisi.
* 📊 **Raporlama:** Rezervasyon verilerini Excel veya PDF olarak dışa aktarma (Export).
* 🇹🇷 **Yerelleştirme:** Tamamen Türkçeleştirilmiş yönetim arayüzü.

---
## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
| :--- | :--- |
| **🚀 ASP.NET Core MVC** | Projenin temel Backend Framework yapısını oluşturur. |
| **🍃 MongoDB** | Verilerin esnek ve performanslı saklandığı NoSQL veritabanı. |
| **🔌 MongoDB.Driver** | C# ile MongoDB arasında asenkron veri iletişimini sağlar. |
| **🎨 Tailwind CSS** | Modern, responsive ve hızlı UI tasarımı için kullanılmıştır. |
| **⚡ JavaScript** | Rezervasyon fiyat hesaplama ve dinamik UI etkileşimleri. |
| **📄 Razor View Engine** | Sunucu taraflı HTML oluşturma (templating) motoru. |
| **🌍 Localization** | Farklı dil (TR/EN) desteği için entegre altyapı. |
| **🔄 AutoMapper** | Entity ve DTO nesneleri arasındaki otomatik dönüşüm. |

---
### 📧 Mail Bildirim Sistemi (SMTP Integration)
Rezervasyon süreci, kullanıcıyı anlık bilgilendiren bir mail mekanizması ile desteklenmiştir:
* **Anlık Onay Maili:** Rezervasyon MongoDB'ye kaydedildiği an kullanıcıya otomatik "Talebiniz Alındı" maili gönderilir.
* **HTML Mail Şablonu:** Şık ve kurumsal tasarıma sahip, CSS ile zenginleştirilmiş mail içeriği.
* **SMTP Altyapısı:** Güvenli portlar üzerinden (SSL/TLS) yüksek teslimat oranlı gönderim.
* **Detaylı Bilgilendirme:** Mail içeriğinde müşteri ismi, seçilen tur ve toplam tutar bilgisi dinamik olarak yer alır.

## 🌐 Çoklu Dil Desteği (Localization)
Sistem, kullanıcı tercihlerine göre **Cookie** tabanlı dil seçimi sunar:
* 🇹🇷 **Türkçe** | 🇺🇸 **English** |

---
## 📸 Screenshots

<p align="center">
  <img src="ProjeResimleri/turlist.png" width="45%" />
  <img src="ProjeResimleri/turplan2.png" width="45%" /> 
</p>
<p align="center">
  <img src="ProjeResimleri/yorumlar.png" width="45%" />
  <img src="ProjeResimleri/gallery.png" width="45%" />
</p>
<p align="center">
  <img src="ProjeResimleri/email.png" width="45%" />
  <img src="ProjeResimleri/dashboard.png" width="45%" />
</p>
  

