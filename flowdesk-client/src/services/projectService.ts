import * as projectApi from "../api/projectApi";
import type {
  Project,
  CreateProjectRequest,
} from "../types/project";

export const getProjects =
  async (): Promise<Project[]> => {
    return await projectApi.getProjects();
  };

export const createProject = async (
  data: CreateProjectRequest
): Promise<Project> => {
  return await projectApi.createProject(
    data
  );
};