import { CommonModule } from '@angular/common';
import { Component, Inject, Input } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ProductsService } from 'src/app/products/products.service';
import { Brand, Category, GetProductsResponse, Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-modify-product',
  templateUrl: './modify-product.component.html',
  styleUrls: ['./modify-product.component.css'],
  standalone: true,
  imports: [MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, ReactiveFormsModule, CommonModule, MatSnackBarModule]
})
export class ModifyProductComponent {

  brands!:Brand[]
  categories!:Category[]
  colors!:string[]

  constructor(public dialogRef : MatDialogRef<ModifyProductComponent>, private productService : ProductsService, private _snackBar : MatSnackBar, @Inject(MAT_DIALOG_DATA) public data: Product){
    this.productService.getAllProducts().subscribe((response: GetProductsResponse) => {
      this.brands = response.brands;
      this.categories = response.categories;
    });
  }

  ModifyProductForm = new FormGroup({
    name: new FormControl(this.data.name, {nonNullable: true, validators: [Validators.required, Validators.minLength(1)]}),
    price: new FormControl('', {nonNullable: true, validators: [Validators.pattern("^[0-9]*$"), Validators.required]}),
    description: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(1)]}),
    brand: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    category: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    colors : new FormControl('', {nonNullable: true}),
    stock: new FormControl('', {nonNullable: true, validators: [Validators.pattern("^[0-9]*$"), Validators.required]}),
  });

  onSubmit(){
    const formData = this.ModifyProductForm.value;
  }
  onCloseClick(){
    this.dialogRef.close();
  }
}
