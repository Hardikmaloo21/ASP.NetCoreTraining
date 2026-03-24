import { Routes } from '@angular/router';
import { ProductComponent } from './product/product';
import { CheckoutComponent } from './checkout/checkout';
import { DashboardComponent } from './dashboard/dashboard';

export const routes: Routes = [
  { path: '', component: ProductComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'dashboard', component: DashboardComponent }
];