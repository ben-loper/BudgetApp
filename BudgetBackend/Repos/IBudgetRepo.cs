using BudgetBackend.DTOs;
using BudgetBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Repos
{
    public interface IBudgetRepo
    {
        public List<MonthlyIncome> GetAllMonthlyIncomes();
        public MonthlyIncome GetMonthlyIncome();
        public MonthlyIncome GetMonthlyIncomeById(int id);
        public MonthlyIncome CreateMonthlyIncome(MonthlyIncome monthlyIncome);
        public MonthlyIncome UpdateMonthlyIncome(MonthlyIncome monthlyIncome);
        public void DeleteMonthlyIncome(MonthlyIncome monthlyIncome);
        public Budget GetBudgetById(int id);
        public MonthlyIncome UpdateBudget(Budget weeklyBudget);
        public MonthlyIncome DeleteBudgetById(int id);
        public MonthlyIncome CreateTransaction(Transaction transaction);
        public MonthlyIncome UpdateTransaction(Transaction transaction);
        public MonthlyIncome DeleteTransactionById(int id);
        public List<MonthlyBill> CreateMonthlyBill(MonthlyBill monthlyBill);
        public List<MonthlyBill> GetMonthlyBillByMonthlyIncomeId(int id);
        public List<MonthlyBill> UpdateMonthlyBill(MonthlyBill monthlyBill);
        public List<MonthlyBill> DeleteMonthlyBillById(int id);
    }
}
