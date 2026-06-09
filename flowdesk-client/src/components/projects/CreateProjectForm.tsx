import { useState } from "react";

type Props = {
  onCreate: (
    name: string,
    description: string
  ) => void;
};

export default function CreateProjectForm({
  onCreate,
}: Props) {
  const [name, setName] = useState("");
  const [description, setDescription] =
    useState("");

  const handleSubmit = (
    e: React.FormEvent
  ) => {
    e.preventDefault();

    onCreate(name, description);

    setName("");
    setDescription("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Create Project</h2>

      <input
        type="text"
        placeholder="Project Name"
        value={name}
        onChange={(e) =>
          setName(e.target.value)
        }
      />

      <br />
      <br />

      <textarea
        placeholder="Description"
        value={description}
        onChange={(e) =>
          setDescription(e.target.value)
        }
      />

      <br />
      <br />

      <button type="submit">
        Create Project
      </button>
    </form>
  );
}