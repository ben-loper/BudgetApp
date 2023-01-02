import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoanDto } from 'src/models/LoanDto';
import { ApiService } from 'src/services/MonthlyIncomeService';

@Component({
  selector: 'app-loan-dialog',
  templateUrl: './loan-dialog.component.html',
  styleUrls: ['./loan-dialog.component.css'],
})
export class LoanDialogComponent implements OnInit {
  public title: string;

  constructor(
    public dialogRef: MatDialogRef<LoanDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: LoanDto,
    private service: ApiService
  ) {}

  lastPaidDateFormControl: FormControl;

  ngOnInit(): void {
    if (this.data.name) {
      this.title = `Edit "${this.data.name}"`;
    } else {
      this.title = 'Create Loan';
    }

    if (this.data.id) {
      this.lastPaidDateFormControl = new FormControl(this.data.lastPaidDate);
    } else {
      this.lastPaidDateFormControl = new FormControl(new Date());
    }
  }

  submit(): void {
    let Loan = new LoanDto();
    Loan.monthlyIncomeId = this.data.monthlyIncomeId;
    Loan.id = this.data.id;
    Loan.name = this.data.name;
    Loan.monthlyAmount = this.data.monthlyAmount;
    Loan.dayOfMonthDue = this.data.dayOfMonthDue;

    if (Loan.id) {
      this.service.updateLoan(Loan).subscribe((response) => {
        this.dialogRef.close(response);
      });
    } else {
      this.service.createLoan(Loan).subscribe((response) => {
        this.dialogRef.close(response);
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
