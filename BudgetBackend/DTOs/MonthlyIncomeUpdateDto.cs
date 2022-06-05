using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.DTOs
{
    public class MonthlyIncomeUpdateDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime LastPayDay { get; set; }
    }
}
