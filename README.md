## Project Structure

### 📂 src (Application Layers)
- **Api**  
  - Controllers  
  - Exception handler  
  - Middlewares  

- **Application**  
  - CQRS – Command/Query/Handler  
  - Validations (FluentValidation)  
  - Abstractions  
  - Mappings  
  - DTOs  

- **Domain**  
  - Entities  
  - DDD  
  - BoundedContexts  
  - Enums  

- **Infrastructure**  
  - External Integrations  
  - Microservice Communication  

- **Persistence**  
  - DbContext  
  - Migrations  
  - Repository Implementations  

---

### 📂 tests (Unit Tests)
Unit tests are written for Api, Application, and Domain layers using **xUnit**.  
- **Api.UnitTests**  
- **Application.UnitTests**  
- **Domain.UnitTests**  

---

## Microservices

The project consists of two separate microservices:  

- **User-Service**  
  - Manages user operations (create, update, delete, list).  
  - URL: [http://localhost:5000](http://localhost:5000)  

- **Content-Service**  
  - Manages content operations (create, update, delete, list).  
  - URL: [http://localhost:5001](http://localhost:5001)  

### Microservice Communication

The services communicate with each other via **RESTful API**.  
For example:  
- When creating new content in **Content-Service**, the provided `UserId` is validated by making a request to **User-Service**.  

---

## 🚀 Docker & Build

### Build & Run
**Docker Compose:**

```bash
docker-compose up --build
