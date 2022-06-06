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
        public DateTime LastPayDay { get; set; }
        public string Name { get; set; }
        public List<WeeklyBudgetDto> WeeklyBudgets { get; set; } = new List<WeeklyBudgetDto>();
        public List<MonthlyBillDto> MonthlyBills { get; set; } = new List<MonthlyBillDto>();

        public void CalculateValues()
        {
            CalculateMonthlyIncomeAmount();

            var remainingAmount = AmountThisMonth;
            LeftOverAfterTransactions = AmountThisMonth;

            foreach (var budget in WeeklyBudgets)
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

            RemainingAmount = remainingAmount;
            LeftOverAfterTransactions -= MonthlyBillsAmount;

            WeeklyBudgets = WeeklyBudgets.OrderBy(b => b.Name).ToList();
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
