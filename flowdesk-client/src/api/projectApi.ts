import axiosClient from "./axiosClient";
import type {
  Project,
  CreateProjectRequest,
} from "../types/project";

export const getProjects = async (): Promise<Project[]> => {
  const response = await axiosClient.get(
    "/projects"
  );

  return response.data;
};

export const createProject = async (
  data: CreateProjectRequest
): Promise<Project> => {
  const response = await axiosClient.post(
    "/projects",
    data
  );

  return response.data;
};