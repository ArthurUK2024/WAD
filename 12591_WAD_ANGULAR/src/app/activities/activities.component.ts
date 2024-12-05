import { Component, OnInit } from '@angular/core';
import { ActivityService } from './activity.service';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrl: './activities.component.css'
})
export class ActivitiesComponent implements OnInit {
  activities: any = []; // List of activitie

  newActivity: any = { activityType: '', duration: 0, caloriesBurned: 0, date: '' }; // Create Form model
  selectedActivity: any = { activityType: '', duration: 0, caloriesBurned: 0, date: '' }; // Edit Form model

  loading: boolean = true; // Loading state

  constructor(private activityService: ActivityService) { }

  ngOnInit(): void {
    this.loadActivities();
  }

  openEditModal(activity: any): void {
    this.selectedActivity = { ...activity }; // Copy activity data to selectedActivity
  }

  loadActivities(): void {
    this.loading = true;
    this.activityService.getActivities().subscribe(
      (data: any[]) => {
        setTimeout(() => {
          this.loading = false;
          this.activities = data;
        }, 0);
      },
      (error) => {
        console.error('Error fetching activities', error);
        this.loading = false;
      }
    );
  }

  // Create a new activity
  createActivity(): void {
    if (this.newActivity.activityType && this.newActivity.duration && this.newActivity.caloriesBurned && this.newActivity.date) {
      this.activityService.createActivity(this.newActivity).subscribe(
        (data) => {
          this.newActivity = { activityType: '', duration: 0, caloriesBurned: 0, date: '' };
          this.loadActivities()
        },
        (error) => {
          console.error('Error creating activity', error);
        }
      );
    } else {
      console.error('Please fill out all fields');
      alert("Please fill out all fields")
    }
  }

  updateActivity(): void {
    this.activityService.updateActivity(this.selectedActivity.id, this.selectedActivity).subscribe(
      (response) => {
        console.log('Activity updated:', response);
        this.loadActivities();
        alert("Activity updated")
      },
      (error) => {
        console.error('Error updating activity:', error);
        alert("Error updating activity")
      }
    );
  }

  deleteActivity(id: number): void {
    this.activityService.deleteActivity(id).subscribe(
      (response) => {
        console.log('Activity deleted:', response);
        alert("Activity deleted")
        window.location.reload();
      },
      (error) => {
        console.error('Ertror deleting activity:', error);
        alert("Error deleting activity")
      }
    );
  }
}
