import { Component, OnInit, Input } from '@angular/core';
import { MatButtonModule} from '@angular/material/button';
import { MatCardModule} from '@angular/material/card'; 
import { ProductItem } from '../product-item';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
})
export class ProductCardComponent implements OnInit{
  @Input()
  productItem!: ProductItem;
  constructor(){
  }

  ngOnInit(): void {
    
  }

}
