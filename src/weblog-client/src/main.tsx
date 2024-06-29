import React from "react";
import ReactDOM from "react-dom/client";
import { App } from "./containers/App/App";
import QueryProvider from "./contexts/QueryProvider";
import { BrowserRouter as Router } from "react-router-dom";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Router>
    <React.StrictMode>
      <QueryProvider>
        <App />
      </QueryProvider>
    </React.StrictMode>
  </Router>
);
