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
    public class MonthlyBillService : IMonthlyBillService
    {
        readonly IMapper _mapper;
        readonly IBudgetRepo _repo;

        public MonthlyBillService(IMapper mapper, IBudgetRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public List<MonthlyBillDto> CreateMonthlyBill(MonthlyBillDto monthlyBill)
        {
            monthlyBill.CreatedDate = DateTime.Now;
            var newMonthlyBill = _mapper.Map<MonthlyBillDto, MonthlyBill>(monthlyBill);

            var savedMonthlyBills = _repo.CreateMonthlyBill(newMonthlyBill);

            var monthlyBills = _mapper.Map<List<MonthlyBill>, List<MonthlyBillDto>>(savedMonthlyBills);

            return monthlyBills;
        }

        public List<MonthlyBillDto> DeleteMonthlyBillById(int id)
        {
            var savedMonthlyBills = _repo.DeleteMonthlyBillById(id);

            return _mapper.Map<List<MonthlyBill>, List<MonthlyBillDto>>(savedMonthlyBills);
        }

        public List<MonthlyBillDto> GetMonthlyBillsForMonthlyIncomeById(int id)
        {
            var savedMonthlyBills = _repo.GetMonthlyBillByMonthlyIncomeId(id);

            return _mapper.Map<List<MonthlyBill>, List<MonthlyBillDto>>(savedMonthlyBills);
        }

        public List<MonthlyBillDto> UpdateMonthlyBill(MonthlyBillDto monthlyBill)
        {
            var updatedMonthlyBill = _mapper.Map<MonthlyBillDto, MonthlyBill>(monthlyBill);

            var savedMonthlyBills = _repo.UpdateMonthlyBill(updatedMonthlyBill);

            var monthlyBills = _mapper.Map<List<MonthlyBill>, List<MonthlyBillDto>>(savedMonthlyBills);

            return monthlyBills;
        }

    }
}
