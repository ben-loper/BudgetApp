﻿using AutoMapper;
using BudgetBackend.DTOs;
using BudgetBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MonthlyIncome, MonthlyIncomeDto>().ForMember(dest => dest.BiWeeklyAmount, opt => opt.MapFrom(src => src.Amount)).ReverseMap();
            CreateMap<WeeklyBudget, WeeklyBudgetDto>().ReverseMap();
            CreateMap<MonthlyBill, MonthlyBillDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}