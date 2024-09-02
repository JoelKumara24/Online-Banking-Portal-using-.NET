using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineBankPortal.Models;

namespace OnlineBankPortal.Controllers;

public class HomeController : Controller
{


	public IActionResult Index()
	{
		return PartialView();
	}
}

