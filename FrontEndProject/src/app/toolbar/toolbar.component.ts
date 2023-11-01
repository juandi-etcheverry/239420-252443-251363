import { Component, OnInit } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTabsModule} from '@angular/material/tabs';

import {MatDividerModule} from '@angular/material/divider';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import {MatMenuModule} from '@angular/material/menu';


@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
  standalone: true,
  imports: [MatIconModule, MatButtonModule, MatToolbarModule, MatTabsModule, MatDividerModule, MatMenuModule]
})
export class ToolbarComponent implements OnInit {
  isLogged: boolean = true;

  constructor(private router:Router, private authService: AuthService){
    this.isLogged = this.authService.hasAuthToken();
  }
  ngOnInit(): void {
    
  }
  goToPage(url: string){
    this.router.navigate([url]);
  }
}
