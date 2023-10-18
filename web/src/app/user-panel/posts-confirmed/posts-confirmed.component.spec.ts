import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostsConfirmedComponent } from './posts-confirmed.component';

describe('PostsConfirmedComponent', () => {
  let component: PostsConfirmedComponent;
  let fixture: ComponentFixture<PostsConfirmedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostsConfirmedComponent]
    });
    fixture = TestBed.createComponent(PostsConfirmedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
