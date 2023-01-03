import { BudgetDto } from './BudgetDto';
import { LoanDto } from './LoanDto';
import { MonthlyBillDto } from './MonthlyBillDto';

export class MonthlyIncomeDto {
  id: number;
  biWeeklyAmount: number;
  amountThisMonth: number;
  transactionAmount: number;
  leftOverAfterTransactions: number;
  lastPayDay: Date;
  name: string;
  remainingAmount: number;
  budgetAmount: number;
  loansAmount: number;
  monthlyBillsAmount: number;
  budgets: BudgetDto[];
  monthlyBills: MonthlyBillDto[];
  loans: LoanDto[];
}
