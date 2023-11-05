import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import url from 'src/utils/url';
import { GetProductReponse, GetProductsResponse, Product, ProductFilterForm } from 'src/utils/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { 
  }

  getProduct(id : string){
    return this.http.get<GetProductReponse>(`${url}/products/${id}`);
  }
  getAllProducts(){
    return this.getProducts({});
  }

  getProducts(
    {
      textInput, 
      brandInput, 
      categoryInput, 
      minPrice, 
      maxPrice
    } : ProductFilterForm
  ){
      let params = new HttpParams();
      if(textInput) params = params.set('Text', textInput);
      if(brandInput) params = params.set('Brand', brandInput);
      if(categoryInput) params = params.set('Category', categoryInput);
      if(minPrice) params = params.set('MinPrice', minPrice);
      if(maxPrice) params = params.set('MaxPrice', maxPrice);
      return this.http.get<GetProductsResponse>(`${url}/products`, {params: params})
  }
}
