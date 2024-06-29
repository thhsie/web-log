# Weblog

Weblog is a simple CRUD (Create, Read, Update, Delete) application.

## Project Structure

This project consists of two main components:

1. **[weblog-client](./src/weblog-client)**: A React-based client application that provides a user interface for interacting with the blog posts.
2. **[Weblog.Api](./src/Weblog.Api)**: A .NET API that provides CRUD endpoints for managing blog posts. For convenience, it is dockerised for local use and also deployed on the cloud.

## Getting Started

1. Clone the repository:

   ```
   git clone https://github.com/thhsie/Weblog.git
   ```

2. Navigate to the project directory:

   ```
   cd Weblog/src
   ```

3. Choose how you want to run the application locally:

#### React client only

- If you prefer to run the frontend only, please refer to the individual **[weblog-client README](./src/weblog-client)**.

#### React client with the API

- If you prefer to run the frontend and backend then setup the webclient as explained in the **[weblog-client README](./src/weblog-client)**. After that, setup the API as explained in the **[Weblog.Api README](./src/Weblog.Api)**. For the latter, it is recommended to follow the Docker option for the easiest setup.
