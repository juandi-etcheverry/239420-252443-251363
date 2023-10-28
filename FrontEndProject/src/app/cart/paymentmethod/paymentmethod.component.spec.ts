import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentmethodComponent } from './paymentmethod.component';

describe('PaymentmethodComponent', () => {
  let component: PaymentmethodComponent;
  let fixture: ComponentFixture<PaymentmethodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PaymentmethodComponent]
    });
    fixture = TestBed.createComponent(PaymentmethodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
