# Employee API tutorial

1. Download & install [docker]("https://www.docker.com/products/docker-desktop/")
2. Turn on `Docker Desktop`
3. Copy source code from this repository to Your computer (`Code` -> `Download ZIP` -> Extract files)
4. Open the downloaded folder (with `.sln` and `docker-compose.yml` files) 
5. In this folder, open preferred terminal (bash, cmd, PowerShell, etc.)
4. Run `docker-compose up`
5. Wait until `employeeapi` container appears and starts up
6. In your browser, navigate to `http://localhost:5000/swagger/index.html`
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