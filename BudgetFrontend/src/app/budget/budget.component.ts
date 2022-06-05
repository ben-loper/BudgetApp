import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { BudgetDto } from 'src/models/BudgetDto';
import { MonthlyIncomeDto } from 'src/models/MonthlyIncomeDto';
import { TransactionDto } from 'src/models/TransactionDto';
import { ApiService } from 'src/services/MonthlyIncomeService';
import { BudgetDialogComponent } from '../shared/budget-dialog/budget-dialog.component';
import { DeleteConfirmDialogComponent } from '../shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { TransactionDialogComponent } from '../shared/transaction-dialog/transaction-dialog.component';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrls: ['./budget.component.css'],
})
export class BudgetComponent implements OnInit {
  constructor(
    private dialog: MatDialog,
    private service: ApiService,
    private route: ActivatedRoute
  ) {}
  public displayedColumns = [
    'name',
    'amount',
    'transactionDate',
    'actionColumn',
  ];

  public dataSource = new MatTableDataSource<TransactionDto>();

  public budget: BudgetDto;
  public transactionTotal: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.budget = new BudgetDto();

    this.route.params.subscribe((result) => {
      this.budget.id = result.id;
    });

    this.service.getBudgetById(this.budget.id).subscribe((result) => {
      this.budget = result;
      this.dataSource.data = this.budget.transactions;

      if (this.budget.transactions.length > 20) {
        this.transactionTotal = this.budget.transactions.length;
      } else {
        this.transactionTotal = 20;
      }
    });
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

  openBudgetDialog(budget: BudgetDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(BudgetDialogComponent, {
      width: '300px',
      data: {
        monthlyIncomeId: this.budget.monthlyIncomeId,
        name: budget.name,
        amount: budget.amount,
        id: budget.id,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateBudgetDisplay(result);
      }
    });
  }

  openTransactionDialog(transaction: TransactionDto): void {
    let dialogRef = null;

    if (transaction.id) {
      dialogRef = this.dialog.open(TransactionDialogComponent, {
        width: '300px',
        data: {
          id: transaction.id,
          budgetId: transaction.budgetId,
          name: transaction.name,
          amount: transaction.amount,
          createdDate: transaction.createdDate,
          transactionDate: transaction.transactionDate,
          budgetName: this.budget.name,
        },
      });
    } else {
      dialogRef = this.dialog.open(TransactionDialogComponent, {
        width: '300px',
        data: {
          budgetId: transaction.budgetId,
          budgetName: this.budget.name,
        },
      });
    }

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateBudgetDisplay(result);
      }
    });
  }

  openDeleteDialog(transaction: TransactionDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      width: '300px',
      data: {
        itemName: transaction.name,
        id: transaction.id,
        endpoint: 'transaction',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateBudgetDisplay(result);
      }
    });
  }

  updateBudgetDisplay(income: MonthlyIncomeDto) {
    for (let i = 0; i < income.weeklyBudgets.length; i++) {
      if (income.weeklyBudgets[i].id === this.budget.id) {
        this.budget = income.weeklyBudgets[i];
        this.dataSource.data = this.budget.transactions;
        break;
      }
    }
  }
}
