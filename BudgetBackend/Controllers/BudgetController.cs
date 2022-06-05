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
        public MonthlyIncomeDto Create(WeeklyBudgetDto weeklyBudget)
        {
            return _service.CreateBudget(weeklyBudget);
        }

        [HttpPut]
        public MonthlyIncomeDto Update(WeeklyBudgetDto weeklyBudget)
        {
            return _service.UpdateBudget(weeklyBudget);
        }

        [HttpGet("{id:int}")]
        public WeeklyBudgetDto Get(int id)
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
