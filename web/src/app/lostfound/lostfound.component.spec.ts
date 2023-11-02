import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LostfoundComponent } from './lostfound.component';

describe('LostfoundComponent', () => {
  let component: LostfoundComponent;
  let fixture: ComponentFixture<LostfoundComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LostfoundComponent]
    });
    fixture = TestBed.createComponent(LostfoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
