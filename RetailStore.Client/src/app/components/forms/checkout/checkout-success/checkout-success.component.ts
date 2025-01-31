import { Component } from '@angular/core';
import { orderModel } from '../../../../models/orderModel';
import { Router, RouterLink } from '@angular/router';

@Component({
    selector: 'app-checkout-success',
    standalone: true,
    imports: [RouterLink],
    templateUrl: './checkout-success.component.html',
    styleUrl: './checkout-success.component.css'
})
export class CheckoutSuccessComponent {
    
    order?: orderModel;

    constructor(private router: Router) {
        const navigation = this.router.getCurrentNavigation();
        this.order = navigation?.extras?.state as orderModel
    }
}
