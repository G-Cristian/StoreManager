# Store Manager
 
A WPF desktop application for visual inventory and space management. Draw regions on a canvas, assign them as containers or items, navigate hierarchies, and search across your entire layout.
 
---
 
## Overview
 
Store Manager lets you map a physical space — a warehouse floor, a room, a field — by drawing regions directly onto a canvas (with an optional background image). Each region can be designated as a **container** (a space that holds other regions) or an **item** (a leaf node with inventory metadata). You can drill into containers level by level, and search across everything by name, description, or custom fields.
 
The terminology is fully configurable: "Container" could be "Room", "Zone", or "Plot" depending on your use case. No code change required — just swap a JSON profile file.
 
---
 
## Features
 
- **Drawing canvas** — lines, curves, rectangles, circles, triangles; zoom, pan, undo/redo
- **Optional background image** — photograph or floor plan as a reference layer
- **Hierarchical navigation** — drill into containers level by level with a breadcrumb trail
- **Region assignment** — link drawn shapes to named containers or items
- **Item metadata** — name, description, SKU, quantity, unit, category, custom fields
- **Search & browser** — full-text search with substring/exact-word filters; jump directly to any result on the canvas
- **UI label profiles** — rename "Container", "Item", "Region" etc. per deployment via `labels.json`
- **Save / Load** — projects saved as `.smproj` files (a ZIP of SQLite + images, no database server needed)
- **Export / Import** — same `.smproj` format for future mobile app handoff
---
 
## Tech Stack
 
| Layer | Technology |
|---|---|
| UI Framework | WPF (.NET 8) |
| Pattern | MVVM via CommunityToolkit.Mvvm |
| Database | SQLite via Microsoft.Data.Sqlite + Dapper |
| DI Container | Microsoft.Extensions.DependencyInjection |
| Testing | xUnit + Moq |
| Installer | Inno Setup |
 
---
 
## Project Structure
 
```
StoreManager/
├── StoreManager.Core/          # Shared logic — models, ViewModels, services, repositories
│   ├── Models/                 # Node, ContainerNode, ItemNode, DrawingShape, Project
│   ├── ViewModels/             # CanvasViewModel, BrowserViewModel, NavigationViewModel, ...
│   ├── Services/               # LabelService, SearchService, ProjectService, ExportImportService
│   ├── Repositories/           # INodeRepository, SqliteNodeRepository, ...
│   └── Interfaces/             # Abstractions for all services and repositories
│
├── StoreManager.Desktop/       # WPF application
│   ├── Views/                  # MainWindow, BrowserPanel, ItemDetailModal
│   ├── Controls/               # DrawingCanvas, BreadcrumbBar, RegionOverlay
│   ├── Extensions/             # LabelExtension (XAML markup extension)
│   └── Config/                 # labels.default.json, labels.construction.json, ...
│
└── StoreManager.Tests/         # xUnit unit tests for Core
```
 
> **Note:** A `StoreManager.Mobile` project (MAUI) will be added in a future phase. The Core library is designed to be shared between desktop and mobile without modification.
 
---
 
## Getting Started (Development)
 
### Prerequisites
 
- [Visual Studio 2022 Community](https://visualstudio.microsoft.com/) with the **.NET Desktop Development** workload
- .NET 8 SDK (included with the VS workload above)
- Git
### Clone and run
 
```bash
git clone https://github.com/YOUR_USERNAME/StoreManager.git
cd StoreManager
```
 
Open `StoreManager.sln` in Visual Studio, set `StoreManager.Desktop` as the startup project, and press **F5**.
 
No external database, server, or additional tools required. The SQLite database file is created automatically on first launch in the user's AppData folder.
 
---
 
## Roadmap
 
### Desktop (current)
 
- [x] Phase 0 — Environment setup
- [x] Phase 1 — Solution & project structure
- [ ] Phase 2 — Core library (models, repositories, services)
- [ ] Phase 3 — Canvas & drawing tools
- [ ] Phase 4 — Regions, containers & hierarchical navigation
- [ ] Phase 5 — Search, browser & item management
- [ ] Phase 6 — Save/load, label profiles & settings
- [ ] Phase 7 — Installer & deployment
### Mobile (planned)
 
- [ ] MAUI app — read-only canvas, search, browse, add items to existing containers
---
 
## Label Configuration
 
The app ships with multiple terminology profiles out of the box. Switch between them in **Settings → UI Labels**, or copy a `.json` file to the config directory and select it as a custom profile.
 
```json
{
  "profileName": "Construction",
  "terms": {
    "container":  "Zone",
    "containers": "Zones",
    "item":       "Asset",
    "items":      "Assets",
    "root":       "Site",
    "region":     "Area",
    "quantity":   "Count"
  }
}
```
 
Bundled profiles: `Default`, `Construction`, `Garden / Field`, `Office`. Custom profiles can be created and saved from within the app.
 
---
 
## Project File Format
 
Projects are saved as `.smproj` files — a standard ZIP archive containing:
 
```
myproject.smproj
├── project.json        # Project metadata (name, active label profile, etc.)
├── data.db             # SQLite database (nodes, shapes, item metadata)
└── images/             # Embedded background images (if any)
```
 
This format requires no database server and is portable across machines. It is also the handoff format for the future mobile app.
 
---
 
## License
 
MIT — see [LICENSE](LICENSE) for details.
