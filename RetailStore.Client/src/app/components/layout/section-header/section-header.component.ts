import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { map, Observable, pipe } from 'rxjs';
import {BreadcrumbComponent, BreadcrumbService} from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  standalone: true,
  imports: [BreadcrumbComponent, AsyncPipe, CommonModule ],
  templateUrl: './section-header.component.html',
  styleUrl: './section-header.component.css'
})
export class SectionHeaderComponent implements OnInit {

    title: string = 'Default Title';
    breadcrumb$?: Observable<any[]>;

    constructor(private breadCrumbService: BreadcrumbService){
        this.breadCrumbService.set('@productDetails', ' ');
        this.breadCrumbService.set('@orderDetails', ' ');
    }

    ngOnInit(): void {
        this.breadcrumb$ = this.breadCrumbService.breadcrumbs$;
    }
}
