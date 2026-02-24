# RaceIntel

RaceIntel is a real-time racing statistics platform providing live telemetry, driver tracking, and performance analytics for NASCAR and Formula 1.

## 🏁 Overview

The platform aggregates data from various racing APIs, providing a centralized dashboard for enthusiasts and analysts. It features live polling for real-time race events, historical data storage, caching for performance, and a modern web interface.

## 🚀 Tech Stack

### Backend
- **Framework:** .NET 10.0 (ASP.NET Core)
- **Data Polling:** Background Services with `HttpClient`
- **Caching:** In-memory caching for live feeds
- **Database:** PostgreSQL 17

### Frontend
- **Framework:** Next.js 16 (App Router)
- **Library:** React 19
- **Styling:** Tailwind CSS 4
- **Language:** TypeScript

### Infrastructure
- **Containerization:** Docker & Docker Compose

## 📁 Project Structure

```text
.
├── backend/            # ASP.NET Core API
│   ├── Admin/          # Secured admin endpoints for data import
│   │   └── Import/     # Controllers and models for importing historical data
│   ├── Data/           # Entity Framework Core DbContext and Entities
│   │   └── Entities/   # Database models (e.g., NascarRaceListBasicYear, NascarWeekendFeed)
│   ├── Nascar/         # NASCAR specific controllers and services
│   │   ├── Models/     # API response models for live and historical feeds
│   │   └── Services/   # API clients, caching, and background polling services
│   ├── F1/             # F1 specific logic (planned)
│   └── Program.cs      # API configuration and service registration
├── frontend/           # Next.js web application
│   ├── app/            # App router pages and components
│   │   ├── components/ # Reusable UI components (Logo, Modal, ThemeToggle)
│   │   ├── f1/         # F1 specific pages and routes
│   │   └── nascar/     # NASCAR specific pages and routes
│   └── public/         # Static assets
└── docker-compose.yml  # Local development orchestration
```

## 🛠️ Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- (Optional) [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- (Optional) [Node.js](https://nodejs.org/)

### Quick Start with Docker

1. Clone the repository.
2. Copy `.env.example` to `.env` and configure your environment variables (e.g., `Admin__Key`, `POSTGRES_PASSWORD`).
3. Run the following command:
   ```bash
   docker-compose up --build
   ```
4. Access the applications:
   - **Frontend:** [http://localhost](http://localhost)
   - **Backend API:** [http://localhost:8080](http://localhost:8080)
   - **NASCAR Live Feed:** [http://localhost:8080/api/nascar/live](http://localhost:8080/api/nascar/live)

## ✨ Features

- **Live NASCAR Tracking:** Automated polling of official NASCAR feeds with configurable intervals.
- **Historical Data Import:** Secured admin endpoints to import and store historical race lists and weekend feeds in PostgreSQL.
- **Race Snapshots:** Quick view of leaders, status, and lap counts.
- **Strategic Signals:** Insights into fuel burn, tire delta, and pit windows.
- **Multi-Series Support:** Dashboard prepared for both NASCAR and Formula 1 data.

## 🛠 Development

### Backend
To run the backend locally without Docker:
```bash
cd backend
dotnet run
```

### Frontend
To run the frontend locally in development mode:
```bash
cd frontend
npm install
npm run dev
```
