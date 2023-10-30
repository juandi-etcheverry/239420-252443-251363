import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductsService } from './products.service';
import { Products } from 'src/utils/interfaces';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productItems: Products | null = null;


  constructor(private changeDetector : ChangeDetectorRef, private productService : ProductsService){
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe((products) => {
      console.log(products);
      this.productItems = products;
      this.changeDetector.detectChanges();
    });

  }
  updateProductList($event: Products){
    this.productItems = $event;
    this.changeDetector.detectChanges();
  }


}
