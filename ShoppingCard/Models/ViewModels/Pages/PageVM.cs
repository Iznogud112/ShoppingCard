using ShoppingCard.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCard.Models.ViewModels.Pages
{
    public class PageVM
    {
        public PageVM()
        {

        }

        public PageVM(PageDTO page)
        {
            Id = page.Id;
            Title = page.Title;
            Slug = page.Slug;
            Body = page.Body;
            Sorting = page.Sorting;
            HasSideBar = page.HasSideBar;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [AllowHtml]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSideBar { get; set; }
    }
}