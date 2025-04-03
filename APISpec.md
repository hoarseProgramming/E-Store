# E-store - API Specification

## Overview

**Version:** 1.0\
This document describes the API endpoints for the E-store API

Authorization: JWT token in request header where stated like:

Authorization: Bearer "Token"

---

## Endpoints

### Categories

#### `POST /api/categories`

- Creates a new category
- **Authorization:** Admin required

**Request Body:** `application/json` 

| Field        | Type   | Required |
|-------------|--------|----------|
| categoryName | string | Yes      |

**Responses:**

`201 Created` if the category is successfully created
```JSON
{
  "Id": "guid",             
  "CategoryName": "string"
}
```
`400 Bad Request` if a category with the same name already exists
```JSON
{
    "message": "Category with name {categoryName} already exists."
}
```
`401 Unauthorized` if unauthenticated

`403 Forbidden` if unauthorized

#### `GET /api/categories`

- Gets a list of all categories.

**Response:**
`200 OK` with a list of categories.
```JSON
[
  {
    "Id": "guid",             
    "CategoryName": "string"
  },
  ...
]
```

#### `GET /api/categories/id`

- Gets the details of a specific category by ID.

**Path Parameters:**
`id` (string, UUID, required)

**Responses:**

 `200 OK`
 ```JSON
{
  "Id": "guid",             
  "CategoryName": "string"
}
```

`404 Not found` if the category with the specified ID is not found.
```JSON
{
    "message": "Category with id {id} not found."
}
```
---

### Customers

#### 1. `POST /api/customers`

- Creates a customer
- **Authorization:** required

- **Request Body:** `application/json` 

| Field      | Type   |  Required |
|-----------|--------|----------|
| firstName | string |  Yes      |
| lastName  | string |  Yes      |
| email     | string |  Yes      |
| address   | string |  Yes      |
| zipCode   | string |  Yes      |
| city      | string |  Yes      |
| country   | string |  Yes      |

**Responses:**

`201 Created` if the customer is successfully created
```JSON
{
  "Id": "c81e728d9d4c2f636f067f89cc14862c",
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "johndoe@example.com",
  "Address": "123 Main St, Apartment 4B",
  "ZipCode": "12345",
  "City": "Anytown",
  "Country": "Exampleland",
  "Orders": []
}
```
`400 Bad Request` if a customer with the same Email already exists
```JSON
{
    "message": "Customer with email {customer.Email} already exists."
}
```
`401 Unauthorized` if unauthenticated

#### 2. `GET /api/customers`

- Retrieves a list of all customers (admin only).
- Authorization: Requires "ADMIN" role.

**Responses:**

`200 OK` List of customers found.
 ```JSON

[
  {
  "Id": "c81e728d9d4c2f636f067f89cc14862c",
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "johndoe@example.com",
  "Address": "123 Main St, Apartment 4B",
  "ZipCode": "12345",
  "City": "Anytown",
  "Country": "Exampleland",
  "Orders": []
  },
  ...
]
```
`401 Unauthorized` if unauthenticated

`403 Forbidden` if unauthorized

#### 3. `GET /api/customers/id`

- Gets the details of a specific customer by ID.
- **Authorization:** required

**Path Parameters:**
`id` (string, UUID, required)

**Responses:**

 `200 OK`
 ```JSON
{
  "Id": "c81e728d9d4c2f636f067f89cc14862c",
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "johndoe@example.com",
  "Address": "123 Main St, Apartment 4B",
  "ZipCode": "12345",
  "City": "Anytown",
  "Country": "Exampleland",
  "Orders": []
}
```
`401 Unauthorized` if unauthenticated

`404 Not found` if the customer with the specified ID is not found.
```JSON
{
    "message": "Customer with id {id} not found."
}
```
---

#### 4. `PUT /api/customers/id`

- Updates an existing customer by ID.
- **Authorization:** required

 **Path Parameters:**
  - `id` (string, UUID, required) The ID of the customer to update.

**Request Body:** `application/json` 

| Field      | Type   | Required |
|-----------|--------|----------|----------|
| id        | guid | Yes |
| firstName | string | Yes      |
| lastName  | string | Yes      |
| email     | string | Yes      |
| address   | string | Yes      |
| zipCode   | string | Yes      |
| city      | string | Yes      |
| country   | string | Yes      |

**Responses:**
 `200 OK` if the customer with the specified ID is found.
 ```JSON
{
  "Id": "c81e728d9d4c2f636f067f89cc14862c",
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "johndoe@example.com",
  "Address": "123 Main St, Apartment 4B",
  "ZipCode": "12345",
  "City": "Anytown",
  "Country": "Exampleland",
  "Orders": []
}
```
`401 Unauthorized` if unauthenticated

`404 Not found` if the customer with the specified ID is not found.
```JSON
{
    "message": "Customer with id {id} not found."
}
```
#### 5. `DELETE /api/customers/id`
- Deletes a customer by ID (admin only).
- **Authorization**: Requires "ADMIN" role.

- **Path Parameters:**
  - `id` (string, UUID, required) The ID of the customer to delete.
- **Responses:**

`200 OK` if the customer with the specified ID is deleted.

`401 Unauthorized` if unauthenticated

`403 Forbidden` if unauthorized

`404 Not found` if the customer with the specified ID is not found.
```JSON
{
    "message": "Customer with id {id} not found."
}
```

---

### Users

#### 1. `POST /api/user/register`

**Request Body:**

| Field   | Type   | Required |
|--------|--------|----------|
| email  | string | Yes      |
| password | string | Yes      |

**Responses:**

`200 OK` if user is successfully registered

`400 Bad Request` if any validation isn't met
```JSON
{
  "type": "error",
  "title": "Invalid Request",
  "status": 400,
  "detail": "The request could not be processed due to invalid input.",
  "instance": "/errors/12345",
  "errors": {
    "email": [
      "Email is required.",
      "Email format is invalid."
    ],
    "password": [
      "Password must be at least 8 characters long."
    ]
  }
}
```


#### Login User

`POST /api/user/login`

- **Query Parameters:**
    - `useCookies` (boolean, optional)
    - `useSessionCookies` (boolean, optional)

- **Request Body:** `application/json` 

| Field                 | Type   | Required |
|----------------------|--------|----------|
| email               | string | Yes      |
| password            | string | Yes      |
| twoFactorCode       | string | No       |
| twoFactorRecoveryCode | string | No       |

**Responses:**

`200 OK` if login was successful
```JSON
{
  "tokenType": "Bearer",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEyMzQ1NiIsIm5hbWUiOiJKb2huIERvZSIsImV4cCI6MTY4Mzg3NTg5MH0.4w5J8vSO2lAd7Iz6zqzx1jHVznsfF6S6o8Ksl60TkWs",
  "expiresIn": 3600,
  "refreshToken": "d5d02d9c-d47d-4f5c-bfcb-6c5e6bdbfd72"
}
```
`401 Unauthorized` if wrong login information is entered

#### `GET /api/user/info`
- Gets user's roles and id
- **Authorization:** required

**Responses:**
`200 OK` if user is logged in
```JSON
{
  "Id": "e2fba453-4a87-4d55-9ac5-49f7f9b319bb",
  "CustomerId": "1c47e3c4-2337-4b65-bf2f-93818d490e12",
  "Roles": [
    {
      "Name": "Admin"
    },
    ...
  ]
}
```
`401 Unauthorized` if unauthenticated

#### `POST /api/user/set-customer`
- Sets customer id for a user
- **Authorization:** required
- 
**Request Body:** `application/json` 

| Field      | Type   | Required |
|-----------|--------|----------|
| email     | string | Yes      | 
| customerId | guid | Yes |

**Responses:**

`200 OK` If customer was set successfully

`400 Bad request` if the user couldn't be updated
```JSON
{
    "message": "Couldn't set customer Id."
}
```
`404 Not found` if the customer with the specified Email is not found.
```JSON
{
    "message": "User not found"
}
```

`401 Unauthorized` if unauthenticated

#### `PUT /api/user/id`
- Update user and customer info
- **Authorization:** required

**Path Parameters:**
- `id` (string, UUID, required)

**Request Body:** `application/json` 

| Field      | Type   | Required |
|-----------|--------|----------|
| firstName | string | Yes      |
| lastName  | string | Yes      |
| email     | string | Yes      |
| address   | string | Yes      |
| zipCode   | string | Yes      |
| city      | string | Yes      |
| country   | string | Yes      |

**Responses:**

`200 OK` if user and customer info is updated

`401 Unauthorized` if unauthenticated

`400 Bad request` if the user or customer couldn't be updated or if user account isn't tied to a customer.
```JSON
{
    "message": "Couldn't update info"
}
```
`404 Not found` if the user or customer ID couldn't be found
```JSON
{
    "message": "User not found."
}
```

---

### Orders

#### `POST /api/orders`
- Creates new order
- **Authorization:** required

**Request Body:** `application/json` 

| Field         | Type   | Required |
|--------------|--------|----------|----------|
| customerId   | guid | Yes |
| orderProducts | array of CreateOrderProductRequest | Yes |


CreateOrderProductRequest:

| Field         | Type    | Required |
|--------------|---------|----------|
| productNumber | integer | Yes      |
| quantity      | integer | No       |

**Responses:**
`201 Created` if the Order is successfully created
```JSON
{
  "Id": "d8c4f2b3-9a57-4bc3-8b6a-8d0b9d3d7eaf",
  "CustomerId": "f3a08a8d-2c0b-44c4-9b9b-6eaa3fca3df4",
  "Products": [
    {
      "ProductNumber": 1,
      "Quantity": 2
    },
    ...
  ]
}
```
`400 Bad Request` if the order couldn't be created
```JSON
{
    "message": "Couldn't create order"
}
```
`401 Unauthorized` if unauthenticated

#### `GET /api/orders`

- Gets all orders (admin only).
- **Authorization**: Requires "ADMIN" role.

**Responses:**

`200 OK` List of orders
```JSON
[
    {
      "Id": "d8c4f2b3-9a57-4bc3-8b6a-8d0b9d3d7eaf",
      "CustomerId": "f3a08a8d-2c0b-44c4-9b9b-6eaa3fca3df4",
      "Products": [
        {
          "ProductNumber": 1,
          "Quantity": 2
        },
        ...
      ]
    },
    ...
]
```

`401 Unauthorized` if unauthenticated

`403 Forbidden` if unauthorized

#### `GET /api/orders/id`
- Gets a specific order by Id
- **Authorization:** required

**Path Parameters:**
`id` (string, UUID, required)
**Responses:**

`200 OK` If the order exists
```JSON
{
  "Id": "d8c4f2b3-9a57-4bc3-8b6a-8d0b9d3d7eaf",
  "CustomerId": "f3a08a8d-2c0b-44c4-9b9b-6eaa3fca3df4",
  "Products": [
    {
      "ProductNumber": 1,
      "Quantity": 2
    },
    ...
  ]
}
```

`401 Unauthorized` if unauthenticated

`404 Not found` if the order with the specified ID is not found.
```JSON
{
    "message": "Order with id {id} not found."
```

---

### Products

#### `POST /api/products`

- Creates new product
- **Authorization**: Requires "ADMIN" role.

**Request Body:**
`application/json` 

| Field          | Type     |Required |
|---------------|---------|----------|
| productNumber | integer | Yes      |
| productName   | string  | Yes      |
| description   | string  | Yes      |
| price        | number  | Yes      |
| categoryId    | guid | No |
| isInAssortment | boolean | Yes |

**Responses:**
`201 Created` if the product is successfully created
```JSON
{
  "ProductNumber": 12345,
  "ProductName": "Gaming Laptop",
  "Description": "High-performance laptop designed for gaming with a 16GB RAM and 1TB SSD.",
  "Price": 1499.99,
  "CategoryId": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
  "Category": {
    "Id": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
    "CategoryName": "Electronics"
  },
  "IsInAssortment": true
}
```
`400 Bad Request` if the order couldn't be created
```JSON
{
    "message": "Couldn't create Product"
}
```
`401 Unauthorized` if unauthenticated
`403 Forbidden` if unauthorized

#### `GET /api/products`

- Gets all products

**Responses:**

`200 OK` List of products
```JSON
[
    {
      "ProductNumber": 12345,
      "ProductName": "Gaming Laptop",
      "Description": "High-performance laptop designed for gaming with a 16GB RAM and 1TB SSD.",
      "Price": 1499.99,
      "CategoryId": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
      "Category": {
        "Id": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
        "CategoryName": "Electronics"
      },
      "IsInAssortment": true
    },
    ...
]
```

#### `GET /api/products/productNumber`

- Gets a product by product number

**Path Parameters:**

`productNumber` (integer, required)

**Responses:**
 `200 OK` if product was found
 ```JSON
{
    "ProductNumber": 12345,
    "ProductName": "Gaming Laptop",
    "Description": "High-performance laptop designed for gaming with a 16GB RAM and 1TB SSD.",
    "Price": 1499.99,
    "CategoryId": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
    "Category": {
    "Id": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
    "CategoryName": "Electronics"
}
```

`404 Not found` if the product with the specified product number is not found.
```JSON
{
    "message": "Product with number {productNumber} not found."
}
```

#### `PUT /api/products/productNumber`
- Update a product
- **Authorization**: Requires "ADMIN" role.

**Path Parameters:**

`productNumber` (integer, required)

**Request Body:** `application/json` 

| Field          | Type     | Required |
|---------------|---------|----------|
| productNumber | integer | Yes      |
| productName   | string  | Yes      |
| description   | string  | Yes      |
| price        | number  | Yes      |
| categoryId    | guid| No |
| isInAssortment | boolean | Yes |

**Responses:**

 `200 OK` if updated
 ```JSON
{
    "ProductNumber": 12345,
    "ProductName": "Gaming Laptop",
    "Description": "High-performance laptop designed for gaming with a 16GB RAM and 1TB SSD.",
    "Price": 1499.99,
    "CategoryId": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
    "Category": {
    "Id": "9f4b43c3-1dfe-4d6e-9a6a-8f6e67b22c88",
    "CategoryName": "Electronics"
}
```
`401 Unauthorized` if unauthenticated

`403 Forbidden` if unauthorized

`404 Not found` if the product with the specified product number is not found.
```JSON
{
    "message": "Product with number {productNumber} not found."
}
```
---
