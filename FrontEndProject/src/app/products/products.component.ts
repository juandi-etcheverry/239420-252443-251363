import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ProductsService } from './products.service';
import { Product } from 'src/utils/interfaces';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];
  cants: {product : Product, cant : number}[] = [];


  constructor(private changeDetector : ChangeDetectorRef, private productService : ProductsService, private cartService : CartService){
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe((response) => {
      console.log(response);
      this.products = response.products;
      this.changeDetector.detectChanges();
      this.updateProductsCant();
    });
  }
  updateProductList($event: Product[]){
    this.products = $event;
    this.updateProductsCant();
    this.changeDetector.detectChanges();
  }
  updateProductsCant(){
    this.cants = [];
    for(let product of this.products){
      this.cants.push({product : product, cant : this.cartService.getCantOfItem(product.id)});
    }
  }
}
