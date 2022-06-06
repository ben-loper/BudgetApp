import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BudgetDto } from 'src/models/BudgetDto';
import { MonthlyIncomeDto } from 'src/models/MonthlyIncomeDto';
import { TransactionDto } from 'src/models/TransactionDto';
import { ApiService } from 'src/services/MonthlyIncomeService';
import { DeleteConfirmDialogComponent } from '../shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { TransactionDialogComponent } from '../shared/transaction-dialog/transaction-dialog.component';
import { BudgetDialogComponent } from '../shared/budget-dialog/budget-dialog.component';
import { MonthlyIncomeDialogComponent } from './monthly-income-dialog/monthly-income-dialog.component';

@Component({
  selector: 'app-monthly-income',
  templateUrl: './monthly-income.component.html',
  styleUrls: ['./monthly-income.component.css'],
})
export class MonthlyIncomeComponent implements OnInit {
  monthlyIncome: MonthlyIncomeDto;
  public flipTotals: boolean;

  constructor(
    private monthlyIncomeService: ApiService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.monthlyIncomeService.getMonthlyIncomes().subscribe((response) => {
      this.monthlyIncome = response;
    });

    this.flipTotals = false;
  }

  toggleTotals() {
    this.flipTotals = !this.flipTotals;
  }

  openEditIncomeDialog(): void {
    const dialogRef = this.dialog.open(MonthlyIncomeDialogComponent, {
      width: '300px',
      data: {
        name: this.monthlyIncome.name,
        amount: this.monthlyIncome.biWeeklyAmount,
        id: this.monthlyIncome.id,
        lastPayDay: this.monthlyIncome.lastPayDay,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.monthlyIncome = result;
      }
    });
  }

  openBudgetDialog(budget: BudgetDto): void {
    let dialogRef = null;

    if (budget) {
      dialogRef = this.dialog.open(BudgetDialogComponent, {
        width: '300px',
        data: {
          monthlyIncomeId: this.monthlyIncome.id,
          name: budget.name,
          amount: budget.amount,
          id: budget.id,
        },
      });
    } else {
      dialogRef = this.dialog.open(BudgetDialogComponent, {
        width: '300px',
        data: {
          monthlyIncomeId: this.monthlyIncome.id,
        },
      });
    }

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.monthlyIncome = result;
      }
    });
  }

  openDeleteDialog(budget: BudgetDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      width: '300px',
      data: {
        itemName: budget.name,
        id: budget.id,
        endpoint: 'budget',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.monthlyIncome = result;
      }
    });
  }

  openTransactionDialog(budgetId: number, budgetName: string): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(TransactionDialogComponent, {
      width: '300px',
      data: {
        budgetId: budgetId,
        budgetName: budgetName,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.monthlyIncome = result;
      }
    });
  }
}
