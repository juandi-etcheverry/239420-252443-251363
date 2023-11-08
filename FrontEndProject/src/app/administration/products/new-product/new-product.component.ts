import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Router } from '@angular/router';
import { ProductsService } from 'src/app/products/products.service';
import { Brand, Category, CreateProductRequest, GetProductsResponse, Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.css'],
  standalone: true,
  imports: [MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule, CommonModule],
})
export class NewProductComponent {

  brands!:Brand[]
  categories!:Category[]
  colors!:string[]
  
  constructor(public dialogRef : MatDialogRef<NewProductComponent>, private productService : ProductsService, private router: Router) {
    this.productService.getAllProducts().subscribe((response: GetProductsResponse) => {
      this.brands = response.brands;
      this.categories = response.categories;
    });
   }

   newProductForm = new FormGroup({
    name: new FormControl('', {nonNullable: true}),
    price: new FormControl('', {nonNullable: true, validators: [Validators.pattern("^[0-9]*$")]}),
    description: new FormControl('', {nonNullable: true}),
    brand: new FormControl('', {nonNullable: true}),
    category: new FormControl('', {nonNullable: true}),
    colors : new FormControl('', {nonNullable: true}),
    stock: new FormControl('', {nonNullable: true}),
  });

  onSubmit(): void{
    const formData = this.newProductForm.value;
    const productData: CreateProductRequest = {
      name: formData.name,
      price: Number(formData.price),
      description: formData.description,
      brand: formData?.brand as Brand,
      category: formData?.category as Category,
      colors: [],
      stock: Number(formData.stock),
    };
    console.log(productData);
    this.productService.addProduct(productData).subscribe((response: Product) => {
      this.dialogRef.close();
    });
    
  }

  onCloseClick(): void{
    this.dialogRef.close();
  }
  
  }

