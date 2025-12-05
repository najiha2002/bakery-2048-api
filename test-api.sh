#!/bin/bash

# Bakery 2048 API Test Script
# Usage: ./test-api.sh

BASE_URL="http://localhost:5130"

echo "=========================================="
echo "Bakery 2048 API Test Script"
echo "=========================================="
echo ""

# Array of sample players with stats
declare -a PLAYERS=(
  "alice:alice@example.com:8500:7200:512:25:5"
  "bob:bob@example.com:12000:9800:1024:40:8"
  "charlie:charlie@example.com:6200:4500:256:18:3"
  "diana:diana@example.com:15000:12000:2048:60:12"
  "emma:emma@example.com:4800:3200:128:12:2"
  "frank:frank@example.com:9500:8000:512:35:7"
  "grace:grace@example.com:11200:9500:1024:45:9"
)

# Test 1: Register and setup 7 sample players
echo "1. Creating 7 sample players with stats..."
PLAYER_IDS=()
PLAYER_TOKENS=()

for i in "${!PLAYERS[@]}"; do
  IFS=':' read -r username email highScore currentScore bestTile games winStreak <<< "${PLAYERS[$i]}"
  
  echo "  Creating player: $username"
  
  # Register player
  REGISTER=$(curl -s -X POST "$BASE_URL/api/auth/register" \
    -H "Content-Type: application/json" \
    -d "{
      \"username\": \"$username\",
      \"email\": \"$email\",
      \"password\": \"Pass123!\"
    }")
  
  # Login player
  LOGIN=$(curl -s -X POST "$BASE_URL/api/auth/login" \
    -H "Content-Type: application/json" \
    -d "{
      \"username\": \"$username\",
      \"password\": \"Pass123!\"
    }")
  
  # Extract token and player ID
  PLAYER_TOKEN=$(echo $LOGIN | grep -o '"token":"[^"]*' | cut -d'"' -f4)
  PLAYER_TOKENS[$i]=$PLAYER_TOKEN
  
  # Get player ID
  PLAYER_ID=$(curl -s -X GET "$BASE_URL/api/players" \
    -H "Authorization: Bearer $PLAYER_TOKEN" | jq -r ".[0].id")
  PLAYER_IDS[$i]=$PLAYER_ID
  
  # Update player stats
  curl -s -X PUT "$BASE_URL/api/players/$PLAYER_ID" \
    -H "Authorization: Bearer $PLAYER_TOKEN" \
    -H "Content-Type: application/json" \
    -d "{
      \"highestScore\": $highScore,
      \"currentScore\": $currentScore,
      \"bestTileAchieved\": $bestTile,
      \"gamesPlayed\": $games,
      \"winStreak\": $winStreak
    }" > /dev/null
  
  echo "    ✓ $username created (Score: $highScore, WinStreak: $winStreak)"
done

echo ""
echo "All 7 players created successfully!"
echo ""

# Use first player's token for remaining tests
TOKEN="${PLAYER_TOKENS[0]}"

# Test 2: Get all players
echo "2. Getting all players..."
curl -s -X GET "$BASE_URL/api/players" \
  -H "Authorization: Bearer $TOKEN" | jq
echo ""

# Test 3: Get all tiles
echo "3. Getting all tiles..."
curl -s -X GET "$BASE_URL/api/tiles" \
  -H "Authorization: Bearer $TOKEN" | jq
echo ""

# Test 4: Register admin
echo "4. Registering admin user..."
ADMIN_REGISTER=$(curl -s -X POST "$BASE_URL/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admintest",
    "email": "admintest@example.com",
    "password": "Admin123!",
    "role": "Admin"
  }')

if echo "$ADMIN_REGISTER" | grep -q "error\|already exists"; then
  echo "Admin already exists or error occurred, trying to login with existing admin..."
  echo "Response: $ADMIN_REGISTER"
else
  echo "Admin registered successfully"
fi
echo ""

# Test 5: Admin login
echo "5. Admin logging in..."
ADMIN_LOGIN=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admintest",
    "password": "Admin123!"
  }')

if echo "$ADMIN_LOGIN" | grep -q "token"; then
  echo "Admin login successful!"
  ADMIN_TOKEN=$(echo $ADMIN_LOGIN | grep -o '"token":"[^"]*' | cut -d'"' -f4)
  echo "Admin token extracted: ${ADMIN_TOKEN:0:50}..."
else
  echo "Admin login failed!"
  echo "Response: $ADMIN_LOGIN"
  ADMIN_TOKEN=""
fi
echo ""

# Test 6: Create new tile (Admin only)
echo "6. Creating new tile (Admin only)..."
if [ -z "$ADMIN_TOKEN" ]; then
  echo "Skipped: No admin token available"
else
  TILE_CREATE=$(curl -s -X POST "$BASE_URL/api/tiles" \
    -H "Authorization: Bearer $ADMIN_TOKEN" \
    -H "Content-Type: application/json" \
    -d '{
      "itemName": "Mega Cake",
      "tileValue": 1024,
      "color": "#FF6B6B",
      "icon": "🎂"
    }')
  
  if echo "$TILE_CREATE" | grep -q "error\|already exists"; then
    echo "Tile creation failed or already exists:"
    echo "$TILE_CREATE" | jq
  else
    echo "Tile created successfully:"
    echo "$TILE_CREATE" | jq
  fi
fi
echo ""

# Test 7: Get top 5 players (sorted by highest score)
echo "7. Getting top 5 players..."
curl -s -X GET "$BASE_URL/api/players?top=5" \
  -H "Authorization: Bearer $TOKEN" | jq
echo ""

# Test 8: Get specific player by ID with WinStreak
echo "8. Getting player by ID (checking WinStreak field)..."
FIRST_PLAYER_ID="${PLAYER_IDS[0]}"
curl -s -X GET "$BASE_URL/api/players/$FIRST_PLAYER_ID" \
  -H "Authorization: Bearer $TOKEN" | jq
echo ""

echo "=========================================="
echo "Test completed!"
echo "=========================================="
