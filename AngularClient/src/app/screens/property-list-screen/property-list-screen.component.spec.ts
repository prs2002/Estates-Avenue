import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyListScreenComponent } from './property-list-screen.component';

describe('PropertyListScreenComponent', () => {
  let component: PropertyListScreenComponent;
  let fixture: ComponentFixture<PropertyListScreenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PropertyListScreenComponent]
    });
    fixture = TestBed.createComponent(PropertyListScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
