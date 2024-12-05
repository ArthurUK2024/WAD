export interface FutureGoal {
  id: number;
  goalName: string;
  description: string;
  targetDate: string;  // Use string or Date depending on how you handle the date
  userId: number;
}

export interface FutureGoalCreate {
  goalName: string;
  description: string;
  targetDate: string;  // Use string or Date depending on how you handle the date
  userId: number;
}
