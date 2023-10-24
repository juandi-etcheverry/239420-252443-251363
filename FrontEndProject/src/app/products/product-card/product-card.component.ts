import { Component, OnInit, Input } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
})
export class ProductCardComponent implements OnInit{

  @Input() name: string;
  @Input() price: string;
  @Input() brand: string;
  @Input() category: string;
  @Input() colour: string;

  constructor(){
    this.name="pelota";
    this.price="$50";
    this.brand="Nike";
    this.category="Juguete";
    this.colour="Rojo";

  }

  ngOnInit(): void {
    
  }

}
