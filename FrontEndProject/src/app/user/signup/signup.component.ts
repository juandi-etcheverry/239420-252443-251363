import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UsersService } from '../users.service';
import { ErrorStatus, SignupResponse } from 'src/utils/interfaces';
import { AuthService } from 'src/app/auth.service';
import { HttpResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CartService } from 'src/app/cart/cart-service';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule, FormsModule, MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatSnackBarModule, MatSnackBarModule, CommonModule],
})
export class SignupComponent {


  constructor(private router: Router, private userService: UsersService, 
    private _snackBar: MatSnackBar, private authService: AuthService, private cartService : CartService) {
      if (authService.hasAuthToken()) {
        _snackBar.open("You are already logged in", 'Close');
        this.goToPage("/");
      }
    }

  validatePasswords = (control: AbstractControl) : { passwordMismatch : boolean} | null => {
    const pw = this.signupForm?.get("password")?.value;
    const confirmpw = this.signupForm?.get("passwordConfirmation")?.value;

    return pw === confirmpw ? null : {
      passwordMismatch : true
    };
  }

  signupForm = new FormGroup({
    email: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.email]}, ),
    address: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(2)]}),
    password: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(5)]}),
    passwordConfirmation: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(5), this.validatePasswords]}),
  });

  goToPage(url: string){
    this.router.navigate([url]);
  }

  onSubmit() {
    const formData = this.signupForm.getRawValue();

    this.userService.signup(formData).subscribe({
      next: (response) => {
        this.authService.setAuthToken(response.headers.get('Authorization') as string);
        this.cartService.signIn();
        this._snackBar.open('User created successfully', 'Close');
        this.goToPage('/products');
      },
       error: (error: ErrorStatus) => {
         this._snackBar.open(error.error.message, 'Close');
       }
    });

  }
}