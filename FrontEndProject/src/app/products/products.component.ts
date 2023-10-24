import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductItem } from './product-item';
import { Brand } from './brand';
import { Category } from './category';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productItems: ProductItem[] = [
    /*{name: "Pelota", price: 50, brand: "Nike", category: "Deporte", colour: "Negro"},
    {name: "Remera", price: 70, brand: "Nike", category: "Deporte", colour: "Rojo"},
    {name: "Buzo", price: 100, brand: "Zara", category: "Ropa", colour: "Verde"},
    {name: "Pistola", price: 800, brand: "Nerf", category: "Jugetes", colour: "Azul"},*/
  ]
  brandItems: Brand[] = [
    {name: "Nike"},
    {name: "Adidas"},
    {name: "Zara"},
    {name: "Apple"},
  ]
  categoryItems: Category[] = [
    {name: "Jugetes"},
    {name: "Ropa"},
    {name: "Deportes"},
    {name: "Electronicos"},
  ]
  constructor(){
 
  }
  ngOnInit(): void {
    
  }
  updateProductList($event: ProductItem[]){
    this.productItems = $event;
    console.log(this.productItems);
  }


}
