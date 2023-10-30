import {Component, OnInit} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from './users.service';
import { ErrorStatus, GetUserResponse } from 'src/utils/interfaces';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { z } from 'zod';

const UpdateUserSchema = z.object({
  correo: z.string().email(),
  direccion: z.string().min(2).max(100),
});

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatDividerModule, MatIconModule, FormsModule, MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatSnackBarModule],
})

export class UserPanelComponent implements OnInit {

  data: GetUserResponse | null = null;
  userId: string = '';

  userForm = new FormGroup({
    correo: new FormControl(''),
    rol: new FormControl(''),
    direccion: new FormControl(''),
  });

  constructor(private router: Router, private usersService: UsersService, 
    private route: ActivatedRoute, private _snackBar: MatSnackBar){

  }

  goToPage(url: string){
    this.router.navigate([url]);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params['id'];
    });

    this.usersService.getUser(this.userId).subscribe({
      next: (response: GetUserResponse) => {
        this.data = response;
      },
      error: (error: ErrorStatus) => {
        if(error.status == 400) this.goToPage('/products');
        if(error.status == 401) this.goToPage('/login');
      }
    });
  }

  onSubmit(): void {
    const formData = this.userForm.value;

    try{
      UpdateUserSchema.parse(formData);
    }
    catch(error){
      if(error instanceof z.ZodError){
        this._snackBar.open(error.errors[0].message, 'Close');
      }
      return;
    }

    this.usersService.updateUser(this.userId, formData.correo ?? '', formData.direccion ?? '', this.data?.role ?? 1)
    .subscribe((response) => {
      this.data = response;
      this._snackBar.open(response.message, 'Close');
    });
  }
}
