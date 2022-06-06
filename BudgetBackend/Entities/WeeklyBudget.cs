﻿using System;
using System.Collections.Generic;

namespace BudgetBackend.Entities
{
    public partial class WeeklyBudget
    {
        public WeeklyBudget()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int MonthlyIncomeId { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }

        public virtual MonthlyIncome MonthlyIncome { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
