import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyScreenComponent } from './property-screen.component';

describe('PropertyScreenComponent', () => {
  let component: PropertyScreenComponent;
  let fixture: ComponentFixture<PropertyScreenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PropertyScreenComponent]
    });
    fixture = TestBed.createComponent(PropertyScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
