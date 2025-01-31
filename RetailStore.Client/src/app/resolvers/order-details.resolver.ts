import { ActivatedRouteSnapshot, Resolve, ResolveFn } from '@angular/router';
import { orderModel } from '../models/orderModel';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class OrderDetailsResolver implements Resolve<orderModel>{

    constructor(private orderService: OrderService){ }

    resolve(route: ActivatedRouteSnapshot): Observable<orderModel> {
        return this.orderService.getOrder(Number.parseInt(route.paramMap.get('id')!));
    }
}
