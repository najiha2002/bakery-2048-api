# Bakery 2048 API

A RESTful API for the Bakery 2048 game, built with ASP.NET Core, Entity Framework Core, PostgreSQL, and JWT authentication.

## Overview

**Bakery 2048** is a themed variation of the popular 2048 puzzle game where players merge bakery items (Flour → Egg → Butter → Sugar → Donut → Cookie → Cupcake → Slice Cake → Whole Cake) to achieve higher scores.

This API provides:
- **User Authentication**: JWT-based registration and login
- **Player Management**: Track player statistics and game progress
- **Tile Management**: Manage bakery item catalog with icons and colors
- **Role-Based Access**: Admin and Player roles with authorization

## Tech Stack

- **Framework**: .NET 8.0 / ASP.NET Core
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT Bearer tokens with BCrypt password hashing
- **API Documentation**: Swagger/OpenAPI with XML comments
- **Environment Config**: DotNetEnv for secrets management

---

## Local Development: Step-by-Step Guide

### 1. Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- Git

### 2. Clone the Repository
```bash
git clone https://github.com/najiha2002/bakery-2048-api.git
cd bakery-2048-api
```

### 3. Set Up Environment Variables
Create a `.env` file in `Bakery2048.API/` with:
```env
DB_HOST=localhost
DB_NAME=bakery2048_db
DB_USER=postgres
DB_PASSWORD=your_local_password
JWT_SECRET_KEY=your_jwt_secret_key
```
Generate a JWT secret key:
```bash
openssl rand -base64 32
```

### 4. Create the Database
```bash
createdb bakery2048_db
```

### 5. Run Migrations
```bash
cd Bakery2048.API
dotnet ef database update
```

This will:
- Create all tables (Users, Players, Tiles)
- Seed 9 Tiles (Flour → Whole Cake)

### 6. Run the API
```bash
dotnet run
```
The API will be running on port 5130.

### 7. Test the API
- Open **Swagger UI**: [http://localhost:5130/swagger](http://localhost:5130/swagger)
- Test endpoints directly: [http://localhost:5130/api/tiles](http://localhost:5130/api/tiles)
- Or use the included `Bakery2048.API/Bakery2048.API.http` file with the REST Client extension

> **Note**: The root URL (http://localhost:5130/) returns 404. Use `/swagger` or `/api/*` endpoints.

---

## Getting Started (Original Instructions)

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL database
- Git

### Setup Instructions

**1. Clone the repository**
```bash
git clone https://github.com/najiha2002/bakery-2048-api.git
cd bakery-2048-api
```

**2. Set up environment variables**

Create a `.env` file in the `Bakery2048.API` folder:
```bash
DB_HOST=localhost
DB_NAME=bakery2048
DB_USER=postgres
DB_PASSWORD=your_password
JWT_SECRET_KEY=your_jwt_secret_key
```

Ask for db password from admin.

Generate a secure JWT secret key:
```bash
openssl rand -base64 32
```

**3. Create the database**
```bash
createdb bakery2048
# or via psql:
psql -U postgres -c "CREATE DATABASE bakery2048;"
```

**4. Run migrations**
```bash
cd Bakery2048.API
dotnet ef database update
```

This will:
- Create all tables (Users, Players, Tiles)
- Seed 9 Tiles (Flour → Whole Cake)

**5. Run the application**
```bash
dotnet run
```

The API will be available at `http://localhost:5130`

## API Endpoints

### Authentication

**Register a new player**
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "player1",
  "email": "player1@example.com",
  "password": "Password123!"
}
```

**Register an admin**
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "admin",
  "email": "admin@example.com",
  "password": "Admin123!",
  "role": "Admin"
}
```

**Login**
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "player1",
  "password": "Password123!"
}
```

### Players

- `GET /api/players` - Get all players (optional `?top=N` query)
- `GET /api/players/{id}` - Get player by ID
- `PUT /api/players/{id}` - Update player (requires authentication)
- `DELETE /api/players/{id}` - Delete player (Admin only)

### Tiles

- `GET /api/tiles` - Get all tiles
- `GET /api/tiles/{id}` - Get tile by ID
- `POST /api/tiles` - Create tile (Admin only)
- `PUT /api/tiles/{id}` - Update tile (Admin only)
- `DELETE /api/tiles/{id}` - Delete tile (Admin only)

## Seeded Data

### Tiles (9 items)
| Value | Name | Icon | Color |
|-------|------|------|-------|
| 2 | Flour | 🌾 | #fcefe6 |
| 4 | Egg | 🥚 | #f2e8cb |
| 8 | Butter | 🧈 | #f5b682 |
| 16 | Sugar | 🍬 | #f29446 |
| 32 | Donut | 🍩 | #f88973ff |
| 64 | Cookie | 🍪 | #ed7056ff |
| 128 | Cupcake | 🧁 | #ede291 |
| 256 | Slice Cake | 🍰 | #fce130 |
| 512 | Whole Cake | 🎂 | #ffdb4a |

## Features

### User & Player System
- Automatic player creation on registration (for non-admin users)
- BCrypt password hashing for security
- JWT tokens with role-based claims
- Username and email validation

### Admin vs Player Roles
- **Players**: Can view and update their own profile
- **Admins**: Full CRUD access to all resources, no player profile created

### Security
- JWT secret stored in environment variables
- Password hashing with BCrypt
- Role-based authorization with `[Authorize(Roles = "Admin")]`
- Unique indexes on username and email

## Testing

Use the included `.http` file with the REST Client extension:
```
Bakery2048.API/Bakery2048.API.http
```

Or use Swagger UI:
```
http://localhost:5130/swagger
```

## Project Structure

```
Bakery2048.API/
├── Controllers/         # API endpoints
├── Services/           # Business logic
├── Data/               # DbContext and migrations
├── Models/             # Entity models
├── DTOs/               # Data transfer objects
├── Migrations/         # EF Core migrations
└── Program.cs          # App configuration
```

## Documentation

For detailed API documentation, use:
- **Swagger UI**: `http://localhost:5130/swagger` (when running the application)
- **REST Client Examples**: [Bakery2048.API.http](Bakery2048.API/Bakery2048.API.http)

