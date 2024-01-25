using EasyToBook.Application.Common.Interfaces;
using EasyToBook.WebApp.Models;
using EasyToBook.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyToBook.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                villaList = _unitOfWork.Villa.GetAll(includeProperties:"Amenities"),
                nmbrOfNights = 1,
                checkInDate = DateOnly.FromDateTime(DateTime.Now),
                checkOutDate = DateOnly.FromDateTime(DateTime.Now)
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
            //new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        }
    }
}
