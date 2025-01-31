import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { CdkStepper } from '@angular/cdk/stepper';
import { Directionality } from '@angular/cdk/bidi';
import { CommonModule, NgTemplateOutlet } from '@angular/common';

@Component({
    selector: 'app-stepper',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './stepper.component.html',
    styleUrl: './stepper.component.css',
    providers: [{ provide: CdkStepper, useExisting: StepperComponent }]
})
export class StepperComponent extends CdkStepper implements OnInit {
    @Input() linearModeSelected = true;

    ngOnInit(): void {
        this.linear = this.linearModeSelected;
    }

    onClick(index: number) {
        this.selectedIndex = index;
    }
}
