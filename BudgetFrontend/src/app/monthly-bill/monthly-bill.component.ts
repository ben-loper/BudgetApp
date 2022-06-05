import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { MonthlyBillDto } from 'src/models/MonthlyBillDto';
import { ApiService } from 'src/services/MonthlyIncomeService';
import { DeleteConfirmDialogComponent } from '../shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { MonthlyBillDialogComponent } from './monthly-bill-dialog/monthly-bill-dialog.component';

@Component({
  selector: 'app-monthly-bill',
  templateUrl: './monthly-bill.component.html',
  styleUrls: ['./monthly-bill.component.css'],
})
export class MonthlyBillComponent implements OnInit, AfterViewInit {
  constructor(
    private dialog: MatDialog,
    private service: ApiService,
    private route: ActivatedRoute
  ) {}
  public displayedColumns = ['name', 'amount', 'actionColumn'];

  public monthlyBillTotals: number;
  public loading: boolean;

  public dataSource = new MatTableDataSource<MonthlyBillDto>();

  public bills: MonthlyBillDto[];
  private monthlyIncomeId: number;
  public totalBills: number;

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
      .getMonthlyBillsForMonthlyIncomeById(this.monthlyIncomeId)
      .subscribe((result) => {
        this.updateBillsDisplay(result);
        this.loading = false;
      });
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

  openMonthlyBillDialog(monthlyBill: MonthlyBillDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(MonthlyBillDialogComponent, {
      width: '300px',
      data: {
        monthlyIncomeId: this.monthlyIncomeId,
        name: monthlyBill.name,
        amount: monthlyBill.amount,
        id: monthlyBill.id,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateBillsDisplay(result);
      }
    });
  }

  openDeleteDialog(monthlyBill: MonthlyBillDto): void {
    let dialogRef = null;

    dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      width: '300px',
      data: {
        itemName: monthlyBill.name,
        id: monthlyBill.id,
        endpoint: 'monthlyBill',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.updateBillsDisplay(result);
      }
    });
  }

  updateBillsDisplay(bills: MonthlyBillDto[]) {
    this.bills = bills;
    this.monthlyBillTotals = 0;

    for (let i = 0; i < bills.length; i++) {
      this.monthlyBillTotals += bills[i].amount;
    }
    this.dataSource.data = bills;

    if (bills.length > 20) {
      this.totalBills = bills.length;
    } else {
      this.totalBills = 20;
    }
  }
}
