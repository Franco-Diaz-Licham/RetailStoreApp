import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { delay, map, Observable } from 'rxjs';
import { productModel } from '../models/productModel';
import { paginationModel } from '../models/paginationModel';
import { productParams } from '../models/productParams';

@Injectable({
    providedIn: 'root'
})
export class ProductService implements OnInit {
    
    baseUrl: string = environment.apiUrl + 'Product';

    constructor(private http: HttpClient){ }

    ngOnInit(): void {
     
    }

    getProducts(productParams: productParams): Observable<paginationModel> {
        let params = new HttpParams();

        params = params.append('pageSize', productParams.pageSize!.toString());
        params = params.append('pageIndex', productParams.pageIndex!.toString());

        if(productParams.brandId) params = params.append('brandId', productParams.brandId!.toString()); 
        if(productParams.typeId) params = params.append('typeId', productParams.typeId!.toString());
        if(productParams.sort) params = params.append('sort', productParams.sort); 
        if(productParams.search) params = params.append('search', productParams.search);

        return this.http.get<paginationModel>(this.baseUrl, { observe: 'response', params }).pipe(
            map((data) => {
                return data.body!;
            })
        );
    }

    getProduct(id: number){
        var url = `${this.baseUrl}/${id}`
        return this.http.get<productModel>(url);
    }
}
