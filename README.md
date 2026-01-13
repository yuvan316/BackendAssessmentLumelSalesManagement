# Data Refresh API

## Overview

The Data Refresh API provides endpoints to refresh database data.

## Prerequisites

- .NET 8
- SQL Server
- Entity Framework Core 8.0.22


## API Endpoints

### 1. Refresh Database

```http
POST /api/datarefresh/refresh?type=All
```



```http
GET /api/datarefresh/logs
```


### 3. Get Latest Refresh Log

```http
GET /api/datarefresh/logs/latest
```


## Database Schema

### RefreshLogs Table

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | GUID | Primary Key | Unique identifier for each refresh operation |
| Type | NVARCHAR(100) | NOT NULL | Refresh type: All, Orders, Customers, or Products |
| Date | DATETIME2 | NOT NULL | Timestamp of the refresh operation |
| Status | NVARCHAR(50) | NOT NULL | Operation status: Success or Failed |
| Count | INT | NOT NULL | Number of records processed |
| Message | NVARCHAR(500) | NOT NULL | Result message or error description |

## Implementation Details

### Service Layer

`IDataRefreshService` 

- `Refresh(string type)` - Executes refresh operation for specified entity type
- `GetLogs()` - Retrieves all refresh logs
- `GetLatestLog()` - Retrieves the most recent refresh log

### Controller

 `DataRefreshController` 
