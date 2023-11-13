import {Component, OnInit} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';
import { ErrorStatus, GetUserResponse, UpdateUserProps } from 'src/utils/interfaces';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { z } from 'zod';

const UpdateUserSchema = z.object({
  email: z.string().email(),
  address: z.string().min(2).max(100),
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
    email: new FormControl(''),
    role: new FormControl(1),
    address: new FormControl(''),
  });

  constructor(private router: Router, private usersService: UsersService, 
    private route: ActivatedRoute, private _snackBar: MatSnackBar){

  }

  goToPage(url: string){
    this.router.navigate([url]);
  }
  goToAdmin(){
    this.goToPage(`/admin`);
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

    const completeFromData: UpdateUserProps = {id: this.userId, ...formData};

    this.usersService.updateUser(completeFromData)
    .subscribe((response) => {
      this.data = response;
      this._snackBar.open(response.message, 'Close', {duration: 2000});
    });
  }
}
