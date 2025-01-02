import { Component } from '@angular/core';
import { ProductItemComponent } from "../product-item/product-item.component";
import { productModel } from '../../../models/productModel';
import { ProductService } from '../../../services/product.service';
import { paginationModel } from '../../../models/paginationModel';

@Component({
    selector: 'app-shop-main',
    standalone: true,
    imports: [ProductItemComponent],
    templateUrl: './shop-main.component.html',
    styleUrl: './shop-main.component.css'
})
export class ShopMainComponent {

    products: productModel[] = [];

    constructor(private productService: ProductService) { }

    ngOnInit(): void {
        this.productService.getProducts().subscribe({
            next: (resp: paginationModel) => {
                this.products = resp.data;
            }
        });
    }
}
