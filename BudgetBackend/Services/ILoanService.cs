using BudgetBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public interface ILoanService
    {
        public MonthlyIncomeDto CreateBudget(BudgetDto budget);
        public MonthlyIncomeDto UpdateBudget(BudgetDto budget);
        public MonthlyIncomeDto DeleteBudget(int id);
        public BudgetDto GetBudgetById(int id);
    }
}
