import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { orderModel, orderToCreateModel } from '../models/orderModel';
import { deliveryMethodModel } from '../models/deliveryMethodModel';
import { map } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
    providedIn: 'root'
})
export class CheckoutService {

    baseUrl: string = environment.apiUrl + 'orders';

    constructor(private http: HttpClient) { }

    createOrder(order: orderToCreateModel) {
        return this.http.post<orderModel>(this.baseUrl, order);
    }
}
