using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Busines.Repositories
{
    public class CatagoryRepository
    {
        public void Update(Category category)
        {
            using var context = new BasiliskTfContext();
            if (category.Id == 0)
            {
                throw new ArgumentNullException("Please include the category ID you want to update");
            }
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void Insert(Category category)
        {
            using var context = new BasiliskTfContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            using var context = new BasiliskTfContext();
            context.Categories.Remove(category);
            context.SaveChanges();
        }

    }
}
