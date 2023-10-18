import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostsUnconfirmedComponent } from './posts-unconfirmed.component';

describe('PostsUnconfirmedComponent', () => {
  let component: PostsUnconfirmedComponent;
  let fixture: ComponentFixture<PostsUnconfirmedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostsUnconfirmedComponent]
    });
    fixture = TestBed.createComponent(PostsUnconfirmedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
