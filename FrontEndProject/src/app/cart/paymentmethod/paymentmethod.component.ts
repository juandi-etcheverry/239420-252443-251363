import { Component, OnInit } from '@angular/core';
import {MatRadioModule} from '@angular/material/radio';
import {NgFor} from '@angular/common';

@Component({
  selector: 'app-paymentmethod',
  templateUrl: './paymentmethod.component.html',
  styleUrls: ['./paymentmethod.component.css'],
  standalone: true,
  imports: [MatRadioModule, NgFor]
})
export class PaymentmethodComponent implements OnInit{
  paymentOption: string;
  options: string[] = ['Credit card', 'Bank debit', 'Paypal', 'Paganza'];

  constructor(){
    this.paymentOption = "";
  }
  ngOnInit(): void {
  }
}
