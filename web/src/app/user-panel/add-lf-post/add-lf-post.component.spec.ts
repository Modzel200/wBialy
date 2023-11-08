import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLfPostComponent } from './add-lf-post.component';

describe('AddLfPostComponent', () => {
  let component: AddLfPostComponent;
  let fixture: ComponentFixture<AddLfPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddLfPostComponent]
    });
    fixture = TestBed.createComponent(AddLfPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
