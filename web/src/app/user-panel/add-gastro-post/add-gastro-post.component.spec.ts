import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGastroPostComponent } from './add-gastro-post.component';

describe('AddGastroPostComponent', () => {
  let component: AddGastroPostComponent;
  let fixture: ComponentFixture<AddGastroPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddGastroPostComponent]
    });
    fixture = TestBed.createComponent(AddGastroPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
