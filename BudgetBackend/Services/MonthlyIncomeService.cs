using AutoMapper;
using BudgetBackend.DTOs;
using BudgetBackend.Entities;
using BudgetBackend.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Services
{
    public class MonthlyIncomeService : IMonthlyIncomeService
    {
        readonly IMapper _mapper;
        readonly IBudgetRepo _repo;

        public MonthlyIncomeService(IMapper mapper, IBudgetRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public List<MonthlyIncomeDto> GetAllMonthlyIncomes()
        {
            List<MonthlyIncomeDto> monthlyIncomes = null;

            var incomes = _repo.GetAllMonthlyIncomes();

            if (incomes != null)
            {
                monthlyIncomes = _mapper.Map<List<MonthlyIncome>, List<MonthlyIncomeDto>>(incomes);
            }

            return monthlyIncomes;
        }

        public MonthlyIncomeDto GetMonthlyIncome()
        {
            MonthlyIncomeDto monthlyIncome = null;

            var income = _repo.GetMonthlyIncome();

            monthlyIncome = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(income);

            monthlyIncome.CalculateValues();

            return monthlyIncome;
        }

        public MonthlyIncomeDto CreateMonthlyIncome(MonthlyIncomeDto monthlyIncome)
        {
            MonthlyIncome savedMonthlyIncome = null;

            savedMonthlyIncome = _mapper.Map<MonthlyIncomeDto, MonthlyIncome>(monthlyIncome);

            savedMonthlyIncome = _repo.CreateMonthlyIncome(savedMonthlyIncome);

            monthlyIncome = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(savedMonthlyIncome);

            return monthlyIncome;
        }

        public MonthlyIncomeDto GetMonthlyIncomeById(int id)
        {
            MonthlyIncomeDto result = null;

            var income = _repo.GetMonthlyIncomeById(id);

            if (income != null)
            {
                result = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(income);
            }

            return result;
        }

        public MonthlyIncomeDto UpdateMonthlyIncome(MonthlyIncomeUpdateDto monthlyIncome)
        {
            MonthlyIncome income = _repo.GetMonthlyIncomeById(monthlyIncome.Id);

            MonthlyIncomeDto updatedIncome = null;

            if (income != null)
            {
                income.Name = monthlyIncome.Name;
                income.Amount = monthlyIncome.Amount;
                income.LastPayDay = monthlyIncome.LastPayDay;
                
                var result = _repo.UpdateMonthlyIncome(income);

                if (result != null)
                {
                    updatedIncome = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(result);
                }

                updatedIncome.CalculateValues();
            }

            return updatedIncome;
        }

        public void DeleteMonthlyIncome(MonthlyIncomeDto monthlyIncome)
        {
            var income = _mapper.Map<MonthlyIncomeDto, MonthlyIncome>(monthlyIncome);

            _repo.DeleteMonthlyIncome(income);
        }

        public List<LoanDto> GetLoansForMonthlyIncome(int id)
        {
            var loans = new List<LoanDto>();

            var savedLoans = _repo.GetLoansByMonthlyIncomeId(id);

            if (savedLoans != null)
            {
                loans = _mapper.Map<List<Loan>, List<LoanDto>>(savedLoans);
            }

            return loans;
        }
    }
}
