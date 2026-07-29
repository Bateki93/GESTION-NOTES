# GestionNotes

Application de gestion des notes scolaires avec API backend .NET 10 et frontend Blazor WebAssembly.

## Architecture

- **GestionNotes.Core** — Modèles et interfaces (services, stores)
- **GestionNotes.Application** — Logique métier et validateurs
- **GestionNotes.Infrastructure** — Entités EF Core, stores, profils AutoMapper, migrations
- **GestionNotes.Api** — API REST (port 5081)
- **GestionNotes.Blazor** — Frontend Blazor WebAssembly

## Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL (14+)

## Configuration de la base de données

1. Créez une base de données PostgreSQL :

```bash
createdb GestionNotesDb
```

2. Modifiez la chaîne de connexion dans `GestionNotes.Api/appsettings.json` :

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=GestionNotesDb;Username=votre_user;Password=votre_mot_de_passe"
}
```

3. Les migrations s'appliquent automatiquement au premier lancement de l'API (`db.Database.Migrate()`).

## Lancement

### 1. API (backend)

```bash
dotnet run --project GestionNotes.Api
```

L'API démarre sur **http://localhost:5081**.
Swagger disponible sur **http://localhost:5081/swagger**.

### 2. Blazor (frontend)

Ouvrez un second terminal :

```bash
dotnet run --project GestionNotes.Blazor
```

Le frontend Blazor pointe sur l'API via `ApiBaseUrl` défini dans `GestionNotes.Blazor/wwwroot/appsettings.json`.

### Accès à l'application

1. Ouvrez le navigateur sur l'URL du Blazor (généralement http://localhost:5000 ou http://localhost:5270)
2. **Inscrivez-vous** via la page Register (un rôle Admin ou Élève vous sera attribué)
3. **Connectez-vous** avec vos identifiants

## Démarrage rapide (développement)

```bash
# Terminal 1 — API
cd GestionNotes
dotnet run --project GestionNotes.Api

# Terminal 2 — Frontend
dotnet run --project GestionNotes.Blazor
```

## Stack technique

- .NET 10 — Clean Architecture
- PostgreSQL + Entity Framework Core
- JWT Bearer Authentication
- AutoMapper + FluentValidation
- Blazor WebAssembly
