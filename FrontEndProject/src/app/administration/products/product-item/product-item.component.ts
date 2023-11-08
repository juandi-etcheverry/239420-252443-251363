import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatRippleModule]
})
export class ProductItemComponent {
  @Input() product!: Product;
  centered = false;
  disabled = true;
  unbounded = false;
}
