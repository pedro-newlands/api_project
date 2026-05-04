# ***Changes***

**Layered Structure and Dependencies**
+ API (Presentation Layer): The entry point of the application. It handles HTTP requests, routing, and middleware configuration.
  - References: Application and Infrastructure.
+ Application Layer: Orchestrates use cases and implements business rules. It contains the services and DTO definitions.
  - References: Domain.
+ Infrastructure Layer: Handles data persistence (EF Core), external integrations, and technical concerns.
  - References: Domain.
+ Domain Layer: The core of the system. Contains Entities, Enums, and Repository Interfaces. It is independent of all other layers.
+ .sln: The root container that organizes all projects (.csproj). It defines project dependencies and build configurations (Debug/Release) for the entire workspace.
+ .editorconfig: A configuration file that enforces consistent coding styles, formatting rules, and naming conventions across different editors and for everyone working on the project.

**Dependency Injection (DI)**
To maintain a clean Program.cs, we use Static Extension Methods within the respective layers (Application and Infrastructure) to register their own services into the DI container.

Application: Exposes AddApplication().

Infrastructure: Exposes AddInfrastructure().

**Business Logic and Repository Pattern**
The architecture utilizes Expression Trees to bridge the gap between business requirements and data access without leaking infrastructure details.
+ Application Services: Contain the business logic and define "what" to filter by creating Expression<Func<T, bool>> predicates.
+ Infrastructure Repositories: Receive these expressions as metadata filters and apply them to the EF Core IQueryable for optimized database execution, returning at the end.

**Communication via DTOs (Data Transfer Objects)**
To prevent the leakage of database schemas and protect domain integrity, Controllers strictly communicate using DTOs now.

Input DTOs: Capture and validate user input.

Output DTOs (Response): Project entities into a flat, safe format for the client.

Result Pattern: Now a <T> TargetDtoType is wrapped into a ResultDto<T>, instead of a <T> TargetEntityType 

**SQL Injection & Scope Management**
+ Context: Occurs during the development phase via the SeedDatabaseAsync() static extension method within the Infrastructure layer.

+ Purpose: Used to execute an external populate.sql file to initialize data for integration tests and local development.

+ Scope Management: To prevent resource leaks, the method utilizes a strictly bounded scope. This ensures that the database context and its underlying connections do not remain open longer than necessary (avoiding a "breathing scope" that could lead to "zombie" connections).


### Planned Improvements
- Password hashing implementation in `LoginService` for production environments
- CD/CI - Automated tests and Github Actions
- Proper input validation
- Data seeding and integration with the PokéAPI
- Front-end integration
- More granular exception handling within transactional operations
- Support for additional Poké Ball types
