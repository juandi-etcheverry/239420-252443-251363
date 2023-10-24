import { Injectable } from '@angular/core';
import { ProductItem } from './product-item';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  //Import from back-end
  productItems: ProductItem[] = [
    {name: "Pelota", price: 50, brand: "Nike", category: "Deporte", colour: "Negro"},
    {name: "Remera", price: 70, brand: "Nike", category: "Deporte", colour: "Rojo"},
    {name: "Buzo", price: 100, brand: "Zara", category: "Ropa", colour: "Verde"},
    {name: "Pistola", price: 800, brand: "Nerf", category: "Jugetes", colour: "Azul"},
  ]

  constructor(private http: HttpClient) { 
  }

  filter(text: string, brand: string, category: string, minPrice: number, maxPrice: number): ProductItem[] {
    return this.productItems.filter((p) =>
      p.name.includes(text) &&
      p.brand.toLowerCase().includes(brand.toLowerCase()) &&
      p.category.toLowerCase().includes(category.toLowerCase()) &&
      (p.price >= minPrice && p.price <= maxPrice)
    );
  }
}
