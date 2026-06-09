import { useEffect, useState } from "react";
import * as projectService from "../services/projectService";
import type { Project } from "../types/project";
import CreateProjectForm from "../components/projects/CreateProjectForm";
import ProjectCard from "../components/projects/ProjectCard";
export default function DashboardPage() {
  const [projects, setProjects] = useState<Project[]>([]);

  useEffect(() => {
    loadProjects();
  }, []);

  const loadProjects = async () => {
    try {
      const data =
        await projectService.getProjects();

      setProjects(data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleCreateProject = async (
    name: string,
    description: string
  ) => {
    try {
      await projectService.createProject({
        name,
        description,
      });

      await loadProjects();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Dashboard</h1>

      <CreateProjectForm
        onCreate={handleCreateProject}
      />

      <h2>Projects</h2>

      {projects.length === 0 ? (
        <p>No projects found</p>
      ) : (
        projects.map((project) => (
  <ProjectCard
    key={project.id}
    project={project}
  />
))
      )}
    </div>
  );
}