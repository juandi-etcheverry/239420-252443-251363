import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/services/users.service';
import { ModifyUserComponent } from './modify-user/modify-user.component';
import { User } from 'src/utils/interfaces';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatRippleModule, MatSnackBarModule]
})
export class UserItemComponent {
  @Input() user!: User;
  centered = false;
  disabled = true;
  unbounded = false;

  @Output() refreshUsers = new EventEmitter<void>();

  constructor(private userService : UsersService, private _snackBar : MatSnackBar, public dialog: MatDialog){}

  onDelete(){
    this.userService.deleteUser(this.user.id).subscribe((response) => {
      this.refreshUsers.emit();
      this._snackBar.open('User Deleted', 'Close', {duration: 2000});
    });
  }
  
  onModify(){
    const dialogRef = this.dialog.open(ModifyUserComponent, {data: this.user});
    dialogRef.afterClosed().subscribe(result => {
      this.refreshUsers.emit();
    });
  }
}
