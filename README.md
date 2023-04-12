# Employee API tutorial

1. Download & install [docker]("https://www.docker.com/products/docker-desktop/")
2. Turn on `Docker Desktop`
3. Copy source code from this repository to Your computer (`Code` -> `Download ZIP` -> Extract files)
4. Open the downloaded folder (with `.sln` and `docker-compose.yml` files) 
5. In this folder, open preferred terminal (bash, cmd, PowerShell, etc.)
4. Run `docker-compose up`
5. Wait until `employeeapi` container appears and starts up
6. For UI, in your browser, navigate to `http://localhost:5000/swagger/index.html`. Endpoints are listed below.
7. With Swagger UI, you should see Employee API

## Demo

### POST request body example (add new employee)

```
{
  "firstName": "New",
  "lastName": "Employee",
  "birthDate": "2000-03-22T05:14:25.624Z",
  "homeAddress": "string",
  "currentSalary": 1000,
  "positionName": "QA"
}
```

### PUT request body example (update existing employee)

```
{
  "firstName": "NewName",
  "lastName": "NewLastName",
  "birthDate": "1998-03-29T08:17:15.237Z",
  "homeAddress": "Some address 123",
  "currentSalary": 990
}
```

### POST request body example (add new role)

```
{
  "position": "InformationAnalist",
  "description": "Writes user stories for developers.",
  "hoursPerWeek": 30
}
```

### PUT request body example (update role)

```
{
  "position": "QA",
  "description": "Tests software.",
  "hoursPerWeek": 30
}
```

### Endpoints

```
GET http://localhost:5000/api/employees
POST http://localhost:5000/api/employees + request body
GET (by position) http://localhost:5000/{position}
PUT http://localhost:5000/{employeeId} + request body
DELETE http://localhost:5000/{id}
```