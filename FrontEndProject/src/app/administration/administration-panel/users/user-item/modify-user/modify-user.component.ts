import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/user/users.service';
import { Inject } from '@angular/core';
import { UpdateUserResponse, User } from 'src/utils/interfaces';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modify-user',
  templateUrl: './modify-user.component.html',
  standalone: true,
  styleUrls: ['./modify-user.component.css'],
  imports: [MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule, CommonModule, MatSnackBarModule]
})
export class ModifyUserComponent {
  
  constructor(public dialogRef : MatDialogRef<ModifyUserComponent>, private userService : UsersService, private _snackBar : MatSnackBar, 
    @Inject(MAT_DIALOG_DATA) public data: User) { }

  ModifyUserForm = new FormGroup({
    email: new FormControl(this.data.email, {nonNullable: true, validators: [Validators.required, Validators.email]}),
    address: new FormControl(this.data.address, {nonNullable: true, validators: [Validators.required, Validators.minLength(1)]}),
    role: new FormControl(this.data.role, {nonNullable: true, validators: [Validators.required, Validators.pattern("^[1-3]$")]}),
  });

  onSubmit(){
    const formData = this.ModifyUserForm.value;
    const userData = {
      id: this.data.id,
      email: formData.email,
      address: formData.address,
      role: formData.role      
    };

    this.userService.updateUser(userData).subscribe((response: UpdateUserResponse) => {
      this.dialogRef.close();
      this._snackBar.open("Product modified successfully", 'Close', {duration: 2000});
    });
  }

  onCloseClick(){
    this.dialogRef.close();
  }
}
