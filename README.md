# Destify Movie

A web application for managing movies and actors, with functionality for ratings and search.

## Prerequisites

Make sure you have the following installed:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Running the Project with Docker

Follow the steps below to set up and run the project using Docker.

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/hugobernardoni/destifyMovie.git
cd <REPOSITORY_FOLDER>
docker-compose up --build

```
This will start the following services:

 - PostgreSQL: Database for storing movies, actors, and users.

 - API (Backend): The .NET Core application for handling requests.

 - Frontend: The React application for the user interface.

### 2. Access the Application
Once the containers are up and running, you can access the application:

- Frontend: http://localhost:3000 and use
```bash
username: admin
password: Admin@123
```


- API (Swagger Documentation): http://localhost:5000/swagger

### 3. Technologies Used
- Frontend: React
- Backend: .NET Core, Entity Framework, JWT Authentication
- Database: PostgreSQL
- Containerization: Docker, Docker Compose
