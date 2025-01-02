import { Component, Input, input } from '@angular/core';
import { productModel } from '../../../models/productModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {

    @Input() product?: productModel;
}
