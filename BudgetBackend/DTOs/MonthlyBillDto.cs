using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.DTOs
{
    public class MonthlyBillDto
    {
        public int Id { get; set; }
        public int MonthlyIncomeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
    }
}
