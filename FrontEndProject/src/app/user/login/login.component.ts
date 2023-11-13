import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { UsersService } from '../users.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/auth.service';
import { ErrorStatus } from 'src/utils/interfaces';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule, MatInputModule, MatFormFieldModule, ReactiveFormsModule, CommonModule, MatButtonModule]
})
export class LoginComponent {

  constructor(private router: Router, private userService: UsersService,
    private _snackBar: MatSnackBar, private authService: AuthService, private cartService : CartService) {
      if (authService.hasAuthToken()) {
        _snackBar.open("You are already logged in", 'Close', {duration: 2000});
        this.goToPage("/");
      }
     }


  loginForm = new FormGroup({
    email: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.email]}, ),
    password: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(5)]}),
  });

  onSubmit() {
    const formData = this.loginForm.getRawValue();
    this.userService.login(formData).subscribe({
      next: (response) => {
        this.authService.setAuthToken(response.headers.get('Authorization') as string);
        this.cartService.signIn();
        this._snackBar.open('Log In successfully', 'Close', {duration: 2000});
        this.goToPage('/products');
      },
      error: (error: ErrorStatus) => {
        this._snackBar.open(error.error.message, 'Close');
      }
    });
  }



  SignUp(){
    this.goToPage("/signup");
  }

  goToPage(url: string){
    this.router.navigate([url]);
  }
}
