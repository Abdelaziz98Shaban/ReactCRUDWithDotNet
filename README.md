# LuftStore CRUD Application

## Introduction

This project is a CRUD (Create, Read, Update, Delete) application built with .NET Core for the backend and React for the frontend. It allows users to manage categories and products in an online store.

## Setup Instructions

### Backend (API)

1. **Database Setup:**
   - Create a SQL Server database named `LuftStore`.
   - Run the following command in the Package Manager Console (PMC) to apply the migration and update the database schema:
     ```
     Update-Database -context applicationdbcontext
     ```

2. **Run Backend API:**
   - Open the solution in Visual Studio.
   - Set the API project (`LuftStore.API`) as the startup project.
   - Run the project (press F5 or click the Run button).
   - The backend API will be hosted at `http://localhost:5027`.

### Frontend (React)

1. **Install Dependencies:**
   - Navigate to the `ClientApp` directory.
   - Run `npm install` to install the necessary dependencies.

2. **Run React App:**
   - After installing dependencies, run `npm start`.
   - The React app will be hosted at `http://localhost:3000`.

## Usage

- Once both the backend API and frontend React app are running, you can navigate to `http://localhost:3000` in your web browser to access the application.
- The application allows you to perform CRUD operations on categories and products.
- Use the provided UI to create, read, update, and delete categories and products.

## Technologies Used

- Backend:
  - .NET Core
  - Entity Framework Core
  - SQL Server

- Frontend:
  - React
  - React Router


## Contributing

Contributions are welcome! If you find any issues or would like to suggest improvements, please open an issue or create a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
