using Microsoft.AspNetCore.Mvc;
using OnlineBankPortal.Data;
using OnlineBankPortal.Models;

namespace OnlineBankPortal.Controllers.BusinessLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // POST: api/Transaction/Deposit
        [HttpPost("Deposit")]
        public IActionResult Deposit([FromBody] DepositRequestModel request)
        {
            if (request == null || request.Amount <= 0)
            {
                return BadRequest("Invalid deposit request data.");
            }

            // You can access the account number as request.AccountNumber
            // and the amount as request.Amount

            // Perform the deposit transaction
            bool deposited = TransactionManager.Deposit(request.AccountNumber, request.Amount, request.Description);

            if (deposited)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Internal server error during deposit transaction.");
            }
        }


        // POST: api/Transaction/Withdrawal
        [HttpPost("Withdraw")]
        public IActionResult Withdraw([FromBody] DepositRequestModel request)
        {
            if (request == null || request.Amount <= 0)
            {
                return BadRequest("Invalid Withdraw request data.");
            }

            // You can access the account number as request.AccountNumber
            // and the amount as request.Amount

            // Perform the deposit transaction
            bool deposited = TransactionManager.Withdraw(request.AccountNumber, request.Amount, request.Description);

            if (deposited)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Internal server error during deposit transaction.");
            }
        }

        // GET: api/Transaction/History/{accountNumber}
        [HttpGet("History/{accountNumber}")]
        public IActionResult GetTransactionHistory(int accountNumber)
        {
            List<Transaction> transactionHistory = TransactionManager.GetTransactionsByAccountNumber(accountNumber);

            if (transactionHistory != null)
            {
                return Ok(transactionHistory);
            }
            else
            {
                return NotFound("Transaction history not found.");
            }
        }
    }

    public class DepositRequestModel
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }

		public string Description { get; set; }
	}

}

