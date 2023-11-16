import { Component, EventEmitter, Input, Output } from '@angular/core';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';
import {NgFor} from '@angular/common';
import {MatSelectModule} from '@angular/material/select';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { ProductsService } from '../products.service';
import { GetProductsResponse, Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-filtered',
  templateUrl: './filtered.component.html',
  styleUrls: ['./filtered.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, MatSlideToggleModule, MatInputModule, MatFormFieldModule, FormsModule, NgFor, MatSelectModule, MatButtonModule, MatDividerModule, MatIconModule]
})
export class FilteredComponent {
 
  filterForm = new FormGroup({
    textInput: new FormControl(''),
    brandInput: new FormControl(''),
    categoryInput: new FormControl(''),
    minPrice: new FormControl(),
    maxPrice: new FormControl()
  });

  @Input()
  products!:Product[]


  brands!:string[]

  categories!:string[]


  @Output()
  submitClicked!:EventEmitter<Product[]>


  constructor(private productService : ProductsService){
    this.submitClicked = new EventEmitter();
    
      this.productService.getAllProducts().subscribe((response: GetProductsResponse) => {
        this.brands = response.brands.map((brand) => {
          if(brand.name !== undefined) return brand.name;
          return "";
        });
        this.categories = response.categories.map((category) => {
          if(category.name !== undefined) return category.name;
          return "";
        });
      });
  }

  onSubmit():void{
    const form = this.filterForm.value;
    this.productService.getProducts(form)
    .subscribe((response) => {
      this.products = response.products;
      this.submitClicked.emit(response.products);
    });
  }
}
