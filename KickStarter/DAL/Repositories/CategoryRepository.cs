using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private SampleContext db;

        public CategoryRepository(SampleContext context)
        {
            this.db = context;
        }
        public void Add(Category category)
        {
            var check = db.Categories.FirstOrDefault(p => p.Name == category.Name);
            if (check == null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            else
                throw (new Exception("Category with this name already exist"));
        }

        public void Delete(int id)
        {
            var check = db.Categories.FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                db.Entry(check).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
                throw (new Exception("Category dosen't exist"));
        }

        public List<Category> DisplayAll()
        {
             var someCategories = db.Categories.ToList();
            if (someCategories != null)
            {
                return someCategories;
            }
            else throw (new Exception("No categories exist"));
        }

        public Category DisplaySingle(int id)
        {
             var someCategory = db.Categories.FirstOrDefault(p => p.Id == id);
            if (someCategory != null)
            {
                return someCategory;
            }
            else throw (new Exception("Category dosn't exist"));
        }

        public Category Update(Category category)
        {
            var oldcategory = db.Categories.AsNoTracking().FirstOrDefault(p => p.Id == category.Id);
            if (category != null)
            {
                oldcategory.Name = category.Name;

                db.Entry(oldcategory).State = EntityState.Modified;
                db.SaveChanges();
                return oldcategory;
            }
            else
            {
                throw (new Exception("Category with this id dosen`t exist"));
            }
        }
    }
}
