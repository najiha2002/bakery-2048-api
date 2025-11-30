## Layers
```
Client (Browser/Postman)
        ↕
Controller (API endpoint - handles HTTP requests)
        ↕
Service (Business logic - optional layer)
        ↕
Database Context (EF Core - talks to database)
        ↕
Model (Database tables/entities)

```