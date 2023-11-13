import { CommonModule } from '@angular/common';
import { Component} from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ProductsService } from 'src/app/services/products.service';
import { Brand, Category, Color, CreateProductRequest, Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.css'],
  standalone: true,
  imports: [MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule, CommonModule, MatSnackBarModule],
})
export class NewProductComponent {

  brands!:Brand[]
  categories!:Category[]
  colors!:Color[]
  
  constructor(public dialogRef : MatDialogRef<NewProductComponent>, private productService : ProductsService, private router: Router, private _snackBar : MatSnackBar) {
    this.productService.getAllBrands().subscribe((response: Brand[]) => {
      this.brands = response;
    });

    this.productService.getAllColors().subscribe((response: Color[]) => {
      this.colors = response;
    });

    this.productService.getAllCategories().subscribe((response: Category[]) => {
      this.categories = response;
    });
   }

   newProductForm = new FormGroup({
    name: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(1)]}),
    price: new FormControl('', {nonNullable: true, validators: [Validators.pattern("^[0-9]*$"), Validators.required]}),
    description: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(1)]}),
    brand: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    category: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    colors : new FormControl([], {nonNullable: true}),
    stock: new FormControl('', {nonNullable: true, validators: [Validators.pattern("^[0-9]*$"), Validators.required]}),
  });

  onSubmit(): void{
    const formData = this.newProductForm.value;
    const productData: CreateProductRequest = {
      name: formData.name,
      price: Number(formData.price),
      description: formData.description,
      brand: formData?.brand as Brand,
      category: formData?.category as Category,
      colors: formData?.colors as Color[],
      stock: Number(formData.stock),
    };
    this.productService.addProduct(productData).subscribe((response: Product) => {
      this.dialogRef.close();
      this._snackBar.open("Product added successfully", 'Close', {duration: 2000});
    });
    
  }

  onCloseClick(): void{
    this.dialogRef.close();
  }
  
  }

