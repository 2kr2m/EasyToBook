using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyToBook.WebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "Name and Description cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Villa.Save();
                TempData["success"] = "Villa has been created successfully!";
                return RedirectToAction("Index", "Villa");
            }
            TempData["eror"] = "Something went wrong!";
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? villa = _unitOfWork.Villa.Get(x => x.Id == villaId);
            if(villa == null)
            {
                return RedirectToAction("Error","Home");
            }

            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa editedVilla)
        {
            if (editedVilla.Name == editedVilla.Description)
            {
                ModelState.AddModelError("", "Name and Description cannot be the same");
            }
            if (ModelState.IsValid & editedVilla.Id>0)
            {
                _unitOfWork.Villa.Update(editedVilla);
                _unitOfWork.Villa.Save();
                TempData["success"] = "Villa has been updated successfully!";
                return RedirectToAction("Index", "Villa");
            }
            TempData["eror"] = "Something went wrong!";
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? villa = _unitOfWork.Villa.Get(x => x.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa deletedVilla)
        {
            Villa? check = _unitOfWork.Villa.Get(u => u.Id == deletedVilla.Id);
            if (check is not null)
            {
                _unitOfWork.Villa.Remove(check);
                _unitOfWork.Villa.Save();
                TempData["success"] = "Villa has been deleted successfully!";
                return RedirectToAction("Index", "Villa");
            }
            TempData["eror"] = "Something went wrong!";
            return View();
        }

    }
}
