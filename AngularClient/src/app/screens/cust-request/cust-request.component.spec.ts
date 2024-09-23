import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustRequestComponent } from './cust-request.component';

describe('CustRequestComponent', () => {
  let component: CustRequestComponent;
  let fixture: ComponentFixture<CustRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustRequestComponent]
    });
    fixture = TestBed.createComponent(CustRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
