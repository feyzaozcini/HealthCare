## Health Care Full Stack Projesi
Proje, ekip olarak geliştirdiğimiz bir web uygulamasıdır. Backend tarafında C# ABP Framework, frontend tarafında ise Blazor ve kullanıcı dostu arayüzler oluşturmak için Syncfusion UI kütüphanesi kullanılmıştır. Uygulama; hasta kayıt, muayene, randevu ve laboratuvar modüllerini içermektedir. Projedeki sorumluluğum, randevu modülünün geliştirilmesiydi. 

## Kullanılan Teknolojiler
[**ABP FRAMEWORK**](https://abp.io/)

[**Blazor Server**](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)

[**Syncfusion UI Components**](https://www.syncfusion.com/blazor-components)

## Arayüzler

## Giriş Sayfası
İşlemler Admin üzerinden gerçekleştirilmektedir.

![](images/23.JPG)

### Anasayfa

![](images/22.JPG)
Giriş Sayfasında Modüllerin Anasayfalarına Erişim Butonları Bulunmaktadır.

## Randevuya Ait Tanımlar
Randevu için gerekli bazı tanımlamalar yapılmaktadır. Doktorlar için özelleştirilmiş çalışma planı tanımları, randevu alımında kullanılan randevu türleri, randevuya özelleştirilmiş kural tanımları ve doktorların hastların randevu almasını istemediğinde kullanılan engel sayfası.

## Doktor Çalışma Zamanları
Doktorlar varsayılan olarak haftaiçi 5 gün sabah 9 akşam 5 olacak şekilde çalışma tanımlamaları mevcuttur. Fakat gerektiğinde özelleştirilebilir olmalıdır bunun için özel çalışma zamanı tanımlamaları bu sayfada gerçekleştirilmektedir.

![](images/1.JPG)

![](images/2.JPG)

## Randevu Türleri
Departmanlara özgü randevu türleri ekleme ve düzenleme sayfasıdır. Burada her randevu türünün süresi değiştirilebilir. Randevu türlerine doktorlar eklenir.

![](images/3.JPG)

![](images/4.JPG)

![](images/5.JPG)

## Randevu Kuralları
Departmana veya doktora özgü randeuv kuralları tanımlanabilir. Burada cinsiyete veya yaşa göre kural tanımlamaları yapılabilmektedir.

![](images/6.JPG)

## Randevu Kara Listesi
Doktor kendisine randevu alan bir hastanın bir daha tekrardan randevu almasını istemeyebilir. Burada doktor ve hasta için bir bloklama uygulanmaktadır. Hasta bu listede olduğu sürece doktordan randevu alamamaktadır. Randevu oluşturma sırasında hata vermektedir.

![](images/7.JPG)

## Randevu Alma Anasayfası
Randevu alınmak istendiğinde açılan anasayfa bu şekildedir. Burada departman seçildiğinde departmana ait tüm randevular gelir daha sonra departmana ait doktorlardan istenilen doktor seçilir ve doktorun kişisel randevuları gösterilir. Hasta için Hasta Seç butonu kullanılır. Arama yapılır eğer aranan hasta mevcut değil ise yeni hasta kaydı alınan bilgiler ile hızlıca kaydedilir. Daha sonra hasta seçilir ve istenilen randevu saati seçilerek randevu oluşturulur. Randevu oluşturulduğunda hastanın kayıt edilen mail adresine randevu bilgileri mail atılır.

![](images/8.JPG)

![](images/9.JPG)

![](images/10.JPG)

![](images/11.JPG)

![](images/13.JPG)

![](images/14.JPG)

## Randevu Listesi
Bu sayfa randevuları listelemektedir. Öncelikle tüm randevuların bulunduğu sayfa mevcuttur burada hastaneye ait tüm hasta sayısı, tüm randevuların sayısı ve bugünün randevularını göstermektedir. Seçilen doktor veya departmana göre sayılar güncellenmektedir. Bir sayfada güncel randevular diğer sayfada tarihi geçmiş randevular lislenmektedir. Randevu durumlarını güncelleştirmek için doktor seçilir ve ilgili hastasının randevu durumunu güncelleyebilir isterse iptal edebilir.

![](images/15.JPG)

![](images/16.JPG)

![](images/17.png)

## Randevu Raporları
Bu sayfada randevular için geniş çağlı bir rapolarma mevcuttur. Bir sayfada chartslar kullanılmıştır diğer sayfasında pivot tablo kullanılmıştır.

![](images/18.JPG)

![](images/19.JPG)

![](images/20.JPG)

![](images/21.JPG)

