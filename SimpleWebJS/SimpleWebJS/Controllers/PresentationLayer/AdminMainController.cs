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
    public class AdminMainController : Controller
    {


        [HttpGet("profileview")]
        public IActionResult GetprofileView()
        {

            // Return the partial view as HTML
            return PartialView("ProfileView");
        }

		[HttpGet("usersView")]
		public IActionResult getAccDetailsPage()
		{

			// Return the partial view as HTML
			return PartialView("UsersView");
		}

		[HttpGet("transactionview")]
        public IActionResult GetTransactionView()
        {

            // Return the partial view as HTML
            return PartialView("Transactionview");
        }
		

		[HttpGet("userEditView")]
        public IActionResult getUserEditPage()
        {

            // Return the partial view as HTML
            return PartialView("UserEditView");
        }




    }
}

