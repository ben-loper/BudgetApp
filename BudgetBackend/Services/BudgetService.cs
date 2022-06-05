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

        public WeeklyBudgetDto GetBudgetById(int id)
        {
            var dto = new WeeklyBudgetDto();

            var savedBudget = _repo.GetWeeklyBudgetById(id);

            dto = _mapper.Map<WeeklyBudget, WeeklyBudgetDto>(savedBudget);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto CreateBudget(WeeklyBudgetDto budget)
        {
            var monthlyIncome = _repo.GetMonthlyIncomeById(budget.MonthlyIncomeId);

            var createdBudget = _mapper.Map<WeeklyBudgetDto, WeeklyBudget>(budget);

            monthlyIncome.WeeklyBudgets.Add(createdBudget);

            _repo.UpdateMonthlyIncome(monthlyIncome);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto UpdateBudget(WeeklyBudgetDto budget)
        {
            var updatedBudget = _mapper.Map<WeeklyBudgetDto, WeeklyBudget>(budget);

            var monthlyIncome = _repo.UpdateWeeklyBudget(updatedBudget);

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
