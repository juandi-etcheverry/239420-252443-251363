import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductItem } from './product-item';
import { Brand } from './brand';
import { Category } from './category';
import { ProductsService } from './products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productItems: ProductItem[] = []


  constructor(private changeDetector : ChangeDetectorRef, productService : ProductsService){
    this.productItems = productService.productItems;
  }
  ngOnInit(): void {
    
  }
  updateProductList($event: ProductItem[]){
    this.productItems = $event;
    this.changeDetector.detectChanges();
    
  }


}
