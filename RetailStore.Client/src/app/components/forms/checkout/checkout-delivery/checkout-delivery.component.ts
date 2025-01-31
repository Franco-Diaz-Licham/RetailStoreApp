import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { deliveryMethodModel } from '../../../../models/deliveryMethodModel';
import { BasketService } from '../../../../services/basket.service';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { DeliveryMethodService } from '../../../../services/delivery-method.service';
import { CdkStepperModule } from '@angular/cdk/stepper';

@Component({
    selector: 'app-checkout-delivery',
    standalone: true,
    imports: [CurrencyPipe, ReactiveFormsModule, CommonModule, CdkStepperModule],
    templateUrl: './checkout-delivery.component.html',
    styleUrl: './checkout-delivery.component.css'
})
export class CheckoutDeliveryComponent implements OnInit {

    @Input() checkoutForm: FormGroup = new FormGroup({});
    
    deliveryMethods: deliveryMethodModel[] = [];

    constructor(private basketService: BasketService, private deliveryMethodService: DeliveryMethodService) { }

    ngOnInit(): void {
        this.deliveryMethodService.getDeliveryMethods().subscribe({
            next: dm => this.deliveryMethods = dm
        })
    }

    setShippingPrice(deliveryMethod: deliveryMethodModel) {
        this.basketService.setShippingPrice(deliveryMethod);
    }
}
