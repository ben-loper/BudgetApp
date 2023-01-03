using System;

namespace BudgetBackend.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public decimal StartingAmount { get; set; }
        public decimal MonthlyAmount { get; set; }
        public int MonthlyIncomeId { get; set; }
        public DateTime LastPaidDate { get; set; }
        public string Name { get; set; }

        private decimal _amountRemaining;

        public decimal AmountRemaining 
        {
            get
            {
                var today = DateTime.Now;
                var currentPayDate = LastPaidDate;

                var keepGoing = true;
                var amountLeft = StartingAmount;

                while (keepGoing)
                {
                    if (LastPaidDate > today || currentPayDate > today)
                    {
                        keepGoing = false;
                    }
                    else
                    {
                        amountLeft -= MonthlyAmount;
                        
                        if (amountLeft <= 0)
                        {
                            keepGoing = false;
                        }

                        currentPayDate.AddMonths(1);
                    }
                }

                return _amountRemaining;
            }
            set { _amountRemaining = value; }
        }
    }
}
