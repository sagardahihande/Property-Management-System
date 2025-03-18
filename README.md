# Property Management System

## Overview
This is a **.NET Console Application** for a **Property Management System**. It allows property managers to view, add, and manage properties while ensuring proper access control and salary calculations.

## Features
- View all properties
- View the property with the lowest value
- View currently occupied properties
- Add new properties
- Update rental prices, values, and occupancy
- Property managers can calculate their monthly salary (10% commission of rentals they manage)
- Search properties by area
- Role-based authentication for property managers

## Technologies Used
- **.NET (C#)** for the console application
- **Object-Oriented Programming (OOP)** principles
- **CQRS Pattern** (Command Query Responsibility Segregation)
- **SOLID Principles** for maintainability

## Installation and Setup
### Prerequisites
- Install **.NET SDK** from [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download)

### Steps to Run the Application
1. **Clone the repository** (or create a new folder and copy the source files).
   ```sh
   git clone <repository-url>
   cd PropertyManagementSystem
   ```
2. **Create a new console project** (if not cloned from a repository).
   ```sh
   dotnet new console -n PropertyManagementSystem
   cd PropertyManagementSystem
   ```
3. **Replace `Program.cs`** with the provided code.
4. **Run the application**.
   ```sh
   dotnet run
   ```

## Usage Guide
1. Start the application and follow the on-screen menu.
2. Enter choices to perform various property management tasks.
3. Property managers must enter valid credentials to update properties and view their salary.
4. When adding a new property, if the manager does not exist, a new one is created with a default password.



