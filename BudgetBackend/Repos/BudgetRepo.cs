using BudgetBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Repos
{
    public class BudgetRepo : IBudgetRepo
    {
        readonly BudgetappContext _context;

        public BudgetRepo(BudgetappContext context)
        {
            _context = context;
        }

        public List<MonthlyIncome> GetAllMonthlyIncomes()
        {
            return _context.MonthlyIncomes
                .Include(m => m.WeeklyBudgets)
                .ThenInclude(m => m.Transactions)
                .Include(m => m.MonthlyBills)
                .ToList();
        }

        public MonthlyIncome GetMonthlyIncome()
        {
            var today = DateTime.Now.Date;

            var year = today.Year;
            var month = today.Month + 1;

            if (today.Month == 12)
            {
                year = today.Year + 1;
                month = 1;
            }

            var firstOfTheMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfTheMonth = new DateTime(year, month, 1).AddDays(-1);

            return _context.MonthlyIncomes
                .Include(m => m.WeeklyBudgets)
                .ThenInclude(m => m.Transactions.Where(t => t.TransactionDate.Date >= firstOfTheMonth && t.TransactionDate.Date <= lastDayOfTheMonth))
                .AsSplitQuery()
                .Include(m => m.MonthlyBills)
                .AsSplitQuery()
                .FirstOrDefault();
        }

        public MonthlyIncome CreateMonthlyIncome(MonthlyIncome monthlyIncome)
        {
            _context.MonthlyIncomes.Add(monthlyIncome);

            _context.SaveChanges();

            return monthlyIncome;
        }

        public MonthlyIncome GetMonthlyIncomeById(int id)
        {
            var today = DateTime.Now.Date;

            Console.WriteLine("Called get monthly income by id");

            var year = today.Year;
            var month = today.Month + 1;

            if (today.Month == 12)
            {
                year = today.Year + 1;
                month = 1;
            }

            var firstOfTheMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfTheMonth = new DateTime(year, month, 1).AddDays(-1);

            var income = _context.MonthlyIncomes.Where(m => m.Id == id)
                .Include(m => m.WeeklyBudgets)
                .AsSplitQuery()
                .Include(m => m.MonthlyBills)
                .AsSplitQuery()
                .FirstOrDefault();

            foreach(var budget in income.WeeklyBudgets)
            {
                budget.Transactions = _context.Transactions.Where(t => t.BudgetId == budget.Id &&  t.TransactionDate.Date >= firstOfTheMonth && t.TransactionDate.Date <= lastDayOfTheMonth).ToList();
            }

            return income;
        }

        public MonthlyIncome UpdateMonthlyIncome(MonthlyIncome monthlyIncome)
        {
            var savedIncome = _context.MonthlyIncomes.Where(m => m.Id == monthlyIncome.Id).FirstOrDefault();

            savedIncome = monthlyIncome;
            
            _context.SaveChanges();

            savedIncome = GetMonthlyIncomeById(savedIncome.Id);

            return savedIncome;
        }

        public WeeklyBudget GetWeeklyBudgetById(int id)
        {
            var today = DateTime.Now.Date;

            var firstOfTheMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfTheMonth = new DateTime(today.Year, today.Month + 1, 1).AddDays(-1);

            return _context.WeeklyBudgets
                .Where(b => b.Id == id)
                .Include(b => b.Transactions.OrderByDescending(t => t.TransactionDate)
                                            .Where(t => t.TransactionDate.Date >= firstOfTheMonth && t.TransactionDate.Date <= lastDayOfTheMonth))
                .FirstOrDefault();
        }

        public MonthlyIncome UpdateWeeklyBudget(WeeklyBudget weeklyBudget)
        {
            var savedBudget = _context.WeeklyBudgets.Where(m => m.Id == weeklyBudget.Id).FirstOrDefault();

            savedBudget.Amount = weeklyBudget.Amount;
            savedBudget.Name = weeklyBudget.Name;

            _context.SaveChanges();

            var monthlyIncome = GetMonthlyIncomeById(savedBudget.MonthlyIncomeId);

            return monthlyIncome;
        }

        public void DeleteMonthlyIncome(MonthlyIncome monthlyIncome)
        {
            _context.Remove(monthlyIncome);

            _context.SaveChanges();
        }

        public MonthlyIncome DeleteBudgetById(int id)
        {
            var budgetToDelete = _context.WeeklyBudgets.Where(b => b.Id == id).Include(b => b.Transactions).FirstOrDefault();

            var monthlyIncomeId = budgetToDelete.MonthlyIncomeId;

            if (budgetToDelete.Transactions.Any())
            {
                _context.Transactions.RemoveRange(budgetToDelete.Transactions);
                _context.SaveChanges();
            }

            _context.Remove(budgetToDelete);
            _context.SaveChanges();

            var monthlyIncome = GetMonthlyIncomeById(monthlyIncomeId);

            return monthlyIncome;
        }

        public MonthlyIncome CreateTransaction(Transaction transaction)
        {
            MonthlyIncome income = null;

            var budget = _context.WeeklyBudgets.Where(b => b.Id == transaction.BudgetId).Include(b => b.Transactions).FirstOrDefault();
            income = _context.MonthlyIncomes.Where(i => i.Id == budget.MonthlyIncomeId).FirstOrDefault();

            budget.Transactions.Add(transaction);

            _context.SaveChanges();
            _context.Update(income);

            income = GetMonthlyIncomeById(budget.MonthlyIncomeId);

            return income;
        }

        public MonthlyIncome UpdateTransaction(Transaction transaction)
        {
            MonthlyIncome income = null;

            var savedTransaction = _context.Transactions.Where(t => t.Id == transaction.Id).Include(t => t.Budget).FirstOrDefault();

            savedTransaction.Name = transaction.Name;
            savedTransaction.Amount = transaction.Amount;
            savedTransaction.TransactionDate = transaction.TransactionDate;

            _context.Transactions.Update(savedTransaction);

            _context.SaveChanges();

            income = GetMonthlyIncomeById(savedTransaction.Budget.MonthlyIncomeId);

            return income;
        }

        public MonthlyIncome DeleteTransactionById(int id)
        {
            MonthlyIncome income = null;

            var transaction = _context.Transactions.Where(t => t.Id == id).FirstOrDefault();

            var budget = _context.WeeklyBudgets.Where(b => b.Id == transaction.BudgetId).FirstOrDefault();

            var monthlyIncomeId = budget.MonthlyIncomeId;

            _context.Remove(transaction);

            _context.SaveChanges();

            income = GetMonthlyIncomeById(monthlyIncomeId);

            return income;
        }

        public List<MonthlyBill> CreateMonthlyBill(MonthlyBill monthlyBill)
        {
            _context.MonthlyBills.Add(monthlyBill);

            _context.SaveChanges();

            return _context.MonthlyBills.Where(b => b.MonthlyIncomeId == monthlyBill.MonthlyIncomeId).ToList();
        }
        public List<MonthlyBill> GetMonthlyBillByMonthlyIncomeId(int id)
        {
            return _context.MonthlyBills.Where(b => b.MonthlyIncomeId == id).ToList();
        }
        public List<MonthlyBill> UpdateMonthlyBill(MonthlyBill monthlyBill)
        {
            var bill = _context.MonthlyBills.Where(b => b.Id == monthlyBill.Id).FirstOrDefault();

            bill.Amount = monthlyBill.Amount;
            bill.Name = monthlyBill.Name;
            
            _context.MonthlyBills.Update(bill);
            
            _context.SaveChanges();

            return _context.MonthlyBills.Where(b => b.MonthlyIncomeId == monthlyBill.MonthlyIncomeId).ToList();
        }
        public List<MonthlyBill> DeleteMonthlyBillById(int id)
        {
            var savedBill = _context.MonthlyBills.Where(b => b.Id == id).FirstOrDefault();
            
            _context.Remove(savedBill);
            
            _context.SaveChanges();

            return _context.MonthlyBills.Where(b => b.MonthlyIncomeId == savedBill.MonthlyIncomeId).ToList();
        }
    }
}
