import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { deliveryMethodModel } from '../models/deliveryMethodModel';


@Injectable({
    providedIn: 'root'
})
export class DeliveryMethodService {

    baseUrl: string = environment.apiUrl + 'DeliveryMethod';

    constructor(private http: HttpClient) { }

    ngOnInit(): void { }

    getDeliveryMethods(): Observable<deliveryMethodModel[]> {
        return this.http.get<deliveryMethodModel[]>(this.baseUrl).pipe(
            map(dm => {
                return dm.sort((a, b) => b.price - a.price)
            })
        );
    }
}
