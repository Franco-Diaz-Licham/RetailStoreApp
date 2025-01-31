import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-password',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './password.component.html',
    styleUrl: './password.component.css'
})
export class PasswordComponent implements ControlValueAccessor{

    @Input() type = 'text';
    @Input() label: string = 'Password';
    @Input() id: string = 'floatingInputPassword';

    constructor(@Self() public controlDir: NgControl) {
        this.controlDir.valueAccessor = this;
    }

    writeValue(obj: any): void { }

    registerOnChange(fn: any): void { }

    registerOnTouched(fn: any): void { }

    get control(): FormControl {
        return this.controlDir.control as FormControl
    }
}
