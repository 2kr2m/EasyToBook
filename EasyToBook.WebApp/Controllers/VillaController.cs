using EasyToBook.Domain.Entities;
using EasyToBook.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyToBook.WebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
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
                _context.Villas.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? villa = _context.Villas.FirstOrDefault(x => x.Id == villaId);
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
                _context.Villas.Update(editedVilla);
                _context.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? villa = _context.Villas.FirstOrDefault(x => x.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa deletedVilla)
        {
            Villa? check = _context.Villas.FirstOrDefault(u => u.Id == deletedVilla.Id);
            if (check is not null)
            {
                _context.Villas.Remove(check);
                _context.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }

    }
}
