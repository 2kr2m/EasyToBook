using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using EasyToBook.Infrastructure.Repositories;
using EasyToBook.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyToBook.WebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var VillaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties:"villa");
            return View(VillaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                )
            };

            //ViewData["VillaList"]=list;
            //ViewData["VillaList"] = list;
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            //ModelState.Remove("Villa");
            bool villaNumberExist = _unitOfWork.VillaNumber.Any(u=>u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid && !villaNumberExist)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.VillaNumber.Save();
                TempData["success"] = "Villa Number has been created successfully!";
                return RedirectToAction("Index", "VillaNumber");
            }
            if (villaNumberExist)
            {
                TempData["error"] = "Villa Number Exists!";
            }
            obj.VillaList = _unitOfWork.VillaNumber.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.villa.Name,
                    Value = u.villa.Id.ToString(),
                }

                );
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()

            {
                VillaList = _unitOfWork.VillaNumber.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.villa.Name,
                    Value = u.villa.Id.ToString(),
                }

                ),
                VillaNumber = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null) {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }


        [HttpPost]
        public IActionResult Update(VillaNumberVM editedVillaNMBRVM)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(editedVillaNMBRVM.VillaNumber);
                _unitOfWork.VillaNumber.Save();
                TempData["success"] = "Villa Number has been updated successfully!";
                return RedirectToAction(nameof(Index), "VillaNumber");
            }
            editedVillaNMBRVM.VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                );
            return View(editedVillaNMBRVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()

            {
                VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                ),
                VillaNumber = _unitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM deletedVillaNumberVM)
        {
            VillaNumber? check = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == deletedVillaNumberVM.VillaNumber.Villa_Number);
            if (check is not null)
            {
                _unitOfWork.VillaNumber.Remove(check);
                _unitOfWork.VillaNumber.Save();
                TempData["success"] = "Villa Number has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Something went wrong!";
            return View(deletedVillaNumberVM);
        }
    }
}


