import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { productModel } from '../models/productModel';
import { paginationModel } from '../models/paginationModel';

@Injectable({
    providedIn: 'root'
})
export class ProductService implements OnInit {
    
    baseUrl: string = environment.apiUrl + 'Product';

    constructor(private http: HttpClient){

    }

    ngOnInit(): void {
        throw new Error('Method not implemented.');
    }

    getProducts() : Observable<paginationModel> {
        return this.http.get<paginationModel>(this.baseUrl);
    }
}
