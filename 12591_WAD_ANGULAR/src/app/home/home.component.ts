import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ActivityService } from '../activities/activity.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  user: any;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.user = this.authService.getUser();
  }
}
