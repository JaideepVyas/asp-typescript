import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProjects } from "../api/projectApi";
import {
  getTasksByProject,
  createTask,
  updateTaskStatus
} from "../api/taskApi";
import TaskCard from "../components/tasks/TaskCard";

type TaskStatus = "Todo" | "InProgress" | "Done";

interface Task {
  id: string;
  title: string;
  description: string;
  status: TaskStatus | number;
  priority: string;
}

interface Project {
  id: string;
  name: string;
  description: string;
}

// helper
const normalizeStatus = (status: any): TaskStatus => {
  if (status === 0 || status === "Todo") return "Todo";
  if (status === 1 || status === "InProgress") return "InProgress";
  if (status === 2 || status === "Done") return "Done";
  return "Todo";
};

export default function ProjectDetailsPage() {
  const { projectId } = useParams<{ projectId: string }>();

  const [project, setProject] = useState<Project | null>(null);
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  useEffect(() => {
    if (!projectId) return;

    const loadData = async () => {
      try {
        setLoading(true);

        const projects = await getProjects();

        const currentProject = projects.find(
          (p) => String(p.id) === String(projectId)
        );

        const taskData = await getTasksByProject(projectId);

        setProject(currentProject ?? null);
        setTasks(taskData);

      } catch (error) {
        console.error("Error loading project details:", error);
        setProject(null);
        setTasks([]);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [projectId]);

  // ✅ STATUS UPDATE HANDLER (NEW)
  const handleStatusChange = async (taskId: string, status: TaskStatus) => {
    if (!projectId) return;

    try {
      await updateTaskStatus(projectId, taskId, {
        status: status === "Todo" ? 0 : status === "InProgress" ? 1 : 2
      });

      // instant UI update
      setTasks(prev =>
        prev.map(t =>
          t.id === taskId ? { ...t, status } : t
        )
      );

    } catch (error) {
      console.error("Error updating task status:", error);
    }
  };

  const handleCreateTask = async () => {
    if (!projectId || !title) return;

    if (title.trim().length < 3) {
      alert("Title must be at least 3 characters");
      return;
    }

    if (description.trim().length === 0) {
      alert("Description is required");
      return;
    }

    try {
      const newTask = await createTask(projectId, {
        title: title.trim(),
        description: description.trim(),
        status: 0,
        priority: 1,
      });

      setTasks(prev => [...prev, newTask]);

      setTitle("");
      setDescription("");
    } catch (error) {
      console.error("Error creating task:", error);
    }
  };

  if (loading) return <p>Loading...</p>;
  if (!project) return <p>Project not found</p>;

  const normalizedTasks = tasks.map(t => ({
    ...t,
    status: normalizeStatus(t.status)
  }));

  const todo = normalizedTasks.filter(t => t.status === "Todo");
  const inProgress = normalizedTasks.filter(t => t.status === "InProgress");
  const done = normalizedTasks.filter(t => t.status === "Done");

  return (
    <div style={{ padding: "20px" }}>
      <h1>{project.name}</h1>
      <p>{project.description}</p>

      <div style={{ marginTop: "20px", marginBottom: "20px" }}>
        <h3>Create Task</h3>

        <input
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={{ marginRight: "10px" }}
        />

        <input
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={{ marginRight: "10px" }}
        />

        <button onClick={handleCreateTask}>
          Add Task
        </button>
      </div>

      <div style={{ display: "flex", gap: "20px", marginTop: "20px" }}>

        <div style={{ flex: 1 }}>
          <h3>Todo</h3>
          {todo.map(task => (
            <TaskCard
              key={task.id}
              task={task}
              onStatusChange={handleStatusChange}
            />
          ))}
        </div>

        <div style={{ flex: 1 }}>
          <h3>In Progress</h3>
          {inProgress.map(task => (
            <TaskCard
              key={task.id}
              task={task}
              onStatusChange={handleStatusChange}
            />
          ))}
        </div>

        <div style={{ flex: 1 }}>
          <h3>Done</h3>
          {done.map(task => (
            <TaskCard
              key={task.id}
              task={task}
              onStatusChange={handleStatusChange}
            />
          ))}
        </div>

      </div>
    </div>
  );
}