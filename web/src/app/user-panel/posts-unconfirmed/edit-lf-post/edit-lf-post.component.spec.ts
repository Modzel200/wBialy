import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLfPostComponent } from './edit-lf-post.component';

describe('EditLfPostComponent', () => {
  let component: EditLfPostComponent;
  let fixture: ComponentFixture<EditLfPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditLfPostComponent]
    });
    fixture = TestBed.createComponent(EditLfPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
