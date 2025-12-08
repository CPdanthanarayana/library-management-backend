# 📚 Library Management System - Backend API

A robust and scalable backend API for the Library Management System built with **ASP.NET Core 10**, featuring RESTful endpoints for secure authentication and efficient book management.

---

## ✨ Features

### 🔐 Authentication API

**User Registration & Login**

* Endpoint: `POST /api/auth/register`
  Validates user data
  Stores hashed passwords in SQLite
  Returns success message

* Endpoint: `POST /api/auth/login`
  Validates credentials
  Generates JWT token
  Returns authenticated user + token

### 📘 Book Management API

**Create Books:**

* Endpoint: `POST /api/books`
* Validates book data
* Stores book information in SQLite
* Returns newly created book with ID

**Read Books:**

* Get all books: `GET /api/books`
* Get single book: `GET /api/books/{id}`
* Efficient data retrieval

**Update Books:**

* Endpoint: `PUT /api/books/{id}`
* Full book information update
* Validation of update data
* Returns updated book information

**Delete Books:**

* Endpoint: `DELETE /api/books/{id}`
* Safe deletion with validation
* Proper status code responses

---

## 🛠️ Technology Stack

| Category             | Technology                        |
| -------------------- | --------------------------------- |
| Framework            | ASP.NET Core 10                   |
| Database             | SQLite with Entity Framework Core |
| Authentication       | JWT (JSON Web Token)              |
| ORM                  | Entity Framework Core             |
| Development Platform | .NET 10                           |
| API Architecture     | REST                              |

---

## 📦 Project Structure

```
├── Controllers/                 # API Controllers
│   ├── AuthController.cs        # Handles user registration & login
│   └── BooksController.cs       # Handles book-related endpoints
│
├── Models/                      # Data Models
│   ├── Book.cs                  # Book entity model
│   └── User.cs                  # User entity model
│
├── Data/                        # Database Context
│   └── AppDbContext.cs
│
├── Migrations/                  # Database Migrations
│
├── appsettings.json             # Main configuration
├── Program.cs                   # Application bootstrap

```

---

## 🚀 Getting Started

### **Prerequisites**

* .NET 10 SDK or later
* Visual Studio 2022 or later
* Git

---

### **Installation**

#### 1️⃣ Clone the repository

```
git clone https://github.com/CPdanthanarayana/library-management-backend.git
```

#### 2️⃣ Navigate to the project directory

```
cd library-management-backend
```

#### 3️⃣ Restore dependencies

```
dotnet restore
```

#### 4️⃣ Apply database migrations

```
dotnet ef database update
```

#### 5️⃣ Run the application

```
dotnet run
```

The API will be available at:

```
http://localhost:5118
```

---

### **Using Visual Studio**

* Open the solution in Visual Studio
* Restore NuGet packages
* Press **F5** to run the application

---

## 🎯 API Endpoints

### **Authentication API**

#### `POST /api/auth/register`

Creates a new user
Returns success message

#### `POST /api/auth/login`

Authenticates user
Returns JWT token

---

### **Books API**

#### `GET /api/books`

Retrieves all books
Returns: array of book objects

#### `GET /api/books/{id}`

Retrieves a specific book
Returns: single book object
404 if not found

#### `POST /api/books`

Creates a new book
Required fields: title, author
Returns: created book object

#### `PUT /api/books/{id}`

Updates an existing book
Returns: updated book object

#### `DELETE /api/books/{id}`

Deletes a book
Returns: 204 No Content
404 if not found

---

## ⚠️ Error Handling

* Proper HTTP status codes
* Detailed error messages
* Validation errors
* Global exception handling

---

## 🔧 Configuration

The application uses the following configuration in **appsettings.json**:

* Database connection string (SQLite)
* CORS policy settings
* JWT authentication settings

*(Swagger is **not** used in this project)*

---

## 🔒 Security

* JWT-based authentication
* CORS configured for the frontend
* Password hashing
* Input validation
* Entity Framework Core protects against SQL injection

---

## 🧪 Testing

Run tests (if added):

```
dotnet test
```

---

## 📄 License

This project is private for academic/assignment use.
A license may be added later if open-sourced.

---

## 👏 Acknowledgments

* ASP.NET Core team for the powerful framework
* Entity Framework Core team
* SQLite team for the lightweight database engine
