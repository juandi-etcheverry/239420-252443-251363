import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { UserComponent } from './components/user/user.component';
import { UserPanelComponent } from './components/user/user-panel/user-panel.component';
import { CartComponent } from './components/cart/cart.component';
import { SignupComponent } from './components/user/signup/signup.component';
import { LoginComponent } from './components/user/login/login.component';
import { AdministrationPanelComponent } from './components/administration/administration-panel/administration-panel.component';
import { ServerDownComponent } from './components/server-down/server-down.component';
import { PagenotfoundComponent } from './components/pagenotfound/pagenotfound.component';

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
