import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MonthlyBillDto } from 'src/models/MonthlyBillDto';
import { ApiService } from 'src/services/MonthlyIncomeService';

@Component({
  selector: 'app-monthly-bill-dialog',
  templateUrl: './monthly-bill-dialog.component.html',
  styleUrls: ['./monthly-bill-dialog.component.css'],
})
export class MonthlyBillDialogComponent implements OnInit {
  public title: string;

  constructor(
    public dialogRef: MatDialogRef<MonthlyBillDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MonthlyBillDto,
    private service: ApiService
  ) {}

  ngOnInit(): void {
    if (this.data.name) {
      this.title = `Edit "${this.data.name}"`;
    } else {
      this.title = 'Create Monthly Bill';
    }
  }

  submit(): void {
    let monthlyBill = new MonthlyBillDto();
    monthlyBill.monthlyIncomeId = this.data.monthlyIncomeId;
    monthlyBill.id = this.data.id;
    monthlyBill.name = this.data.name;
    monthlyBill.amount = this.data.amount;

    if (monthlyBill.id) {
      this.service.updateMonthlyBill(monthlyBill).subscribe((response) => {
        this.dialogRef.close(response);
      });
    } else {
      this.service.createMonthlyBill(monthlyBill).subscribe((response) => {
        this.dialogRef.close(response);
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
