import axiosClient from "./axiosClient";
import type {
  CreateTaskDto,
  UpdateTaskStatusDto,
  AssignTaskDto,
  TaskResponseDto,
} from "../types/task";

export const getTasksByProject = async (
  projectId: string
): Promise<TaskResponseDto[]> => {
  const response = await axiosClient.get<TaskResponseDto[]>(
    `/projects/${projectId}/tasks`
  );
  return response.data;
};

export const createTask = async (
  projectId: string,
  data: any
) => {
  const response = await axiosClient.post(
    `/projects/${projectId}/tasks`,
    {
      Title: data.title,
      Description: data.description,
      Status: data.status,
      Priority: data.priority,
    }
  );

  return response.data;
};
/**
 * Update task status (Kanban move)
 */
export const updateTaskStatus = async (
  projectId: string,
  taskId: string,
  data: UpdateTaskStatusDto
): Promise<TaskResponseDto> => {
  const response = await axiosClient.put<TaskResponseDto>(
    `/projects/${projectId}/tasks/${taskId}/status`,
    data
  );
  return response.data;
};

/**
 * Assign task to user
 */
export const assignTask = async (
  projectId: string,
  taskId: string,
  data: AssignTaskDto
): Promise<TaskResponseDto> => {
  const response = await axiosClient.put<TaskResponseDto>(
    `/projects/${projectId}/tasks/${taskId}/assign`,
    data
  );
  return response.data;
};