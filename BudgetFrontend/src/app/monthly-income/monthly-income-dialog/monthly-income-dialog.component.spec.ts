import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyIncomeDialogComponent } from './monthly-income-dialog.component';

describe('MonthlyIncomeDialogComponent', () => {
  let component: MonthlyIncomeDialogComponent;
  let fixture: ComponentFixture<MonthlyIncomeDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyIncomeDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyIncomeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
