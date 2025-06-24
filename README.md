# GorevTakipSistemiAPI

1. Bağımlılıkları Yükle
Projeyi klonladıktan sonra projenin bulunduğu klasörde terminal açıp:

```bash
dotnet restore
```
Bu komut NuGet paketlerini indirir.

2. Veritabanını Oluştur ve Güncelle (Migrations)
Code First kullanıyorsan ve migration'ların varsa, veritabanını oluşturmak için şu adımları uygula:

Öncelikle veritabanı bağlantı stringinin appsettings.json dosyasında doğru olduğundan emin ol (başka bilgisayarda veritabanı ayarların farklı olabilir).

Sonra terminalde proje klasöründe şunu çalıştır:

```bash
dotnet ef database update
```
Bu komut, migration'ları veritabanına uygular ve veritabanını oluşturur/günceller.

Projeyi artık çalıştırablirsiniz.
