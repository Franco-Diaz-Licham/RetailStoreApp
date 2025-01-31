import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { productModel } from '../../../models/productModel';
import { CurrencyPipe } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from '../../../services/basket.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

    product?: productModel;
    quantity = 1;

    constructor(private route: ActivatedRoute, private breadCrumbService: BreadcrumbService, private basketService: BasketService){

    }

    ngOnInit(): void {
        this.route.data.subscribe((data) => {
            console.log(data);
            this.product = data['product'];
            this.breadCrumbService.set('@productDetails', this.product?.name!);
        });
    }

    addItemToBasket(){
        this.basketService.addItemToBasket(this.product!, this.quantity);
    }

    incrementQuantity(){
        this.quantity++;
    }

    decrementQuantity(){
        if(this.quantity > 1){
            this.quantity--;
        }
    }
}
