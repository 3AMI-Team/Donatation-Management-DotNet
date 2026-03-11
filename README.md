## Base URL

All endpoints are relative to the base URL of the API server.  
Example: `https://api.example.com`

---

## Employees

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | Retrieve a list of all employees |
| GET | `/api/employees/{id}` | Retrieve details of a specific employee by ID |
| POST | `/api/employees` | Create a new employee |
| PUT | `/api/employees/{id}` | Update an existing employee by ID |
| DELETE | `/api/employees/{id}` | Delete an employee by ID |
| GET | `/api/employees/{id}/registered-cases` | Get all cases registered by a specific employee |
| GET | `/api/employees/{id}/handled-distributions` | Get all distributions handled by a specific employee |

---

## Donors

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/donors` | Retrieve a list of all donors |
| GET | `/api/donors/{id}` | Retrieve details of a specific donor by ID |
| POST | `/api/donors` | Create a new donor |
| PUT | `/api/donors/{id}` | Update an existing donor by ID |
| DELETE | `/api/donors/{id}` | Delete a donor by ID |
| GET | `/api/donors/{id}/cases` | Get all cases associated with a specific donor |

---

## Categories

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Retrieve a list of all categories |
| GET | `/api/categories/{id}` | Retrieve details of a specific category by ID |
| POST | `/api/categories` | Create a new category |
| PUT | `/api/categories/{id}` | Update an existing category by ID |
| DELETE | `/api/categories/{id}` | Delete a category by ID |
| GET | `/api/categories/{id}/cases` | Get all cases belonging to a specific category |

---

## Cases

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/cases` | Retrieve a list of all cases |
| GET | `/api/cases/{id}` | Retrieve details of a specific case by ID |
| POST | `/api/cases` | Create a new case |
| PUT | `/api/cases/{id}` | Update an existing case by ID |
| DELETE | `/api/cases/{id}` | Delete a case by ID |
| GET | `/api/cases/{id}/distributions` | Get all distributions made to a specific case |
| GET | `/api/cases/{id}/remaining` | Get the remaining amount needed for a case |
| GET | `/api/cases/{id}/isfunded` | Check whether a case is fully funded |

---

## Distributions

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/distributions` | Retrieve a list of all distributions |
| GET | `/api/distributions/{id}` | Retrieve details of a specific distribution by ID |
| POST | `/api/distributions` | Create a new distribution |
| PUT | `/api/distributions/{id}` | Update an existing distribution by ID |
| DELETE | `/api/distributions/{id}` | Delete a distribution by ID |
| GET | `/api/distributions/bycase/{caseId}` | Get all distributions for a specific case |
| POST | `/api/distributions/even` | Distribute funds evenly among multiple cases |

---

## Notes

- All endpoints accept and return JSON data.
- Replace `{id}` and `{caseId}` in the paths with the actual integer identifiers.
- For POST and PUT requests, include the appropriate request body as defined by the backend.
- Authentication may be required; refer to the authentication documentation for details.