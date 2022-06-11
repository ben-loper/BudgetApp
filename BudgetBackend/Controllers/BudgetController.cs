using BudgetBackend.DTOs;
using BudgetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BudgetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _service;

        private readonly ILogger<BudgetController> _logger;

        public BudgetController(ILogger<BudgetController> logger, IBudgetService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public MonthlyIncomeDto Create(BudgetDto budget)
        {
            return _service.CreateBudget(budget);
        }

        [HttpPut]
        public MonthlyIncomeDto Update(BudgetDto budget)
        {
            return _service.UpdateBudget(budget);
        }

        [HttpGet("{id:int}")]
        public BudgetDto Get(int id)
        {
            return _service.GetBudgetById(id);
        }

        [HttpDelete("{id:int}")]
        public MonthlyIncomeDto Delete(int id)
        {
            return _service.DeleteBudget(id);
        }
    }
}
