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
    public class TransactionService : ITransactionService
    {
        readonly IMapper _mapper;
        readonly IBudgetRepo _repo;

        public TransactionService(IMapper mapper, IBudgetRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public MonthlyIncomeDto CreateTransaction(TransactionDto transaction)
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            transaction.TransactionDate = TimeZoneInfo.ConvertTimeFromUtc(transaction.TransactionDate, easternZone);
            transaction.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), easternZone);

            var newTransaction = _mapper.Map<TransactionDto, Transaction>(transaction);

            var monthlyIncome = _repo.CreateTransaction(newTransaction);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto UpdateTransaction(TransactionDto transaction)
        {
            var newTransaction = _mapper.Map<TransactionDto, Transaction>(transaction);

            var monthlyIncome = _repo.UpdateTransaction(newTransaction);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }

        public MonthlyIncomeDto DeleteTransaction(int id)
        {
            var monthlyIncome = _repo.DeleteTransactionById(id);

            var dto = _mapper.Map<MonthlyIncome, MonthlyIncomeDto>(monthlyIncome);

            dto.CalculateValues();

            return dto;
        }
    }
}
