import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { paginationModel } from '../models/paginationModel';
import { Observable } from 'rxjs';
import { productBrandModel } from '../models/productBrandModel';

@Injectable({
  providedIn: 'root'
})
export class ProductBrandService {

    baseUrl: string = environment.apiUrl + 'ProductBrand';

    constructor(private http: HttpClient){

    }

    ngOnInit(): void {
     
    }

    getProductBrands() : Observable<productBrandModel[]> {
        return this.http.get<productBrandModel[]>(this.baseUrl);
    }
}
