# ICS - Activity tracking app
(ICS = C# class in BUT FIT)

## Goal
The goal was to create a usable and easily expandable application that meets the requirements of the [assignment](https://github.com/nesfit/ICS/tree/master/Project) (in Czech though). The application must not crash or freeze. If the user fills in something incorrectly, he is notified with a validation message.

## Used technology
- .NET 7.0
- MAUI
- Entity Framework Core
- SQLite
- Fody

## Architecture
Solution is divided into 3 app projects:
- DAL - Data Access Layer
- BL - Bussiness Layer
- App - UI

And 3 test projects:
- Common.Tests
- DAL.Tests
- BL.Tests

## Basic functionality (from project assingment)
- The application must allow CRUD operations to be performed on all data.
- The application is controlled from the perspective of the selected user when the application is launched.
- User can create other users.
- User can edit information about himself.
- User can add an activity record (he will be listed as the person performing the activity).
- The user sees the list of projects and can log in to the project.
- User can filter activities by start and end.
- The user can filter the activities in a user-friendly way without entering the date for the last week, month, previous month and year.
- A user can only perform one activity at a time. Thus, the recorded activities must not overlap.