import { TestBed } from '@angular/core/testing';

import { ProductGuard } from './product-guard';

describe('ProductGuard', () => {
  let service: ProductGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductGuard);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
