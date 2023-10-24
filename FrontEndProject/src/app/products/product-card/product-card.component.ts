import { Component, OnInit, Input } from '@angular/core';
import { MatButtonModule} from '@angular/material/button';
import { MatCardModule} from '@angular/material/card'; 
import { ProductItem } from '../product-item';
import { MatRippleModule} from '@angular/material/core';
import { MatInputModule} from '@angular/material/input';
import { MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule} from '@angular/forms';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatCheckboxModule, FormsModule, MatFormFieldModule, MatInputModule, MatRippleModule, RouterModule],
})
export class ProductCardComponent implements OnInit{
  @Input()
  productItem!: ProductItem;
  centered = false;
  disabled = false;
  unbounded = false;
  constructor(private router:Router){
  }

  ngOnInit(): void {
  }

  onAddClick($event : Event){
    $event.stopPropagation()
    alert("Agregar al carrito");
  }

  onCardClick(id: number){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }
}
