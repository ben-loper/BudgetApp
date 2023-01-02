using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.DTOs
{
    public class MonthlyIncomeDto
    {
        public int Id { get; set; }
        public decimal BiWeeklyAmount { get; set; }
        public decimal AmountThisMonth { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal LeftOverAfterTransactions { get; set; }
        public decimal MonthlyBillsAmount { get; set; }
        public decimal LoansAmount { get; set; }
        public DateTime LastPayDay { get; set; }
        public string Name { get; set; }
        public List<BudgetDto> Budgets { get; set; } = new List<BudgetDto>();
        public List<MonthlyBillDto> MonthlyBills { get; set; } = new List<MonthlyBillDto>();
        public List<LoanDto> Loans { get; set; } = new List<LoanDto>();

        public void CalculateValues()
        {
            CalculateMonthlyIncomeAmount();

            var remainingAmount = AmountThisMonth;
            LeftOverAfterTransactions = AmountThisMonth;

            foreach (var budget in Budgets.Where(b => !b.IsMisc))
            {
                budget.CalculateValues();

                remainingAmount -= budget.AmountThisMonth;
                BudgetAmount += budget.AmountThisMonth;

                TransactionAmount += budget.TotalTransactionsForTheMonth;
                LeftOverAfterTransactions -= budget.TotalTransactionsForTheMonth;
            }

            foreach (var monthlyBill in MonthlyBills)
            {
                remainingAmount -= monthlyBill.Amount;
                BudgetAmount += monthlyBill.Amount;
                MonthlyBillsAmount += monthlyBill.Amount;
            }

            foreach(var loan in Loans)
            {
                if (loan.AmountRemaining > 0)
                {
                    remainingAmount -= loan.AmountRemaining;
                    BudgetAmount += loan.MonthlyAmount;
                    LoansAmount += loan.MonthlyAmount;
                }                
            }

            RemainingAmount = remainingAmount;
            LeftOverAfterTransactions -= MonthlyBillsAmount;
            LeftOverAfterTransactions -= LoansAmount;

            // Calculate misc budget data
            var miscBudget = Budgets.Where(b => b.IsMisc).FirstOrDefault();

            miscBudget.Amount = remainingAmount;
            miscBudget.AmountThisMonth = remainingAmount;
            miscBudget.IsWeekly = false;
            miscBudget.CalculateValues();

            TransactionAmount += miscBudget.TotalTransactionsForTheMonth;
            LeftOverAfterTransactions -= miscBudget.TotalTransactionsForTheMonth;

            Budgets = Budgets.OrderBy(b => b.Name).ToList();
        }

        private void CalculateMonthlyIncomeAmount()
        {
            var today = DateTime.Now.Date;
            var firstOfMonth = new DateTime(today.Year, today.Month, 1);

            var year = firstOfMonth.Year;
            var month = firstOfMonth.Month + 1;

            var lastPayDay = new DateTime(LastPayDay.Year, LastPayDay.Month, LastPayDay.Day);

            if (firstOfMonth.Month == 12)
            {
                year = firstOfMonth.Year + 1;
                month = 1;
            }

            var nextMonth = new DateTime(year, month, firstOfMonth.Day);

            // The last pay day falls after the first of the current month. Will need to look in the past for when the first paycheck will be for the month
            if (firstOfMonth < lastPayDay)
            {
                var keepGoing = true;
                
                while (keepGoing)
                {
                    var testDate = new DateTime(lastPayDay.Year, lastPayDay.Month, lastPayDay.Day).AddDays(-14);

                    if (testDate.Month != today.Month)
                    {
                        keepGoing = false;
                    }
                    else
                    {
                        lastPayDay = lastPayDay.AddDays(-14);
                    }
                }
            }
            else
            {
                // Last paycheck is in the past, just need to keep adding until we make it past the first of the month
                while (firstOfMonth > lastPayDay)
                {
                    lastPayDay = lastPayDay.AddDays(14);
                }
            }

            

            // The first paycheck of the month falls in the current month

            while (lastPayDay < nextMonth)
            {
                AmountThisMonth += BiWeeklyAmount;
                lastPayDay = lastPayDay.AddDays(14);
            }            
        }
    }
}
