
Sayın Gençay Yıldız Hocamın Youtube Kanalında Yayınlamış Olduğu .Net Core-Angular eğitim serisidir.
Link : https://www.youtube.com/playlist?list=PLQVXoXFVVtp1DFmoTL4cPTWEWiqndKexZ

Projede kullanılan teknoloji ve yaklaşımlar:

Backend
<ul>
  <li>Onion Architecture</li>
  <li>Entity Framework Core - Code First</li>
 <li>Sync-Async Repository Design Pattern</li>
 <li>CQRS-Mediator Pattern</li>
 <li>Azure BlobStorage & Local Storage Service</li>
 <li>Mail Service</li>
 <li>.Net Core Identity With JWT Authentication</li>
 <li>Google  OAuth2 Authentication</li>
 <li>Rol Based Access Control</li>
 <li>Serilog</li>
 <li>SignalR</li>
 <li>Global Excaption Handler</li>
 <li>Table Per Hierarchy Approach</li>
 <li>FluentValidation</li>
  <li>Docker</li>
   <li>PostgreSQL</li>

</ul>
# ETicaret Server

Bu proje, bir e-ticaret uygulamasının backend sunucu tarafını içermektedir.

## Proje Yapısı

Proje, Clean Architecture prensiplerine uygun olarak geliştirilmiştir ve aşağıdaki katmanlardan oluşmaktadır:

- **Core**
  - ETicaretServer.Application
  - ETicaretServer.Domain
- **Infrastructure**
  - ETicaretServer.Infrastructure
  - ETicaretServer.Persistence
- **Presentation**
  - ETicaretServer.API

## Teknolojiler

- .NET 6.0
- Entity Framework Core
- Azure Storage
- CQRS Pattern
- MediatR
- AutoMapper
- FluentValidation

## Kurulum

1. Projeyi klonlayın
2. `appsettings.json` dosyasını konfigüre edin
3. Veritabanı migration'larını çalıştırın:

