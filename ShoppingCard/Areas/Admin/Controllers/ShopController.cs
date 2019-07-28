using ShoppingCard.Models.Data;
using ShoppingCard.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCard.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            //Declare list of model
            List<CategoriesVM> listCategoriesVM;

            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Init the list
                listCategoriesVM = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoriesVM(x)).ToList(); 
            }

            //Return view with list
            return View(listCategoriesVM);
        }
    }
}