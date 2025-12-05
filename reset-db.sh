#!/bin/bash

# Reset Database Script
# This will drop and recreate the database with fresh migrations

echo "=========================================="
echo "Reset Database"
echo "=========================================="
echo ""
echo "⚠️  WARNING: This will delete ALL data!"
echo ""
read -p "Are you sure you want to reset the database? (yes/no): " confirm

if [ "$confirm" != "yes" ]; then
  echo "Aborted."
  exit 0
fi

echo ""
echo "Stopping containers..."
docker-compose down -v

echo ""
echo "Starting containers with fresh database..."
docker-compose up -d --build

echo ""
echo "Waiting for database to be ready..."
sleep 8

echo ""
echo "✓ Database reset complete!"
echo "You can now run ./test-api.sh to populate with sample data"
echo ""
