import { CommonModule } from '@angular/common';
import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
    selector: 'app-email',
    standalone: true,
    imports: [ReactiveFormsModule, CommonModule],
    templateUrl: './email.component.html',
    styleUrl: './email.component.css'
})
export class EmailComponent implements ControlValueAccessor {

    @Input() type = 'text';
    @Input() label: string = 'Email address';

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
