using BudgetBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public interface IBudgetService
    {
        public MonthlyIncomeDto CreateBudget(WeeklyBudgetDto budget);
        public MonthlyIncomeDto UpdateBudget(WeeklyBudgetDto budget);
        public MonthlyIncomeDto DeleteBudget(int id);
        public WeeklyBudgetDto GetBudgetById(int id);
    }
}
