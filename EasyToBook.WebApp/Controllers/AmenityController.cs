using EasyToBook.Application.Common.Interfaces;
using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Repositories;
using EasyToBook.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyToBook.WebApp.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Amenities = _unitOfWork.Amenity.GetAll(includeProperties: "villa");
            return View(Amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()

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
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            //ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.VillaNumber.Save();
                TempData["success"] = "Villa Number has been created successfully!";
                return RedirectToAction(nameof(Index),"Amenity");
            }
            obj.VillaList = _unitOfWork.Amenity.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.villa.Name,
                    Value = u.villa.Id.ToString(),
                }

                );
            return View(obj);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()

            {
                VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                ),
                Amenity = _unitOfWork.Amenity.Get(x => x.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }


        [HttpPost]
        public IActionResult Update(AmenityVM editedAmenity)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(editedAmenity.Amenity);
                _unitOfWork.Amenity.Save();
                TempData["success"] = "Amenity has been updated successfully!";
                return RedirectToAction(nameof(Index), "Amenity");
            }
            editedAmenity.VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                );
            return View(editedAmenity);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()

            {
                VillaList = _unitOfWork.Villa.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                ),
                Amenity = _unitOfWork.Amenity.Get(x => x.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM deletedAmenityVM)
        {
            Amenity? check = _unitOfWork.Amenity.Get(u => u.Id == deletedAmenityVM.Amenity.Id);
            if (check is not null)
            {
                _unitOfWork.Amenity.Remove(check);
                _unitOfWork.Amenity.Save();
                TempData["success"] = "Amenity has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Something went wrong!";
            return View(deletedAmenityVM);
        }
    }
}
