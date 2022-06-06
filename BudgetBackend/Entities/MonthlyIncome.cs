using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class MonthlyIncome
    {
        public MonthlyIncome()
        {
            MonthlyBills = new HashSet<MonthlyBill>();
            WeeklyBudgets = new HashSet<WeeklyBudget>();
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime LastPayDay { get; set; }

        public virtual ICollection<MonthlyBill> MonthlyBills { get; set; }
        public virtual ICollection<WeeklyBudget> WeeklyBudgets { get; set; }
    }
}
