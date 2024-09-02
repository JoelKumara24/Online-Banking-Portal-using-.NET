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
    public class LoginController : Controller
    {
        [HttpGet("defaultview")]
        public IActionResult GetDefaultView()
        {

            // Return the partial view as HTML
            return PartialView("LoginDefaultView");
        }

        [HttpGet("authview")]
        public IActionResult GetLoginAuthenticatedView()
        {


            // Return the partial view as HTML
            return PartialView("LoginViewAuthenticated");



        }



        [HttpGet("error")]
        public IActionResult GetLoginErrorView()
        {
            return PartialView("LoginErrorView");
        }


        [HttpGet("auth/{username}/{password}")]
        public IActionResult Authenticate(string username, string password)
        {
            // Return the partial view as HTML

            RestClient restClient = new RestClient("http://localhost:5130");
            RestRequest restRequest = new RestRequest($"/api/userprofile/Username/{username}", Method.Get);
            restRequest.AddUrlSegment("username", username);
            RestResponse restResponse = restClient.Execute(restRequest);
            // Console.WriteLine(restResponse.Content);
            UserProfile result = JsonConvert.DeserializeObject<UserProfile>(restResponse.Content);


            if (result.Username == username && result.PasswordHash == password)
            {
                return Ok();
				
            }
            return NotFound();

        }

    }
}

