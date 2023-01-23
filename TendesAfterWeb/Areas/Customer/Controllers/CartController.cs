using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using TendesAfter.DataAccess.Repository;
using TendesAfter.DataAccess.Repository.IRepository;
using TendesAfter.Models.ViewModels;

namespace TendesAfterWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ShoppingCartVM ShoppingCartVM { get; set; }

		public int OrderTotal { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product")
			};
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}

        public IActionResult Plus(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(U => U.id == cartID);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(U => U.id == cartID);
			if(cart.Count <= 1) 
			{
				_unitOfWork.ShoppingCart.Remove(cart);
			}
            _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(U => U.id == cartID);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        







    }
}
