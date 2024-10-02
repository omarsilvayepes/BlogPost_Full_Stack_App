import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBlogpostComponent } from './delete-blogpost.component';

describe('DeleteBlogpostComponent', () => {
  let component: DeleteBlogpostComponent;
  let fixture: ComponentFixture<DeleteBlogpostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteBlogpostComponent]
    });
    fixture = TestBed.createComponent(DeleteBlogpostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
