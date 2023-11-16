import { NgForOf, NgIf } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SinglePurchase } from 'src/utils/interfaces';

@Component({
  selector: 'app-purchase-history-item',
  templateUrl: './purchase-history-item.component.html',
  styleUrls: ['./purchase-history-item.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatRippleModule, MatSnackBarModule, NgForOf, NgIf]
})
export class PurchaseHistoryItemComponent {
  @Input() purchase!: SinglePurchase
  centered = false;
  disabled = true;
  unbounded = false;


  constructor(){
  }

  ngOnInit() {
  }
}
