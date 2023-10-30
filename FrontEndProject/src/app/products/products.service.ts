import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import url from 'src/utils/url';
import { Products } from 'src/utils/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { 
  }

  getAllProducts(){
    return this.getProducts("", "", "", 0, Number.MAX_VALUE);
  }

  getProducts(text?: string, brand?: string, category?: string, minPrice?: number, maxPrice?: number) {
    
    let params = new HttpParams();
    if(text) params = params.set('Text', text);
    if(brand) params = params.set('Brand', brand);
    if(category) params = params.set('Category', category);
    if(minPrice) params = params.set('MinPrice', minPrice);
    if(maxPrice) params = params.set('MaxPrice', maxPrice);
    
    return this.http.get<Products>(`${url}/products`, {params: params})
  }
}
