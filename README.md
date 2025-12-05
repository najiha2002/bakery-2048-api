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
- **Containerization**: Docker and Docker Compose

---

## Quick Start

### Prerequisites
- [Docker](https://www.docker.com/get-started) and Docker Compose
- Git

### Setup

1. **Clone and navigate to the repository**
```bash
git clone https://github.com/najiha2002/bakery-2048-api.git
cd bakery-2048-api
```

2. **Configure environment (optional)**
```bash
cp .env.example .env
# Edit .env if needed, or use defaults
```

3. **Start the application**
```bash
# Run in foreground (view logs)
docker-compose up --build

# OR run in background (detached mode)
docker-compose up -d --build
```

This automatically:
- Starts PostgreSQL on port 5432
- Builds and starts the API on port 5130
- Runs migrations and seeds data
- Creates persistent database volume

4. **Access the API**
- Swagger UI: http://localhost:5130/swagger
- API Base URL: http://localhost:5130/api/

**Useful Docker commands:**
```bash
docker-compose logs -f      # View logs
docker-compose down         # Stop containers
docker-compose restart api  # Restart API only
```

---

## Manual Setup (Without Docker)

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Steps

1. **Clone repository**
```bash
git clone https://github.com/najiha2002/bakery-2048-api.git
cd bakery-2048-api
```

2. **Configure environment**

Create `.env` in the root directory:
```env
DB_HOST=localhost
DB_NAME=bakery2048_db
DB_USER=postgres
DB_PASSWORD=your_password
JWT_SECRET_KEY=your_jwt_secret_key
```

Generate JWT secret:
```bash
openssl rand -base64 32
```

3. **Create database**
```bash
createdb bakery2048_db
```

4. **Run migrations**
```bash
cd Bakery2048.API
dotnet ef database update
```

5. **Start the API**
```bash
dotnet run
```

Access at http://localhost:5130/swagger

---

## Frontend Integration

Once your backend is running, use the Bakery 2048 frontend:

- **Frontend URL**: https://najiha2002.github.io/bakery-2048/
- The frontend communicates with your local backend at `http://localhost:5130`
- CORS is pre-configured for localhost
- Register, login, play the game, and view player stats via the web interface

**Troubleshooting:**
- Ensure backend is running on port 5130
- Check browser console for connection errors

---

## Admin Account Setup

By default, new users are created with the Player role. For admin access:

**Via cURL:**
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

**Via Swagger:**
1. Go to http://localhost:5130/swagger
2. Expand `POST /api/auth/register`
3. Click "Try it out" and use:
```json
{
  "username": "admin",
  "email": "admin@example.com",
  "password": "Admin123!",
  "role": "Admin"
}
```

**Login to get JWT token:**
```bash
curl -X POST "http://localhost:5130/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "Admin123!"
  }'
```

Use the returned token in Authorization headers:
```
Authorization: Bearer <your-token>
```

---

## Testing & Sample Data

### Quick Test with Sample Data

Populate the database with 7 sample players and test all endpoints:

```bash
./test-api.sh
```

This script will:
- Create 7 players with realistic stats (scores, win streaks, games played)
- Register an admin user
- Test all CRUD operations
- Verify WinStreak field is returned in responses
- Create a new tile (1024 value)
- Display top 5 players leaderboard

### Reset Database

To clear all data and start fresh:

```bash
./reset-db.sh
```

This will:
- Stop all containers
- Delete all database volumes
- Rebuild and restart with a clean database
- Ready for new test data

**Example workflow:**
```bash
# Reset database
./reset-db.sh

# Populate with sample data
./test-api.sh
```

---

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Players
- `GET /api/players` - Get all players (supports `?top=N` query)
- `GET /api/players/{id}` - Get player by ID
- `PUT /api/players/{id}` - Update player (requires authentication)
- `DELETE /api/players/{id}` - Delete player (Admin only)

### Tiles
- `GET /api/tiles` - Get all tiles
- `GET /api/tiles/{id}` - Get tile by ID
- `POST /api/tiles` - Create tile (Admin only)
- `PUT /api/tiles/{id}` - Update tile (Admin only)
- `DELETE /api/tiles/{id}` - Delete last tile only (Admin only)

**Tile Constraints:**
- Values must be powers of 2 (2, 4, 8, 16, 32, etc.)
- Only the last tile in progression can be deleted
- No duplicate tile values or names allowed

---

## Seeded Data

### Default Tiles (9 bakery items)
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

---

## Testing

**REST Client (.http file):**
```
Bakery2048.API/Bakery2048.API.http
```

**Swagger UI:**
```
http://localhost:5130/swagger
```

---

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

---

## Security Features

- BCrypt password hashing
- JWT tokens with role-based claims
- Role-based authorization (`[Authorize(Roles = "Admin")]`)
- Environment-based secrets management
- Unique constraints on username and email

---

## Railway Deployment

The app supports Railway's `DATABASE_URL` environment variable. When deploying to Railway:

1. Add PostgreSQL database to your Railway project
2. Set environment variables:
   - `JWT_SECRET_KEY` (required)
   - `PORT` (optional, Railway sets this automatically)
3. Railway automatically injects `DATABASE_URL`

The app will prioritize `DATABASE_URL` over individual DB variables.

---

## Documentation

- **Swagger UI**: http://localhost:5130/swagger
- **REST Client Examples**: [Bakery2048.API.http](Bakery2048.API/Bakery2048.API.http)
- **API Documentation**: [API.md](API.md)
