# GorevTakipSistemiAPI

1. Bağımlılıkları Yükle
Projeyi klonladıktan sonra projenin bulunduğu klasörde terminal açıp:

```bash
dotnet restore
```
Bu komut NuGet paketlerini indirir.

2. Veritabanını Oluştur
Code First kullanılarak geliştirilme yapılmıştır. Veri tabanını oluşturmak için aşağıdaki komutu yazınız.
```bash
dotnet ef database update
```
Bu komut, migration'ları veritabanına uygular ve veritabanını oluşturur.

