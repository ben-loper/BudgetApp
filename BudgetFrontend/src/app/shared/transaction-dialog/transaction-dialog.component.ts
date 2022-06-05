import { Component, Inject, OnInit } from '@angular/core';
import { Form, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TransactionDto } from 'src/models/TransactionDto';
import { ApiService } from 'src/services/MonthlyIncomeService';

export interface ITransactionDialogData {
  id: number;
  budgetId: number;
  budgetName: string;
  name: string;
  amount: number;
  createdDate: Date;
  transactionDate: Date;
}

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.css'],
})
export class TransactionDialogComponent implements OnInit {
  public title: string;

  constructor(
    public dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ITransactionDialogData,
    private service: ApiService
  ) {}
  createdDateFormControl: FormControl;
  transactionDateFormControl: FormControl;

  ngOnInit(): void {
    if (this.data.createdDate) {
      this.createdDateFormControl = new FormControl(this.data.createdDate);
    } else {
      this.createdDateFormControl = new FormControl(new Date());
    }

    if (this.data.transactionDate) {
      this.transactionDateFormControl = new FormControl(
        this.data.transactionDate
      );
    } else {
      this.transactionDateFormControl = new FormControl(new Date());
    }

    if (this.data.name) {
      this.title = `Edit "${this.data.name}" for "${this.data.budgetName}"`;
    } else {
      this.title = `Add Transaction for "${this.data.budgetName}"`;
    }
  }

  create(): void {
    const transaction = new TransactionDto();

    transaction.budgetId = this.data.budgetId;
    transaction.amount = this.data.amount;
    transaction.createdDate = this.createdDateFormControl.value;
    transaction.transactionDate = this.transactionDateFormControl.value;
    transaction.id = this.data.id;
    transaction.name = this.data.name;

    if (transaction.id) {
      this.service.updateTransaction(transaction).subscribe((response) => {
        this.dialogRef.close(response);
      });
    } else {
      this.service.createTransaction(transaction).subscribe((response) => {
        this.dialogRef.close(response);
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
