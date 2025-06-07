# Quotation Management System

A web-based quotation management system built with ASP.NET Core that allows employees to prepare quotations and customers to view and respond to them.

## Features

### Employee Features (I4)
- Login with employee credentials
- View a dashboard of all quotation requests
- See statistics of New, Prepared, Accepted, and Rejected quotations
- Select and prepare quotations by:
  - Setting service rates
  - Applying discounts
  - Adding comments
  - Calculating total prices
- Track quotation status and customer responses

### Customer Features (I6)
- Login with customer credentials
- View list of quotations prepared by employees
- Review quotation details including:
  - Service rates
  - Applied discounts
  - Total prices
  - Comments from employees
- Accept or reject quotations
- Track response history

## Technology Stack
- ASP.NET Core 6.0
- Entity Framework Core with SQLite
- Bootstrap 5
- Font Awesome Icons
- Razor Pages

## Getting Started

### Prerequisites
- .NET 6.0 SDK
- Visual Studio 2022 or VS Code

### Test Credentials

1. Employee Account:
   - Email: employee@test.com
   - Password: Test123!

2. Customer Account:
   - Email: customer@test.com
   - Password: Test123!

### Running the Application


   dotnet restore
   dotnet ef database update
   dotnet run
   ```
4. Open a browser and navigate to `http://localhost:5211`

## Project Structure

- `/Pages` - Razor Pages for the web interface
- `/BusinessLogic` - Business logic and entities
- `/DataAccess` - Data access layer with Entity Framework
- `/Application` - Application services
- `/wwwroot` - Static files (CSS, JS, images)

## Database Schema

### Users Table
- Id (Primary Key)
- FirstName
- FamilyName
- EmailAddress
- PhoneNumber
- CompanyName
- Address
- Password
- Role

### Quotations Table
- Id (Primary Key)
- clientName
- dateIssued
- status
- Rate
- Discount
- TotalPrice
- CustomerDecision
- Comments
- PreparedBy
- CustomerResponseDate

## License
MIT License
