import { CdkStepper, CdkStepperModule } from '@angular/cdk/stepper';
import { Component, Input } from '@angular/core';
import { BasketService } from '../../../../services/basket.service';
import { ToastrService } from 'ngx-toastr';
import { BasketSummaryComponent } from '../../../basket/basket-summary/basket-summary.component';

@Component({
    selector: 'app-checkout-review',
    standalone: true,
    imports: [BasketSummaryComponent, CdkStepperModule],
    templateUrl: './checkout-review.component.html',
    styleUrl: './checkout-review.component.css'
})
export class CheckoutReviewComponent {
    @Input() appStepper?: CdkStepper;

    constructor(private basketService: BasketService, private toastr: ToastrService) { }

    createPaymentIntent() {
        this.basketService.createPaymentIntent().subscribe({
            next: () => {
                this.appStepper?.next();
            },
            error: error => this.toastr.error(error.message)
        })
    }
}
