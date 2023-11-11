import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/user/users.service';
import { CreateUserRequest } from 'src/utils/interfaces';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css'],
  standalone: true,
  imports: [MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule, CommonModule, MatSnackBarModule],

})
export class NewUserComponent {

  constructor(public dialogRef : MatDialogRef<NewUserComponent>, private userService: UsersService,
    private _snackBar : MatSnackBar) { }



  validatePasswords = (control: AbstractControl) : { passwordMismatch : boolean} | null => {
    const pw = this.createUserForm?.get("password")?.value;
    const confirmpw = this.createUserForm?.get("passwordConfirmation")?.value;

    return pw === confirmpw ? null : {
      passwordMismatch : true
    };
  }

  createUserForm = new FormGroup({
    email: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.email]}),
    address: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(2)]}),
    role: new FormControl(1, {nonNullable: true, validators: [Validators.required, Validators.pattern("^[1-3]$")]}),
    password: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(5)]}),
    passwordConfirmation: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(5), this.validatePasswords]}),
  });

  onSubmit(): void {
    const formData = this.createUserForm.value;
    const userData: CreateUserRequest = {
      email: formData.email as string,
      address: formData.address as string,
      role: Number(formData.role),
      password: formData.password as string,
      passwordConfirmation: formData.passwordConfirmation as string,
    };

    this.userService.createUser(userData).subscribe((response) => {
      this.dialogRef.close();
      this._snackBar.open("User added successfully", 'Close', {duration: 2000});
    })
    
  }

  onCloseClick(): void{
    this.dialogRef.close();
  }

}
