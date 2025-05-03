# Bug Ticketing System API

A simple API for managing software projects, bugs, and user assignments. Supports localization and role-based access.

## Features

- **User Management**: Register and login with roles (Manager, Developer, Tester).
- **Project Management**: Create and track projects.
- **Bug Tracking**: Report bugs, assign users, and manage statuses.
- **Attachments**: Upload and manage files linked to bugs.
- **Localization**: Supports Arabic (`ar`) and other languages via `Accept-Language` header.

---

## API Endpoints

### 🔐 Authentication

| Method | Endpoint               | Description          |
|--------|------------------------|----------------------|
| `POST` | `/api/users/register`  | Register a new user  |
| `POST` | `/api/users/login`     | Login and get token  |

#### Register User
```http
POST /api/users/register
Content-Type: application/json
Accept-Language: ar

{
  "name": "Abdoo",
  "email": "abdo11@gmail.com",
  "password": "Abdo@123",
  "role": "Manager"
}
```
#### Login User
```http
POST /api/users/login
Content-Type: application/json

{
  "userName": "Abdoo",
  "password": "Abdo@123"
}
```
🗂 Project Management
Method	Endpoint	Description
POST	/api/projects	Create a new project
GET	/api/projects	List all projects
GET	/api/projects/{id}	Get project details by ID

#### Create Project
 ```http

POST /api/projects
Content-Type: application/json
Accept-Language: ar

{
  "name": "EF Project",
  "description": "EF Project Final"
}
```
🐞 Bug Management
Method	Endpoint	Description
POST	/api/bugs	Report a new bug
GET	/api/bugs	List all bugs
GET	/api/bugs/{id}	Get bug details by ID
### Create Bug
 ```http

POST /api/bugs
Content-Type: application/json
Accept-Language: ar

{
  "title": "Migration Crash",
  "description": "The migration crashes when seeding data.",
  "status": "Open",
  "projectId": "b9e062ff-3be5-4b4f-a498-57bb8969c89e"
}
```
### 👥 User-Bug Assignments
Method	Endpoint	Description
```http
POST	/api/bugs/{bugId}/assignees?userId={id}	Assign a user to a bug
DELETE	/api/bugs/{bugId}/assignees/{userId}	Remove a user from a bug
Assign User to Bug
POST /api/bugs/5EB6951B-F203-4285-523C-08DD81AB83E1/assignees?userId=1D03EC7D-05E7-4731-92DF-08DD813277D6
```

### 📎 File Attachments
Method	Endpoint	Description
```http
POST	/api/bugs/{bugId}/attachments	Upload an attachment
GET	/api/bugs/{bugId}/attachments	List all attachments
DELETE	/api/bugs/{bugId}/attachments/{fileId}	Delete an attachment
Upload Attachment
POST /api/bugs/FE94F9AF-C169-4E03-AA32-2D79DF75378C/attachments
Content-Type: multipart/form-data
# Include the file in the request body (e.g., using Postman)
```


🌍 Localization
Add the Accept-Language header to requests to get responses in your preferred language:

ar for Arabic

en for English (default)
🛠 Built With
ASP.NET Core
Entity Framework Core
JWT Authentication


🧑‍💻 Developer
by Mostafa Bolbol Ramadan

Full Stack .NET Developer | ITI Trainee

📬 Contact
For support, bugs, or feature requests, feel free to open an issue in this repository.
engmostafabr@gmail.com
## End points
![image](https://github.com/user-attachments/assets/79a90fa7-9520-4df0-b4a1-89af9b7a1cbf)
![image](https://github.com/user-attachments/assets/4ab5c78a-429f-48b2-94a1-2d577d9f1847)
