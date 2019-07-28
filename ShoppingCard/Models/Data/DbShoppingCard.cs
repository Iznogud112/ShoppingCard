using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCard.Models.Data
{
    public class DbShoppingCard : DbContext
    {
        public DbSet<PageDTO> Pages { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
    }
}