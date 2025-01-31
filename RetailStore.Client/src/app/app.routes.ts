import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ShopMainComponent } from './components/shop/shop-main/shop-main.component';
import { ProductDetailsComponent } from './components/shop/product-details/product-details.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ProductDetailsResolver } from './resolvers/product-details.resolver';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';
import { BasketComponent } from './components/basket/basket/basket.component';
import { CheckoutComponent } from './components/basket/checkout/checkout.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { skip } from 'rxjs';
import { AuthGuard } from './guards/auth.guard';
import { CheckoutSuccessComponent } from './components/forms/checkout/checkout-success/checkout-success.component';
import { AllOrdersComponent } from './components/orders/all-orders/all-orders.component';
import { OrderComponent } from './components/orders/order/order.component';
import { OrderDetailsResolver } from './resolvers/order-details.resolver';

export const routes: Routes = [
    { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
    {
        path: 'shop', children: [
            { path: '', pathMatch: 'full', component: ShopMainComponent, data: { breadcrumb: 'Shop' } },
            { path: ':id', component: ProductDetailsComponent, resolve: { product: ProductDetailsResolver }, data: { breadcrumb: { alias: 'productDetails' } } }
        ]
    },
    {
        path: 'account', data: { breadcrumb: { skip: true } }, children: [
            { path: 'login', component: LoginComponent, data: { breadcrumb: 'login' } },
            { path: 'register', component: RegisterComponent, data: { breadcrumb: 'register' } }
        ]
    },
    { path: 'basket', component: BasketComponent, data: { breadcrumb: 'Basket' } },
    {
        path: 'checkout', canActivate: [AuthGuard], runGuardsAndResolvers: 'always', children: [
            { path: '', pathMatch: 'full', component: CheckoutComponent,  data: { breadcrumb: 'Checkout' }},
            { path: 'success', component: CheckoutSuccessComponent, data: { breadcrumb: { skip: true } } }
        ]
    },
    {
        path: 'orders', canActivate: [AuthGuard], children: [
            { path: '', pathMatch: 'full', component: AllOrdersComponent,  data: { breadcrumb: 'Orders' }},
            { path: ':id', component: OrderComponent, resolve: { order: OrderDetailsResolver }, data: { breadcrumb: { alias: 'orderDetails' } } }
        ]
    },
    { path: 'not-found', component: NotFoundComponent, data: { breadcrumb: 'Not Found' } },
    { path: 'server-error', component: ServerErrorComponent, data: { breadcrumb: 'Server Error' } },
    { path: "errors", component: TestErrorsComponent, data: { breadcrumb: 'Test Error' } },
    { path: '**', component: NotFoundComponent, pathMatch: 'full', data: { breadcrumb: 'Not Found' } }
]
