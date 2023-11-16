import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { MatIconModule } from '@angular/material/icon';
import { AuthInterceptor } from './services/interceptors/auth.interceptor';
import { ServerErrorInterceptor } from './services/interceptors/server-error.interceptor';
import { PaymentmethodComponent } from './components/cart/paymentmethod/paymentmethod.component';
import { CartComponent } from './components/cart/cart.component';
import { CartItemComponent } from './components/cart/cart-item/cart-item.component';
import { AdministrationPanelComponent } from './components/administration/administration-panel/administration-panel.component';
import { ProductItemComponent } from './components/administration/administration-panel/products/product-item/product-item.component';
import { UserItemComponent } from './components/administration/administration-panel/users/user-item/user-item.component';
import { NewProductComponent } from './components/administration/administration-panel/products/new-product/new-product.component';
import { ModifyProductComponent } from './components/administration/administration-panel/products/product-item/modify-product/modify-product.component';
import { PurchaseHistoryItemComponent } from './components/administration/administration-panel/purchase-history-item/purchase-history-item.component';
import { NewUserComponent } from './components/administration/administration-panel/users/user-item/new-user/new-user.component';
import { PagenotfoundComponent } from './components/pagenotfound/pagenotfound.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { TabsComponent } from './components/tabs/tabs.component';
import { ProductCardComponent } from './components/products/product-card/product-card.component';
import { FilteredComponent } from './components/products/filtered/filtered.component';
import { UserPanelComponent } from './components/user/user-panel/user-panel.component';
import { SignupComponent } from './components/user/signup/signup.component';
import { LoginComponent } from './components/user/login/login.component';
import { ProductsComponent } from './components/products/products.component';
import { UserComponent } from './components/user/user.component';
import { ServerDownComponent } from './components/server-down/server-down.component';


@NgModule({
  declarations: [
    AppComponent,
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
