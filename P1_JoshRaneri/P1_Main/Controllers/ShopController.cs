using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P1_Main.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly LogicClass _logicClass;
        public ShopController(ILogger<ShopController> logger, LogicClass logicClass)
        {
            _logicClass = logicClass;
            _logger = logger;
        }
        // GET: ShopInventoryController

        public ActionResult Shop()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return View("~/Home/Index");
            }
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(_logicClass.GetCurrentUser(claimsIdentity));
            return View("ShopView", livmList);
        }

        // GET: ShopInventoryController/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            int productId = id;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return View("~/Home/Index");
            }
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(_logicClass.GetCurrentUser(claimsIdentity));
            LocationInventoryViewModel livm = livmList.FirstOrDefault(x => x.ProductId == productId);
            return View("ItemDetailView", livm);
        }

        // GET: ShopInventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("AddToCart")]
        public ActionResult AddToCart(LocationInventoryViewModel livm)
        {
            if (!ModelState.IsValid)
            {
                return View("ShopView");
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return View("~/Home/Index");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(user);
            livmList = _logicClass.AddToCart(livmList, livm, user);
            return View("ShopView", livmList);
        }

        // POST: ShopInventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopInventoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopInventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopInventoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopInventoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
