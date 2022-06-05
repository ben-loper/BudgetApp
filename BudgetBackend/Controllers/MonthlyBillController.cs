using BudgetBackend.DTOs;
using BudgetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BudgetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthlyBillController : ControllerBase
    {
        private readonly IMonthlyBillService _service;

        private readonly ILogger<MonthlyBillController> _logger;

        public MonthlyBillController(ILogger<MonthlyBillController> logger, IMonthlyBillService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public List<MonthlyBillDto> Create(MonthlyBillDto monthlyBill)
        {
            return _service.CreateMonthlyBill(monthlyBill);
        }

        [HttpPut]
        public List<MonthlyBillDto> Update(MonthlyBillDto monthlyBill)
        {
            return _service.UpdateMonthlyBill(monthlyBill);
        }

        [HttpGet("{monthlyIncomeId:int}")]
        public List<MonthlyBillDto> Get(int monthlyIncomeId)
        {
            return _service.GetMonthlyBillsForMonthlyIncomeById(monthlyIncomeId);
        }

        [HttpDelete("{id:int}")]
        public List<MonthlyBillDto> Delete(int id)
        {
            return _service.DeleteMonthlyBillById(id);
        }
    }
}
