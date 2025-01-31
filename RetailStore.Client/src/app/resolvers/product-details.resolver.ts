import { ActivatedRouteSnapshot, MaybeAsync, Resolve, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { productModel } from '../models/productModel';
import { Injectable } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ProductDetailsResolver implements Resolve<productModel>{

    constructor(private productService: ProductService){ }

    resolve(route: ActivatedRouteSnapshot): Observable<productModel> {
        return this.productService.getProduct(Number.parseInt(route.paramMap.get('id')!));
    }
}
