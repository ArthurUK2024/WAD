import { TestBed } from '@angular/core/testing';

import { FutureGoalsService } from './future-goals.service';

describe('FutureGoalsService', () => {
  let service: FutureGoalsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FutureGoalsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
