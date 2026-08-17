# backend-blog-generator:

# README — AI Blog Generator

## Overview

**AI Blog Generator** is a social blogging platform that allows users to generate, manage, publish, and interact with AI-powered blog content. The platform combines **AI-assisted content generation** with social blogging features such as likes, comments, bookmarks, reposts, follows, profiles, and a public blog feed.

The system is designed as a modern full-stack application with an **ASP.NET Core Web API backend**, **Angular + TypeScript frontend**, and **SQL Server database**.

## Features

### Authentication & Authorization

* User registration and login
* JWT-based authentication
* Access and refresh tokens
* Token refresh functionality
* Logout
* Role-based authorization
* Secure password handling

### AI Blog Generation

Users can generate blog content by providing:

* Category
* Topic
* Target audience
* Tone
* Word count
* Language

AI-powered operations include:

* Generate blog
* Regenerate blog
* Expand blog
* Shorten blog
* Generate blog images

### Blog Management

* Create and generate blogs
* View personal blogs
* View individual blog details
* Edit/manage blog content
* Delete blogs
* Publish blogs
* Blog visibility management
* Blog versions/history
* Download blogs as PDF
* Blog images

### Social Features

The platform allows users to interact with published content through:

* Like/unlike blogs
* Comments
* Bookmarks
* Reposts
* Follow/unfollow users
* Followers and following lists
* Public user profiles
* View another user's published blogs
* Blog views
* Blog reporting

### Profile Management

Users can:

* View their profile
* Update profile information
* Upload profile picture
* Delete profile picture
* Change password
* Delete account
* View public profiles
* View followers
* View following users

### Credits & Plans

The platform uses a credit-based system for AI operations.

Example operations:

| Operation       | Credits |
| --------------- | ------: |
| Blog Generation |       5 |
| Regenerate Blog |       5 |
| Expand Blog     |       2 |
| Shorten Blog    |       2 |
| Generate Image  |      10 |

New users receive an initial amount of free credits.

The platform also supports subscription/payment plans.

### Payments

* Plan management
* Payment records
* Credit-based plans
* Stripe payment integration

### Notifications

Users can receive notifications for relevant social activities such as interactions and follows.

---

## Tech Stack

### Frontend

* Angular
* TypeScript

### Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core
* JWT Authentication
* BCrypt
* Fluent API
* Data Annotations
* RESTful APIs

### Database

* Microsoft SQL Server
* Entity Framework Core
* EF Core Migrations

### Other Technologies

* Postman — API testing
* Git & GitHub — Version control
* QuestPDF — PDF generation
* Stripe — Payments
* AI API — AI-powered blog generation

---

## Architecture

The backend follows a modular structure separating API, business logic, data access, domain models, and service models.

```text
AI Blog Generator
│
├── Frontend
│   ├── Angular
│   ├── TypeScript
│
└── Backend
    │
    ├── Controllers
    │
    ├── BAL
    │   └── Business Logic / Services
    │
    ├── DAL
    │   └── Entity Framework Core
    │
    ├── DomainModels
    │   └── Database Entities
    │
    ├── ServiceModels
    │   └── DTOs / Request / Response Models
    │
    ├── Interfaces
    │   └── Service Contracts
    │
    ├── Configurations
    │   └── EF Core Fluent Configurations
    │
    ├── Foundation
    │   └── Exceptions / Common Components
    │
    └── Enums
```

---

## Main Modules

The backend is organized into functional modules:

```text
Authentication
Profile
Blog
AI Blog Generation
Social Interaction
Comments
Likes
Bookmarks
Reposts
Follow
Notifications
Payments
Plans
Feedback
Issues
Reports
Categories
Tags
Badges
```

---

## Database

The application uses SQL Server with Entity Framework Core.

Major entities include:

```text
Users
RefreshTokens
Blogs
BlogVersions
BlogImages
Plans
Payments
Feedbacks
Issues
DeletedAccounts
Likes
Comments
Bookmarks
Reposts
Follows
Notifications
BlogReports
UserBadges
Categories
Tags
BlogTags
Badges
```

### Main Relationships

```text
User
 │
 ├── Blogs
 ├── RefreshTokens
 ├── Likes
 ├── Comments
 ├── Bookmarks
 ├── Reposts
 ├── Follows
 ├── Notifications
 ├── Payments
 └── UserBadges
       │
       ▼
      Badges

Blog
 │
 ├── BlogVersions
 ├── BlogImages
 ├── Likes
 ├── Comments
 ├── Bookmarks
 ├── Reposts
 ├── BlogReports
 └── Tags
```

---

## API Structure

The API follows RESTful endpoint conventions.

### Authentication

```text
POST /api/auth/register
POST /api/auth/login
POST /api/auth/refresh-token
POST /api/auth/logout
```

### Profile

```text
GET    /api/profile
PUT    /api/profile
PUT    /api/profile/change-password
POST   /api/profile/upload-picture
DELETE /api/profile/picture
DELETE /api/profile
```

### Public Profile

```text
GET    /api/profile/{userId}
GET    /api/profile/{userId}/blogs

POST   /api/profile/{userId}/follow
DELETE /api/profile/{userId}/follow

GET    /api/profile/followers
GET    /api/profile/following

GET    /api/profile/{userId}/followers
GET    /api/profile/{userId}/following
```

### AI Blog Generation

```text
POST /api/blogs/generate
POST /api/blogs/{blogId}/regenerate
POST /api/blogs/{blogId}/expand
POST /api/blogs/{blogId}/shorten
POST /api/blogs/{blogId}/generate-image
```

### Blog Management

```text
GET    /api/blogs
GET    /api/blogs/{blogId}
DELETE /api/blogs/{blogId}
GET    /api/blogs/{blogId}/download-pdf
```

Additional endpoints are implemented for social interactions, comments, bookmarks, reposts, notifications, payments, reports, categories, tags, and other modules.

---

## Authentication Flow

The application uses JWT-based authentication.

```text
User
 │
 ▼
Login
 │
 ▼
Access Token + Refresh Token
 │
 ├──────────────► Access protected APIs
 │
 ▼
Access Token Expired
 │
 ▼
Refresh Token
 │
 ▼
New Access Token
```

Protected endpoints require a valid JWT access token.

---

## Credit System

AI operations consume credits from the user's available balance.

```text
User
 │
 ▼
AI Operation
 │
 ▼
Check Available Credits
 │
 ├── Insufficient ──► Reject Request
 │
 └── Sufficient
          │
          ▼
      AI Operation
          │
          ▼
    Deduct Credits
          │
          ▼
    Return Result
```

Credits cannot become negative.

---

## Blog Visibility

Blogs can be controlled using visibility settings such as:

* Public
* Private

Published public blogs can appear in the social/public feed and can be viewed by other users.

---

## Getting Started

### Prerequisites

Install the following:

* .NET SDK
* Node.js
* npm
* SQL Server
* Angular-CLI
* Visual Studio or VS Code
* Git

### Clone the Repository

```bash
git clone <repository-url>
cd AI_Blog_Generator
```

### Backend Setup

Navigate to the backend project:

```bash
cd BlogGenerator
```

Restore dependencies:

```bash
dotnet restore
```

Update the database connection string in:

```text
appsettings.json
```

Run EF Core migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

The API will be available at the configured HTTP/HTTPS URL.

### Frontend Setup

Navigate to the frontend:

```bash
cd frontend
```

Install dependencies:

```bash
npm install
```

Start the development server:

```bash
npm run dev
```

---

## Environment Configuration

Sensitive configuration values should be stored in environment variables or development secrets rather than committed directly to source control.

Typical configuration includes:

```text
Database Connection String
JWT Secret
JWT Issuer
JWT Audience
AI API Key
Stripe Secret Key
Stripe Webhook Secret
```

---

## API Testing

API endpoints can be tested using **Postman**.

Recommended testing flow:

```text
Register
   ↓
Login
   ↓
Get Access Token
   ↓
Authorize Protected Endpoints
   ↓
Test Profile APIs
   ↓
Test Blog APIs
   ↓
Test Social APIs
   ↓
Test AI APIs
   ↓
Test Payment APIs
```

---

## Project Goals

The main goals of the project are to:

* Simplify blog creation using AI
* Provide a complete social blogging experience
* Allow users to build public profiles
* Enable users to discover and interact with content
* Provide efficient blog management
* Implement secure authentication and authorization
* Provide a scalable backend architecture
* Implement a credit-based AI usage system

---

## Future Enhancements

Potential future improvements include:

* Advanced blog search
* Personalized content recommendations
* Advanced analytics
* AI-powered SEO optimization
* AI title and keyword suggestions
* Scheduled publishing
* Rich text editor
* Real-time notifications
* Advanced moderation
* Improved recommendation algorithms

---

## Development Status

**AI Blog Generator is currently under active development.**

Core backend modules, authentication, profile management, blog management, social functionality, database relationships, DTOs, configurations, and API testing are being implemented incrementally.

---

## Author

**Laveezah**

Backend Developer Intern ,
Reno Softwares

**Primary focus:** ASP.NET Core, C#, Entity Framework Core, SQL Server, REST APIs, and Angular/TypeScript.
