import { TestBed } from '@angular/core/testing';

import { CustReqService } from './cust-req.service';

describe('CustReqService', () => {
  let service: CustReqService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustReqService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
