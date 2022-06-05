import { BudgetDto } from './BudgetDto';
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
  monthlyBillsAmount: number;
  weeklyBudgets: BudgetDto[];
  monthlyBills: MonthlyBillDto[];
}
