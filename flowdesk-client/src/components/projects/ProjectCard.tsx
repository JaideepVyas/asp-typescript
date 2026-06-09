import type { Project } from "../../types/project";
import { useNavigate } from "react-router-dom";

type Props = {
  project: Project;
};

export default function ProjectCard({
  project,
}: Props) {
  const navigate = useNavigate();

  return (
    <div
      onClick={() =>
        navigate(
          `/projects/${project.id}`
        )
      }
    >
      <h3>{project.name}</h3>

      <p>{project.description}</p>

      <hr />
    </div>
  );
}