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
- **Containerization**: Docker and Docker Compose

---

## Local Development: Step-by-Step Guide

### Option 1: Docker (Recommended for Quick Setup)

**Prerequisites:**
- [Docker](https://www.docker.com/get-started) and Docker Compose
- Git

**Steps:**

1. **Clone the repository**
```bash
git clone https://github.com/najiha2002/bakery-2048-api.git
cd bakery-2048-api
```

2. **Create environment file (optional)**
```bash
cp .env.example .env
# Edit .env if needed, or use defaults
```

3. **Generate JWT secret key (optional but recommended)**
```bash
openssl rand -base64 32
# Copy the output and add to .env as JWT_SECRET_KEY
```

4. **Start the application**
```bash
# Run in foreground (logs visible, blocks terminal)
docker-compose up --build

# OR run in background (detached mode, frees terminal)
docker-compose up -d --build
```

This will:
- Start PostgreSQL database on port 5432
- Build and start the API on port 5130
- Automatically run migrations and seed data
- Create persistent data volume for the database

**Useful commands:**
```bash
# View logs (if running in background)
docker-compose logs -f

# Stop containers
docker-compose down

# Restart containers
docker-compose restart
```

5. **Access the API**
- Swagger UI: [http://localhost:5130/swagger](http://localhost:5130/swagger)
- API endpoints: [http://localhost:5130/api/*](http://localhost:5130/api/)

> **Tip**: If you ran `docker-compose up` without `-d`, press `Ctrl+C` to stop. For background mode, use `docker-compose down` to stop.

6. **Create an Admin Account**

By default, new registrations create Player accounts. To create an admin account with full access:

**Using curl:**
```bash
curl -X POST "http://localhost:5130/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "email": "admin@example.com",
    "password": "Admin123!",
    "role": "Admin"
  }'
```

**Using Swagger UI:**
1. Go to [http://localhost:5130/swagger](http://localhost:5130/swagger)
2. Expand `POST /api/auth/register`
3. Click "Try it out"
4. Use this JSON body:
   ```json
   {
     "username": "admin",
     "email": "admin@example.com",
     "password": "Admin123!",
     "role": "Admin"
   }
   ```
5. Click "Execute"

Then login to get your admin JWT token:
```bash
curl -X POST "http://localhost:5130/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "Admin123!"
  }'
```

Copy the `token` from the response and use it in Swagger's "Authorize" button or in API requests:
```
Authorization: Bearer <your-token-here>
```

---

### Option 2: Manual Setup (Without Docker)

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
Create a `.env` file in the **root directory** with:
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

### 8. Run frontend

Once your local backend is running (API on http://localhost:5130), you can use the Bakery 2048 frontend:

- Visit [Bakery 2048 Frontend](https://najiha2002.github.io/bakery-2048/)
- The frontend is a static web app hosted on GitHub Pages. It communicates with your local backend via API calls to `http://localhost:5130`.
- Make sure your browser allows requests to `localhost:5130` (CORS is enabled by default in the backend).
- You can register, login, play the game, and view player stats using the web interface.
- For admin actions (like creating/updating/deleting tiles), login as an admin and use the admin dashboard in the frontend.

**Troubleshooting:**
- If you see errors about connecting to the API, ensure your backend is running and accessible at `http://localhost:5130`.
- If you change the backend port, update the frontend configuration (if needed) to match.

### 9. Create an Admin Account (Optional)

By default, role is set as Player. To access admin-only endpoints (create/update/delete tiles, delete players), register an admin user:

```bash
curl -X POST "http://localhost:5130/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "email": "admin@example.com",
    "password": "Admin123!",
    "role": "Admin"
  }'
```

**Or via Swagger:**
1. Go to `/swagger`
2. Expand `POST /api/auth/register`
3. Click "Try it out"
4. Use this JSON:
```json
{
  "username": "admin",
  "email": "admin@example.com",
  "password": "Admin123!",
  "role": "Admin"
}
```
5. Click "Execute"

Then login to get your JWT token:
```bash
curl -X POST "http://localhost:5130/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "Admin123!"
  }'
```

Copy the `token` from the response and use it in the Authorization header:
```
Authorization: Bearer <your-token-here>
```

Unfortunately, token can't be passed in this Swagger version, hence, alternatively, run this command with Authorization token replaced.

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

Create a `.env` file in the **root directory**:
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

