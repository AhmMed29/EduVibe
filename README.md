# EduVibe API

A backend REST API for a Student Management System built with ASP.NET Core 9, Entity Framework Core, and ASP.NET Core Identity.

## Tech Stack

- **Framework:** ASP.NET Core 9 Web API
- **ORM:** Entity Framework Core 9
- **Database:** SQL Server
- **Auth:** ASP.NET Core Identity + JWT Bearer
- **Architecture:** 3-Layer (PL / BLL / DAL)

## Project Structure

```
EduVibe/
├── PL/          → Presentation Layer (Controllers, Middlewares, Program.cs)
├── BLL/         → Business Logic Layer (Services, DTOs, Interfaces, Mappers)
└── DAL/         → Data Access Layer (DbContext, Entities, Migrations)
```

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (local or remote)

### Setup

1. **Clone the repository**
   ```bash
   git clone <repo-url>
   cd EduVibe
   ```

2. **Set JWT Key via User Secrets**
   ```bash
   cd PL
   dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyHere_Min32Chars"
   ```

3. **Update the connection string** in `PL/appsettings.json`

4. **Apply migrations**
   ```bash
   dotnet ef database update --project DAL --startup-project PL
   ```

5. **Run the API**
   ```bash
   dotnet run --project PL
   ```

6. **Open Swagger** at `https://localhost:<port>/swagger`

## Auth Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/auth/register` | Register new user (Student or Instructor) | Public |
| POST | `/api/auth/login` | Login and get JWT token | Public |

### Register Request Body
```json
{
  "fname": "Ahmed",
  "lname": "Ghazi",
  "email": "ahmed@example.com",
  "password": "Test@1234",
  "confirmPassword": "Test@1234",
  "phoneNumber": "01012345678",
  "role": "Student"
}
```

> **Note:** `role` accepts `"Student"` or `"Instructor"` only. Defaults to `"Student"` if not provided.

### Login Request Body
```json
{
  "email": "ahmed@example.com",
  "password": "Test@1234"
}
```

### Seeded Admin Account
```
Email:    admin@system.com
Password: Admin@123456
```

## Default Roles
- `Admin`
- `Manager`
- `Instructor`
- `Student`

