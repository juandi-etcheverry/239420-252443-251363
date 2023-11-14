import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import url from 'src/utils/url';
import { Brand, Category, Color, CreateProductRequest, GetProductReponse, GetProductsResponse, Product, ProductFilterForm, UpdateProductRequest } from 'src/utils/interfaces';
import { Observable } from 'rxjs';

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

  addProduct({
    name,
    price,
    description,
    brand,
    category,
    colors,
    stock,
  } : CreateProductRequest){
    return this.http.post<Product>(`${url}/products`, {
      Name: name,
      Price: price,
      Description: description,
      Brand: brand,
      Category: category,
      Colors: colors,
      Stock: stock,
    });
    }

    deleteProduct(id : string){
      return this.http.delete<Product>(`${url}/products/${id}`);
    }

    updateProduct({
      id,
      name,
      price,
      description,
      brand,
      category,
      colors,
      stock,
      promotionsApply
    } : UpdateProductRequest){
      return this.http.put<Product>(`${url}/products`, {
        Id : id,
        Name: name,
        Price: price,
        Description: description,
        Brand: brand,
        Category: category,
        Colors: colors,
        Stock: stock,
        promotionsApply: promotionsApply
      });
    }

    getAllColors(): Observable<Color[]>{
      return this.http.get<Color[]>(`${url}/colors`);
    }

    getAllCategories(): Observable<Category[]>{
      return this.http.get<Category[]>(`${url}/categories`);
    }

    getAllBrands(): Observable<Brand[]>{
      return this.http.get<Brand[]>(`${url}/brands`);
    }
}
