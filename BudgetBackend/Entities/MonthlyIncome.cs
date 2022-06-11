using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class MonthlyIncome
    {
        public MonthlyIncome()
        {
            Budgets = new HashSet<Budget>();
            MonthlyBills = new HashSet<MonthlyBill>();
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime LastPayDay { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<MonthlyBill> MonthlyBills { get; set; }
    }
}
