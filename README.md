# Restaurants API - Project Overview

- This project is designed for restaurant management. It is developed as a .NET 8 Web API, following the principles of Clean Architecture to ensure a clear separation of concerns.

---

### **Technologies and Libraries**

- **MediatR:** Implements the CQRS pattern.
- **Microsoft Identity Package:** Handles user identity, authentication, authorization, and access control.
- **Entity Framework:** ORM for database interaction.
- **Serilog:** Logging framework for structured logging.
- **FluentValidation:** Validates data before saving.
- **Custom Exception Middleware:** Catches errors globally and returns structured error messages in API responses.

### **Testing and QA**

- **xUnit:** Unit and integration testing framework.
- **Moq:** Mocking dependencies for isolated unit tests.
- **FluentAssertions:** Provides readable and expressive assertions.

### **Deployment and DevOps**

- **Azure App Service & Azure Sql:** The API is deployed on Azure.
- **CI/CD Piplines:** Automates build, test and deployment processes to streamline updates and bug fixes.



