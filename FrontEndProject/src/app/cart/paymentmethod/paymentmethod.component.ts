import { Component, OnInit } from '@angular/core';
import {MatRadioModule} from '@angular/material/radio';
import {NgFor} from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';

@Component({
  selector: 'app-paymentmethod',
  templateUrl: './paymentmethod.component.html',
  styleUrls: ['./paymentmethod.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatRadioModule, NgFor]
})
export class PaymentmethodComponent implements OnInit{
  paymentOption: string;
  options: string[] = ['Visa', 'MasterCard', 'Santander', 'ITAU', 'BBVA',  'Paypal', 'Paganza'];

  constructor(public dialog: MatDialog){
    this.paymentOption = "";
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(DialogAnimationsExampleDialog, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
  ngOnInit(): void {
  }

}


@Component({
  selector: 'dialog-animations-example-dialog',
  templateUrl: 'dialog-animations-example-dialog.html',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, MatSnackBarModule],
})
export class DialogAnimationsExampleDialog {
  constructor(public dialogRef: MatDialogRef<DialogAnimationsExampleDialog>, private _snackBar: MatSnackBar) {}

  processPurchase(){
    this._snackBar.open('Successfull purchase!', 'Close');
  }
}

