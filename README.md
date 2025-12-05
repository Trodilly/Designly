# Designly Application

This repository contains a sample .NET Core solution demonstrating a multi-layered application architecture, including a RESTful API with JWT authentication and a Razor Pages client that consumes it. The project is structured to adhere to Clean Architecture principles, emphasizing modularity, maintainability, and testability.

## Project Structure

The solution is composed of several projects, each with a distinct responsibility:

*   **`Designly.Domain`**:
    *   Contains the core business entities (`Employee`, `User`). This layer is framework-agnostic and holds the enterprise-wide business rules.
*   **`Designly.Application`**:
    *   Defines interfaces for services and repositories (e.g., `IEmployeeRepository`, `IAuthService`, `ITokenGenerator`).
    *   Holds Data Transfer Objects (DTOs) and application-specific business logic (e.g., `AuthService`).
    *   Depends only on `Designly.Domain`.
*   **`Designly.Infrastructure`**:
    *   Provides concrete implementations for the interfaces defined in `Designly.Application`.
    *   Includes in-memory data repositories (`InMemoryEmployeeRepository`, `InMemoryUserRepository`) for demonstration purposes.
    *   Contains the `JwtTokenGenerator` implementation.
    *   Depends on `Designly.Application` and `Designly.Domain`.
*   **`Designly.API`**:
    *   The ASP.NET Core Web API project.
    *   Handles HTTP requests, routing, and serialization.
    *   Controllers depend on abstractions (`IEmployeeRepository`, `IAuthService`) from the `Designly.Application` layer.
    *   Configures JWT authentication and Swagger UI for API exploration.
    *   Depends on `Designly.Application`, `Designly.Infrastructure`, and `Designly.Domain`.
*   **`Designly.Client`**:
    *   An ASP.NET Core Razor Pages application that acts as a client to the `Designly.API`.
    *   Demonstrates user login, token retrieval, and access to protected API endpoints.
    *   Includes basic UI for listing users and logging out.
*   **`Designly.Tests`**:
    *   An xUnit test project for comprehensive unit testing of the `Designly.Application` services and `Designly.API` controllers using `Moq` for dependency mocking.

## Features

*   **Employee Management**: CRUD operations for employee records (in-memory).
*   **User Authentication**: Secure login mechanism using JSON Web Tokens (JWT).
*   **Protected API Endpoints**: Access to sensitive data (e.g., user lists, employee data) is restricted to authenticated users.
*   **Razor Pages Client**: A simple web client to demonstrate user login, JWT token handling, and consumption of protected API endpoints.
*   **Logout Functionality**: Clears authentication state on the client side.
*   **Swagger/OpenAPI**: API documentation and interactive testing via Swagger UI.

## Installation

### Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Steps

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/Designly.git
    cd Designly
    ```
2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```
3.  **Build the solution:**
    ```bash
    dotnet build
    ```

## Usage (How to Run)

To run the application, you need to start both the API and the Client projects.

1.  **Start the API project:**
    Open a terminal in the `Designly.API` directory and run:
    ```bash
    dotnet run --project Designly.API.csproj --launch-profile https
    ```
    The API will typically run on `https://localhost:7176` (check terminal output for exact URL).
    You can explore API endpoints via Swagger UI at `https://localhost:7176/swagger`.

2.  **Start the Client project:**
    Open another terminal in the `Designly.Client` directory and run:
    ```bash
    dotnet run --project Designly.Client.csproj --launch-profile https
    ```
    The client will typically run on `https://localhost:7260` (check terminal output for exact URL).

### Login Credentials

Use the following mock credentials for testing:

*   **Username:** `admin`
*   **Password:** `password123`

### Client Navigation

*   **Login Page**: Navigate to the client's root URL (e.g., `https://localhost:7260/`).
*   **Users List**: After successful login, you will be redirected to `/Users` to see the list of users fetched from the protected API.
*   **Swagger API Link**: Available in the footer for direct access to API documentation.

## Testing

To run the unit tests:

1.  Open a terminal in the root directory of the solution.
2.  Run the tests:
    ```bash
    dotnet test Designly.Tests/Designly.Tests.csproj
    ```
