import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatRadioChange, MatRadioModule } from '@angular/material/radio';
import { NgFor } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialog,
  MatDialogRef,
  MatDialogModule,
} from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CartService } from '../cart.service';
import { CartItem, ErrorStatus, PurchaseResponse } from 'src/utils/interfaces';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-paymentmethod',
  templateUrl: './paymentmethod.component.html',
  styleUrls: ['./paymentmethod.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatRadioModule, NgFor],
})
export class PaymentmethodComponent implements OnInit {
  @Output() deleteCart = new EventEmitter<void>();
  @Output() change = new EventEmitter<string>();
  paymentOption: string;
  options: { label: string; value: string }[] = [
    { label: 'Visa', value: 'CreditVisa' },
    { label: 'MasterCard', value: 'CreditMastercard' },
    { label: 'Santander', value: 'DebitSantander' },
    { label: 'ITAU', value: 'DebitItau' },
    { label: 'BBVA', value: 'DebitBBVA' },
    { label: 'Paypal', value: 'Paypal' },
    { label: 'Paganza', value: 'Paganza' },
  ];

  constructor(public dialog: MatDialog, private cartService: CartService) {
    this.paymentOption = '';
  }

  openDialog(
    enterAnimationDuration: string,
    exitAnimationDuration: string
  ): void {
    this.cartService.setPaymentMethod(this.paymentOption);
    const dialogRef = this.dialog.open(ConfirmPurchase, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
    });

    dialogRef.componentInstance.deleteCart.subscribe(() => {
      this.deleteCart.emit();
    });
  }

  ngOnInit(): void {}

  updatePaymentMethod($event: MatRadioChange) {
    this.paymentOption = $event.value;
    this.change.emit(this.paymentOption);
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

  constructor(
    public dialogRef: MatDialogRef<ConfirmPurchase>,
    private _snackBar: MatSnackBar,
    private cartService: CartService
  ) {}

  processPurchase() {
    this.cartService.addPurchase().subscribe({
      next: (response: PurchaseResponse) => {
        this.deleteCart.emit();
        this._snackBar.open('Successful purchase!', 'Close', {
          duration: 2000,
        });
      },
      error: (error: ErrorStatus) => {
        this._snackBar.open('Error in purchase!', 'Close', { duration: 2000 });
      },
    });
  }
}
