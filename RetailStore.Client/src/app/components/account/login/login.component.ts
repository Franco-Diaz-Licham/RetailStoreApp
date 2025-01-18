import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../../services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EmailComponent } from '../../inputs/email/email.component';
import { PasswordComponent } from "../../inputs/password/password.component";

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule, FormsModule, EmailComponent, PasswordComponent],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css',

})
export class LoginComponent implements OnInit {

    loginForm: FormGroup = new FormGroup({});
    validationErrors: string[] = [];
    returnUrl: string = '';

    constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {
        this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
        this.createLoginForm();
    }

    createLoginForm() {
        this.loginForm = new FormGroup({
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required, Validators.minLength(4)])
        })
    }

    login() {
        this.accountService.login(this.loginForm.value).subscribe({
            next: () => {
                console.log('user logged in'); 
                this.router.navigateByUrl(this.returnUrl!);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
}
