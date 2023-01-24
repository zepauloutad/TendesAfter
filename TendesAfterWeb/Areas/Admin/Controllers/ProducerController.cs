
using TendesAfter.DataAccess.Repository.IRepository;
using TendesAfter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TendesAfter.Utility;

namespace TendesAfterWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
	public class ProducerController : Controller
    {
		
		private readonly IUnitOfWork _unitOfWork;

        public ProducerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Producer> objProducerList = _unitOfWork.Producer.GetAll();
            return View(objProducerList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Automaticamente injeta uma chave, e essa chave deve ser valida no inicio, medida de segurança basicamente
        public IActionResult Create(Producer obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Producer.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Producer created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _unitOfWork.Producer.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
                return NotFound();
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Automaticamente injeta uma chave, e essa chave deve ser valida no inicio, medida de segurança basicamente
        public IActionResult Edit(Producer obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Producer.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Producer updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var ProducerFromDb = _unitOfWork.Producer.GetFirstOrDefault(u => u.Id == id);
            if (ProducerFromDb == null)
                return NotFound();
            return View(ProducerFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Automaticamente injeta uma chave, e essa chave deve ser valida no inicio, medida de segurança basicamente
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Producer.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Producer.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Producer deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
