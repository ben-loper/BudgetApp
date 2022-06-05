import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyBillDialogComponent } from './monthly-bill-dialog.component';

describe('MonthlyBillDialogComponent', () => {
  let component: MonthlyBillDialogComponent;
  let fixture: ComponentFixture<MonthlyBillDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyBillDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyBillDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
