import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEventPostComponent } from './add-event-post.component';

describe('AddEventPostComponent', () => {
  let component: AddEventPostComponent;
  let fixture: ComponentFixture<AddEventPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEventPostComponent]
    });
    fixture = TestBed.createComponent(AddEventPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
