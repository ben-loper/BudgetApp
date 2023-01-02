export class LoanDto {
  id: number;
  monthlyIncomeId: number;
  startingAmount: number;
  monthlyAmount: number;
  dayOfMonthDue: number;
  name: string;
  lastPaidDate: Date;
  amountRemaining: number;
}
