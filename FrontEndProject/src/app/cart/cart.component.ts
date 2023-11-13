import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { CartItemComponent } from './cart-item/cart-item.component';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { CartItem } from "src/utils/interfaces";
import { CartService } from './cart.service';
import { CommonModule } from '@angular/common';
import { PaymentmethodComponent } from './paymentmethod/paymentmethod.component';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { UsersService } from '../user/users.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [PaymentmethodComponent, MatButtonModule, MatDividerModule, MatIconModule, CartItemComponent, CommonModule, MatSnackBarModule]
})
export class CartComponent implements OnInit{

  
  cartItems: CartItem[] = this.cartService.items;
  showPayment : Boolean = false;
  buttonMessage : String = "Purchase";
  userLogged : Boolean = false;
  
  constructor(private cartService : CartService, public dialog: MatDialog, private authService: AuthService,
    private userService: UsersService, private _snackBar : MatSnackBar){
}

  ngOnInit(): void {
    this.userLogged = this.authService.hasAuthToken();
    this.cartService.loadCartFromLocalStorage();
    this.cartItems = this.cartService.items;
  }

  get total():number{
    return this.cartService.total;
  }

  showHide(){
    this.cartItems = this.cartService.items;
    if(this.cartItems.length == 0){
      this._snackBar.open('Cannot purchase 0 items', 'Close', {duration: 2000});
    }
    else{
      if(this.userLogged){
        if(this.showPayment){
          this.hidePaymentPanel();
        }else{
          this.showPaymentPanel();
        }
      }
      else{
        this.openDialog('0ms', '0ms');
      }
    }
  }
  showPaymentPanel(){
    this.showPayment = true;
    this.buttonMessage = "Cancel purchase";
  }
  hidePaymentPanel(){
    this.showPayment = false;
    this.buttonMessage = "Purchase";
  }
  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(askForLogIn, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
  Refresh(){
    this.cartItems = this.cartService.items;
    if(this.cartItems.length == 0){
      this.hidePaymentPanel();
    }
  }

  deleteCart(){
    this.userService.getLoggedUser()?.subscribe({
      next: (user) => {
        this.cartService.removeCartId(user.id);
        this.cartItems = [];
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.Refresh();
  }
}

@Component({
  selector: 'dialog-animations-example-dialog',
  templateUrl: 'askForLogIn.html',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatSnackBarModule],
})
export class askForLogIn {
  constructor(public dialogRef: MatDialogRef<askForLogIn>, private _snackBar: MatSnackBar, private router : Router) {}

  LogInNeeded(){
    this._snackBar.open('Successfull purchase!', 'Close', {duration: 2000});
  }

  redirectToLogin(){
    this.router.navigate(['/login']);
  }
}


