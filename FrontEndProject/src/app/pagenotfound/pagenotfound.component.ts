import { Component } from '@angular/core';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pagenotfound',
  templateUrl: './pagenotfound.component.html',
  styleUrls: ['./pagenotfound.component.css'],
  standalone: true,
  imports: [MatButtonModule]
})
export class PagenotfoundComponent {

  constructor(private router : Router) { }

  returnToHome(){
    this.goToPage('/products');
  }

  goToPage(url: string){
    this.router.navigate([url]);
  }
}
