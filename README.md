# LegalContractsApi

A web application for managing legal contracts, developed with .NET 8 on the backend and optionally Vue 3 on the frontend.

---

## 🧩 API Features

* Create a legal contract (`POST /api/contracts`)
* Get all contracts (`GET /api/contracts/all`)
* Get all contracts (paginated version) (`GET /api/contracts?pageNumber={pageNumber}&pageSize={pageSize}`)
* Get a contract by ID (`GET /api/contracts/{id}`)
* Update a contract (`PUT /api/contracts/{id}`)
* Delete a contract (`DELETE /api/contracts/{id}`)

Each contract contains:

* A unique identifier (GUID)
* Author name
* Legal entity name
* Free-text description
* Creation and last update dates

---

## ⚙️ Technologies Used

* .NET 8
* Entity Framework Core (SQL Server)
* Swagger/OpenAPI
* xUnit + Moq (unit testing)

---

## 🚀 How to Run the Project Locally

### Prerequisites

* .NET 8 SDK
* SQL Server or LocalDB (Windows)
* Visual Studio or VS Code (recommended)

### Clone the Repository

```bash
git clone https://github.com/dancedisaster/LegalContractsApi.git
cd LegalContractsApi
```

### Restore Packages & Apply Migrations

```bash
dotnet restore
dotnet ef database update
```

```bash
Every time you have to add a new migration, run (Each time you need to add a new migration, use a naming convention such as V001, V002, and so on):
	dotnet ef migrations add <MigrationName>
```	

### Run the API

```bash
dotnet run
```

Access Swagger at: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## 🧪 Running Tests

```bash
cd LegalContractsApi
dotnet test
```

---

## 📁 Project Structure

```
LegalContractsApi/
├── Controllers/
├── DTOs/
├── Models/
├── Repositories/
├── Services/
├── Data/
└── Program.cs
```

---

## 📌 Notes

* The project uses EF Core with SQL Server but can be adapted to SQLite/PostgreSQL easily.
* The app is designed with clean architecture principles and is testable.
* No authentication is implemented — focus is on clean CRUD functionality.

---

## 📬 Contact

For questions, suggestions, or contributions, open a GitHub Issue or Pull Request.

---

Developed by \Henrique Mendes · \ July 2025
