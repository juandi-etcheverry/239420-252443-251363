import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule],
})
export class ProductDetailComponent implements OnInit {


  constructor(private router : Router){

  }
  ngOnInit(): void {
    
  }
  goToPage(url: string){
    this.router.navigate([url]);
  }
  onAddClick(){
    alert("agregar al carrito");
  }
}

