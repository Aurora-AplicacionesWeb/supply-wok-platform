# Supply Wok Platform

## Project Overview

The `Supply Wok Platform` is a robust and scalable backend application designed to manage restaurant operations, supplier catalogs, inventory, and authentication. Built with .NET, it adheres to Domain-Driven Design (DDD) principles and implements Command Query Responsibility Segregation (CQRS), promoting modularity, maintainability, and testability.

This platform is structured around distinct Bounded Contexts, ensuring clear separation of concerns and enabling independent development and deployment of core functionalities.

## Table of Contents

1.  Architecture Overview
2.  Domain-Driven Design (DDD) Concepts
3.  Key Features & Best Practices Implemented
4.  Bounded Contexts
    *   IAM (Identity and Access Management)
    *   Profiles
    *   Operations
5.  Technologies Used
6.  Getting Started
    *   Prerequisites
    *   Setup Instructions
7.  Project Structure
8.  Documentation
9.  License

## Architecture Overview

The project's architecture is driven by **Domain-Driven Design (DDD)** principles and implements **Command Query Responsibility Segregation (CQRS)**. This approach organizes the codebase to align closely with the business domain, separating operations that change state (Commands) from operations that read state (Queries).

*   **Domain Layer**: Contains the core business logic, entities, aggregates, value objects, and domain services. It is the heart of the application and has no dependencies on other layers.
*   **Application Layer**: Orchestrates domain objects to fulfill use cases (commands and queries). It defines application services, command handlers, and query handlers. It depends on the Domain layer.
*   **Infrastructure Layer**: Provides implementations for interfaces defined in the Domain and Application layers (e.g., database repositories, external service clients, authentication providers). It depends on the Application and Domain layers.
*   **Interfaces Layer (Presentation)**: Handles external communication, typically REST APIs. It defines controllers, API resources, and assemblers for transforming between API models and application/domain models. It depends on the Application layer.

## Domain-Driven Design (DDD) Concepts

The project embraces DDD to manage complexity in its core business domains:

*   **Bounded Contexts**: The application is explicitly divided into `IAM`, `Profiles`, `Operations`, `Inventory`, `Iot`, `Spm`, `Purchasing`, `Analytics`, and `Subscriptions` bounded contexts, each with its own ubiquitous language and domain model.
*   **Aggregates**: Key domain objects like `KitchenOrder` (in Operations) and `User` (in IAM) are defined as aggregate roots. They encapsulate a cluster of domain objects (entities and value objects) and ensure transactional consistency within their boundaries.
*   **Entities**: Objects with a distinct identity that runs through time and different representations (e.g., `Table`, `Dish`).
*   **Value Objects**: Objects that measure, quantify, or describe a thing in the domain and are immutable (e.g., `PersonName`, `EmailAddress`, `StreetAddress` in Profiles). They are compared by their values, not their identity.
*   **Domain Services**: Operations that don't naturally fit within an entity or value object (e.g., `ITokenService`, `IHashingService` in IAM).

## Key Features & Best Practices Implemented

*   **Command Query Responsibility Segregation (CQRS)**: Commands are used to update data, and Queries are used to retrieve data. This separation allows for independent scaling and optimization of read and write models.
*   **Cancellation Tokens**: Integrated across all asynchronous operations (application services, repositories, controllers, middleware) to enable graceful cancellation of long-running tasks, improving responsiveness and resource management.
*   **Refined Error Management**:
    *   **`Result<T>` Pattern**: Used consistently in application services to explicitly represent success or failure of an operation, avoiding exceptions for control flow.
    *   **Domain-Specific Error Enums**: Strongly-typed enums (e.g., `IamError`, `ProfilesError`, `OperationsError`) provide clear, categorized error codes for business rule violations.
    *   **RFC 7807 Problem Details**: All API error responses adhere to this standard, providing machine-readable and consistent error information to clients.
    *   **Global Exception Handling Middleware**: Catches unhandled exceptions, logs them, and returns standardized `ProblemDetails` responses, preventing sensitive information leakage.
*   **Clean API Design with `ActionResultAssemblers`**:
    *   **Thin Controllers**: Controllers are kept lean, focusing on request handling and delegating response formatting.
    *   **Static Assemblers**: Dedicated static assembler classes (e.g., `IamActionResultAssembler`, `OperationsActionResultAssembler`) encapsulate the logic for transforming `Result<T>` objects into `IActionResult`s, including mapping error enums to HTTP status codes and generating localized `ProblemDetails`. This centralizes response logic and ensures consistency.
*   **Persistence**: Utilizes Entity Framework Core for data access, supporting MySQL databases.
*   **Messaging/Mediation**: Employs Cortex.Mediator for handling commands and publishing domain events, promoting loose coupling between components.
*   **Authentication & Authorization**: Implements JWT-based authentication with custom middleware for request authorization.

## Bounded Contexts

The application is logically divided into the following Bounded Contexts, each managing a specific area of the business domain:

### IAM (Identity and Access Management)

The IAM Bounded Context is responsible for all aspects of user identity, authentication, and access control within the platform. It provides the foundational security mechanisms for the entire application.

*   **Scope**: User authentication, registration, authorization, and token management.
*   **Key Features**:
    *   **User Registration**: Allows new users to securely sign up and create accounts.
    *   **User Authentication**: Handles user sign-in, verifying credentials (username/password), and issuing secure JSON Web Tokens (JWTs) for subsequent authorized access.
    *   **Token Management**: Generates, validates, and manages the lifecycle of JWTs, ensuring secure session management.
    *   **Password Hashing**: Securely stores user passwords using robust, industry-standard hashing algorithms.
    *   **User Management**: Provides functionalities for retrieving and managing user accounts (e.g., fetching user details by ID or username).
*   **Aggregates**: `User`
*   **Domain Services**: `ITokenService` (for JWT operations), `IHashingService` (for password security)
*   **API Endpoints**: `/api/v1/authentication` (for sign-in/sign-up), `/api/v1/users` (for user queries)

### Profiles

The Profiles Bounded Context manages the personal and contact information associated with users. It focuses on maintaining accurate, consistent, and complete user profiles, which are distinct from their authentication credentials handled by IAM.

*   **Scope**: Management of restaurant and supplier contact details.
*   **Key Features**:
    *   **Profile Creation & Retrieval**: Allows users to create and retrieve their business profiles.
    *   **Contact Information Management**: Handles street address details including street, district, city, and country.
*   **Aggregates**: `RestaurantProfile`, `SupplierProfile`
*   **Value Objects**: `PersonName`, `EmailAddress`, `StreetAddress` (ensuring immutability and conceptual integrity of these data points)
*   **API Endpoints**: `/api/v1/restaurantprofiles`, `/api/v1/supplierprofiles`

### Operations

The Operations Bounded Context is dedicated to the creation, organization, and management of internal restaurant activities such as dishes, categories, tables, and kitchen orders.

*   **Scope**: Restaurant catalog, table organization, and order management.
*   **Key Features**:
    *   **Dish Management**: Enables the creation of new dishes with categories.
    *   **Table Management**: Manages physical tables in the restaurant space.
    *   **Order Workflow**: Manages kitchen orders, tracking what dishes correspond to what tables.
*   **Aggregates**: `KitchenOrder`, `Table`
*   **Entities**: `Dish`, `DishCategory`, `KitchenOrderItem`
*   **API Endpoints**: `/api/v1/dishes`, `/api/v1/tables`, `/api/v1/kitchenorders`

## Technologies Used

*   **.NET 10**: Core framework for the application.
*   **ASP.NET Core**: For building RESTful APIs.
*   **Entity Framework Core**: ORM for database interactions (MySQL).
*   **MySQL**: Relational database.
*   **Cortex.Mediator**: For implementing the Mediator pattern.
*   **BCrypt.Net-Next**: For secure password hashing.
*   **System.IdentityModel.Tokens.Jwt**: For JWT generation and validation.
*   **Swashbuckle.AspNetCore**: For OpenAPI/Swagger documentation.

## Getting Started

### Prerequisites

*   .NET 10 SDK
*   MySQL Server (or Docker for local development)
*   Git

### Setup Instructions

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Aurora-AplicacionesWeb/supply-wok-platform
    cd supply-wok-platform
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd Aurora.SupplyWok.Platform
    ```

3.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```

4.  **Configure Database:**
    *   Ensure your MySQL server is running.
    *   Update the `DefaultConnection` string in `appsettings.json` (and `appsettings.Development.json`) to point to your MySQL instance.
    *   The application is configured to automatically create the database if it doesn't exist on startup and will apply the needed Migrations.

5.  **Run the application:**
    ```bash
    dotnet run
    ```

6.  **Access Swagger UI:**
    Open your browser and navigate to `http://localhost:5109/swagger` to explore the API endpoints.

## Project Structure

The project is organized by **Bounded Contexts** at the top level, with each context further decomposed into architectural layers:

```
Aurora.SupplyWok.Platform/
├── Iam/                     # IAM Bounded Context
├── Profiles/                # Profiles Bounded Context
├── Operations/              # Operations Bounded Context
├── Inventory/               # Inventory Bounded Context
├── Iot/                     # IoT Bounded Context
├── Purchasing/              # Purchasing Bounded Context
├── Spm/                     # Spm Bounded Context
├── Subscriptions/           # Subscriptions Bounded Context
├── Analytics/               # Analytics Bounded Context
├── Shared/                  # Shared Bounded Context (cross-cutting concerns)
├── Program.cs               # Application startup and DI configuration
├── appsettings.json         # Configuration files
└── Aurora.SupplyWok.Platform.csproj # Project file
```

## Documentation

Comprehensive documentation, including architectural diagrams and detailed explanations of core components, can be found in the `docs/` directory.

*   **Class Diagram**: [`docs/class-diagram.puml`](docs/class-diagram.puml)

## License

This project is licensed under the **MIT License**. See the [`LICENSE.md`](LICENSE.md) file for details.
