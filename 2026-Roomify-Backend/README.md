# Room Booking Backend API

Backend API for a room reservation system with role-based access control (Admin & User).  
Built using ASP.NET Core Web API and Entity Framework Core.

---

## Features

### Authentication
JWT-based authentication using Authorization Header (Bearer Token).

### Building & Room Management (Admin)  
Each Room is associated with a Building.

### Room Booking (User)
Users can create and update their own bookings with date validation.

### Booking Approval (Admin)
Admin can approve or reject user bookings.  
All status changes are recorded in booking history.

### Reservation Monitoring
All bookings created by users are automatically visible to Admin.

### Pagination & Filtering
Supports pagination and filtering by status, date, building, and room.

### RESTful API
Implements standard HTTP methods and status codes with role-based access control.

### Database Migration
Database schema is managed using Entity Framework Core migrations.

---

### User
- View booking list
- Create booking
- Update own booking
- View booking by ID

### Admin
- All User permissions
- Approve or reject bookings
- Delete bookings
- View booking history

---

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)

---

## API Endpoints

### Bookings

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/Bookings` | User | Create booking |
| GET | `/api/Bookings` | User/Admin | Get all bookings |
| GET | `/api/Bookings/{id}` | User/Admin | Get booking by ID |
| DELETE | `/api/Bookings/{id}` | Admin | Delete booking |
| PUT | `/api/Bookings/user/{id}` | User | Update own booking |
| PUT | `/api/Bookings/admin/{id}/status` | Admin | Update booking status |
| GET | `/api/Bookings/{id}/history` | User/Admin | Get booking history |

---

### Buildings

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/Buildings` | Admin | Create building |
| GET | `/api/Buildings` | User/Admin | Get all buildings |

---

### Users

| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/Users/register` | User/Admin | Create new account  |
| GET | `/api/Users/login` | User/Admin | Get all account |

---

## Project Structure

```
RoomifyBackend
│
├── Controllers (including DTO definitions)
├── Models
├── Data
├── Migrations
├── appsettings.json
└── Program.cs
```

---

## Installation & Setup

### 1️⃣ Clone Repository

```bash
git clone https://github.com/Zahrins/2026-Roomify-Backend.git
cd 2026-Roomify-Backend
```

### 2️⃣ Install Dependency

```bash
dotnet restore
```

---

## Environment Configuration

Create a file:

```
appsettings.Development.json
```

Example configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=roomify_db;Username=postgres;Password=your_password"
  },
  "Jwt": {
    "Key": "your_super_secret_key",
    "Issuer": "RoomifyAPI",
    "Audience": "RoomifyUser"
  }
}
```

Make sure this file is not committed.
Add it to .gitignore:

```
appsettings.Development.json
```

---

## Database Setup

Ensure PostgreSQL is running.

Create a database:

```
roomify_db
```

Run migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Running the Application

```bash
dotnet run
```

Server will run at:

```
https://localhost:7252
```

Swagger documentation:

```
https://localhost:7252/swagger
```

---
## Testing API

You can test the API using Swagger:

```
https://localhost:7252/swagger
```

Or use Postman with header:

```
Authorization: Bearer {your_token}
```

---
