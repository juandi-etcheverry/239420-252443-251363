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
import { CartItemComponent } from './cart/cart-item/cart-item.component';
import { ProductCardComponent } from './products/product-card/product-card.component';
import { FilteredComponent } from './products/filtered/filtered.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import {MatIconModule} from '@angular/material/icon';
import { PaymentmethodComponent } from './cart/paymentmethod/paymentmethod.component';


@NgModule({
  declarations: [
    AppComponent,
    NuestroComponenteComponent,
    ProductsComponent,
    UserComponent,
    PagenotfoundComponent,
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
    HttpClientModule,
    ProductDetailComponent,
    UserPanelComponent,
    CartComponent,
    CartItemComponent,
    MatIconModule,
    PaymentmethodComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
