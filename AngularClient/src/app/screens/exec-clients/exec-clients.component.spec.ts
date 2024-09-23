import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExecClientsComponent } from './exec-clients.component';

describe('ExecClientsComponent', () => {
  let component: ExecClientsComponent;
  let fixture: ComponentFixture<ExecClientsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExecClientsComponent]
    });
    fixture = TestBed.createComponent(ExecClientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
