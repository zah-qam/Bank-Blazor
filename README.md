# Overview
"BankBlazor" is a web-based banking application built with "Blazor WebAssembly" and "ASP.NET Core Web API". It simulates core banking features such as:

- Viewing customer information and associated accounts.
- Depositing and withdrawing money from an account.
- Transferring money between accounts.
- Viewing transaction history per account.

This project is intended as an educational exercise to learn component-based frontend development in Blazor and structured API interaction.

Technologies Used

--Client (Frontend):

-Blazor WebAssembly.
-Bootstrap: for styling
-HttpClien: for communication with the backend
-Newtonsoft.Json: for JSON serialization/deserialization

--Server (Backend):

-ASP.NET Core Web API.
-Entity Framework Core.
-SQL Server: for data persistence

Features

--Customer Page

-Displays detailed information about a hardcoded customer (CustomerId = 1)
-Lists all associated accounts
-Buttons for:

  - Making a deposit
  - Making a withdrawal
  - Transferring funds

--Deposit/Withdraw/Transfer

- Each operation has its own page (Deposit, Withdraw, Transfer)
- Form inputs to specify the amount (and target account for transfers)
- Operates on a (hardcoded customer), but account IDs are passed via URL

--Transaction History

-Displays a table of transactions for the selected account
-Columns include: date, type, operation, amount, and resulting balance

--How to Run

1. Clone the repository

   git clone https://github.com/zah-qam/BankBlazor
   

2. Run the backend (Web API):

   -Open the "BankBlazor.API" project in Visual Studio.
   -Press F5 to start the API (usually runs at => https://localhost:7155).

3. Run the frontend (Blazor WebAssembly):

   -Open the "BankBlazor.Client" project in Visual Studio.
   -Press F5 to launch it in your browser (usually => https://localhost:7249).

4. Navigate to the customer page:
   
   https://localhost:7249/customer/1

## Run with Docker (VS Code / terminal)

Why: this repo has 3 moving parts (Blazor UI, Web API, SQL Server). Docker Compose starts them together and wires networking so the client can call the API and the API can reach the database.

1. Install Docker Desktop (Windows) and make sure it’s running.

2. From the repo root, create a `.env` file (this keeps secrets out of source control):

   - Copy `.env.example` to `.env`
   - Set `SA_PASSWORD` to a strong password

   Notes:
   - `.env` is ignored by git, so each developer sets their own local `SA_PASSWORD` once and keeps it on their machine.
   - SQL Server enforces password complexity for `SA_PASSWORD` (generally: at least 8 chars, and include upper/lowercase, a number, and a symbol).

3. Start everything:

   docker compose up --build

4. Open the app:

   http://localhost:8080/customer/1

Notes:
- The web container serves the Blazor static files and reverse-proxies `/api/*` to the API container, so the client uses relative `/api/...` URLs (no hardcoded localhost ports).
- SQL Server runs in a container and stores its data in a named Docker volume (`mssql-data`).
 - If you change `SA_PASSWORD` after the database volume has been created, you may need to recreate the volume:

    docker compose down -v
    docker compose up --build
   
-Implementation Notes

 -All navigation is based on "NavLink" components in Blazor.
 -Routes use parameters, for example:
  /transactions/{accountId} => displays transaction history for an account
 -The customer and account IDs are currently hardcoded for demonstration
 -Data is fetched using "HttpClient" and deserialized using "Newtonsoft.Json"
 -Buttons on the customer page link to separate pages for each transaction type

 -Developed by a student in a system development program with a focus on "Industrial IT Security".
 -Created as part of a course assignment using C#, Blazor, and Web API.

# Setup
**Use this for DB connection** "Server=localhost;Database=BankBlazor;Trusted_Connection=True;TrustServerCertificate=true;Command Timeout=180"

**Database First Scaffolding:** Server=localhost;Database=BankBlazor;Trusted_Connection=True;TrustServerCertificate=true;Command Timeout=180" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data
