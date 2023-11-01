import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductsService } from './products.service';
import { Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];


  constructor(private changeDetector : ChangeDetectorRef, private productService : ProductsService){
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe((response) => {
      console.log(response);
      this.products = response.products;
      this.changeDetector.detectChanges();
    });

  }
  updateProductList($event: Product[]){
    this.products = $event;
    this.changeDetector.detectChanges();
  }

}
