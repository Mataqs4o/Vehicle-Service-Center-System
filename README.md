# Vehicle Service Center System

ASP.NET Core MVC application for managing customer vehicles, mechanics, and service appointments.

## Features
- Customer, Vehicle, ServiceRecord, and Mechanic domain entities.
- Repository + Service layers for business logic separation.
- AutoMapper profile for view model/entity translation.
- Data annotations + custom validation (`ValidServiceDateAttribute`).
- Role-based authorization for Administrator, Customer, Mechanic.
- Email notification abstraction with a console-based implementation.
- Bootstrap-based responsive UI for booking service appointments.
- Unit tests for repository and service layers.

## Setup
1. Install .NET 9 SDK.
2. Restore packages:
   ```bash
   dotnet restore
   ```
3. Run the app:
   ```bash
   dotnet run --project WebApplication2
   ```
4. Run tests:
   ```bash
   dotnet test
   ```

## User Guide
- Navigate to `/ServiceAppointments/Create`.
- Enter Vehicle ID, appointment date, description, and estimated cost.
- Submit to create a service record and trigger a confirmation email notification.

## Test Cases and Results
- `ServiceRecordServiceTests.BookAppointmentAsync_AddsRecordAndSendsEmail`
- `GenericRepositoryTests.AddAsync_PersistsCustomer`

> In this environment, `dotnet` may be unavailable, so execute tests on a machine with the .NET 9 SDK installed.
