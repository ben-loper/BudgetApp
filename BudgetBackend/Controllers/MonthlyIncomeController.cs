using BudgetBackend.DTOs;
using BudgetBackend.Entities;
using BudgetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthlyIncomeController : ControllerBase
    {
        private readonly IMonthlyIncomeService _service;

        private readonly ILogger<MonthlyIncomeController> _logger;

        public MonthlyIncomeController(ILogger<MonthlyIncomeController> logger, IMonthlyIncomeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public MonthlyIncomeDto Get()
        {
            return _service.GetMonthlyIncome();
        }

        [HttpPost]
        public MonthlyIncomeDto Create(MonthlyIncomeDto monthlyIncome)
        {
            return _service.CreateMonthlyIncome(monthlyIncome);
        }

        [HttpPut]
        public MonthlyIncomeDto Update(MonthlyIncomeUpdateDto monthlyIncome)
        {
            return _service.UpdateMonthlyIncome(monthlyIncome);
        }

        [HttpDelete]
        public ActionResult Delete(MonthlyIncomeDto income)
        {
            _service.DeleteMonthlyIncome(income);

            return Ok();
        }

        [HttpGet("{monthlyIncomeId:int}/loans")]
        public List<LoanDto> Get(int monthlyIncomeId)
        {
            return _service.GetLoansForMonthlyIncome(monthlyIncomeId);
        }
    }
}
