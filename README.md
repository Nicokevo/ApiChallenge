# RESTful API Challenge: Contact Management

## **Purpose**

This project is designed to demonstrate skills in developing a RESTful API. It assesses technical knowledge, creativity, problem-solving skills, and adherence to best practices in software development.

---

## **Scope**

The API should provide the following functionalities:

### **Features**
1. **Create** a contact record.
2. **Retrieve** a contact record.
3. **Update** a contact record.
4. **Delete** a contact record.
5. **Search** for a contact record by:
   - Email.
   - Phone number.
6. **Retrieve all** contact records from the same city or state.

### **Contact Record Fields**
- Name.
- Company.
- Profile image.
- Email address.
- Birthdate.
- Phone numbers (work and personal).
- Address (including city and state).

---

## **Technical Requirements**

1. **Design Pattern**  
   Incorporate at least one design pattern in the project.

2. **SOLID Principles**  
   Ensure the code adheres to SOLID principles to promote maintainability and scalability.

3. **Unit Testing**  
   Include a unit test for at least one of the endpoints.

---

## **Setup and Usage**

### **Requirements**
- [List technologies, programming languages, or frameworks required for the project.]
- Ensure you have [dependencies, e.g., Node.js, Python, etc.] installed on your system.

### **Steps to Run the Project**
1. Clone the repository:
   ```bash
   git clone [repository-url]
   ```
2. Navigate to the project directory:
   ```bash
   cd [project-folder]
   ```
3. Install dependencies:
   ```bash
   [installation-command, e.g., npm install]
   ```
4. Run the application:
   ```bash
   [start-command, e.g., npm start]
   ```
5. Access the API at:  
   `http://localhost:[port]/[endpoint]`

### **Testing**
Run unit tests with:
```bash
[unit-test-command, e.g., npm test]
```

---

## **Endpoints Overview**

| Method | Endpoint                 | Description                                |
|--------|--------------------------|--------------------------------------------|
| POST   | `/contacts`              | Create a new contact record.              |
| GET    | `/contacts/:id`          | Retrieve a specific contact by ID.        |
| PUT    | `/contacts/:id`          | Update a contact by ID.                   |
| DELETE | `/contacts/:id`          | Delete a contact by ID.                   |
| GET    | `/contacts/search`       | Search for a contact by email or phone.   |
| GET    | `/contacts/location`     | Retrieve contacts by city or state.       |

---

## **Development Notes**

- Followed RESTful conventions for endpoint design.
- Used [design pattern name, e.g., Repository Pattern] to decouple logic.
- Adhered to SOLID principles for clean and scalable code.
- Tested the API using [testing framework, e.g., Jest].

