import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {MatRadioModule} from '@angular/material/radio';
import {NgFor} from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { CartService } from '../../services/cart.service';
import { ErrorStatus, PurchaseResponse } from 'src/utils/interfaces';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-paymentmethod',
  templateUrl: './paymentmethod.component.html',
  styleUrls: ['./paymentmethod.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatRadioModule, NgFor]
})
export class PaymentmethodComponent implements OnInit{
  @Output() deleteCart = new EventEmitter<void>();
  paymentOption: string;
  options: string[] = ['Visa', 'MasterCard', 'Santander', 'ITAU', 'BBVA',  'Paypal', 'Paganza'];

  constructor(public dialog: MatDialog){
    this.paymentOption = "";
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    const dialogRef = this.dialog.open(ConfirmPurchase, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
    });

    dialogRef.componentInstance.deleteCart.subscribe(() => {
      this.deleteCart.emit();
    });
  }
  
  ngOnInit(): void {
  }

}


@Component({
  selector: 'dialog-animations-example-dialog',
  templateUrl: 'confirmPurchase.html',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatSnackBarModule],
})
export class ConfirmPurchase {

  @Output() deleteCart = new EventEmitter<void>();

  constructor(public dialogRef: MatDialogRef<ConfirmPurchase>, private _snackBar: MatSnackBar,
              private cartService: CartService, private authService: AuthService) {}

  processPurchase(){
    this.cartService.addPurchase().subscribe({
      next: (response: PurchaseResponse) => {
        this.deleteCart.emit();
        this._snackBar.open('Successful purchase!', 'Close', {duration: 2000});
      },
      error: (error: ErrorStatus) => {
        this._snackBar.open('Error in purchase!', 'Close', {duration: 2000});
      }});
    }

}