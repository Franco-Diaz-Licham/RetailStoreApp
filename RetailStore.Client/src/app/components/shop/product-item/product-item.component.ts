import { Component, Input, input, OnInit } from '@angular/core';
import { productModel } from '../../../models/productModel';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent{

    @Input() product?: productModel;
    
    constructor(private router: Router, private breadCrumbService: BreadcrumbService){

    }

    navigateToProduct(){
        this.breadCrumbService.set('@productDetails', ' ');
        this.router.navigateByUrl(`/shop/${this.product?.id}`);
    }
}
