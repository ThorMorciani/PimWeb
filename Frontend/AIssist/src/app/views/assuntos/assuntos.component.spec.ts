import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssuntosComponent } from './assuntos.component';

describe('AssuntosComponent', () => {
  let component: AssuntosComponent;
  let fixture: ComponentFixture<AssuntosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssuntosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssuntosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
