import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        if (response) {
          this.router.navigate(['/']);
        } else {
          alert('Login failed. Please check your credentials.');
        };
      },
      error: (error) => {
        console.error('Login failed:', error);
        alert('Internal server error');
      }
    });
  }
}
