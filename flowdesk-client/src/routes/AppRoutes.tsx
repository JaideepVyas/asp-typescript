import { BrowserRouter, Routes, Route } from "react-router-dom";

import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import DashboardPage from "../pages/DashboardPage";
import ProjectDetailsPage from "../pages/ProjectDetailsPage";
export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/login"
          element={<LoginPage />}
        />

        <Route
          path="/register"
          element={<RegisterPage />}
        />
        <Route
  path="/dashboard"
  element={<DashboardPage />}
/>
<Route
  path="/projects/:projectId"
  element={<ProjectDetailsPage />}
/>
      </Routes>
    </BrowserRouter>
  );
}