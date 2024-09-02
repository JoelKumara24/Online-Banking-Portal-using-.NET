using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBankPortal.Models;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBankPortal.Controllers.PresentationLayer
{
    [Route("/[controller]")]
    public class MainController : Controller
    {


        [HttpGet("profileview")]
        public IActionResult GetprofileView()
        {

            // Return the partial view as HTML
            return PartialView("ProfileView");
        }

		[HttpGet("accountview")]
		public IActionResult getAccDetailsPage()
		{

			// Return the partial view as HTML
			return PartialView("AccountView");
		}

		[HttpGet("transactionview")]
        public IActionResult GetTransactionView()
        {

            // Return the partial view as HTML
            return PartialView("Transactionview");
        }
		

		[HttpGet("transferView")]
        public IActionResult getQuickTransferPage()
        {

            // Return the partial view as HTML
            return PartialView("TransferView");
        }




    }
}

