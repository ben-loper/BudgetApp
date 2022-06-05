using BudgetBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public interface IMonthlyIncomeService
    {
        public List<MonthlyIncomeDto> GetAllMonthlyIncomes();
        public MonthlyIncomeDto GetMonthlyIncome();
        public MonthlyIncomeDto GetMonthlyIncomeById(int id);
        public MonthlyIncomeDto CreateMonthlyIncome(MonthlyIncomeDto monthlyIncome);
        public MonthlyIncomeDto UpdateMonthlyIncome(MonthlyIncomeUpdateDto monthlyIncome);
        public void DeleteMonthlyIncome(MonthlyIncomeDto monthlyIncome);
    }
}
