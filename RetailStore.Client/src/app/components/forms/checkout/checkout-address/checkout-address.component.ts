import { Component, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from '../../../inputs/text-input/text-input.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-checkout-address',
    standalone: true,
    imports: [TextInputComponent, ReactiveFormsModule, CdkStepperModule, RouterLink],
    templateUrl: './checkout-address.component.html',
    styleUrl: './checkout-address.component.css'
})
export class CheckoutAddressComponent {

    @Input() checkoutForm: FormGroup = new FormGroup({});

    constructor(private accountService: AccountService, private toastr: ToastrService) { }

    saveUserAddress() {
        var address = this.checkoutForm?.get('addressForm')?.value;
        this.accountService.updateUserAddress(address).subscribe({
            next: () => {
                this.toastr.success('Address saved');
                this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);
            }
        })
    }
}
