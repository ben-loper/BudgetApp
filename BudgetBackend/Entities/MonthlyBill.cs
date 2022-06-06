using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class MonthlyBill
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public int MonthlyIncomeId { get; set; }
        public string Name { get; set; }

        public virtual MonthlyIncome MonthlyIncome { get; set; }
    }
}
