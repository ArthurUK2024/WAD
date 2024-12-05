import { Component, OnInit } from '@angular/core';
import { FutureGoalsService } from './future-goals.service';
import { FutureGoal, FutureGoalCreate } from './future-goals.model';

@Component({
  selector: 'app-future-goals',
  templateUrl: './future-goals.component.html',
  styleUrl: './future-goals.component.css'
})
export class FutureGoalsComponent {
  futureGoals: FutureGoal[] = [];

  newGoal: FutureGoalCreate = {
    goalName: '',
    description: '',
    targetDate: '',
    userId: 0
  };

  selectedGoal: FutureGoal = {
    id: 0,
    goalName: '',
    description: '',
    targetDate: '',
    userId: 0
  };

  constructor(private futureGoalsService: FutureGoalsService) { }

  ngOnInit(): void {
    this.loadGoals();
  }

  loadGoals(): void {
    this.futureGoalsService.getFutureGoals().subscribe(
      (data: FutureGoal[]) => {
        this.futureGoals = data;
      },
      (error) => {
        console.error('Error fetching future goals', error);
      }
    );
  }

  selectGoal(goal: FutureGoal): void {
    this.selectedGoal = { ...goal }
  }

  updateGoal(): void {
    this.futureGoalsService.updateFutureGoal(this.selectedGoal.id, this.selectedGoal).subscribe(
      (data) => {
        this.loadGoals()
        alert('Goal updated successfully');
      },
      (error) => {
        alert('Error updating goal');
      }
    );
  }

  createGoal(): void {
    this.futureGoalsService.createFutureGoal(this.newGoal).subscribe(
      (data) => {
        this.newGoal = {
          goalName: '',
          description: '',
          targetDate: '',
          userId: 0
        };
        this.loadGoals()
        alert('Goal created successfully');
      },
      (error) => {
        alert('Error creating goal');
      }
    );
  }

  deleteGoal(id: number): void {
    this.futureGoalsService.deleteFutureGoal(id).subscribe(
      () => {
        alert('Goal deleted');
        window.location.reload();
      },
      (error) => {
        alert('Error deleting goal');
      }
    );
  }
}
