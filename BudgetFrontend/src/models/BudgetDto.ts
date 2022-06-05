import { TransactionDto } from './TransactionDto';

export class BudgetDto {
  id: number;
  monthlyIncomeId: number;
  amount: number;
  amountThisMonth: number;
  name: string;
  totalTransactionsForTheMonth: number;
  amountRemaining: number;
  transactions: TransactionDto[];
}
