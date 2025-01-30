# Restaurants API - Project Overview

- This project is designed for restaurant management. It is developed as a .NET 8 Web API, following the principles of Clean Architecture to ensure a clear separation of concerns.

---

### **Technologies and Libraries**

- **MediatR:** Implements the CQRS pattern.
- **Microsoft Identity Package:** Handles user identity, authentication, authorization, and access control.
- **Entity Framework:** ORM for database interaction.
- **Serilog:** Logging framework for structured logging.

### **Features and Functionality**

- **FluentValidation:** Validates data before saving.
- **Custom Exception Middleware:** Catches errors globally and returns structured error messages in API responses.
