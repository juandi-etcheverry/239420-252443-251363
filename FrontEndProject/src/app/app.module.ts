import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NuestroComponenteComponent } from './nuestro-componente/nuestro-componente.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsComponent } from './tabs/tabs.component';
import { ProductsComponent } from './products/products.component';
import { UserComponent } from './user/user.component';
import { CartComponent } from './cart/cart.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { Routes} from '@angular/router';
import { CartItemComponent } from './cart/cart-item/cart-item.component';
import { ProductCardComponent } from './products/product-card/product-card.component';
import { FilteredComponent } from './products/filtered/filtered.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';



const routes: Routes = [
  {path: '', redirectTo: 'products', pathMatch: 'full'},
  {path: 'products', component: ProductsComponent},
  {path: 'user', component: UserComponent},
  {path: 'cart', component: CartComponent},
  {path: '**', component: PagenotfoundComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    NuestroComponenteComponent,
    ProductsComponent,
    UserComponent,
    CartComponent,
    PagenotfoundComponent,
    CartItemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToolbarComponent,
    TabsComponent,
    ProductCardComponent,
    FormsModule,
    ReactiveFormsModule,
    FilteredComponent,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
