import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { orderModel } from '../../../models/orderModel';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketSummaryComponent } from '../../basket/basket-summary/basket-summary.component';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-order',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './order.component.html',
    styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit {

    order?: orderModel;

    constructor(private route: ActivatedRoute, private breadCrumbService: BreadcrumbService) {

    }

    ngOnInit(): void {
        this.route.data.subscribe((data) => {
            this.order = data['order'];
            this.breadCrumbService.set('@orderDetails', `Order #${this.order?.id.toString()!} - ${this.order?.status.toString()!}`);
        });
    }
}
