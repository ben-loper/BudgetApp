import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BudgetDto } from 'src/models/BudgetDto';
import { MonthlyIncomeDto } from 'src/models/MonthlyIncomeDto';
import { TransactionDto } from 'src/models/TransactionDto';
import { MonthlyIncomeUpdateDto } from 'src/models/MonthlyIncomeUpdateDto';
import { IDeleteDialogData } from 'src/app/shared/delete-confirm-dialog/delete-confirm-dialog.component';
import { MonthlyBillDto } from 'src/models/MonthlyBillDto';
import { environment } from 'src/environments/environment';
import { LoanDto } from 'src/models/LoanDto';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getMonthlyIncomes(): Observable<MonthlyIncomeDto> {
    return this.http.get<MonthlyIncomeDto>(
      `${environment.apiUrl}/MonthlyIncome`
    );
  }

  updateMonthlyIncome(
    income: MonthlyIncomeUpdateDto
  ): Observable<MonthlyIncomeDto> {
    return this.http.put<MonthlyIncomeDto>(
      `${environment.apiUrl}/MonthlyIncome`,
      income
    );
  }

  createBudget(budget: BudgetDto): Observable<BudgetDto> {
    return this.http.post<BudgetDto>(`${environment.apiUrl}/Budget`, budget);
  }

  updateBudget(budget: BudgetDto): Observable<MonthlyIncomeDto> {
    return this.http.put<MonthlyIncomeDto>(
      `${environment.apiUrl}/Budget`,
      budget
    );
  }

  getBudgetById(id: number): Observable<BudgetDto> {
    return this.http.get<BudgetDto>(`${environment.apiUrl}/Budget/${id}`);
  }

  deleteItem(item: IDeleteDialogData): Observable<MonthlyIncomeDto> {
    return this.http.delete<MonthlyIncomeDto>(
      `${environment.apiUrl}/${item.endpoint}/${item.id}`
    );
  }

  createMonthlyBill(
    monthlyBills: MonthlyBillDto
  ): Observable<MonthlyBillDto[]> {
    return this.http.post<MonthlyBillDto[]>('/MonthlyBill', monthlyBills);
  }

  updateMonthlyBill(monthlyBill: MonthlyBillDto): Observable<MonthlyBillDto[]> {
    return this.http.put<MonthlyBillDto[]>(
      `${environment.apiUrl}/MonthlyBill`,
      monthlyBill
    );
  }

  getMonthlyBillsForMonthlyIncomeById(
    monthlyIncomeId: number
  ): Observable<MonthlyBillDto[]> {
    return this.http.get<MonthlyBillDto[]>(
      `${environment.apiUrl}/MonthlyBill/${monthlyIncomeId}`
    );
  }

  createTransaction(transaction: TransactionDto): Observable<MonthlyIncomeDto> {
    return this.http.post<MonthlyIncomeDto>(
      `${environment.apiUrl}/Transaction`,
      transaction
    );
  }

  updateTransaction(transaction: TransactionDto): Observable<MonthlyIncomeDto> {
    return this.http.put<MonthlyIncomeDto>(
      `${environment.apiUrl}/Transaction`,
      transaction
    );
  }

  getLoansForMonthlyIncomeById(id: number): Observable<LoanDto[]> {
    return this.http.get<LoanDto[]>(
      `${environment.apiUrl}/MonthlyIncome/${id}/Loans`
    );
  }

  getLoanById(id: number): Observable<LoanDto> {
    return this.http.get<LoanDto>(`${environment.apiUrl}/Loan/${id}`);
  }

  createLoan(loan: LoanDto): Observable<LoanDto> {
    return this.http.post<LoanDto>(`${environment.apiUrl}/Loan`, loan);
  }

  updateLoan(loan: LoanDto): Observable<LoanDto> {
    return this.http.put<LoanDto>(`${environment.apiUrl}/Loan`, loan);
  }
}
