export type TaskStatus = "Todo" | "InProgress" | "Done";

export type TaskPriority = "Low" | "Medium" | "High";

export interface TaskResponseDto {
  id: string;
  title: string;
  description: string;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate?: string;
  projectId: string;
  assignedUserId?: string;
  createdAt: string;
}

export interface CreateTaskDto {
  title: string;
  description: string;
  priority: TaskPriority;
  dueDate?: string;
}

export interface UpdateTaskStatusDto {
  status: TaskStatus;
}

export interface AssignTaskDto {
  userId: string;
}