import { TestBed } from '@angular/core/testing';

import { RoleserviceService } from './roleservice.service';

describe('RoleserviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoleserviceService = TestBed.get(RoleserviceService);
    expect(service).toBeTruthy();
  });
});
