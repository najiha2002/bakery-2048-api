## API Endpoints

### Players

#### Get All Players
```http
GET /api/players
```

**Query Parameters:**
- `top` (optional, integer) - Limit results to top N players by highest score

**Response:** `200 OK`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "username": "najiha",
    "email": "najiha@example.com",
    "highestScore": 12450,
    "currentScore": 8500,
    "gamesPlayed": 25,
    "dateCreated": "2025-11-29T15:00:00Z"
  }
]
```

**Example:**
```bash
# Get all players
curl -X GET "http://localhost:5130/api/players"

# Get top 10 players
curl -X GET "http://localhost:5130/api/players?top=10"
```

---

#### Get Player by ID
```http
GET /api/players/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Player ID

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "najiha",
  "email": "najiha@example.com",
  "highestScore": 12450,
  "currentScore": 8500,
  "gamesPlayed": 25,
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `404 Not Found` - Player not found

**Example:**
```bash
curl -X GET "http://localhost:5130/api/players/3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

---

#### Create Player
```http
POST /api/players
```

**Request Body:**
```json
{
  "username": "najiha",
  "email": "najiha@example.com"
}
```

**Validation Rules:**
- `username`: Required, max 100 characters
- `email`: Required, valid email format, max 255 characters

**Response:** `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "najiha",
  "email": "najiha@example.com",
  "highestScore": 0,
  "currentScore": 0,
  "gamesPlayed": 0,
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `400 Bad Request` - Validation failed

**Example:**
```bash
curl -X POST "http://localhost:5130/api/players" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "najiha",
    "email": "najiha@example.com"
  }'
```

---

#### Update Player
```http
PUT /api/players/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Player ID

**Request Body:**
```json
{
  "username": "najiha",
  "email": "najiha@example.com",
  "highestScore": 15000,
  "currentScore": 10000,
  "gamesPlayed": 30
}
```

**Response:** `204 No Content`

**Error Responses:**
- `400 Bad Request` - Validation failed
- `404 Not Found` - Player not found

**Example:**
```bash
curl -X PUT "http://localhost:5130/api/players/3fa85f64-5717-4562-b3fc-2c963f66afa6" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "najiha",
    "email": "najiha@example.com",
    "highestScore": 15000,
    "currentScore": 10000,
    "gamesPlayed": 30
  }'
```

---

#### Delete Player
```http
DELETE /api/players/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Player ID

**Response:** `204 No Content`

**Error Responses:**
- `404 Not Found` - Player not found

**Example:**
```bash
curl -X DELETE "http://localhost:5130/api/players/3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

---

### Tiles

#### Get All Tiles
```http
GET /api/tiles
```

**Response:** `200 OK`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "itemName": "Croissant",
    "tileValue": 2,
    "color": "#FFE4B5",
    "icon": "🥐",
    "dateCreated": "2025-11-29T15:00:00Z"
  }
]
```

**Example:**
```bash
curl -X GET "http://localhost:5130/api/tiles"
```

---

#### Get Tile by ID
```http
GET /api/tiles/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Tile ID

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "itemName": "Croissant",
  "tileValue": 2,
  "color": "#FFE4B5",
  "icon": "🥐",
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `404 Not Found` - Tile not found

---

#### Create Tile
```http
POST /api/tiles
```

**Request Body:**
```json
{
  "itemName": "Croissant",
  "tileValue": 2,
  "color": "#FFE4B5",
  "icon": "🥐"
}
```

**Validation Rules:**
- `itemName`: Required, max 100 characters
- `tileValue`: Required, must be greater than 0
- `color`: Required, valid hex color format (#RGB or #RRGGBB)
- `icon`: Optional, max 500 characters

**Response:** `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "itemName": "Croissant",
  "tileValue": 2,
  "color": "#FFE4B5",
  "icon": "🥐",
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `400 Bad Request` - Validation failed

**Example:**
```bash
curl -X POST "http://localhost:5130/api/tiles" \
  -H "Content-Type: application/json" \
  -d '{
    "itemName": "Croissant",
    "tileValue": 2,
    "color": "#FFE4B5",
    "icon": "🥐"
  }'
```

---

#### Update Tile
```http
PUT /api/tiles/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Tile ID

**Request Body:**
```json
{
  "itemName": "Croissant",
  "tileValue": 4,
  "color": "#FFD700",
  "icon": "🥐"
}
```

**Response:** `204 No Content`

**Error Responses:**
- `400 Bad Request` - Validation failed
- `404 Not Found` - Tile not found

---

#### Delete Tile
```http
DELETE /api/tiles/{id}
```

**Path Parameters:**
- `id` (required, GUID) - Tile ID

**Response:** `204 No Content`

**Error Responses:**
- `404 Not Found` - Tile not found

---

### PowerUps

#### Get All PowerUps
```http
GET /api/powerups
```

**Response:** `200 OK`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "powerUpName": "Time Freeze",
    "description": "Freezes the timer for 10 seconds",
    "powerUpType": 0,
    "isUnlocked": true,
    "iconUrl": "https://example.com/freeze.png",
    "usageCount": 5,
    "dateCreated": "2025-11-29T15:00:00Z"
  }
]
```

**Example:**
```bash
curl -X GET "http://localhost:5130/api/powerups"
```

---

#### Get PowerUp by ID
```http
GET /api/powerups/{id}
```

**Path Parameters:**
- `id` (required, GUID) - PowerUp ID

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "powerUpName": "Time Freeze",
  "description": "Freezes the timer for 10 seconds",
  "powerUpType": 0,
  "isUnlocked": true,
  "iconUrl": "https://example.com/freeze.png",
  "usageCount": 5,
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `404 Not Found` - PowerUp not found

---

#### Create PowerUp
```http
POST /api/powerups
```

**Request Body:**
```json
{
  "powerUpName": "Time Freeze",
  "description": "Freezes the timer for 10 seconds",
  "powerUpType": 0,
  "isUnlocked": false,
  "iconUrl": "https://example.com/freeze.png",
  "usageCount": 0
}
```

**Validation Rules:**
- `powerUpName`: Required, max 100 characters
- `description`: Required
- `powerUpType`: Required, integer (0-3)
- `iconUrl`: Optional, max 500 characters

**PowerUp Types:**
- `0` - ScoreMultiplier
- `1` - TimeFreeze
- `2` - UndoMove
- `3` - Hint

**Response:** `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "powerUpName": "Time Freeze",
  "description": "Freezes the timer for 10 seconds",
  "powerUpType": 0,
  "isUnlocked": false,
  "iconUrl": "https://example.com/freeze.png",
  "usageCount": 0,
  "dateCreated": "2025-11-29T15:00:00Z"
}
```

**Error Responses:**
- `400 Bad Request` - Validation failed

**Example:**
```bash
curl -X POST "http://localhost:5130/api/powerups" \
  -H "Content-Type: application/json" \
  -d '{
    "powerUpName": "Time Freeze",
    "description": "Freezes the timer for 10 seconds",
    "powerUpType": 0,
    "isUnlocked": false,
    "iconUrl": "https://example.com/freeze.png",
    "usageCount": 0
  }'
```

---

#### Update PowerUp
```http
PUT /api/powerups/{id}
```

**Path Parameters:**
- `id` (required, GUID) - PowerUp ID

**Request Body:**
```json
{
  "powerUpName": "Time Freeze",
  "description": "Freezes the timer for 15 seconds",
  "powerUpType": 0,
  "isUnlocked": true,
  "iconUrl": "https://example.com/freeze.png",
  "usageCount": 10
}
```

**Response:** `204 No Content`

**Error Responses:**
- `400 Bad Request` - Validation failed
- `404 Not Found` - PowerUp not found

---

#### Delete PowerUp
```http
DELETE /api/powerups/{id}
```

**Path Parameters:**
- `id` (required, GUID) - PowerUp ID

**Response:** `204 No Content`

**Error Responses:**
- `404 Not Found` - PowerUp not found

---

## Error Handling

All endpoints follow standard HTTP status codes:

- `200 OK` - Successful GET request
- `201 Created` - Successful POST request
- `204 No Content` - Successful PUT/DELETE request
- `400 Bad Request` - Invalid request data or validation errors
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

**Error Response Format:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Email": ["The Email field is required."]
  }
}
```