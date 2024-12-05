# WAD

# Fitness Tracker

## Overview
The **Fitness Tracker** is a web application created to fullfill the WAD module requirements. The application allows users to log activities, set future fitness goals, and view their activity history. It is built using **Angular** for the frontend and **ASP.NET Core** for the backend, with **Entity Framework Core** for database management.

This project is part of my coursework for **Web Application Development** at the university and is designed to showcase CRUD functionality with a user authentication system.

## Features
- **User Authentication**: Users can sign up, log in, and securely store their credentials.
- **Activity Logging**: Users can log activities such as running, cycling, swimming, etc.
- **Future Goals**: Users can set fitness goals with target dates.
- **User-Specific Data**: All activities and goals are associated with the authenticated user.
- **Responsive Design**: The frontend is built to be responsive

## Technologies Used

| Technology      | Description                                      |
|-----------------|--------------------------------------------------|
| **Frontend**    | Angular, Bootstrap, TypeScript                  |
| **Backend**     | ASP.NET Core, Entity Framework Core             |
| **Database**    | SQL Server                                      |
| **Authentication** | JWT (JSON Web Tokens)                        |
| **Tools**       | Visual Studio Code                              |

## Requirements
- **.NET SDK** (for backend API)
- **Node.js** and **npm** (for frontend Angular application)
- **SQL Server** or a similar database system


## API Endpoints

| Method   | Endpoint                     | Description                                             |
|----------|------------------------------|---------------------------------------------------------|
| **POST** | `/api/Auth/login`             | Login with username and password.                       |
| **POST** | `/api/Auth/signup`            | Sign up a new user with username, email, and password.  |
| **POST** | `/api/Auth/verify`            | Verify the JWT token sent by the user.                  |
| **GET**  | `/api/Activity`               | Get a list of activities for the logged-in user.        |
| **GET**  | `/api/Activity/{id}`          | Get a specific activity by ID.                          |
| **POST** | `/api/Activity`               | Create a new activity for the logged-in user.           |
| **PUT**  | `/api/Activity/{id}`          | Update an existing activity.                            |
| **DELETE**| `/api/Activity/{id}`         | Delete an activity by ID.                               |
| **GET**  | `/api/FutureGoal`             | Get all future goals for the logged-in user.            |
| **POST** | `/api/FutureGoal`             | Create a new future goal.                               |
| **PUT**  | `/api/FutureGoal/{id}`        | Update a specific future goal.                          |
| **DELETE**| `/api/FutureGoal/{id}`       | Delete a future goal by ID.                             |


## Folder Structure

### Backend (ASP.NET Core)

| Folder/Files            | Description                                                       |
|-------------------------|-------------------------------------------------------------------|
| **Controllers**          | Contains API controllers like `ActivityController`, `FutureGoalController`, and `AuthController`. |
| **Models**               | Contains models like `User`, `Activity`, `FutureGoal`.            |
| **DTOs**                 | Contains DTO classes to transfer data between the client and server, e.g., `UserDto`, `ActivityDto`, `FutureGoalDto`. |
| **Data**                 | Contains the `ApplicationDbContext` for interacting with the database. |
| **Migrations**           | Contains the Entity Framework migrations files for database updates. |
| **Properties**           | Contains `launchSettings.json` for debugging and configuration.  |
| **wwwroot**              | Contains static files (e.g., images, JavaScript, CSS) for serving from the server. |


## Conclusion

The **Fitness Tracker** web application provides an effective way for users to track their fitness activities and set personal goals.
Overall, the **Fitness Tracker** app demonstrates key principles of web application development, including:
- **Full-stack development**: Combining frontend and backend technologies to build a complete application.
- **Authentication & Authorization**: Securing endpoints with JWT tokens to ensure user-specific data handling.
- **Database Management**: Using **SQL Server** and **Entity Framework Core** to interact with the database and handle CRUD operations.
- **Responsive Design**: Creating a mobile-friendly frontend with **Angular** and **Bootstrap** to provide an optimal user experience across devices.
