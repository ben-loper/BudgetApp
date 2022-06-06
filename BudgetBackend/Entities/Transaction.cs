using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Name { get; set; }

        public virtual WeeklyBudget Budget { get; set; }
    }
}
