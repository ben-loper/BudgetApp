import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BudgetDto } from 'src/models/BudgetDto';
import { ApiService } from 'src/services/MonthlyIncomeService';

@Component({
  selector: 'app-budget-dialog',
  templateUrl: './budget-dialog.component.html',
  styleUrls: ['./budget-dialog.component.css'],
})
export class BudgetDialogComponent implements OnInit {
  public title: string;

  constructor(
    public dialogRef: MatDialogRef<BudgetDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BudgetDto,
    private service: ApiService
  ) {}

  ngOnInit(): void {
    if (this.data.name) {
      this.title = `Edit "${this.data.name}"`;
    } else {
      this.title = 'Create Budget';
    }
  }

  submit(): void {
    let budget = new BudgetDto();
    budget.monthlyIncomeId = this.data.monthlyIncomeId;
    budget.id = this.data.id;
    budget.name = this.data.name;
    budget.amount = this.data.amount;
    budget.isWeekly = this.data.isWeekly;

    if (budget.id) {
      this.service.updateBudget(budget).subscribe((response) => {
        this.dialogRef.close(response);
      });
    } else {
      this.service.createBudget(budget).subscribe((response) => {
        this.dialogRef.close(response);
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
