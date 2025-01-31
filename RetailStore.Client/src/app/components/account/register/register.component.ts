import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../../../services/account.service';
import { EmailComponent } from '../../inputs/email/email.component';
import { PasswordComponent } from '../../inputs/password/password.component';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { TextInputComponent } from '../../inputs/text-input/text-input.component';

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [EmailComponent, PasswordComponent, ReactiveFormsModule, TextInputComponent],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

    registerForm!: FormGroup;
    complexPassword = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$";
    validationErrors: string[] = [];

    constructor(private accountService: AccountService, private formBuilder: FormBuilder, private router: Router) { }
    ngOnInit(): void {
        this.initializeForm();
    }

    initializeForm() {
        this.registerForm = this.formBuilder.group({
            displayName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(10)]],
            email: [null, [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
            password: [null, [Validators.required, Validators.minLength(4), Validators.pattern(this.complexPassword)]],
            confirmPassword: [null, [Validators.required]]
        });

        // add extra validation for confirmPassword
        this.registerForm.controls['confirmPassword'].addValidators(this.matchPassword('password'));
        this.registerForm.controls['password'].valueChanges.subscribe(() => {
            this.registerForm.controls['confirmPassword'].updateValueAndValidity();
        })
    }

    matchPassword(matchTo: string): ValidatorFn {
        return (control: AbstractControl) => {
            var formControls = control?.parent?.controls as { [key: string]: AbstractControl };
            return control?.value === formControls[matchTo]?.value ? null : { isMatching: true };
        }
    }

    validateEmailNotTaken(): AsyncValidatorFn {
        return (control: AbstractControl) => {
            return control.valueChanges.pipe(
                debounceTime(500),
                take(1),
                switchMap(() => {
                    return this.accountService.checkEmailExists(control.value).pipe(
                        map((result: HttpResponse<boolean>) => result.body ? { emailExists: true } : null),
                        finalize(() => control.markAsTouched())
                    )
                })
            );
        }
    }

    register() {
        this.accountService.register(this.registerForm.value).subscribe({
            next: () => {
                this.router.navigateByUrl("/shop");
            },
            error: (error: any) => {
                this.validationErrors = error.error.validationErrors;
            }
        });
    }
}
