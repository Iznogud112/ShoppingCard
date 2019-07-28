using ShoppingCard.Models.Data;
using ShoppingCard.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCard.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Declare list of PageVM
            List<PageVM> listPage;

            using (DbShoppingCard db = new DbShoppingCard())
            {
                listPage = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            } 
            //Init the list

            //Return view with list
            return View(listPage);
        }

        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // POST: Admin/Pages/AddPage/id
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            //Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Declare slug
                string slug;

                //Init pageDTO
                PageDTO dto = new PageDTO();

                //DTO title
                //Model binding in action
                dto.Title = model.Title;

                //Check for and set slug if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                //Make sure title and slug are unique
                if (db.Pages.Any(t => t.Title == model.Title) || db.Pages.Any(s => s.Slug == slug))
                {
                    ModelState.AddModelError("","Title or Slug already exists");
                    return View(model);
                }

                //DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;
                dto.Sorting = 50;

                //Save DTO
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            //Set TempData message
            TempData["SM"] = "You have added new page";

            //Redirect
            return RedirectToAction("AddPage");
        }

        // GET: Admin/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            //Declare pageVM
            PageVM pageVM;

            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Get the page
                PageDTO dto = db.Pages.Find(id);
                //Confirm page exists
                if(dto == null)
                {
                    return Content("The page does not exists");
                }
                //init pageVM
                pageVM = new PageVM(dto);
            }

            //Return view with model
            return View(pageVM);
        }

        // POST: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            //Check model state
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Get page id
                int id = model.Id;

                //Init slug
                string slug = "home";

                //Get the page
                PageDTO dto = db.Pages.Find(id);

                //DTO the title
                dto.Title = model.Title;

                //Check for the slug set it if need be
                if(model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }


                //Check sure title and slug are unique
                //Lambda expresion
                if (db.Pages.Where(x => x.Id != id).Any(t => t.Title == model.Title) || (db.Pages.Where(x => x.Id != id).Any(s => s.Slug == slug)))
                {
                    ModelState.AddModelError("", "The title or slug already exists");
                    return View(model);
                }

                //DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;

                
                //Save the DTO
                db.SaveChanges();
            }

            //Set TempData message
            TempData["SM"] = "You have edited the page";

            //Redirect view with Edit Page

            return RedirectToAction("Index");
        }

        // GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            //Declare PageVM
            PageVM pageVM;

            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Get the page
                PageDTO dto = db.Pages.Find(id);

                //Confirm page exists
                if (dto == null)
                {
                    return Content("The page does not exists");
                }

                //init PageVM
                pageVM = new PageVM(dto);
            }

            //Return view with pageVM
            return View(pageVM);
        }

        // GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            using (DbShoppingCard db = new DbShoppingCard())
            {
                //Get the page
                PageDTO dto = db.Pages.Find(id);

                //Remove the page
                db.Pages.Remove(dto);

                //Save
                db.SaveChanges();
            }

            //Redirect
            return RedirectToAction("Index");
        }
    }
}