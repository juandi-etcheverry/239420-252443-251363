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
import { Brand } from '../brand';
import { Category } from '../category';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { ProductsService } from '../products.service';
import { ProductItem } from '../product-item';

interface Brands{
  value: string;
}
interface Categories{
  value: string;
}
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
    minPrice: new FormControl(0),
    maxPrice: new FormControl(Number.MAX_VALUE)
  });

  @Input()
  products!:ProductItem[]


  brand!:Brand[]

  category!:Category[]

  @Output()
  submitClicked!:EventEmitter<ProductItem[]>


  constructor(private productService : ProductsService){
    this.submitClicked = new EventEmitter();
    this.brand = productService.brandItems;
    this.category = productService.categoryItems;
  }

  onSubmit():void{
    const form = this.filterForm.value;
    const filterProducts = this.productService.filter(form.textInput ?? '', form.brandInput ?? '', form.categoryInput ?? '', form.minPrice ?? 0, form.maxPrice ?? Number.MAX_VALUE);
    this.submitClicked.emit(filterProducts);
  }
}
