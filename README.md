# FlexiDesk---Smart-Office-Management-System
# 1. Project Overview: FlexiDesk

FlexiDesk is a high-performance, scalable Co-working Space Management System built with .NET 8/9 and SQL Server. It solves the core business challenge of efficiently managing shared office resources (desks, meeting rooms, private pods) while preventing double-bookings and optimizing space utilization.

## Key Business Goals

1. **Zero-Conflict Scheduling**: Guaranteed atomic reservations using SQL transactions.
2. **High Performance**: Optimized for fast searches even with thousands of concurrent bookings.
3. **Data Integrity**: Strict validation of business rules (no past bookings, no overlaps).

---

# 2. Technical Stack & Architecture

This project demonstrates senior-level architectural patterns:

- **Clean Architecture**: Strict separation between Domain, Application, Infrastructure, and API layers.
- **Persistence**: EF Core (Code-First) for CRUD operations + Dapper for high-performance reporting.
- **Repository & Unit of Work**: Decoupling business logic from data access for maximum testability.
- **Validation**: Fluent validation and domain-level logic checks.
- **Logging & Observability**: Structured logging with Serilog and Correlation IDs.

---

# 3. Core Features

- **Resource Management**: Categorization of spaces (Desks vs. Meeting Rooms) with dynamic pricing.
- **Smart Search**: Filter available resources by date/time range using covering indexes.
- **Concurrency Handling**: Implementation of database transactions to handle race conditions when two users click "Book" at the same instant.
- **User Dashboard**: View personal booking history with optimized eager loading (no N+1 problem).

---

# 4. Database Design & Optimization

The system is built on a robust SQL schema. Key optimizations include:

- **Covering Index**: An index on `(ResourceId, StartTime, EndTime)` allows the "Check Availability" query to run entirely in memory without hitting the main table rows.
- **Normalization**: 3rd Normal Form (3NF) to ensure data consistency.
- **Soft Deletes**: Resources are never fully removed; they are marked `IsDeleted` to preserve booking history for analytics.

---

# 5. How to Run & Test

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server (LocalDB or Docker)

## Installation

1. Clone the repo:
   ```bash
   git clone https://github.com/yourusername/FlexiDesk.git



   
