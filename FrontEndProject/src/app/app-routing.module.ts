import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { UserComponent } from './user/user.component';
import { CartComponent } from './cart/cart.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { UserPanelComponent } from './user/user-panel/user-panel.component';
import { SignupComponent } from './user/signup/signup.component';
import { LoginComponent } from './user/login/login.component';
import { AdministrationPanelComponent } from './administration/administration-panel/administration-panel.component';
import { ServerDownComponent } from './server-down/server-down.component';

const routes: Routes = [
  {path: '', redirectTo: 'products', pathMatch: 'full'},
  {path: 'products', component: ProductsComponent},
  {path: 'products/:id', component: ProductDetailComponent},
  {path: 'user', component: UserComponent},
  {path: 'users/:id', component: UserPanelComponent},
  {path: 'cart', component: CartComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin', component: AdministrationPanelComponent},
  {path: 'serverdown', component: ServerDownComponent},
  {path: '**', component: PagenotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
