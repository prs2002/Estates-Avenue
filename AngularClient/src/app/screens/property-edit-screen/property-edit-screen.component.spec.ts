import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyEditScreenComponent } from './property-edit-screen.component';

describe('PropertyEditScreenComponent', () => {
  let component: PropertyEditScreenComponent;
  let fixture: ComponentFixture<PropertyEditScreenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PropertyEditScreenComponent]
    });
    fixture = TestBed.createComponent(PropertyEditScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
