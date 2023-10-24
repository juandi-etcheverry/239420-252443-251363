import { Component } from '@angular/core';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormsModule} from '@angular/forms';
import {NgFor} from '@angular/common';
import {MatSelectModule} from '@angular/material/select';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';

interface Brands{
  value: string;
}
interface Categories{
  value: string;
}
@Component({
  selector: 'app-filtered',
  templateUrl: './filtered.component.html',
  styleUrls: ['./filtered.component.css'],
  standalone: true,
  imports: [MatSlideToggleModule, MatInputModule, MatFormFieldModule, FormsModule, NgFor, MatSelectModule, MatButtonModule, MatDividerModule, MatIconModule]
})
export class FilteredComponent {
  brands: Brands[] = [
    {value: 'Nike'},
    {value: 'Adidas'},
    {value: 'Puma'},
  ]
  categories: Categories[] = [
    {value: 'Jugetes'},
    {value: 'Ropa'},
    {value: 'Tecnologia'},    
  ]
}
