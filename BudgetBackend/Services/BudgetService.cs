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
    public class BudgetService : IBudgetService
    {
        readonly IMapper _mapper;
        readonly IBudgetRepo _repo;

        public BudgetService(IMapper mapper, IBudgetRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public BudgetDto GetBudgetById(int id)
        {
            var dto = new BudgetDto();

            var savedBudget = _repo.GetBudgetById(id);

            if (savedBudget.IsMisc)
            {
                var monthlyIncome = _repo.GetMonthlyIncomeById(savedBudget.MonthlyIncomeId);
                var income = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);
                income.CalculateValues();

                dto = income.Budgets.Where(b => b.Id == id).FirstOrDefault();
            }
            else
            {
                dto = _mapper.Map<Budget, BudgetDto>(savedBudget);

                dto.CalculateValues();
            }

            return dto;
        }

        public MonthlyIncomeDto CreateBudget(BudgetDto budget)
        {
            var monthlyIncome = _repo.GetMonthlyIncomeById(budget.MonthlyIncomeId);

            var createdBudget = _mapper.Map<BudgetDto, Budget>(budget);

            monthlyIncome.Budgets.Add(createdBudget);

            _repo.UpdateMonthlyIncome(monthlyIncome);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto UpdateBudget(BudgetDto budget)
        {
            var updatedBudget = _mapper.Map<BudgetDto, Budget>(budget);

            var monthlyIncome = _repo.UpdateBudget(updatedBudget);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto DeleteBudget(int id)
        {
            var monthlyIncome = _repo.DeleteBudgetById(id);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }
    }
}
