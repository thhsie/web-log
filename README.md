# Weblog

Weblog is a full-stack web application that includes a React client and a .NET minimal API. The application allows users to manage blog posts.

## Prerequisites

- Docker Desktop

## Getting Started

1. Clone the repository:

   ```
   git clone https://github.com/thhsie/Weblog.git
   ```

2. Navigate to the project directory:

   ```
   cd Weblog
   ```

3. Build and run the Docker containers:

   ```
   docker-compose up --build
   ```

   This will build and start the minimal API and the client applications.

   > If you prefer to run React client app only, please refer to the individual README file in the `weblog-client` directory.

## Project Structure

The Weblog project consists of two main components:

1. **Weblog.Api**: A .NET 8 minimal API that provides a CRUD (Create, Read, Update, Delete) interface for managing blog posts.
2. **Weblog.Client**: A React-based client application that consumes the minimal API and provides a user interface for interacting with the blog posts.
