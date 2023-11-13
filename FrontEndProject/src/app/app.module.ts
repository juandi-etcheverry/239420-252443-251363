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
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { UserPanelComponent } from './user/user-panel/user-panel.component';
import { MatIconModule } from '@angular/material/icon';
import { PaymentmethodComponent } from './cart/paymentmethod/paymentmethod.component';
import { SignupComponent } from './user/signup/signup.component';
import { AuthInterceptor } from './auth.interceptor';
import { LoginComponent } from './user/login/login.component';
import { AdministrationPanelComponent } from './administration/administration-panel/administration-panel.component';
import { ProductItemComponent } from './administration/administration-panel/products/product-item/product-item.component';
import { NewProductComponent } from './administration/administration-panel/products/new-product/new-product.component';
import { ModifyProductComponent } from './administration/administration-panel/products/product-item/modify-product/modify-product.component';
import { UserItemComponent } from './administration/administration-panel/users/user-item/user-item.component';
import { NewUserComponent } from './administration/administration-panel/users/user-item/new-user/new-user.component';
import { PurchaseHistoryItemComponent } from './administration/administration-panel/purchase-history-item/purchase-history-item.component';
import { ServerErrorInterceptor } from './server-error.interceptor';
import { ServerDownComponent } from './server-down/server-down.component';


@NgModule({
  declarations: [
    AppComponent,
    NuestroComponenteComponent,
    ProductsComponent,
    UserComponent,
    ServerDownComponent,
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
    SignupComponent,
    LoginComponent,
    ProductItemComponent,
    UserItemComponent,
    AdministrationPanelComponent,
    NewProductComponent,
    ModifyProductComponent,
    NewUserComponent,
    PurchaseHistoryItemComponent,
    PagenotfoundComponent,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS, 
      useClass: ServerErrorInterceptor, 
      multi: true
    }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
