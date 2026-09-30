interface Task {
  id: number;
  title: string;
  description: string;
  dueDate: string;
  isCompleted?: boolean;
}

export type { Task };
