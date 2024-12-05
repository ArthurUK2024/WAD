import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ActivityService } from '../activities/activity.service';
import { FutureGoalsService } from '../future-goals/future-goals.service';
import { FutureGoal } from '../future-goals/future-goals.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  user: any;
  recentActivities: any[] = []; // Last 3 activities
  recentFutureGoals: FutureGoal[] = []; // Last 3 activities
  constructor(private authService: AuthService, private activityService: ActivityService, private futureGoalsService: FutureGoalsService) { }

  ngOnInit(): void {
    this.user = this.authService.getUser();
    this.activityService.getRecentActivities().subscribe(
      (activities: any[]) => {
        this.recentActivities = activities;
      },
      (error) => {
        console.error('Error fetching recent activities:', error);
      }
    );

    this.futureGoalsService.getRecentGoals().subscribe(
      (goals: FutureGoal[]) => {
        this.recentFutureGoals = goals;
      },
      (error) => {
        console.error('Error fetching recent activities:', error);
      }
    );
  }
}
