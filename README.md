# Weblog

Weblog is a simple CRUD (Create, Read, Update, Delete) app built using Vite, React, and Bun as a technical assignment. Additionally a minimal API is included.

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

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

   This will build and start the app.

   > If you prefer to run the React client app only, please refer to the individual README file in the `weblog-client` directory.

## Project Structure

The Weblog project consists of two main components:

1. **weblog-client**: A React-based client application that provides a user interface for interacting with the blog posts.
2. **Weblog.Api**: A .NET minimal API that provides a CRUD (Create, Read, Update, Delete) interface for managing blog posts.
