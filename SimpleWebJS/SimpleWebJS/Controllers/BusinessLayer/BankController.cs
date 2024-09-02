using Microsoft.AspNetCore.Mvc;
using OnlineBankPortal.Data;
using OnlineBankPortal.Models;

namespace OnlineBankPortal.Controllers.BusinessLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        // GET: api/Bank/GetAllUserProfiles
        [HttpGet("GetAllUserProfiles")]
        public ActionResult<IEnumerable<UserProfile>> GetAllUserProfiles()
        {
            try
            {
                List<UserProfile> userProfiles = UserProfileManager.GetAllUserProfiles();
                return Ok(userProfiles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // GET: api/Bank/GetAllAccounts
        [HttpGet("GetAllAccounts")]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            try
            {
                List<Account> accounts = AccountManager.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // GET: api/Bank/GetAllTransactions
        [HttpGet("GetAllTransactions")]
        public ActionResult<IEnumerable<Transaction>> GetAllTransactions()
        {
            try
            {
                List<Transaction> transactions = TransactionManager.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}