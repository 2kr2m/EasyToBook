using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using EasyToBook.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyToBook.WebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var VillaNumbers = _context.VillaNumbers.Include(u=>u.villa).ToList();
            return View(VillaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            
            {
                VillaList = _context.Villas.ToList().Select(
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
            bool villaNumberExist = _context.VillaNumbers.Any(u=>u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid && !villaNumberExist)
            {
                _context.VillaNumbers.Add(obj.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number has been created successfully!";
                return RedirectToAction("Index", "VillaNumber");
            }
            if (villaNumberExist)
            {
                TempData["error"] = "Villa Number Exists!";
            }
            obj.VillaList = _context.Villas.ToList().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                );
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()

            {
                VillaList = _context.Villas.ToList().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                ),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId)
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
                _context.VillaNumbers.Update(editedVillaNMBRVM.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number has been updated successfully!";
                return RedirectToAction("Index", "VillaNumber");
            }
            editedVillaNMBRVM.VillaList = _context.Villas.ToList().Select(
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
                VillaList = _context.Villas.ToList().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }

                ),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId)
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
            VillaNumber? check = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == deletedVillaNumberVM.VillaNumber.Villa_Number);
            if (check is not null)
            {
                _context.VillaNumbers.Remove(check);
                _context.SaveChanges();
                TempData["success"] = "Villa Number has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Something went wrong!";
            return View(deletedVillaNumberVM);
        }
    }
}


