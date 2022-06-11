import { TransactionDto } from './TransactionDto';

export class BudgetDto {
  id: number;
  monthlyIncomeId: number;
  amount: number;
  amountThisMonth: number;
  name: string;
  isWeekly: boolean;
  totalTransactionsForTheMonth: number;
  amountRemaining: number;
  transactions: TransactionDto[];
}
