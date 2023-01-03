using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class Loan
    {
        public int Id { get; set; }
        public decimal StartingAmount { get; set; }
        public decimal MonthlyAmount { get; set; }
        public int MonthlyIncomeId { get; set; }
        public DateTime LastPaidDate { get; set; }
        public string Name { get; set; }

        public virtual MonthlyIncome MonthlyIncome { get; set; }
    }
}
