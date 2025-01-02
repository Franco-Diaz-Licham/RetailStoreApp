import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { productModel } from './models/productModel';
import { ProductService } from './services/product.service';
import { map, pipe } from 'rxjs';
import { paginationModel } from './models/paginationModel';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, NavBarComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

    products: productModel[] = [];

    constructor(private productService: ProductService) {

    }

    ngOnInit(): void {
        this.productService.getProducts().subscribe({
            next: (resp: paginationModel) => {
                this.products = resp.data;
            }
        });
    }
}
