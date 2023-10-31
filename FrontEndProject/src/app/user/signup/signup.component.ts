import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { z } from 'zod'; 
import { UsersService } from '../users.service';
import { ErrorStatus, SignupResponse } from 'src/utils/interfaces';
import { AuthService } from 'src/app/auth.service';
import { HttpResponse } from '@angular/common/http';


const SignupSchema = z.object({
  email: z.string().email(),
  address: z.string().min(2).max(100),
  password: z.string().min(5).max(100),
  confirmPassword: z.string().min(5).max(100),
}).refine(data => data.password === data.confirmPassword, {
  message: 'Passwords must match',
});


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule, FormsModule, MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatSnackBarModule, MatSnackBarModule],
})
export class SignupComponent {


  constructor(private router: Router, private userService: UsersService, 
    private _snackBar: MatSnackBar, private authService: AuthService) {}

  signupForm = new FormGroup({
    email: new FormControl(''),
    address: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });

  goToPage(url: string){
    this.router.navigate([url]);
  }

  onSubmit() {
    const formData = this.signupForm.value;

    try {
      SignupSchema.parse(formData);

    } catch (error) {
      if(error instanceof z.ZodError){
        this._snackBar.open(error.errors[0].message, 'Close');
      }
      return;
    }

    this.userService.signup(formData.email ?? '', formData.address ?? '', formData.password ?? '', formData.confirmPassword ?? '').subscribe({
      next: (response) => {
        this.authService.setAuthToken(response.headers.get('Authorization') as string);
        this._snackBar.open('User created successfully', 'Close');
        this.goToPage('/products');
      },
       error: (error: ErrorStatus) => {
         console.log(error);
         this._snackBar.open(error.error.message, 'Close');
       }
    });

  }
}