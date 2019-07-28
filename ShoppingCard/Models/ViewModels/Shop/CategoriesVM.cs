using ShoppingCard.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCard.Models.ViewModels.Shop
{
    public class CategoriesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }

        public CategoriesVM()
        {

        }

        public CategoriesVM(CategoryDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Slug = dto.Slug;
            Sorting = dto.Sorting;
        }
    }
}