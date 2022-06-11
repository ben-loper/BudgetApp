using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.DTOs
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public int MonthlyIncomeId { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountThisMonth { get; set; }
        public decimal TotalTransactionsForTheMonth { get; set; }
        public decimal AmountRemaining { get; set; }
        public string Name { get; set; }
        public bool IsWeekly { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

        public void CalculateValues()
        {
            if (IsWeekly)
            {
                AmountThisMonth = Amount * NumberOfWeeksThisMonth();
            }
            else
            {
                AmountThisMonth = Amount;
            }

            AmountRemaining = AmountThisMonth;

            foreach (var transaction in Transactions)
            {
                TotalTransactionsForTheMonth += transaction.Amount;
                AmountRemaining -= transaction.Amount;
            }

            Transactions = Transactions.OrderByDescending(t => t.TransactionDate).ToList();
        }

        private int NumberOfWeeksThisMonth()
        {
            int result = 0;
            var today = DateTime.Now.Date;

            var firstSunday = new DateTime(today.Year, today.Month, 1);

            while (firstSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                firstSunday = firstSunday.AddDays(1);
            }

            while (firstSunday.Month == today.Month)
            {
                result++;
                firstSunday = firstSunday.AddDays(7);
            }

            return result;
        }
    }
}
