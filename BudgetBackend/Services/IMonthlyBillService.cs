using BudgetBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public interface IMonthlyBillService
    {
        public List<MonthlyBillDto> CreateMonthlyBill(MonthlyBillDto monthlyBill);
        public List<MonthlyBillDto> UpdateMonthlyBill(MonthlyBillDto monthlyBill);
        public List<MonthlyBillDto> DeleteMonthlyBillById(int id);
        public List<MonthlyBillDto> GetMonthlyBillsForMonthlyIncomeById(int id);
    }
}
