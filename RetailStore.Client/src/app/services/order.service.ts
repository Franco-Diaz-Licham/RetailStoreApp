import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { orderModel } from '../models/orderModel';
import { map, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class OrderService {


    baseUrl: string = environment.apiUrl + 'orders';

    constructor(private http: HttpClient) { }

    ngOnInit(): void { }

    getOrders(): Observable<orderModel[]> {
        return this.http.get<orderModel[]>(this.baseUrl).pipe(
            map(dm => {
                return dm.sort((a, b) => b.id - a.id)
            })
        );
    }

    getOrder(id: number): Observable<orderModel> {
        return this.http.get<orderModel>(this.baseUrl + `/${id}`);
    }
}
