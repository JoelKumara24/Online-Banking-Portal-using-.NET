using Microsoft.AspNetCore.Mvc;
using OnlineBankPortal.Data;
using OnlineBankPortal.Models;

namespace OnlineBankPortal.Controllers.BusinessLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // POST: api/Account
        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("Invalid account data.");
            }

            bool created = AccountManager.CreateAccount(account);

            if (created)
            {
                return Ok("Bank account created successfully.");
            }
            else
            {
                return StatusCode(500, "Internal server error while creating bank account.");
            }
        }

        // GET: api/Account/{accountNumber}
        [HttpGet("{accountNumber}")]
        public IActionResult GetAccountByAccountNumber(int accountNumber)
        {
            Account account = AccountManager.GetAccountByAccountNumber(accountNumber);

            if (account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound("Bank account not found.");
            }
        }

        // PUT: api/Account
        [HttpPut]
        public IActionResult UpdateAccount([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("Invalid account data.");
            }

            bool updated = AccountManager.UpdateAccount(account);

            if (updated)
            {
                return Ok("Bank account updated successfully.");
            }
            else
            {
                return StatusCode(500, "Internal server error while updating bank account.");
            }
        }

        // DELETE: api/Account/{accountNumber}
        [HttpDelete("{accountNumber}")]
        public IActionResult DeleteAccount(int accountNumber)
        {
            bool deleted = AccountManager.DeleteAccount(accountNumber);

            if (deleted)
            {
                return Ok("Bank account deleted successfully.");
            }
            else
            {
                return NotFound("Bank account not found or unable to delete.");
            }
        }
    }
}
