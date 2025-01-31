import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { loadStripe, Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement } from '@stripe/stripe-js';
import { BasketService } from '../../../../services/basket.service';
import { CheckoutService } from '../../../../services/checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { basketActiveModel, basketModel } from '../../../../models/basketModel';
import { firstValueFrom } from 'rxjs';
import { orderToCreateModel } from '../../../../models/orderModel';
import { addressModel } from '../../../../models/userModel';
import { TextInputComponent } from '../../../inputs/text-input/text-input.component';
import { CdkStepperModule } from '@angular/cdk/stepper';

@Component({
    selector: 'app-checkout-payment',
    standalone: true,
    imports: [TextInputComponent, ReactiveFormsModule, CdkStepperModule],
    templateUrl: './checkout-payment.component.html',
    styleUrl: './checkout-payment.component.css'
})
export class CheckoutPaymentComponent implements OnInit {
    
    @Input() checkoutForm: FormGroup = new FormGroup({});
    @ViewChild('cardNumber') cardNumberElement?: ElementRef;
    @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
    @ViewChild('cardCvc') cardCvcElement?: ElementRef;

    stripe: Stripe | null = null;
    cardNumber?: StripeCardNumberElement;
    cardExpiry?: StripeCardExpiryElement;
    cardCvc?: StripeCardCvcElement;
    cardNumberComplete = false;
    cardExpiryComplete = false;
    cardCvcComplete = false;
    cardErrors: any;
    loading = false;
    pKey = 'pk_test_51QkefGDsvgOhyhtjJ5v5HIOwtAzdn3q0V7ws1gDamO6V6WqVzP5qThirPJgjf1s5OyG4rVbbJZoh0nJaAZK9jeJ1002RaxlGk2';

    constructor(private basketService: BasketService, private checkoutService: CheckoutService, private toastr: ToastrService, private router: Router) { }

    ngOnInit(): void {
        loadStripe(this.pKey).then(stripe => {
            this.stripe = stripe;
            const elements = stripe?.elements();

            if (elements) {
                this.cardNumber = elements.create('cardNumber');
                this.cardNumber.mount(this.cardNumberElement?.nativeElement);
                this.cardNumber.on('change', event => {
                    this.cardNumberComplete = event.complete;
                    if (event.error) this.cardErrors = event.error.message;
                    else this.cardErrors = null;
                })

                this.cardExpiry = elements.create('cardExpiry');
                this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
                this.cardExpiry.on('change', event => {
                    this.cardExpiryComplete = event.complete;
                    if (event.error) this.cardErrors = event.error.message;
                    else this.cardErrors = null;
                })

                this.cardCvc = elements.create('cardCvc');
                this.cardCvc.mount(this.cardCvcElement?.nativeElement);
                this.cardCvc.on('change', event => {
                    this.cardCvcComplete = event.complete;
                    if (event.error) this.cardErrors = event.error.message;
                    else this.cardErrors = null;
                })
            }
        })
    }

    // global complete property
    get paymentFormComplete() {
        let output = this.checkoutForm?.get('paymentForm')?.valid && this.cardNumberComplete && this.cardExpiryComplete && this.cardCvcComplete;
        return output;
    }

    async submitOrder() {
        this.loading = true;
        const basket = this.basketService.getCurrentBasketValue();

        if (!basket) throw new Error('cannot get basket');

        try {
            const createdOrder = await this.createOrder(basket);
            const paymentResult = await this.confirmPaymentWithStripe(basket);

            if (paymentResult.paymentIntent) {
                this.basketService.deleteBasket(basket);
                const navigationExtras: NavigationExtras = { state: createdOrder };
                this.router.navigate(['checkout/success'], navigationExtras);
            } else {
                this.toastr.error(paymentResult.error.message);
            }
        } catch (error: any) {
            console.log(error);
            this.toastr.error(error.message)
        } finally {
            this.loading = false;
        }
    }

    private async confirmPaymentWithStripe(basket: basketModel | null) {
        if (!basket) throw new Error('Basket is null');

        var options = {
            payment_method: {
                card: this.cardNumber!,
                billing_details: {
                    name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
                }
            }
        };

        const result = this.stripe?.confirmCardPayment(basket.clientSecret!, options);
        if (!result) throw new Error('Problem attempting payment with stripe');
        return result;
    }

    private async createOrder(basket: basketActiveModel | null) {
        if (!basket) throw new Error('Basket is null');
        const orderToCreate = this.getOrderToCreate(basket);
        return firstValueFrom(this.checkoutService.createOrder(orderToCreate));
    }

    private getOrderToCreate(basket: basketModel ): orderToCreateModel {
        const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
        const shipToAddress = this.checkoutForm?.get('addressForm')?.value as addressModel;

        if (!deliveryMethodId || !shipToAddress) throw new Error('Problem with basket');

        return {
            basketId: basket.id,
            deliveryMethodId: deliveryMethodId,
            shipToAddress: shipToAddress
        }
    }
}
