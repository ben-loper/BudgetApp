import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MonthlyIncomeUpdateDto } from 'src/models/MonthlyIncomeUpdateDto';
import { ApiService } from 'src/services/MonthlyIncomeService';

export interface IMonthlyIncomeDialogData {
  id: number;
  name: string;
  amount: number;
  lastPayDay: Date;
}

@Component({
  selector: 'app-monthly-income-dialog',
  templateUrl: './monthly-income-dialog.component.html',
  styleUrls: ['./monthly-income-dialog.component.css'],
})
export class MonthlyIncomeDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<MonthlyIncomeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IMonthlyIncomeDialogData,
    private service: ApiService
  ) {}

  private income: MonthlyIncomeUpdateDto;
  lastPayDayFormControl: UntypedFormControl;

  ngOnInit() {
    if (this.data.id) {
      this.lastPayDayFormControl = new UntypedFormControl(this.data.lastPayDay);
    } else {
      this.lastPayDayFormControl = new UntypedFormControl(new Date());
    }
  }

  submit(): void {
    this.income = new MonthlyIncomeUpdateDto();
    this.income.name = this.data.name;
    this.income.amount = this.data.amount;
    this.income.id = this.data.id;
    this.income.lastPayDay = this.lastPayDayFormControl.value;

    this.service.updateMonthlyIncome(this.income).subscribe((response) => {
      this.dialogRef.close(response);
    });
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
