import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProductsService } from 'src/app/products/products.service';
import { Product } from 'src/utils/interfaces';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ProductItemComponent } from './products/product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { NewProductComponent } from './products/new-product/new-product.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UsersService } from 'src/app/user/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administration-panel',
  templateUrl: './administration-panel.component.html',
  styleUrls: ['./administration-panel.component.css'],
  standalone: true,
  imports: [ MatButtonModule, MatDividerModule, MatIconModule, ProductItemComponent, CommonModule, MatSnackBarModule]
})
export class AdministrationPanelComponent {
  products : Product[] = [];

  constructor(private productService : ProductsService, public dialog: MatDialog, private _snackBar : MatSnackBar, 
    private userService: UsersService, private router: Router) { }

  ngOnInit(): void {
    this.userService.getLoggedUser()?.subscribe((user) => {
      if(!user || user.role === 1){
        this._snackBar.open("You don't have access to this page", "Close", {
          duration: 2000,
        });
        this.goToPage("/products");
      }
    });

    this.productService.getAllProducts().subscribe((response) => {
      this.products = response.products;
    });
  }

  goToPage(url: string){
    this.router.navigate([url]);
  }

  addNewProduct(){
    const dialogRef = this.dialog.open(NewProductComponent);
    dialogRef.afterClosed().subscribe(result => {
      this.refresh();
    });
    
  }

  refresh(){
    this.productService.getAllProducts().subscribe((response) => {
      this.products = response.products;
    });
  }
}

