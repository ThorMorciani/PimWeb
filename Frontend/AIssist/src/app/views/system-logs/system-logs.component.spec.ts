import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemLogsComponent } from './system-logs.component';

describe('SystemLogsComponent', () => {
  let component: SystemLogsComponent;
  let fixture: ComponentFixture<SystemLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemLogsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SystemLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
