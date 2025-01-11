import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { productModel } from '../../../models/productModel';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit{

    product?: productModel;

    constructor(private productService: ProductService, private route: ActivatedRoute){ }

    ngOnInit(): void {
        this.route.data.subscribe((data) => {
            this.product = data['product'];
        });
    }
}
