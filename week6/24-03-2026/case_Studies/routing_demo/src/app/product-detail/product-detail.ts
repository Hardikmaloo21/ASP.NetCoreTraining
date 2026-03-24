import { Component , OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductService } from '../product.service';
import { Product } from '../product';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule],
  template: `
    <div *ngIf="loading" class="loading">Loading product...</div>
    <div *ngIf="errorMsg" class="error">{{ errorMsg }}</div>
    <div *ngIf="product" class="card">
      <h2>{{ product.name }}</h2>
      <p>ID: {{ product.productID }}</p>
      <p>Price: ₹{{ product.price }}</p>
    </div>
  `
})
export class ProductDetail implements OnInit, OnDestroy {

  product: Product | null = null;
  loading = true;
  errorMsg = '';
  private destroy$ = new Subject<void>();

  constructor(
    private route : ActivatedRoute,
    private service : ProductService
  ) {}

  ngOnInit() {
    this.route.paramMap.pipe(
      takeUntil(this.destroy$)
    ).subscribe((params: any) => {  // Type assertion for snapshot compat
      const idStr = params.get('id');
      if (idStr) {
        const id = Number(idStr);
        this.loading = true;
        this.errorMsg = '';
        const fetchedProduct = this.service.getProductById(id);
        this.product = fetchedProduct || null;
        if (!this.product) {
          this.errorMsg = 'Product not found';
        }
        this.loading = false;
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
