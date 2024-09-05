#MovieStore Projesi
###Bu proje, .NET Core ile geliştirilen bir film mağazası API'sidir. Proje, kullanıcıların film satın almasını, favori türlerini yönetmesini ve siparişleri kaydetmesini sağlayan bir RESTful API sunar. Proje aynı zamanda Entity Framework Core kullanarak verilerin yönetimini sağlar ve Unit Testler ile kod kalitesini arttırmayı hedefler.

##İçindekiler
###Kullanılan Teknolojiler
###Projede Kullanılan Yapılar
###Projede Yer Alan Sınıflar
###Veri Modeli
###Kurulum
###Testler

##Kullanılan Teknolojiler
.NET Core 6: .NET platformunun son sürümü ile API geliştirilmiştir.
Entity Framework Core: Veritabanı yönetimi için kullanılan bir ORM aracıdır. Bu projede in-memory veritabanı kullanılmıştır.
AutoMapper: Veritabanı modellerini DTO'lara (Data Transfer Object) dönüştürmek için kullanılmıştır.
FluentValidation: Girdi doğrulaması ve verilerin kurallara uygunluğunu sağlamak için kullanılmıştır.
XUnit: Projede birim testler XUnit test frameworkü ile yazılmıştır.
Postman: API isteklerini test etmek için kullanılan bir araçtır.
Projede Kullanılan Yapılar
Dependency Injection (DI): Uygulamada bağımlılıkların dışarıdan enjekte edilmesi için kullanılır. Bu projede yer alan sınıflar DI ile yönetilmekte ve servislerin yaşam döngüsü kontrol edilmektedir.

DTO (Data Transfer Object): Veritabanı modelleri ile dışarıya sunulan API modelleri arasında veri taşıma katmanı olarak kullanılır. DTO’lar sayesinde veritabanı ile direkt ilişki kurulmaktan kaçınılır.

Middleware: Uygulamanın istek ve yanıt aşamalarında kullanılan katmanlar. Özellikle hata yönetimi ve JWT ile kimlik doğrulama aşamalarında kullanılmıştır.

Unit of Work & Repository Pattern: Projede veri tabanı işlemlerini yönetmek için Unit of Work deseni uygulanmıştır. Veritabanı işlemleri belirli sınıflar aracılığıyla soyutlanmıştır.

Projede Yer Alan Sınıflar
Commands
CreateCustomerCommand: Yeni bir müşteri eklemek için kullanılır. Müşteri eklendiğinde, favori türler ve satın alınan filmler de güncellenir.

CreateOrderCommand: Yeni bir sipariş eklemek için kullanılır. Sipariş oluşturulduğunda, siparişteki filmler müşterinin satın aldığı filmler listesine eklenir.

DeleteOrderCommand: Var olan bir siparişi silmek için kullanılır. Sipariş silindiğinde, o siparişe ait filmler müşterinin satın aldığı filmler listesinden kaldırılır.

UpdateOrderCommand: Var olan bir siparişi güncellemek için kullanılır. Siparişi güncellerken, eski sipariş silinir ve yerine yeni bilgilerle bir sipariş eklenir.

Validators
CreateCustomerCommandValidator: Müşteri oluşturma komutunun doğrulama kurallarını içerir. E-posta benzersizliği ve minimum karakter uzunluğu gibi kontroller burada yapılır.

CreateOrderCommandValidator: Sipariş oluşturma işlemlerinde doğrulama kurallarını içerir. Özellikle siparişin geçerli bir müşteri ID'si ile yapılması kontrol edilir.

Queries
GetCustomerByNameQuery: Belirli bir isimle müşteri araması yapar. İsim benzersiz olmalı ve aynı isimle birden fazla müşteri bulunmamalıdır.

GetMoviesQuery: Tüm filmleri listelemek için kullanılır. Filmlerin isimleri ve türleri gibi bilgiler döndürülür.

Veri Modeli
Proje veritabanı modelleri aşağıdaki gibidir:

Customer: Müşterilerle ilgili bilgiler içerir. Müşterinin favori film türleri ve satın aldığı filmler gibi özellikleri bulunmaktadır.

Movie: Filmlerle ilgili temel bilgileri içerir. Bir film, birden fazla türe sahip olabilir.

Order: Siparişlerle ilgili bilgiler içerir. Müşteri ID'si, sipariş tarihi ve siparişteki filmler gibi bilgileri tutar.

Genre: Film türlerini temsil eder. Bir film bir veya birden fazla türe ait olabilir.

Kurulum
