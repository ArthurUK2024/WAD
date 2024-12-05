import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = '12591_WAD_ANGULAR';
  constructor(private authService: AuthService, private router: Router) { }

  logout() {
    this.router.navigate(['/logout']);
  }
}
