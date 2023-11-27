import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGastroPostComponent } from './edit-gastro-post.component';

describe('EditGastroPostComponent', () => {
  let component: EditGastroPostComponent;
  let fixture: ComponentFixture<EditGastroPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditGastroPostComponent]
    });
    fixture = TestBed.createComponent(EditGastroPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
