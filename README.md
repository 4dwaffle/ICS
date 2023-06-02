# ICS - Activity tracking app
(ICS = C# class in BUT FIT)

## Goal
The goal is to create a usable and easily expandable application that meets the requirements of the assignment. The application must not crash or freeze. If the user fills in something incorrectly, he is notified with a validation message.

## Used technology
- .NET 7.0
- MAUI
- EntityFramework
- SQLite

## Architecture
Solution is divided into 3 main parts:
- DAL - Data Access Layer
- BL - Bussiness Layer
- App

## Data
### User
- Name
- Surname
- Photo (URL)
- (Activities)
- (Projects)

### Activity
- Name
- Start
- End
- Tag
- Description

### Project
- Name
- Desctiption
- (Activities)
- (Users)

> () - relations between entities

