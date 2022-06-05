using BudgetBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public interface ITransactionService
    {
        public MonthlyIncomeDto CreateTransaction(TransactionDto transaction);
        public MonthlyIncomeDto UpdateTransaction(TransactionDto transaction);
        public MonthlyIncomeDto DeleteTransaction(int id);
    }
}
