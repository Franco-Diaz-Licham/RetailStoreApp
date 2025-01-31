import { Component, OnInit } from '@angular/core';
import { orderModel } from '../../../models/orderModel';
import { OrderService } from '../../../services/order.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-all-orders',
    standalone: true,
    imports: [RouterLink, CommonModule],
    templateUrl: './all-orders.component.html',
    styleUrl: './all-orders.component.css'
})
export class AllOrdersComponent implements OnInit {
    orders: orderModel[] = [];

    constructor(private orderService: OrderService, private breadCrumbService: BreadcrumbService, private router: Router) { }

    ngOnInit(): void {
        this.getOrders();
    }

    getOrders() {
        this.orderService.getOrders().subscribe({
            next: orders => this.orders = orders
        })
    }

    navigateToOrder(id: number){
        this.breadCrumbService.set('@orderDetails', ' ');
        this.router.navigateByUrl(`/orders/${id}`);
    }
}
