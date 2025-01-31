import { Component, OnInit } from '@angular/core';
import { OrderTotalsComponent } from '../order-totals/order-totals.component';
import { StepperComponent } from '../../forms/checkout/stepper/stepper.component';
import { CheckoutAddressComponent } from '../../forms/checkout/checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from '../../forms/checkout/checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from '../../forms/checkout/checkout-review/checkout-review.component';
import { CheckoutPaymentComponent } from '../../forms/checkout/checkout-payment/checkout-payment.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { AccountService } from '../../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BasketService } from '../../../services/basket.service';

@Component({
    selector: 'app-checkout',
    standalone: true,
    imports: [OrderTotalsComponent, StepperComponent, CheckoutAddressComponent, CheckoutDeliveryComponent, CheckoutReviewComponent, CheckoutPaymentComponent, CdkStepperModule],
    templateUrl: './checkout.component.html',
    styleUrl: './checkout.component.css'
})
export class CheckoutComponent implements OnInit {

    checkoutForm: FormGroup = new FormGroup({});

    constructor(private fb: FormBuilder, private accountService: AccountService, private basketService: BasketService) { }

    ngOnInit(): void {
        this.generateForm();
        this.getAddressFormValues();
        this.getDeliveryMethodValue();
    }

    getAddressFormValues() {
        this.accountService.getUserAddress().subscribe({
            next: address => {
                if(address){
                    this.checkoutForm.get('addressForm')?.patchValue(address);
                }
            }
        })
    }

    getDeliveryMethodValue() {
        const basket = this.basketService.getCurrentBasketValue();
        if (basket && basket.deliveryMethodId) {
            this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')?.patchValue(basket.deliveryMethodId.toString());
        }
    }

    generateForm(){
        this.checkoutForm = this.fb.group({
            addressForm: this.fb.group({
                firstName: ['', Validators.required],
                lastName: ['', Validators.required],
                street: ['', Validators.required],
                city: ['', Validators.required],
                state: ['', Validators.required],
                zipcode: ['', Validators.required],
            }),
            deliveryForm: this.fb.group({
                deliveryMethod: ['', Validators.required]
            }),
            paymentForm: this.fb.group({
                nameOnCard: ['', Validators.required]
            })
        });
    }
}
