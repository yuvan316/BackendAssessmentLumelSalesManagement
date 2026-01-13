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



 --DB Script used

 CREATE TABLE #SalesStaging
(
    OrderId INT,
    ProductId NVARCHAR(20),
    CustomerId NVARCHAR(20),
    ProductName NVARCHAR(200),
    Category NVARCHAR(100),
    Region NVARCHAR(50),
    DateOfSale DATE,
    QuantitySold INT,
    UnitPrice DECIMAL(10,2),
    Discount DECIMAL(5,2),
    ShippingCost DECIMAL(10,2),
    PaymentMethod NVARCHAR(50),
    CustomerName NVARCHAR(200),
    CustomerEmail NVARCHAR(200),
    CustomerAddress NVARCHAR(300)
);
INSERT INTO #SalesStaging VALUES
(1001,'P123','C456','UltraBoost Running Shoes','Shoes','North America','2023-12-15',2,180.00,0.1,10.00,'Credit Card','John Smith','johnsmith@email.com','123 Main St, Anytown, CA 12345'),
(1002,'P456','C789','iPhone 15 Pro','Electronics','Europe','2024-01-03',1,1299.00,0.0,15.00,'PayPal','Emily Davis','emilydavis@email.com','456 Elm St, Otherville, NY 54321'),
(1003,'P789','C456','Levi''s 501 Jeans','Clothing','Asia','2024-02-28',3,59.99,0.2,5.00,'Debit Card','John Smith','johnsmith@email.com','123 Main St, Anytown, CA 12345'),
(1004,'P123','C101','UltraBoost Running Shoes','Shoes','South America','2024-03-10',1,180.00,0.0,8.00,'Credit Card','Sarah Johnson','sarahjohnson@email.com','789 Oak St, New City, TX 75024'),
(1005,'P234','C789','Sony WH-1000XM5 Headphones','Electronics','North America','2024-04-22',1,349.99,0.15,12.00,'PayPal','Emily Davis','emilydavis@email.com','456 Elm St, Otherville, NY 54321'),
(1006,'P456','C101','iPhone 15 Pro','Electronics','Asia','2024-05-18',2,1299.00,0.05,20.00,'Debit Card','Sarah Johnson','sarahjohnson@email.com','789 Oak St, New City, TX 75024');
INSERT INTO ProductCategories(CategoryName)
SELECT DISTINCT s.Category
FROM #SalesStaging s
WHERE NOT EXISTS (
    SELECT 1
    FROM ProductCategories pc
    WHERE pc.CategoryName = s.Category
);
INSERT INTO ProductDetails(ProductId, ProductName, CategoryId, UnitPrice)
SELECT DISTINCT
    s.ProductId,
    s.ProductName,
    pc.CategoryId,
    s.UnitPrice
FROM #SalesStaging s
JOIN ProductCategories pc
    ON pc.CategoryName = s.Category
WHERE NOT EXISTS (
    SELECT 1
    FROM ProductDetails p
    WHERE p.ProductId = s.ProductId
);
INSERT INTO CustomerDetails(CustomerId, CustomerName, CustomerEmail, CustomerAddress)
SELECT DISTINCT
    s.CustomerId,
    s.CustomerName,
    s.CustomerEmail,
    s.CustomerAddress
FROM #SalesStaging s
WHERE NOT EXISTS (
    SELECT 1
    FROM CustomerDetails c
    WHERE c.CustomerId = s.CustomerId
);
INSERT INTO [OrderDetails]
(
    OrderId,
    ProductId,
    CustomerId,
    Region,
    DateOfSale,
    Quantity,
    UnitPrice,
    Discount,
    ShippingCost,
    PaymentMethod
)
SELECT
    s.OrderId,
    s.ProductId,
    s.CustomerId,
    s.Region,
    s.DateOfSale,
    s.QuantitySold,
    s.UnitPrice,
    s.Discount,
    s.ShippingCost,
    s.PaymentMethod
FROM #SalesStaging s
WHERE NOT EXISTS (
    SELECT 1
    FROM [OrderDetails] o
    WHERE o.OrderId = s.OrderId
);
Drop table #SalesStaging;

