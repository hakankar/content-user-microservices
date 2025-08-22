
## Proje Yapısı

### 📂 src (Uygulama Katmanları)
- **Api**  
  - Controllers  
  - Exception handler
  - Middlewares

- **Application**  
  - İş mantığı (CQRS – Command/Query)  
  - Service ve Handler’lar  
  - Validations (FluentValidation)
  - Abstractions
  - Mappings
  - DTOs  

- **Domain**  
  - Domain modelleri  
  - Entities
  - DDD
  - BoundedContexts
  - Enums

- **Infrastructure**  
  - External Integrations
  - Microservice Communication  

- **Persistence**  
  - DbContext,
  - Migrations 
  - Repository Implementations  

---

### 📂 tests (Birim Testler)
xUnit kullanılarak Api, Application ve Domain katmanları için birim testler yazılmıştır. 
- **Api.UnitTests**  

- **Application.UnitTests**  

- **Domain.UnitTests**
---
## Mikroservisler

Proje iki ayrı mikroservisten oluşmaktadır:  

- **User-Service**  
  - Kullanıcı yönetimi (ekleme, güncelleme, silme, listeleme) işlemlerini gerçekleştirir.  
  - URL: [http://localhost:5000](http://localhost:5000)  

- **Content-Service**  
  - İçerik yönetimi (ekleme, güncelleme, silme, listeleme) işlemlerini gerçekleştirir.  
  - URL: [http://localhost:5001](http://localhost:5001)  

### Mikroservisler Arası İletişim

Servisler birbirleriyle **RESTful API** üzerinden haberleşmektedir.  
Örneğin:  
- **Content-Service** içerisinde yeni bir içerik oluşturulurken, verilen `UserId`’nin geçerli olup olmadığı **User-Service**’e yapılan bir istek ile kontrol edilmektedir. 
## 🚀 Docker & Build

### Build & Run
**Docker Compose:**

```bash
docker-compose up --build
