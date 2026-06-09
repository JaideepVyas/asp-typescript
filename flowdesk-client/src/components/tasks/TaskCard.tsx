type TaskStatus = "Todo" | "InProgress" | "Done";

type Task = {
  id: string;
  title: string;
  description: string;
  status: TaskStatus;
  priority: string;
  assignedUserId?: string | null;
};

type User = {
  id: string;
  fullName: string;
};

type Props = {
  task: Task;
  users?: User[];
  onStatusChange?: (taskId: string, status: TaskStatus) => void;
  onAssign?: (taskId: string, userId: string) => void;
};

export default function TaskCard({
  task,
  users = [],
  onStatusChange,
  onAssign,
}: Props) {
  const getPriorityColor = (priority: string) => {
    switch (priority) {
      case "High":
        return "#ef4444";
      case "Medium":
        return "#f59e0b";
      case "Low":
        return "#10b981";
      default:
        return "#6b7280";
    }
  };

  return (
    <div
      style={{
        background: "#fff",
        padding: "12px",
        borderRadius: "8px",
        marginBottom: "10px",
        borderLeft: `5px solid ${getPriorityColor(task.priority)}`,
        boxShadow: "0 1px 4px rgba(0,0,0,0.1)",
      }}
    >
      <h4>{task.title}</h4>

      <p style={{ fontSize: "14px" }}>
        {task.description}
      </p>

      <div
        style={{
          fontSize: "12px",
          marginBottom: "8px",
        }}
      >
        Priority: <b>{task.priority}</b> | Status: <b>{task.status}</b>
      </div>

      {/* STATUS BUTTONS */}
      <div
        style={{
          display: "flex",
          gap: "6px",
          flexWrap: "wrap",
        }}
      >
        <button
          onClick={() =>
            onStatusChange?.(task.id, "Todo")
          }
        >
          Todo
        </button>

        <button
          onClick={() =>
            onStatusChange?.(task.id, "InProgress")
          }
        >
          In Progress
        </button>

        <button
          onClick={() =>
            onStatusChange?.(task.id, "Done")
          }
        >
          Done
        </button>
      </div>

      {/* ASSIGN USER UI */}
      <div style={{ marginTop: "10px" }}>
        <small>
          Assigned:{" "}
          <b>
            {task.assignedUserId
              ? task.assignedUserId
              : "Unassigned"}
          </b>
        </small>

        <div style={{ marginTop: "6px" }}>
          <select
            onChange={(e) => {
              if (onAssign && e.target.value) {
                onAssign(task.id, e.target.value);
              }
            }}
            defaultValue=""
          >
            <option value="">
              Assign user
            </option>

            {users.map((u) => (
              <option
                key={u.id}
                value={u.id}
              >
                {u.fullName}
              </option>
            ))}
          </select>
        </div>
      </div>
    </div>
  );
}