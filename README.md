# FlowDesk

FlowDesk is a project management application built with ASP.NET Core Web API, React, TypeScript, and PostgreSQL.

It allows users to create projects, manage tasks, update task status, and assign tasks through a Kanban-style workflow.

## Tech Stack

- ASP.NET Core Web API
- React
- TypeScript
- PostgreSQL
- Entity Framework Core
- JWT Authentication
- Swagger
- Axios

## Features

### Authentication

- User registration
- User login
- JWT-based authentication
- Protected endpoints

### Projects

- Create projects
- View projects
- Project ownership support

### Tasks

- Create tasks
- View tasks by project
- Update task status
- Assign tasks to users
- Kanban workflow

## API Endpoints

### Users

```http
POST /api/users
POST /api/users/login
GET  /api/users
```

### Projects

```http
POST /api/projects
GET  /api/projects
```

### Tasks

```http
POST /api/projects/{projectId}/tasks
GET  /api/projects/{projectId}/tasks
PUT  /api/projects/{projectId}/tasks/{taskId}/status
PUT  /api/projects/{projectId}/tasks/{taskId}/assign
```

## Database Tables

### users

```text
id
full_name
email
password_hash
```

### projects

```text
id
name
description
owner_id
created_at
```

### task_items

```text
id
title
description
status
priority
project_id
assigned_user_id
created_at
```

## Project Structure

```text
FlowDesk
│
├── FlowDesk.API
│   ├── Controllers
│   ├── DTOs
│   ├── Services
│   ├── Interfaces
│   ├── Models
│   ├── Data
│   └── Middleware
│
└── flowdesk-client
    ├── api
    ├── components
    ├── pages
    ├── routes
    └── types
```

## Running the Project

### Backend

```bash
cd FlowDesk.API
dotnet restore
dotnet ef database update
dotnet run
```

Swagger:

```text
http://localhost:5049/swagger
```

### Frontend

```bash
cd flowdesk-client
npm install
npm run dev
```

Frontend:

```text
http://localhost:5173
```

## Future Improvements

- Tailwind CSS integration
- Responsive UI
- Dashboard page
- Logout functionality
- Project member management
- Role-based authorization
- Search and filtering
- Due dates and notifications

## Author

Jaideep Vyas

GitHub:
https://github.com/JaideepVyas
