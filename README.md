# Weblog

Weblog is a simple CRUD (Create, Read, Update, Delete) app built using Vite, React, and Bun as a technical assignment. Additionally a minimal API is included.

## Project Structure

This project consists of two main components:

1. **[weblog-client](./weblog-client)**: A React-based client application that provides a user interface for interacting with the blog posts.
2. **[Weblog.Api](./Weblog.Api)**: A .NET minimal API that provides a CRUD (Create, Read, Update, Delete) interface for managing blog posts. For ease of use, it has been dockerised.


## Getting Started

1. Clone the repository:

   ```
   git clone https://github.com/thhsie/Weblog.git
   ```

2. Navigate to the project directory:

   ```
   cd Weblog
   ```

3. Pick how you want to run this app locally:

 #### React client only
   -  If you prefer to run the frontend only, please refer to the individual **[weblog-client README](./weblog-client/README.md)**.
   
#### React client with the API 
   - If you prefer to run the frontend and backend then setup the webclient as explained in **[weblog-client README](./weblog-client/README.md)**. After that, setup the API as explained in **[Weblog.Api README](./Weblog.Api/README.md)**. For the latter, it is strongly recommended to follow the Docker option for ease of setup.
   


