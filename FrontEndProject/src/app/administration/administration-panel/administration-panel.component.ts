import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ProductsService } from 'src/app/products/products.service';
import { User } from 'src/app/user/user-model';
import { Product } from 'src/utils/interfaces';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ProductItemComponent } from './products/product-item/product-item.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { NewProductComponent } from './products/new-product/new-product.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-administration-panel',
  templateUrl: './administration-panel.component.html',
  styleUrls: ['./administration-panel.component.css'],
  standalone: true,
  imports: [ MatButtonModule, MatDividerModule, MatIconModule, ProductItemComponent, CommonModule, MatSnackBarModule]
})
export class AdministrationPanelComponent {
  products : Product[] = [];

  constructor(private productService : ProductsService, public dialog: MatDialog, private _snackBar : MatSnackBar) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe((response) => {
      this.products = response.products;
    });
  }

  addNewProduct(){
    const dialogRef = this.dialog.open(NewProductComponent);
    dialogRef.afterClosed().subscribe(result => {
      this._snackBar.open('Product Added', 'Close');
      this.refresh();
    });
    
  }

  refresh(){
    this.productService.getAllProducts().subscribe((response) => {
      this.products = response.products;
    });
  }
}

