import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodRecuperacaoComponent } from './cod.recuperacao.component';

describe('CodRecuperacaoComponent', () => {
  let component: CodRecuperacaoComponent;
  let fixture: ComponentFixture<CodRecuperacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CodRecuperacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CodRecuperacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
