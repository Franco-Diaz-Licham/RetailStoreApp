import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ShopMainComponent } from './components/shop/shop-main/shop-main.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SectionHeaderComponent } from "./components/layout/section-header/section-header.component";


@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, NavBarComponent, ShopMainComponent, NgxSpinnerModule, SectionHeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    constructor() {

    }

    ngOnInit(): void {

    }
}
