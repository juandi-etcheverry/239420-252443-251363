import { Component, OnInit } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTabsModule} from '@angular/material/tabs';

import {MatDividerModule} from '@angular/material/divider';
import { Router } from '@angular/router';
import { CartService } from '../cart/cart-service';


@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
  standalone: true,
  imports: [MatIconModule, MatButtonModule, MatToolbarModule, MatTabsModule, MatDividerModule]
})
export class ToolbarComponent implements OnInit {

  constructor(private router:Router){
  }
  ngOnInit(): void {
    
  }
  goToPage(url: string){
    this.router.navigate([url]);
  }
}
