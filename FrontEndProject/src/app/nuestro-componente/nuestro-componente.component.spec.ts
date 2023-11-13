import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NuestroComponenteComponent } from './nuestro-componente.component';

describe('NuestroComponenteComponent', () => {
  let component: NuestroComponenteComponent;
  let fixture: ComponentFixture<NuestroComponenteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NuestroComponenteComponent]
    });
    fixture = TestBed.createComponent(NuestroComponenteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
