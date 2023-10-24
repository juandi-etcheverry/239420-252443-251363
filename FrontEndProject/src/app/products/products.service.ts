import { Injectable } from '@angular/core';
import { ProductItem } from './product-item';
import { HttpClient } from '@angular/common/http';
import { Brand } from './brand';
import { Category } from './category';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  //Import from back-end
  productItems: ProductItem[] = [
    {name: "Pelota", price: 50, brand: "Nike", category: "Deporte", colour: "Negro"},
    {name: "Remera", price: 70, brand: "Nike", category: "Ropa", colour: "Rojo"},
    {name: "Buzo", price: 100, brand: "Zara", category: "Ropa", colour: "Verde"},
    {name: "Pistola", price: 800, brand: "Nerf", category: "Jugetes", colour: "Azul"},
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
    {name: "Deporte"},
    {name: "Electronicos"},
  ]

  constructor(private http: HttpClient) { 
  }

  filter(text: string, brand: string, category: string, minPrice: number, maxPrice: number): ProductItem[] {
    const newList = this.productItems.filter((p) =>
      p.name.includes(text) &&
      p.brand.toLowerCase().includes(brand.toLowerCase()) &&
      p.category.toLowerCase().includes(category.toLowerCase()) &&
      (p.price >= minPrice && p.price <= maxPrice)
    );
    return newList;
  }
}
