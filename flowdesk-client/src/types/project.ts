export interface Project {
  id: string;
  name: string;
  description: string;
  ownerId: string;
  createdAt: string;
}

export interface CreateProjectRequest {
  name: string;
  description: string;
}