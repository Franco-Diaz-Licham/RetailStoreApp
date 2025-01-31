import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { productTypeModel } from '../models/productTypeModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductTypeService {

    baseUrl: string = environment.apiUrl + 'ProductType';

    constructor(private http: HttpClient){

    }

    ngOnInit(): void {
     
    }

    getProductTypes() : Observable<productTypeModel[]> {
        return this.http.get<productTypeModel[]>(this.baseUrl);
    }
}
