# 🛒 Tea-Commerce – Microservices E-Commerce Project

This project is a microservices-based e-commerce system composed of 3 main services:

### 1. **User Service**

Handles user registration, authentication, and role-based authorization.

How to log in properly:

- **Register a new user**  
  `POST /Account/register`  
  Assign a role ("Administrator", "Customer", "Support), username and password.

- **Login**  
  `POST /Login/login`  
  Returns a JWT token.

- **Authorize in Swagger**  
  After logging in, click the **Authorize** button in Swagger UI and enter your token in the format:

  ```
  Bearer yourJWTtokenHere
  ```

- **Admin-only test endpoint**  
  `GET /Login` – accessible only for users with the `Administrator` role.

> ⚠️ The file `appsettings.json` is ignored .gitignore due to sensitive JWT configuration.

---

### 2. **Product Service**

This service is responsible for handling all product-related operations and customer data access.

**Main features:**

- **Products**  
  Full CRUD operations (Create, Read, Update, Delete) on products.

- **Product Categories**  
  Full CRUD support for managing product categories.

- **Customer Details**  
  Provides endpoints to fetch customer details (read-only).
---

### 3. **ShoppingCartService**

Processes shopping cart orders and handles invoice generation and email delivery.

- Requires a valid **Cart ID** to generate and send an invoice.
- After confirming the cart, a PDF invoice is automatically emailed to the customer.

#### SMTP Configuration

To enable email sending, update the `appsettings.json` file in `ShoppingCartService` with your Gmail and an app-specific password:

```
"Smtp": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "example@gmail.com",
  "Password": ""
}
```

---

## Manual Invoice Generation

In specific scenarios (e.g. long-term company contracts), you can manually generate an invoice using a dedicated endpoint in **UserService**, without involving the shopping cart.

---

## How To Get Started?

1. Clone the repository.
2. Configure each microservice:
   - Set up `appsettings.json` for each service.
   - Add SMTP and JWT necessary information in Shopping Cart and UserService.
3. Use Swagger UI to test endpoints.
4. Log in and authorize using your JWT token.

---

## Requirements

- .NET 8 SDK
- Gmail account (for SMTP)
- App-specific Gmail password
- Git

---

## Contact

For questions or suggestions, feel free to open an issue or submit a pull request.
