using BudgetBackend.DTOs;
using BudgetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BudgetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public MonthlyIncomeDto Create(TransactionDto transaction)
        {
            return _service.CreateTransaction(transaction);
        }

        [HttpPut]
        public MonthlyIncomeDto Update(TransactionDto transaction)
        {
            return _service.UpdateTransaction(transaction);
        }

        [HttpDelete("{id:int}")]
        public MonthlyIncomeDto Delete(int id)
        {
            return _service.DeleteTransaction(id);
        }
    }
}
