import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MonthlyIncomeComponent } from './monthly-income/monthly-income.component';
import { BudgetComponent } from './budget/budget.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MonthlyIncomeDialogComponent } from './monthly-income/monthly-income-dialog/monthly-income-dialog.component';
import { BudgetDialogComponent } from './shared/budget-dialog/budget-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { DeleteConfirmDialogComponent } from './shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { TransactionDialogComponent } from './shared/transaction-dialog/transaction-dialog.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { ToolBarComponent } from './shared/tool-bar/tool-bar.component';
import { MonthlyBillComponent } from './monthly-bill/monthly-bill.component';
import { MonthlyBillDialogComponent } from './monthly-bill/monthly-bill-dialog/monthly-bill-dialog.component';
import { MatSortModule } from '@angular/material/sort';
import { LoanComponent } from './loan/loan.component';
import { LoanDialogComponent } from './loan/loan-dialog/loan-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    MonthlyIncomeComponent,
    BudgetComponent,
    MonthlyIncomeDialogComponent,
    BudgetDialogComponent,
    DeleteConfirmDialogComponent,
    TransactionDialogComponent,
    ToolBarComponent,
    MonthlyBillComponent,
    MonthlyBillDialogComponent,
    LoanComponent,
    LoanDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    MatMenuModule,
    MatIconModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatTableModule,
    MatDividerModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
