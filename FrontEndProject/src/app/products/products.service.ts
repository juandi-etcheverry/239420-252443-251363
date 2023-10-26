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
    {id: 1, name: "Pelota", price: 50, brand: "Nike", category: "Deporte", colour: "Negro"},
    {id: 2, name: "Remera", price: 70, brand: "Nike", category: "Ropa", colour: "Rojo"},
    {id: 3, name: "Buzo", price: 100, brand: "Zara", category: "Ropa", colour: "Verde"},
    {id: 4, name: "Pistola", price: 800, brand: "Nerf", category: "Jugetes", colour: "Azul"},
    {id: 5, name: "Pelota", price: 60, brand: "Adidas", category: "Deporte", colour: "Azul"},
    {id: 6, name: "Remera", price: 80, brand: "Zara", category: "Ropa", colour: "Roja"},
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
      p.name.toLowerCase().includes(text.toLowerCase()) &&
      p.brand.toLowerCase().includes(brand.toLowerCase()) &&
      p.category.toLowerCase().includes(category.toLowerCase()) &&
      (p.price >= minPrice && p.price <= maxPrice)
    );
    return newList;
  }
}
