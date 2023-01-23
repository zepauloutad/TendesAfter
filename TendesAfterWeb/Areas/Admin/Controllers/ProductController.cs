using TendesAfter.DataAccess;
using TendesAfter.DataAccess.Repository;
using TendesAfter.DataAccess.Repository.IRepository;
using TendesAfter.Models;
using TendesAfter.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using TendesAfter.Utility;

namespace TendesAfter.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin +","+ SD.Role_Employee)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
           return View(); 
        }

       

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
			
			if (id == null || id == 0)
            {
                //Create product
                //ViewBag.CategoryList = CategoryList;
                //ViewBag.CoverTypeList = CoverTypeList;
                return View(productVM);
            }
            else
            {
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.id== id);
                return View(productVM);

                //Update product
            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Automaticamente injeta uma chave, e essa chave deve ser valida no inicio, medida de segurança basicamente
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName + extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl=@"\Images\products\" + fileName + extension;
                }
                if(obj.product.id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productList }); 
	    }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successful" });
        }
        #endregion
    }


}
