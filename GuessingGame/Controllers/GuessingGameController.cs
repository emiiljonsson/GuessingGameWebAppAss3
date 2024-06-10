using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers
{
    public class GuessingGameController : Controller
    {
        private const string SessionKeyNumber = "_Number";

        [HttpGet]
        public IActionResult Index()
        {
            var random = new Random();
            int number = random.Next(1, 101);
            HttpContext.Session.SetInt32(SessionKeyNumber, number);

            return View();
        }

        [HttpPost]
        public IActionResult Index(int? guess)
        {
            if (!guess.HasValue)
            {
                ViewBag.Message = "Var god och ange ett giltigt nummer.";
                return View();
            }

            int? number = HttpContext.Session.GetInt32(SessionKeyNumber);
            if (!number.HasValue)
            {
                return RedirectToAction("Index");
            }

            if (guess.Value == number.Value)
            {
                ViewBag.Message = "Grattis! Du gissade r�tt!";
                var random = new Random();
                number = random.Next(1, 101);
                HttpContext.Session.SetInt32(SessionKeyNumber, number.Value);
            }
            else if (guess.Value < number.Value)
            {
                ViewBag.Message = "F�r l�gt. F�rs�k igen.";
            }
            else
            {
                ViewBag.Message = "F�r h�gt. F�rs�k igen.";
            }

            return View();
        }
    }
}
