import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, map, Observable, ReplaySubject } from 'rxjs';
import { userModel } from '../models/userModel';
import { Router } from '@angular/router';
import { AddressModel } from '../models/addressModel';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    baseUrl: string = environment.apiUrl + 'Account';
    private currentUserSource = new ReplaySubject<userModel | null>(1);
    currentUser$ = this.currentUserSource.asObservable();

    constructor(private http: HttpClient, private route: Router) {
        this.currentUserSource.next(null);
    }


    loadCurrentUser(token: string) {
        let headers = new HttpHeaders();
        headers = headers.set('Authorization', `Bearer ${token}`);

        return this.http.get<userModel>(this.baseUrl, { headers }).pipe(
            map((user: userModel) => {
                this.currentUserSource.next(user);
                localStorage.setItem('token', user.token);
            })
        )
    }

    login(values: any) {
        let url = `${this.baseUrl}/login`;

        return this.http.post<userModel>(url, values).pipe(
            map((user: userModel) => {
                if (user) {
                    this.currentUserSource.next(user);
                    localStorage.setItem('token', user.token);
                }
            })
        );
    }

    register(values: any) {
        let url = `${this.baseUrl}/register`;

        return this.http.post<userModel>(url, values).pipe(
            map((user: userModel) => {
                if (user) {
                    this.currentUserSource.next(user);
                    localStorage.setItem('token', user.token)
                }
            })
        );
    }

    logout() {
        localStorage.removeItem('token');
        this.currentUserSource.next(null);
        this.route.navigateByUrl('/');
    }

    checkEmailExists(email: string): Observable<HttpResponse<boolean>> {
        let url = `${this.baseUrl}/emailExists`
        let params = new HttpParams();
        params = params.append('email', email);

        return this.http.get<boolean>(url, { observe: 'response', params });
    }

    getUserAddress() {
        return this.http.get<AddressModel>(this.baseUrl + '/address');
    }

    updateUserAddress(address: AddressModel) {
        return this.http.put(this.baseUrl + '/address', address);
    }
}
