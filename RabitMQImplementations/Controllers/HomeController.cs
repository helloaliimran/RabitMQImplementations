using Microsoft.AspNetCore.Mvc;
using RabitMQImplementations.Models;
using System.Diagnostics;

namespace RabitMQImplementations.Controllers
{
	public class HomeController : Controller
	{
		
		private readonly IRabbitMQAuthMessageSender _messageSender;
		private readonly IConfiguration _configuration;
		public HomeController( IRabbitMQAuthMessageSender messageSender,IConfiguration configuration)
		{
		
			_messageSender = messageSender;
			_configuration = configuration;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{

			_messageSender.SendMessage("I am sender !", _configuration.GetValue<string>("QueueName:RegisterUserQueue")) ;
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}