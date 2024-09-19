import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExecDashboardComponent } from './exec-dashboard.component';

describe('ExecDashboardComponent', () => {
  let component: ExecDashboardComponent;
  let fixture: ComponentFixture<ExecDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExecDashboardComponent]
    });
    fixture = TestBed.createComponent(ExecDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
