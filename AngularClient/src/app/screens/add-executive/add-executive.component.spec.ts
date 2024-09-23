import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExecutiveComponent } from './add-executive.component';

describe('AddExecutiveComponent', () => {
  let component: AddExecutiveComponent;
  let fixture: ComponentFixture<AddExecutiveComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddExecutiveComponent]
    });
    fixture = TestBed.createComponent(AddExecutiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
