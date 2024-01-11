using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace EasyToBook.WebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villa");
                    
                    using var fileStream = new FileStream(Path.Combine(imagePath,fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\Villa\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }
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

                if (editedVilla.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(editedVilla.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villa");
                    
                    if(!string.IsNullOrEmpty(editedVilla.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, editedVilla.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    editedVilla.Image.CopyTo(fileStream);
                    editedVilla.ImageUrl = @"\images\Villa\" + fileName;
                }

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
                if (!string.IsNullOrEmpty(check.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, check.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
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
