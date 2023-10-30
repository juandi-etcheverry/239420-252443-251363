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
import { ProductItem, Products } from 'src/utils/interfaces';

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
  products!:Products


  brands!:string[]

  categories!:string[]


  @Output()
  submitClicked!:EventEmitter<Products>


  constructor(private productService : ProductsService){
    this.submitClicked = new EventEmitter();
    
      this.productService.getAllProducts().subscribe((products: Products) => {
        this.brands = Array.from(new Set(products.products.map(product => product.brand)));
        this.categories = Array.from(new Set(products.products.map(product => product.category)));
    });
  }

  onSubmit():void{
    const form = this.filterForm.value;
    console.log(form)
    this.productService.getProducts(form.textInput ?? '', form.brandInput ?? '', form.categoryInput ?? '', form.minPrice ?? 0, form.maxPrice ?? Number.MAX_VALUE)
    .subscribe((products) => {
      this.products = products;
      this.submitClicked.emit(products);
    });
  }
}
