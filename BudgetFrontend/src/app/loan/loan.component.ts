import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { LoanDto } from 'src/models/LoanDto';
import { ApiService } from 'src/services/MonthlyIncomeService';
import { DeleteConfirmDialogComponent } from '../shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { LoanDialogComponent } from './loan-dialog/loan-dialog.component';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css'],
})
export class LoanComponent implements OnInit, AfterViewInit {
  constructor(
    private dialog: MatDialog,
    private service: ApiService,
    private route: ActivatedRoute
  ) {}
  public displayedColumns = ['name', 'amount', 'actionColumn'];

  public loansMonthlyTotal: number;
  public loading: boolean;

  public dataSource = new MatTableDataSource<LoanDto>();

  public loans: LoanDto[];
  private monthlyIncomeId: number;
  public loanCount: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    this.loading = true;

    this.route.params.subscribe((result) => {
      this.monthlyIncomeId = result.monthlyIncomeId;
    });

    this.service
      .getLoansForMonthlyIncomeById(this.monthlyIncomeId)
      .subscribe((result) => {
        this.updateLoansDisplay(result);
        this.loading = false;
      });
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

  openLoanDialog(loan: LoanDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(LoanDialogComponent, {
      width: '300px',
      data: {
        monthlyIncomeId: this.monthlyIncomeId,
        name: loan.name,
        monthlyAmount: loan.monthlyAmount,
        id: loan.id,
        dayOfMonthDue: loan.dayOfMonthDue,
        lastPaidDate: loan.lastPaidDate,
        amountRemaining: loan.amountRemaining,
        startingAmount: loan.startingAmount,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateLoansDisplay(result);
      }
    });
  }

  openDeleteDialog(loan: LoanDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      width: '300px',
      data: {
        itemName: loan.name,
        id: loan.id,
        endpoint: 'loan',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateLoansDisplay(result);
      }
    });
  }

  updateLoansDisplay(loans: LoanDto[]) {
    this.loans = loans;
    this.loansMonthlyTotal = 0;

    for (let i = 0; i < loans.length; i++) {
      this.loansMonthlyTotal += loans[i].monthlyAmount;
    }
    this.dataSource.data = loans;

    if (loans.length > 20) {
      this.loanCount = loans.length;
    } else {
      this.loanCount = 20;
    }
  }
}
