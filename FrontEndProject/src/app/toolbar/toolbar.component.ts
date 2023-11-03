import { Component, OnInit } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTabsModule} from '@angular/material/tabs';
import {MatMenuModule} from '@angular/material/menu';

import {MatDividerModule} from '@angular/material/divider';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { UsersService } from '../user/users.service';
import { User } from '../user/user-model';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart/cart-service';


@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
  standalone: true,
  imports: [MatIconModule, MatButtonModule, MatToolbarModule, MatTabsModule, MatDividerModule, MatMenuModule, CommonModule]
})
export class ToolbarComponent implements OnInit {

  isLoggedIn : boolean = this.authService.hasAuthToken();
  user : User | null = null;
  avatar : string = "";
  constructor(private router:Router, private authService:AuthService, private userService : UsersService, private cartService : CartService){
    router.events.subscribe((event) => {
      this.isLoggedIn = this.authService.hasAuthToken();
      this.updateAvatar();
      if (this.user === null && this.isLoggedIn) {
        this.fetchUserData(() => {
          this.updateAvatar();
        });
      }
    })
  }

  ngOnInit(): void {
    this.fetchUserData(this.updateAvatar);
  }

  goToProfile(){
    this.fetchUserData(() => {
      this.router.navigate([`/users/${this.user?.id}`]);
    })
  }

  logout(){
    this.userService.logout();
    this.user = null;
    this.authService.removeAuthToken();
    this.cartService.removeCartFromLocalStorage();
    this.goToPage('/products');
  }

  goToLogin(){
    this.goToPage('/login');
  }

  goToPage(url: string){
    this.router.navigate([url]);
  }

  private fetchUserData(internalSubscribeLogic : () => void) {
    return this.userService.getLoggedUser()?.subscribe(({id, email, address, role}) => {
      this.user = {
        id, email, address, role
      }
      internalSubscribeLogic();
    })
  }

  private updateAvatar() {
    this.avatar = this.user?.email.split("@")[0] ?? "";
  }
}
