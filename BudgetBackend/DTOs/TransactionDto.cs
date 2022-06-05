using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
